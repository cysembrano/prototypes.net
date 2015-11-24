using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Convergys.Assist.Repository.BusinessModels;

namespace Convergys.Assist.WebUI.Models
{
    public abstract class BaseModel
    {
        public EmployeeView LoggedOnEmployee { get; set; }

        protected string ParseTimezoneValue(string timezoneValue)
        {
            string returnTimezone = string.Empty;
            if ((timezoneValue.Substring(0, 5) != "(UTC)"))
            {
                if (timezoneValue.Substring(4, 1) == "-")
                {
                    returnTimezone = timezoneValue.Substring(5, 1) == "0" ?
                        String.Format("-{0}", timezoneValue.Substring(6, 4).Replace(':', '.')) : //e.g. From -05:00 to -5.00
                        String.Format("-{0}", timezoneValue.Substring(5, 5).Replace(':', '.')); //e.g. From -11:00 to -11.00
                }
                else
                {
                    returnTimezone = timezoneValue.Substring(5, 1) == "0" ?
                                             timezoneValue.Substring(6, 4).Replace(':', '.') : //e.g. From +05:00 to 5.00
                                             timezoneValue.Substring(5, 5).Replace(':', '.'); //e.g. From +11:00 to 11.00
                }
            }
            else
            {
                returnTimezone = "0.00";
            }
            return returnTimezone;
        }
    }
}