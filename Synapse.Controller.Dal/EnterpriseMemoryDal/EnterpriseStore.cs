using System;

using System.Collections.Generic;

using Suplex.Security.AclModel.DataAccess;
using Synapse.Services.Enterprise.Api;


namespace Synapse.Services.Controller.Dal
{
    public class EnterpriseStore : SuplexStore, IEnterpriseStore
    {
        public List<PlanContainer> Containers { get; set; }
        public List<PlanItem> PlanItems { get; set; }

        public List<PlanExecuteReaderItem> ExecuteReaders { get; set; }
        public List<PlanHistoryWriterItem> HistoryWriters { get; set; }
    }
}