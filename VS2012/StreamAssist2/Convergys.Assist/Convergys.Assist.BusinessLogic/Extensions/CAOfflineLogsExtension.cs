using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convergys.Assist.Data;
using Convergys.Assist.Repository.BusinessModels;

namespace Convergys.Assist.BusinessLogic.Extensions
{
    public static class CAOfflineLogsExtension
    {
        public static OfflineSearchView[] ToOfflineSearchViews(this IEnumerable<CAOfflineLogsLookup> offlineLogs)
        {
            return offlineLogs.Select(b => b.ToOfflineSearchView()).ToArray();
        }

        public static OfflineSearchView ToOfflineSearchView(this CAOfflineLogsLookup offlineLogs)
        {
            return new OfflineSearchView()
            {
                OfflineLogId = offlineLogs.OfflineLogId,
                CreationDate = offlineLogs.CreationDate,
                Team = offlineLogs.Team,
                EmpId = offlineLogs.EmpId,
                Status = offlineLogs.Status,
                OfflineActivityType = offlineLogs.OfflineActivityType,
                OfflineActivityTypeId = offlineLogs.OfflineActivityTypeId,
                OfflineContactType = offlineLogs.OfflineContactType,
                OfflineContactTypeId = offlineLogs.OfflineContactTypeId,
                TeamId = offlineLogs.TeamId,
                CaseIdentity = offlineLogs.CaseIdentity,
                FirstName = offlineLogs.FirstName,
                Comments = offlineLogs.Comments,
                TotalElapsedTime = offlineLogs.TotalElapsedTime
            };
        }

        public static OfflineContact[] ToOfflineContacts(this IEnumerable<CAOfflineContactType> contactTypes)
        {
            return contactTypes.Select(h => h.ToOfflineContact()).ToArray();
        }

        public static OfflineContact ToOfflineContact(this CAOfflineContactType contactType)
        {
            return new OfflineContact()
            {
                Id = contactType.OfflineContactTypeId,
                Description = contactType.OfflineContactType,
                TeamId = contactType.TeamId
            };
        }

        public static OfflineActivity[] ToOfflineActivities(this IEnumerable<CAOfflineActivityType> activityTypes)
        {
            return activityTypes.Select(y => y.ToOfflineActivity()).ToArray();
        }

        public static OfflineActivity ToOfflineActivity(this CAOfflineActivityType ActivityType)
        {
            return new OfflineActivity()
            {
                Id = ActivityType.OfflineActivityTypeId,
                Description = ActivityType.OfflineActivityType,
                TeamId = ActivityType.TeamId
            };
        }

        public static OfflineEvent[] ToOfflineEvents(this IEnumerable<CAOfflineEvents> offlineEvents)
        {
            return offlineEvents.Select(y => y.ToOfflineEvent()).ToArray();
        }

        public static OfflineEvent ToOfflineEvent(this CAOfflineEvents offlineEvent)
        {
            return new OfflineEvent()
            {
                Id = offlineEvent.OfflineEventId,
                LogId = offlineEvent.OfflineLogId,
                Start = offlineEvent.StartDateTime,
                End = offlineEvent.EndDateTime,
                Status = offlineEvent.Status,
                CreationDate = offlineEvent.CreationDate,
                Comments = offlineEvent.Comments
            };
        }
    }
}
