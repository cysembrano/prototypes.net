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
    public class OfflineRepository : SQLRepositoryBase, IOfflineRepository
    {
        public IEnumerable<OfflineSearchView> SearchOfflineActivities(OfflineSearchFilter filter)
        {
            if (filter == null) throw new ArgumentNullException("filter");

            OfflineSearchView[] offlines = new OfflineSearchView[] { };
            using (SADataModel context = new SADataModel())
            {
                var of = context.CAOfflineLogsLookup.AsQueryable();

                if (filter.TeamId.HasValue)
                    of = of.Where(x => x.TeamId == filter.TeamId.Value);
                else if (filter.TeamIds != null)
                    of = of.Where(x => filter.TeamIds.Contains(x.TeamId));

                if (filter.EmpId.HasValue)
                    of = of.Where(x => x.EmpId == filter.EmpId.Value);

                if (filter.Status.HasValue)
                    of = of.Where(x => x.Status == filter.Status);
                if (filter.From.HasValue)
                {
                    var schedfrm = filter.From.Value.AddDays(-1).Date;
                    of = of.Where(x => x.CreationDate.Value > schedfrm);
                }
                if (filter.To.HasValue)
                {
                    var schedTo = filter.To.Value.AddDays(1).Date;
                    of = of.Where(x => x.CreationDate.Value < schedTo);
                }

                if (!string.IsNullOrWhiteSpace(filter.Keyword))
                {
                    of = of.Where(x =>
                                       x.OfflineContactType.Contains(filter.Keyword)
                                    || x.OfflineActivityType.Contains(filter.Keyword)
                                    || x.CaseIdentity.Contains(filter.Keyword)
                                    || x.Comments.Contains(filter.Keyword)
                                    || x.FirstName.Contains(filter.Keyword)
                                 );
                }

                offlines = of.ToOfflineSearchViews();
            }
            return offlines;
        }

        public OfflineSearchView SearchOfflineActivityById(EmployeeView securityCard, int offlineId)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");

            using (SADataModel dataModel = new SADataModel())
            {
                return dataModel.CAOfflineLogsLookup.FirstOrDefault(b => b.OfflineLogId == offlineId).ToOfflineSearchView();
            }
        }

        public bool DeleteOffline(EmployeeView securityCard, int offlineId)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");

            using (var dbContext = new SADataModel())
            {
                
                string updateFormat = "UPDATE streamassist_dbo.tblOfflineLogs SET Active = 0 WHERE OfflineLogId = {0}";
                int records = dbContext.Database.ExecuteSqlCommand(String.Format(updateFormat, offlineId));

                if (records < 1)
                {
                    Log4NetManager.Instance.Warn(this.GetType(), "Delete Offline Failed.");
                    return false;
                }
                else
                {
                    Log4NetManager.Instance.Info(this.GetType(), String.Format("Deleted Offline Id({0}) by User Id({1})", offlineId, securityCard.EmpId));
                    return true;
                }

            }
        }

        public void SetOfflineStatus(EmployeeView securityCard, int OfflineId, Enum_OfflineStatus status)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");
            using (var dbContext = new SADataModel())
            {
                var found = dbContext.CAOfflineLogs.FirstOrDefault(t => t.OfflineLogId == OfflineId);
                if (found != null)
                {
                    found.Status = status.GetHashCode();

                    var records = dbContext.SaveChanges();

                    if (records < 1)
                        Log4NetManager.Instance.Warn(this.GetType(), "Update Status failed.");
                    else
                        Log4NetManager.Instance.Info(this.GetType(), String.Format("Saved Offline ({0}) Status to {1}", OfflineId, status));

                }
            }
        }

        public OfflineContact[] GetOfflineContactByTeam(EmployeeView securityCard, int teamId)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");

            using (SADataModel context = new SADataModel())
            {
                return context.CAOfflineContactType.Where(h => h.TeamId == teamId).ToOfflineContacts();
            }
        }

        public OfflineActivity[] GetOfflineActivityByTeam(EmployeeView securityCard, int teamId)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");

            using (SADataModel context = new SADataModel())
            {
                return context.CAOfflineActivityType.Where(h => h.TeamId == teamId).ToOfflineActivities();
            }
        }

        public OfflineEvent[] GetOfflineEventsByLog(EmployeeView securityCard, int offlineLogId)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");

            using (SADataModel context = new SADataModel())
            {
                return context.CAOfflineEvents.Where(h => h.OfflineLogId == offlineLogId).ToOfflineEvents();
            }
        }

        public int SaveOfflineEvent(EmployeeView securityCard, OfflineEvent offlineEvent)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");
            if (offlineEvent == null) throw new ArgumentNullException("offlineEvent");

            using (SADataModel dataModel = new SADataModel())
            {
                CAOfflineEvents offEvents = new CAOfflineEvents()
                {
                    Comments = offlineEvent.Comments,
                    StartDateTime = offlineEvent.Start,
                    EndDateTime = offlineEvent.End,
                    Status = offlineEvent.Status,
                    CreationDate = DateTime.Now,
                    OfflineLogId = offlineEvent.LogId
                };
                var returnedOfflineEvent = dataModel.CAOfflineEvents.Add(offEvents);
                dataModel.SaveChanges();

                return returnedOfflineEvent.OfflineEventId;
            }
        }

        public CAOfflineLogs WireUp(EmployeeView securityCard, CAOfflineLogs offlineData, OfflineSearchView offlineView)
        {
            if (offlineData == null)
            {
                offlineData = new CAOfflineLogs();
                offlineData.CreationDate = DateTime.Now;
                offlineData.ModifiedDate = null;
            }
            else
            {
                offlineData.ModifiedDate = DateTime.Now;
            }
            offlineData.TeamId = Convert.ToDecimal(offlineView.TeamId);
            offlineData.EmpId = offlineView.EmpId;
            offlineData.CaseIdentity = offlineView.CaseIdentity;
            offlineData.Comments = offlineView.Comments;
            offlineData.OfflineActivityTypeId = offlineView.OfflineActivityTypeId;
            offlineData.OfflineContactTypeId = offlineView.OfflineContactTypeId;
            offlineData.Status = offlineView.Status;
            offlineData.TotalElapsedTime = offlineView.TotalElapsedTime;

            return offlineData;
            
        }

        public int AddOfflineLog(EmployeeView securityCard, OfflineSearchView offlineView)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");
            if (offlineView == null) throw new ArgumentNullException("callback");

            CAOfflineLogs offlineLog = WireUp(securityCard, null, offlineView);

            using (SADataModel dataModel = new SADataModel())
            {
                var returnedOffline = dataModel.CAOfflineLogs.Add(offlineLog);
                dataModel.SaveChanges();

                return returnedOffline.OfflineLogId;
            }
        }

        public int EditOfflineLog(EmployeeView securityCard, OfflineSearchView offlineView)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");
            if (offlineView == null) throw new ArgumentNullException("offlineView");

            using (SADataModel dataModel = new SADataModel())
            {
                var offlineData = dataModel.CAOfflineLogs.FirstOrDefault(b => b.OfflineLogId == offlineView.OfflineLogId);

                if (offlineData == null)
                    throw new ArgumentException(string.Format("Error with editing Offline Activity ({0})", offlineView.OfflineLogId));

                offlineData = WireUp(securityCard, offlineData, offlineView);
                //History here.

                dataModel.SaveChanges();

                return offlineData.OfflineLogId;
            }

        }

        public bool DeleteOfflineEvent(EmployeeView securityCard, int offlineEventId)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");

            using (var dbContext = new SADataModel())
            {

                string updateFormat = "UPDATE streamassist_dbo.tblOfflineEvents SET Active = 0 WHERE OfflineEventId = {0}";
                int records = dbContext.Database.ExecuteSqlCommand(String.Format(updateFormat, offlineEventId));

                if (records < 1)
                {
                    Log4NetManager.Instance.Warn(this.GetType(), "Delete Offline Event Failed.");
                    return false;
                }
                else
                {
                    Log4NetManager.Instance.Info(this.GetType(), String.Format("Deleted Offline Event Id({0}) by User Id({1})", offlineEventId, securityCard.EmpId));
                    return true;
                }

            }
        }

        public int AddContactType(EmployeeView securityCard, OfflineContact contact)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");

            using (SADataModel context = new SADataModel())
            {
                CAOfflineContactType contactType = new CAOfflineContactType();
                contactType.OfflineContactType = contact.Description;
                contactType.CreateDate = DateTime.Now;
                contactType.DateChanged = null;
                contactType.ModifiedBy = securityCard.EmpId;
                contactType.TeamId = contact.TeamId;

                var returnedContact = context.CAOfflineContactType.Add(contactType);
                context.SaveChanges();

                if (returnedContact != null)
                    Log4NetManager.Instance.Info(this.GetType(),
                        String.Format("Added Offline contact ({0}) - {1} for Team {2}", returnedContact.OfflineContactTypeId, returnedContact.OfflineContactType, securityCard.Team));
                else
                    Log4NetManager.Instance.Warn(this.GetType(), "No Contact Type was added");


                return returnedContact.OfflineContactTypeId;

            }
        }

        public void DeleteContactType(EmployeeView securityCard, int contactId)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");

            using (var dbContext = new SADataModel())
            {
                string updateFormat = "UPDATE streamassist_dbo.tblOfflineContactType SET Active = 0 WHERE OfflineContactTypeId = {0}";
                int records = dbContext.Database.ExecuteSqlCommand(String.Format(updateFormat, contactId));

                if (records < 1)
                    Log4NetManager.Instance.Warn(this.GetType(), "Delete Contact Type did not deactivate any item");
                else
                    Log4NetManager.Instance.Info(this.GetType(), String.Format("Deactivated Contact Type ({0})", contactId));

            }
        }

        public void SaveContactType(EmployeeView securityCard, int contactId, string description)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");
            using (var dbContext = new SADataModel())
            {
                var found = dbContext.CAOfflineContactType.FirstOrDefault(t => t.OfflineContactTypeId == contactId);
                if (found != null)
                {
                    found.OfflineContactType = description;
                    found.DateChanged = DateTime.Now;
                    found.ModifiedBy = securityCard.EmpId;
                    var records = dbContext.SaveChanges();

                    if (records < 1)
                        Log4NetManager.Instance.Warn(this.GetType(), "Save Contact Type failed.");
                    else
                        Log4NetManager.Instance.Info(this.GetType(), String.Format("Saved Cantact Type ({0}) to {1}", contactId, description));

                }
            }

        }

        public int AddActivityType(EmployeeView securityCard, OfflineActivity activity)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");

            using (SADataModel context = new SADataModel())
            {
                CAOfflineActivityType activityType = new CAOfflineActivityType();
                activityType.OfflineActivityType = activity.Description;
                activityType.CreateDate = DateTime.Now;
                activityType.DateChanged = null;
                activityType.ModifiedBy = securityCard.EmpId;
                activityType.TeamId = activity.TeamId;

                var returnedActivity = context.CAOfflineActivityType.Add(activityType);
                context.SaveChanges();

                if (returnedActivity != null)
                    Log4NetManager.Instance.Info(this.GetType(),
                        String.Format("Added Activity Type ({0}) - {1} for Team {2}", returnedActivity.OfflineActivityTypeId, returnedActivity.OfflineActivityType, securityCard.Team));
                else
                    Log4NetManager.Instance.Warn(this.GetType(), "No Activity Type was added");


                return returnedActivity.OfflineActivityTypeId;

            }
        }

        public void DeleteActivityType(EmployeeView securityCard, int activityId)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");

            using (var dbContext = new SADataModel())
            {
                string updateFormat = "UPDATE streamassist_dbo.tblOfflineActivityType SET Active = 0 WHERE OfflineActivityTypeId = {0}";
                int records = dbContext.Database.ExecuteSqlCommand(String.Format(updateFormat, activityId));

                if (records < 1)
                    Log4NetManager.Instance.Warn(this.GetType(), "Delete Activity Type did not deactivate any item");
                else
                    Log4NetManager.Instance.Info(this.GetType(), String.Format("Deactivated Activity Type ({0})", activityId));

            }
        }

        public void SaveActivityType(EmployeeView securityCard, int activityId, string description)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");
            using (var dbContext = new SADataModel())
            {
                var found = dbContext.CAOfflineActivityType.FirstOrDefault(t => t.OfflineActivityTypeId == activityId);
                if (found != null)
                {
                    found.OfflineActivityType = description;
                    found.ModifiedBy = securityCard.EmpId;
                    found.DateChanged = DateTime.Now;
                    var records = dbContext.SaveChanges();

                    if (records < 1)
                        Log4NetManager.Instance.Warn(this.GetType(), "Save Activity Type failed.");
                    else
                        Log4NetManager.Instance.Info(this.GetType(), String.Format("Saved Activity Type ({0}) to {1}", activityId, description));

                }
            }

        }
    }
}
