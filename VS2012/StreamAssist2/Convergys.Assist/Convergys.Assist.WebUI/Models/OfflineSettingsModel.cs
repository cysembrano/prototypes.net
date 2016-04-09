using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Convergys.Assist.Repository.BusinessModels;

namespace Convergys.Assist.WebUI.Models
{
    public class OfflineSettingsModel : BaseModel
    {
        public Team[] TeamList { get; set; }
        public int? TeamSelectedId { get; set; }
    }
}