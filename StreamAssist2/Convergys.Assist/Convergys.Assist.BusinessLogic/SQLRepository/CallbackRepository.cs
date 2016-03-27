using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convergys.Assist.Data;
using Convergys.Assist.Repository;
using Convergys.Assist.Repository.BusinessModels;
using Convergys.Assist.Repository.SearchFilters;
using Convergys.Assist.BusinessLogic.Extensions;
using Convergys.Assist.Logging;
using Convergys.Assist.Repository.Enums;

namespace Convergys.Assist.BusinessLogic.SQLRepository
{
    public class CallbackRepository : SQLRepositoryBase, ICallbackRepository
    {
        public const int SCHEDULEDAYSPAST = -14;

        /// <summary>
        /// Min access is Support Professional (1)
        /// </summary>
        public IEnumerable<CallbackSearchView> SearchCallbacks(CallbackSearchFilter filter)
        {

            if (filter == null) throw new ArgumentNullException("filter");  

            CallbackSearchView[] callBacks = new CallbackSearchView[] { };
            using (SADataModel context = new SADataModel())
            {

                var cb = context.CACallbackLogsSearchView.AsQueryable();

                if (filter.TeamId.HasValue)
                    cb = cb.Where(x => x.TeamId == filter.TeamId.Value);
                else if(filter.TeamIds != null)
                    cb = cb.Where(x => filter.TeamIds.Contains(x.TeamId.Value));


                if (filter.EmpId.HasValue)
                    cb = cb.Where(x => x.AssignedToEmpId == filter.EmpId.Value
                                    || x.LogCreatorEmpId == filter.EmpId.Value);

                if (filter.Status.HasValue)
                    cb = cb.Where(x => x.CallbackStatusId == filter.Status);
                if (filter.From.HasValue)
                {
                    var schedfrm = filter.From.Value.AddDays(-1).Date;
                    cb = cb.Where(x => x.AgentCallbackTimeStart.Value > schedfrm);
                }
                if (filter.To.HasValue)
                {
                    var schedTo = filter.To.Value.AddDays(1).Date;
                    cb = cb.Where(x => x.AgentCallbackTimeStart.Value < schedTo);
                }
                if (!string.IsNullOrWhiteSpace(filter.Keyword))
                {
                    cb = cb.Where(x =>
                                       x.CustomerName.Contains(filter.Keyword)
                                    || x.CallReferenceNumber.Contains(filter.Keyword)
                                    || x.Contact1Phone.Contains(filter.Keyword)
                                    || x.Team.Contains(filter.Keyword)
                                    || x.AssignedTo.Contains(filter.Keyword)
                                    || x.LogCreator.Contains(filter.Keyword)
                                    || x.CallbackReasonType.Contains(filter.Keyword)
                                 );
                }
                callBacks = cb.ToCallbackSearchViews();
            }

            return callBacks;
        }

        /// <summary>
        /// Used by Add/Edit/View
        /// </summary>
        private CACallbackLogs WireUp(EmployeeView securityCard, CACallbackLogs callBackData, Callback callback)
        {
            bool isNew = false;
            if (callBackData == null)
            {
                callBackData = new CACallbackLogs();
                isNew = true;
            }

            bool isTeamAdminBelow = securityCard.RoleId.GetValueOrDefault() < Enum_Roles.TeamManager.GetHashCode(); 

            callBackData.TeamId = isTeamAdminBelow ? 
                Convert.ToDecimal(securityCard.TeamId.GetValueOrDefault()) : 
                callback.AssignedToTeamId;
            
            callBackData.TeamName = isTeamAdminBelow ?
                securityCard.Team :
                callback.AssignedToTeamName;


            callBackData.EmpName = callback.AssignedToEmpName;
            callBackData.EmpId = callback.AssignedToEmpId;
            callBackData.CreatedBy = securityCard.EmpId;
            callBackData.CreatedByName = securityCard.FirstName;
            
            callBackData.CallReferenceNumber = callback.CallRefNumber;
            callBackData.CustomerName = callback.CustomerName;
            callBackData.Contact1Phone = callback.Contact1Phone;
            callBackData.Contact2Phone = callback.Contact2Phone;
            callBackData.Comments = callback.Comments;
            callBackData.CallbackReasonTypeIdRef = callback.CallbackReasonId;
            callBackData.CallbackReasonTypeText = callback.CallbackReasonText;
            callBackData.CallbackReasonTypeId = callback.CallbackReasonId;

            callBackData.CallbackStatusId = callback.CallbackStatusId;
            callBackData.Status = null;
                        
            if(isNew)
            {
                callBackData.CreationDate = DateTime.Now;
                callBackData.ModifiedDate = null;
            }
            else
            {
                callBackData.ModifiedDate = DateTime.Now;
            }
            callBackData.AgentCallbackTimeStart = callback.AgentCallbackTimeStart;
            callBackData.AgentCallbackTimeEnd = callback.AgentCallbackTimeEnd;
            callBackData.CustomerCallbackTimeStart = callback.CustomerCallbackTimeStart;
            callBackData.CustomerCallbackTimeEnd = callback.CustomerCallbackTimeEnd;
            callBackData.CallbackTimeStart = null;
            callBackData.CallbackTimeEnd = null;

            return callBackData;
        }

        public int AddCallback(EmployeeView securityCard, Callback callback)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");
            if (callback == null) throw new ArgumentNullException("callback");

