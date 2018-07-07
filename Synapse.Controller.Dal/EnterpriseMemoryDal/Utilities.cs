using System;
using System.Collections.Generic;

using Synapse.Services.Enterprise.Api;

namespace Synapse.Services.Controller.Dal
{
    public partial class EnterpriseMemoryDal
    {
        PlanContainer GetPlanContainer(string planUniqueName, bool returnTail, out PlanItem planItem)
        {
            string[] paths = planUniqueName.Split( '\\' );

            List<PlanContainer> list = _store.Containers;

            bool ok = true;
            PlanContainer root = null;
            PlanContainer tail = null;
            for( int i = 0; i < paths.Length - 1; i++ )
            {
                PlanContainer curr = list.Find( x => x.Name.Equals( paths[i], StringComparison.OrdinalIgnoreCase ) );
                if( curr != null )
                {
                    if( tail == null )
                    {
                        tail = curr.Clone( true );
                        tail.Security = curr.Security;
                        root = tail;
                    }
                    else
                    {
                        PlanContainer child = curr.Clone( true );
                        child.Security = curr.Security;
                        tail.Children.Add( child );
                        tail = child;
                    }

                    list = curr.Children;
                }
                else
                {
                    ok = false;
                    break;
                }
            }

            planItem = null;
            if( ok )
            {
                planItem = _store.PlanItems.Find( p =>
                    p.UniqueName.Equals( paths[paths.Length - 1], StringComparison.OrdinalIgnoreCase ) && p.PlanContainerUId == tail.UId );
                ok = planItem != null;
            }

            return ok ? returnTail ? tail : root : null;
        }
    }
}