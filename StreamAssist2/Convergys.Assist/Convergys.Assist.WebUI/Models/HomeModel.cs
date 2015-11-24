using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Convergys.Assist.Repository.BusinessModels;

namespace Convergys.Assist.WebUI.Models
{
    public class HomeModel : BaseModel
    {
        public const int SCHEDULEDAYSPAST = 4;
        public int CallbackCount { get; set; }
        public DateTime FromDate
        {
            get
            {
                return DateTime.Now.Date;
            }
        }
        public DateTime ToDate
        {
            get
            {
                //by default look back 14 days ago.
                int result;
                if (!Int32.TryParse(ConfigurationManager.AppSettings["CallbackSearchModel.Default.IntScheduledDaysInterval"], out result))
                    result = SCHEDULEDAYSPAST;

                return DateTime.Now.AddDays(result).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
        }
        public int? Team
        {
            get
            {
                return this.LoggedOnEmployee.TeamId;
            }
        }
        public decimal? EmpId
        {
            get
            {
                return this.LoggedOnEmployee.EmpId;
            }
        }
    }
}