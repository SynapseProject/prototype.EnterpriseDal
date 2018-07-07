using System;
using System.Collections.Generic;

using Suplex.Security.AclModel;
using Suplex.Security.Principal;

using Synapse.Services.Controller.Dal;
using Synapse.Services.Enterprise.Api;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            EnterpriseStore store = new EnterpriseStore();
            EnterpriseMemoryDal dal = new EnterpriseMemoryDal( store );

            Group fooGroup = new Group { UId = Guid.NewGuid(), Name = "fooGroup", IsLocal = true };
            Group gooGroup = new Group { UId = Guid.NewGuid(), Name = "gooGroup", IsLocal = true };
            User steve = new User { UId = Guid.NewGuid(), Name = "steve" };

            dal.UpsertGroup( fooGroup );
            dal.UpsertUser( steve );
            dal.UpsertGroupMembership( new GroupMembershipItem( group: fooGroup, member: steve ) );

            PlanContainer top = new PlanContainer { UId = Guid.NewGuid(), Name = "top" };
            top.Security.Dacl.Add( new AccessControlEntry<FileSystemRight> { TrusteeUId = gooGroup.UId.Value, Right = FileSystemRight.Execute } );
            PlanContainer child = new PlanContainer { UId = Guid.NewGuid(), Name = "child" };
            child.Security.Dacl.Add( new AccessControlEntry<FileSystemRight> { TrusteeUId = fooGroup.UId.Value, Right = FileSystemRight.Execute } );
            top.Children.Add( child );

            store.Containers = new List<PlanContainer>
            {
                top
            };

            PlanItem planItem = new PlanItem
            {
                Name = "xxx",
                UniqueName = "xxx",
                PlanContainerUId = child.UId
            };
            store.PlanItems = new List<PlanItem>
            {
                planItem
            };


            bool hasAccess = dal.HasAccess( "steve", @"top\child\xxx" );
        }
    }
}