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
    public interface ICallbackRepository
    {
        IEnumerable<CallbackSearchView> SearchCallbacks(CallbackSearchFilter filter);

        int AddCallback(EmployeeView securityCard, Callback callback);

        int EditCallback(EmployeeView securityCard, Callback callback);

        Callback ViewCallback(EmployeeView securityCard, int callbackId);

        bool DeleteCallback(EmployeeView securityCard, int callbackId);

        CallbackReason[] GetCallbackReasonsByTeam(EmployeeView securityCard, int teamId);

        int AddReason(EmployeeView securityCard, CallbackReason reason);

        void DeleteReason(EmployeeView securityCard, int reasonId);

        void SaveReason(EmployeeView securityCard, int reasonId, string reason);

        void SetCallbackStatus(EmployeeView securityCard, int callbackId, Enum_CallbackStatus status);
    }
}
