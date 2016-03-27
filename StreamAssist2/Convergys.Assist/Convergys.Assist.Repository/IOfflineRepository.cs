using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convergys.Assist.Repository.BusinessModels;
using Convergys.Assist.Repository.Enums;
using Convergys.Assist.Repository.SearchFilters;

namespace Convergys.Assist.Repository
{
    public interface IOfflineRepository
    {
        IEnumerable<OfflineSearchView> SearchOfflineActivities(OfflineSearchFilter filter);

        bool DeleteOffline(EmployeeView securityCard, int offlineId);

        OfflineSearchView SearchOfflineActivityById(EmployeeView securityCard, int offlineId);

        void SetOfflineStatus(EmployeeView securityCard, int OfflineId, Enum_OfflineStatus status);

        OfflineContact[] GetOfflineContactByTeam(EmployeeView securityCard, int teamId);

        OfflineActivity[] GetOfflineActivityByTeam(EmployeeView securityCard, int teamId);

        OfflineEvent[] GetOfflineEventsByLog(EmployeeView securityCard, int offlineLogId);

        int SaveOfflineEvent(EmployeeView securityCard, OfflineEvent offlineEvent);

        int AddOfflineLog(EmployeeView securityCard, OfflineSearchView offlineView);

        int EditOfflineLog(EmployeeView securityCard, OfflineSearchView offlineView);

        bool DeleteOfflineEvent(EmployeeView securityCard, int offlineEventId);

        int AddContactType(EmployeeView securityCard, OfflineContact contact);

        void DeleteContactType(EmployeeView securityCard, int contactId);

        void SaveContactType(EmployeeView securityCard, int contactId, string description);

        int AddActivityType(EmployeeView securityCard, OfflineActivity activity);

        void DeleteActivityType(EmployeeView securityCard, int activityId);

        void SaveActivityType(EmployeeView securityCard, int activityId, string description);
    }
}
