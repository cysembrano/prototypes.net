using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Convergys.Assist.Repository.BusinessModels;
using Convergys.Assist.Repository.Enums;

namespace Convergys.Assist.WebUI.Models
{
    public class CallbackShowModel : BaseModel
    {
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
        public bool IsAgentDropDownAvailable 
        {
            get
            {
                if (this.LoggedOnEmployee == null)
                    return false;

                return this.LoggedOnEmployee.RoleId.Value >= Enum_Roles.TeamAdmin.GetHashCode();
            } 
        }
        public string AgentSelectedTimezone { get; set; }
        public string CustSelectedTimezone { get; set; }
        public int? ReasonSelectedId { get; set; }


        /// <summary>
        /// Status dropdown lookup ref.
        /// </summary>
        public Dictionary<int, string> CallbackStatusOptions = Enum.GetValues(typeof(Enum_CallbackStatus))
            .Cast<Enum_CallbackStatus>().ToDictionary(t => (int)t, t => t.ToString());


        public int ActionTypeId { get; set; }
        public EActionType ActionType 
        {
            get
            {
                return (EActionType)Enum.ToObject(typeof(EActionType), this.ActionTypeId);
            }
        }
        public Callback Callback { get; set; }

        public IEnumerable<SelectListItem> AgentTimeZoneCollection
        {
            get
            {
                return new SelectList(TimeZoneInfo.GetSystemTimeZones()
                    .Select(q => new SelectListItem
                    {
                        Text = q.DisplayName,
                        Value = (ParseTimezoneValue(q.DisplayName))
                    }
                    ), "Value", "Text", this.AgentSelectedTimezone);
            }
        }

        public IEnumerable<SelectListItem> CustTimeZoneCollection
        {
            get
            {
                return new SelectList(TimeZoneInfo.GetSystemTimeZones()
                    .Select(q => new SelectListItem
                    {
                        Text = q.DisplayName,
                        Value = (ParseTimezoneValue(q.DisplayName))
                    }
                    ), "Value", "Text", this.CustSelectedTimezone);
            }
        }




    }


}