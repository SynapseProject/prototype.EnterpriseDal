using System.Collections.Generic;
using System.Linq;

using Suplex.Security.AclModel;
using Synapse.Core;

namespace Synapse.Services.Controller.Dal
{
    public interface IControllerDal : IPlanExecuteReader, IPlanHistoryWriter
    {
        object GetDefaultConfig();
        Dictionary<string, string> Configure(ISynapseDalConfig conifg);
    }

    public interface IPlanExecuteReader
    {
        bool HasAccess(string securityContext, string planUniqueName, FileSystemRight right = FileSystemRight.Execute);

        void HasAccessOrException(string securityContext, string planUniqueName, FileSystemRight right = FileSystemRight.Execute);

        IEnumerable<string> GetPlanList(string filter = null, bool isRegexFilter = true);

        Plan GetPlan(string planUniqueName);
    }

    public interface IPlanHistoryWriter
    {
        IEnumerable<long> GetPlanInstanceIdList(string planUniqueName);

        Plan CreatePlanInstance(string planUniqueName);

        Plan GetPlanStatus(string planUniqueName, long planInstanceId);

        void UpdatePlanStatus(Plan plan);

        void UpdatePlanStatus(PlanUpdateItem item);

        void UpdatePlanActionStatus(string planUniqueName, long planInstanceId, ActionItem actionItem);

        void UpdatePlanActionStatus(ActionUpdateItem item);
    }
}