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

        public PlanExecuteReaderItem DefaultExecuteReader
        {
            get
            {
                if( ExecuteReaders?.Count == 0 )
                    throw new Exception( "No ExecuteReaders configured." );

                return ExecuteReaders[0];
            }
        }

        public PlanHistoryWriterItem DefaultHistoryWriter
        {
            get
            {
                if( HistoryWriters?.Count == 0 )
                    throw new Exception( "No HistoryWriters configured." );

                return HistoryWriters[0];
            }
        }
    }
}