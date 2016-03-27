using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convergys.Assist.Data;
using Convergys.Assist.Repository.BusinessModels;

namespace Convergys.Assist.BusinessLogic.Extensions
{
    public static class CACallbackLogsExtension
    {
        public static CallbackSearchView[] ToCallbackSearchViews(this IEnumerable<CACallbackLogsSearchView> callbackLogs)
        {
            return callbackLogs.Select(b => b.ToCallbackSearchView()).ToArray();
        }

        public static CallbackSearchView ToCallbackSearchView(this CACallbackLogsSearchView callbackLog)
        {
            return new CallbackSearchView()
            {
                CallbackLogId = callbackLog.CallbackLogId,
                TeamName = callbackLog.Team,
                AssignedToName = callbackLog.AssignedTo,
                CreatorName = callbackLog.LogCreator,
                CustomerName = callbackLog.CustomerName,
                AgentCallbackTimeEnd = callbackLog.AgentCallbackTimeEnd,
                AgentCallbackTimeStart = callbackLog.AgentCallbackTimeStart,
                CallbackStatusId = callbackLog.CallbackStatusId,
                CallReferenceNumber = callbackLog.CallReferenceNumber,
                Contact1Phone = callbackLog.Contact1Phone,
                CreatorId = callbackLog.CreatedBy,
                CallbackReasonDescription = callbackLog.CallbackReasonType,
                TeamActive = callbackLog.TeamActive,
                AssignedToActive = callbackLog.AssignedToActive,
                CreatorActive = callbackLog.LogCreatorActive,
                ReasonTypeActive = callbackLog.ReasonTypeActive,
                CreationDate = callbackLog.CreationDate,
                AssignedToTeamId = callbackLog.TeamId,
                AssignedToEmpId = callbackLog.AssignedToEmpId,
                CreatedByEmpId = callbackLog.LogCreatorEmpId
              
            };
        }

        public static Callback ToCallback(this CACallbackLogs callbackLog)
        {
            return new Callback()
            {
                Id = callbackLog.CallbackLogId,
                AssignedToTeamId = callbackLog.TeamId,
                AssignedToTeamName = callbackLog.TeamName,
                AssignedToEmpId = callbackLog.EmpId,
                AssignedToEmpName = callbackLog.EmpName,
                CreatedByEmpId = callbackLog.CreatedBy,
                CreatedByEmpName = callbackLog.CreatedByName,
                CustomerName = callbackLog.CustomerName,
                CallRefNumber = callbackLog.CallReferenceNumber,
                Contact1Phone = callbackLog.Contact1Phone,
                Contact2Phone = callbackLog.Contact2Phone,
                Comments = callbackLog.Comments,
                AgentCallbackTimeStart = callbackLog.AgentCallbackTimeStart,
                AgentCallbackTimeEnd = callbackLog.AgentCallbackTimeEnd,
                CustomerCallbackTimeStart = callbackLog.CustomerCallbackTimeStart,
                CustomerCallbackTimeEnd = callbackLog.CustomerCallbackTimeEnd,
                CallbackStatusId = callbackLog.CallbackStatusId,
                CallbackReasonText = callbackLog.CallbackReasonTypeText,
                CallbackReasonId = callbackLog.CallbackReasonTypeIdRef
            };
        }

        public static CallbackReason[] ToCallbackReasons(this IEnumerable<CACallbackReasonType> callbackReasons)
        {
            return callbackReasons.Select(b => b.ToCallbackReason()).ToArray();
        }

        public static CallbackReason ToCallbackReason(this CACallbackReasonType callbackReason)
        {
            return new CallbackReason()
            {
                Id = callbackReason.CallbackReasonTypeId,
                CreateDate = callbackReason.CreateDate,
                Description = callbackReason.CallbackReasonType,
                ModifiedDate = callbackReason.DateChanged,
                TeamId = callbackReason.TeamId,
                ModifiedBy = callbackReason.ModifiedBy
            };
        }
    }
}
