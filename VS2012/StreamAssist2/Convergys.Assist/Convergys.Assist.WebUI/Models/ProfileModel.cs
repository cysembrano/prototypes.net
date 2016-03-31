using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Convergys.Assist.WebUI.Models
{
    public class ProfileModel : BaseModel
    {
        public string NewPwd { get; set; }
        public string OldPwd { get; set; }
        public bool ChangeSuccess { get; set; }
        public bool PwdChangeOccured { get; set; }
        public bool PwdChangeSuccess { get; set; }

        public string SelectedTimezone { get; set; }
        public bool IsDst { get; set; }

        public IEnumerable<SelectListItem> ChangeTimeZoneCollection
        {
            get
            {
                return new SelectList(TimeZoneInfo.GetSystemTimeZones()
                    .Select(q => new SelectListItem
                    {
                        Text = q.DisplayName,
                        Value = (ParseTimezoneValue(q.DisplayName))
                    }
                    ), "Value", "Text", this.SelectedTimezone);
            }
        }

        public bool TZChangeOccured { get; set; }
        public bool TZChangeSuccess { get; set; }
    }
}