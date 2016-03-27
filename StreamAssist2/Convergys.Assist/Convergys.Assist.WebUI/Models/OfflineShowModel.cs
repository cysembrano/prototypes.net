using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Convergys.Assist.Repository.BusinessModels;
using Convergys.Assist.Repository.Enums;

namespace Convergys.Assist.WebUI.Models
{
    public class OfflineShowModel : BaseModel
    {
        public OfflineSearchView Offline { get; set; }

        public Team[] TeamList { get; set; }
        public bool IsTeamDropDownAvailable
        {
            get
            {
                if (this.LoggedOnEmployee == null)
                    return false;

                return this.LoggedOnEmployee.RoleId.Value >= Enum_Roles.TeamManager.GetHashCode();
            }
        }

        public int ActionTypeId { get; set; }
        public EActionType ActionType
        {
            get
            {
                return (EActionType)Enum.ToObject(typeof(EActionType), this.ActionTypeId);
            }
        }

        public OfflineShowModel(OfflineSearchView offline)
        {
            this.Offline = offline;
        }
        public OfflineShowModel() { }

        public Dictionary<int, string> OfflineStatusOptions = Enum.GetValues(typeof(Enum_OfflineStatus))
    .Cast<Enum_OfflineStatus>().ToDictionary(t => (int)t, t => t.ToString());

        public IEnumerable<OfflineEvent> OfflineEvents { get; set; }
        public string JSONOfflineEvents { get; set; }
    }
}