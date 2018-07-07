using System;
using System.Collections.Generic;

using Suplex.Security.AclModel;
using Suplex.Security.Principal;

using Synapse.Services.Enterprise.Api;

namespace Synapse.Services.Controller.Dal
{
    public partial class EnterpriseMemoryDal
    {
        public bool HasAccess(string securityContext, string planUniqueName, FileSystemRight right = FileSystemRight.Execute)
        {
            PlanItem planItem = new PlanItem();
            PlanContainer root = GetPlanContainer( planUniqueName, false, out planItem );
            if( root == null )
                throw new Exception( "Plan path invalid or Plan not found." );

            User user = _store.Users.GetByNameOrDefault<User>( securityContext );
            if( user == null || !user.IsEnabled )
                return false;

            //stores the Guids from Suplex + ActiveDirectory for user's group membership
            List<Guid> groupMembership = new List<Guid>();

            //get the user's groupMembership from Suplex
            IEnumerable<GroupMembershipItem> gm = GetGroupMembership( user.UId.Value );
            foreach( GroupMembershipItem gmi in gm )
                groupMembership.Add( gmi.GroupUId );

            //get the Active Directory groupMembership, resolve the groups from Suplex
            List<string> adgm = DirectoryServicesHelper.GetGroupMembership( securityContext, ldapRoot: null, externalGroups: null );
            foreach( string group in adgm )
            {
                Group g = _store.Groups.GetByNameOrDefault<Group>( group );
                if( g != null && g.IsEnabled )
                    groupMembership.Add( g.UId.Value );
            }

            //delete any aces assigned to Trustees to which the user /does not belong/
            PlanContainer planCont = root;
            while( planCont != null )
            {
                for( int n = planCont.Security.Dacl.Count - 1; n >= 0; n-- )
                    if( !groupMembership.Contains( planCont.Security.Dacl[n].TrusteeUId.Value ) )
                        planCont.Security.Dacl.RemoveAt( n );

                planCont = planCont.Children?.Count > 0 ? planCont.Children[0] : null;
            }

            //return root.EvalSecurity().GetByTypeRight( right ).AccessAllowed;
            //root.EvalSecurity();
            //return planCont.Security.Results.GetByTypeRight( right ).AccessAllowed;
            //eval security from top->down, return result from bottom node (last node found in while loop above)
            root.EvalSecurity();
            return planCont.Security.HasAccess( right );
        }

        public void HasAccessOrException(string securityContext, string planUniqueName, FileSystemRight right = FileSystemRight.Execute)
        {
            if( !HasAccess( securityContext, planUniqueName, right ) )
                throw new Exception( $"Access denied: [{securityContext}] does not have [{right}] rights to [{planUniqueName}]." );
        }
    }
}