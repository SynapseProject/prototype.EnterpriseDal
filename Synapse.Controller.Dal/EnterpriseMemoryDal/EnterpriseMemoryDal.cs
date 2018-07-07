using System;
using System.Collections.Generic;

using Suplex.Security.AclModel;
using Suplex.Security.AclModel.DataAccess;
using Synapse.Core;
using Synapse.Services.Enterprise.Api;

namespace Synapse.Services.Controller.Dal
{
    public partial class EnterpriseMemoryDal : MemoryDal, IPlanExecuteReader
    {
        EnterpriseStore _store = null;

        public EnterpriseMemoryDal(EnterpriseStore store) : base( store )
        {
            _store = store;
        }

        public Plan GetPlan(string planUniqueName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetPlanList(string filter = null, bool isRegexFilter = true)
        {
            throw new NotImplementedException();
        }
    }
}