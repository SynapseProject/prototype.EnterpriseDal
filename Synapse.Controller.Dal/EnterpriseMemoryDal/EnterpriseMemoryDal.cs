using System;
using System.Collections.Generic;

using Suplex.Security.AclModel.DataAccess;

namespace Synapse.Services.Controller.Dal
{
    public partial class EnterpriseMemoryDal : MemoryDal
    {
        EnterpriseStore _store = null;

        public EnterpriseMemoryDal(EnterpriseStore store) : base( store )
        {
            _store = store;
        }
    }
}