            CACallbackLogs callBackLog = WireUp(securityCard, null, callback);

            using (SADataModel dataModel = new SADataModel())
            {
                var returnedCallback = dataModel.CACallbackLogs.Add(callBackLog);
                dataModel.SaveChanges();

                return returnedCallback.CallbackLogId;
            }
            
        }

        public int EditCallback(EmployeeView securityCard, Callback callback)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");
            if (callback == null) throw new ArgumentNullException("callback");

            using (SADataModel dataModel = new SADataModel())
            {
                var callBackData = dataModel.CACallbackLogs.FirstOrDefault(b => b.CallbackLogId == callback.Id);

                if (callBackData == null)
                    throw new ArgumentException(string.Format("Error with editing Callback ({0})", callback.Id));

                callBackData = WireUp(securityCard, callBackData, callback);
                //History here.

                dataModel.SaveChanges();

                return callBackData.CallbackLogId;
            }           

        }

        public Callback ViewCallback(EmployeeView securityCard, int callbackId)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");

            using (SADataModel dataModel = new SADataModel())
            {
                return dataModel.CACallbackLogs.FirstOrDefault(b => b.CallbackLogId == callbackId).ToCallback();
            }
        }

        public bool DeleteCallback(EmployeeView securityCard, int callbackId)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");
            using (var dbContext = new SADataModel())
            {
                string updateFormat = "UPDATE streamassist_dbo.tblCallbackLogs SET Active = 0 WHERE CallbackLogId = {0}";
                int records = dbContext.Database.ExecuteSqlCommand(String.Format(updateFormat, callbackId));

                if (records < 1)
                {
                    Log4NetManager.Instance.Warn(this.GetType(), "Delete Callback Failed.");
                    return false;
                }
                else
                {
                    Log4NetManager.Instance.Info(this.GetType(), String.Format("Deleted Callback Id({0}) by User Id({1})", callbackId, securityCard.EmpId));
                    return true;
                }

            }
        }

        public CallbackReason[] GetCallbackReasonsByTeam(EmployeeView securityCard, int teamId)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");

            using (SADataModel context = new SADataModel())
            {
                return context.CACallbackReasonType.Where(h => h.TeamId == teamId).ToCallbackReasons();
            }
        }

        public int AddReason(EmployeeView securityCard, CallbackReason reason)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");

            using (SADataModel context = new SADataModel())
            {
                CACallbackReasonType reasonType = new CACallbackReasonType();
                reasonType.CallbackReasonType = reason.Description;
                reasonType.CreateDate = DateTime.Now;
                reasonType.DateChanged = null;
                reasonType.ModifiedBy = securityCard.EmpId;
                reasonType.TeamId = reason.TeamId;

                var returnedReason = context.CACallbackReasonType.Add(reasonType);
                context.SaveChanges();

                if (returnedReason != null)
                    Log4NetManager.Instance.Info(this.GetType(),
                        String.Format("Added Callback reason ({0}) - {1} for Team {2}", returnedReason.CallbackReasonTypeId, returnedReason.CallbackReasonType, returnedReason.TeamId));
                else
                    Log4NetManager.Instance.Warn(this.GetType(), "No Callback Reason was added");


                return returnedReason.CallbackReasonTypeId;

            }
        }
        
        public void DeleteReason(EmployeeView securityCard, int reasonId)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");

            using (var dbContext = new SADataModel())
            {
                string updateFormat = "UPDATE streamassist_dbo.tblCallbackReasonType SET Active = 0 WHERE CallbackReasonTypeId = {0}";
                int records = dbContext.Database.ExecuteSqlCommand(String.Format(updateFormat, reasonId));

                if (records < 1)
                    Log4NetManager.Instance.Warn(this.GetType(), "Delete Reason did not deactivate any item");
                else
                    Log4NetManager.Instance.Info(this.GetType(), String.Format("Deactivated Callback Reason ({0})", reasonId));

            }
        }

        public void SaveReason(EmployeeView securityCard, int reasonId, string reason)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");
            using (var dbContext = new SADataModel())
            {
                var found = dbContext.CACallbackReasonType.FirstOrDefault(t => t.CallbackReasonTypeId == reasonId);
                if (found != null)
                {
                    found.CallbackReasonType = reason;
                    found.DateChanged = DateTime.Now;
                    var records = dbContext.SaveChanges();

                    if (records < 1)
                        Log4NetManager.Instance.Warn(this.GetType(), "Save Reason failed.");
                    else
                        Log4NetManager.Instance.Info(this.GetType(), String.Format("Saved Callback Reason ({0}) to {1}", reasonId, reason));

                }
            }

        }

        public void SetCallbackStatus(EmployeeView securityCard, int callbackId, Enum_CallbackStatus status)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");
            using (var dbContext = new SADataModel())
            {
                var found = dbContext.CACallbackLogs.FirstOrDefault(t => t.CallbackLogId == callbackId);
                if (found != null)
                {
                    found.CallbackStatusId = status.GetHashCode();
                    found.Status = status.GetHashCode();

                    var records = dbContext.SaveChanges();

                    if (records < 1)
                        Log4NetManager.Instance.Warn(this.GetType(), "Update Status failed.");
                    else
                        Log4NetManager.Instance.Info(this.GetType(), String.Format("Saved Callback ({0}) Status to {1}", callbackId, status));

                }
            }
        }
    }
}
