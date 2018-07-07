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
            PlanItem planItem = new PlanItem();
            PlanContainer planContainer = GetPlanContainer( planUniqueName, true, out planItem );

            PlanExecuteReaderItem peri = _store.DefaultExecuteReader;
            if( planContainer.HasPlanExecuteReaderKey )
                peri = _store.ExecuteReaders.SingleOrDefault( r => r.Key.Equals( planContainer.PlanExecuteReaderKey, StringComparison.OrdinalIgnoreCase ) );

            if( peri == null )
                throw new Exception( $"Could not load PlanExecuteReader [{planContainer.PlanExecuteReaderKey}]." );

            IPlanExecuteReader reader = AssemblyLoader.Load<IPlanExecuteReader>( peri.Type, string.Empty );
            return reader.GetPlan( planItem.PlanFile );
        }

        public IEnumerable<string> GetPlanList(string filter = null, bool isRegexFilter = true)
        {
            throw new NotImplementedException();
        }
    }
}