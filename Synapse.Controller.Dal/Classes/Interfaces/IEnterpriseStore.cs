using System;

using System.Collections.Generic;

using Synapse.Services.Enterprise.Api;



namespace Synapse.Services.Controller.Dal
{
    public interface IEnterpriseStore
    {
        List<PlanContainer> Containers { get; set; }
        List<PlanItem> PlanItems { get; set; }

        List<PlanExecuteReaderItem> ExecuteReaders { get; set; }
        List<PlanHistoryWriterItem> HistoryWriters { get; set; }
    }
}