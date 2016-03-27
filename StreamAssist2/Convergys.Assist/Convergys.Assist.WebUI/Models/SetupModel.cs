using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Convergys.Assist.Repository.BusinessModels;
using Convergys.Assist.Repository.Enums;

namespace Convergys.Assist.WebUI.Models
{
    public class SetupModel : BaseModel
    {
        public Team[] TeamList { get; set; }
        public int? SelectedTeamId { get; set; }
        public string RoleChangeSelectedEmp { get; set; }
        public bool RoleChangeOccured { get; set; }
        public bool RoleChangeSuccess { get; set; }
        public int RoleChangeSelectedRole { get; set; }

        public IEnumerable<SelectListItem> Roles
        {
            get
            {
                foreach (object item in Enum.GetValues(typeof(Enum_Roles)))
                {
                    var role = (Enum_Roles)item;
                    if(role.GetHashCode() <= this.LoggedOnEmployee.RoleId.Value)
                        yield return new SelectListItem() { Text = role.ToString(), Value = role.GetHashCode().ToString() };
                }
            }
        }
    }
}