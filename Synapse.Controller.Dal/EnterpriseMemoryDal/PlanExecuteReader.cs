using System;
using System.Collections.Generic;
using System.Linq;

using Suplex.Security.AclModel.DataAccess;
using Synapse.Core;
using Synapse.Core.Utilities;
using Synapse.Services.Enterprise.Api;

namespace Synapse.Services.Controller.Dal
{
    public partial class EnterpriseMemoryDal : IPlanExecuteReader
    {
        public Plan GetPlan(string planUniqueName)
        {
            PlanContainer planContainer = GetPlanContainer( planUniqueName, returnTail: true, planItem: out PlanItem planItem );
            IPlanExecuteReader reader = GetPlanExecuteReader( planContainer );
            return reader.GetPlan( planItem.PlanFile );
        }

        public IEnumerable<string> GetPlanList(string filter = null, bool isRegexFilter = true)
        {
            PlanContainer planContainer = GetPlanContainer( filter, returnTail: true );
            IPlanExecuteReader reader = GetPlanExecuteReader( planContainer );
            return reader.GetPlanList( planContainer.PlansUri, isRegexFilter );
        }

        IPlanExecuteReader GetPlanExecuteReader(PlanContainer planContainer)
        {
            PlanExecuteReaderItem ri = _store.DefaultExecuteReader;
            if( planContainer.HasPlanExecuteReaderKey )
                ri = _store.ExecuteReaders.SingleOrDefault( r => r.Key.Equals( planContainer.PlanExecuteReaderKey, StringComparison.OrdinalIgnoreCase ) );

            if( ri == null )
                throw new Exception( $"Could not load PlanExecuteReader [{planContainer.PlanExecuteReaderKey}]." );

            return AssemblyLoader.Load<IPlanExecuteReader>( ri.Type, string.Empty );
        }
    }
}