using System;
using System.Collections.Generic;
using System.Linq;

using Suplex.Security.AclModel;
using Suplex.Security.Principal;

using Synapse.Services.Enterprise.Api;

namespace Synapse.Services.Controller.Dal
{
    public partial class EnterpriseMemoryDal
    {
        PlanContainer GetPlanContainer(string planUniqueName)
        {
            string[] paths = planUniqueName.Split( '\\' );

            List<PlanContainer> list = _store.Containers;

            bool ok = true;
            PlanContainer root = null;
            PlanContainer rtn = null;
            for( int i = 0; i < paths.Length - 1; i++ )
            {
                PlanContainer curr = list.Find( x => x.Name.Equals( paths[i], StringComparison.OrdinalIgnoreCase ) );
                if( curr != null )
                {
                    if( rtn == null )
                    {
                        rtn = curr.Clone( true );
                        rtn.Security = curr.Security;
                        root = rtn;
                    }
                    else
                    {
                        PlanContainer child = curr.Clone( true );
                        child.Security = curr.Security;
                        rtn.Children.Add( child );
                        rtn = child;
                    }

                    list = curr.Children;
                }
                else
                {
                    ok = false;
                    break;
                }
            }

            if( ok )
            {
                PlanItem planItem = _store.PlanItems.Find( p =>
                    p.UniqueName.Equals( paths[paths.Length - 1], StringComparison.OrdinalIgnoreCase ) && p.PlanContainerUId == rtn.UId );
                ok = planItem != null;
            }

            return ok ? root : null;
        }
    }
}