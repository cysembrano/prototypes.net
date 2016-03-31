using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Convergys.Assist.Repository.BusinessModels;
using Convergys.Assist.Repository.Enums;
using Convergys.Assist.Repository.SearchFilters;
using PagedList;

namespace Convergys.Assist.WebUI.Models
{
    public class OfflineSearchModel : BaseModel
    {
        public const int SCHEDULEDAYSPAST = 4;
        public OfflineSearchFilter SearchFilter { get; set; }
        public IPagedList<OfflineSearchView> SearchResults { get; set; }
        private int? currentPage;
        public int? CurrentPage
        {
            get
            {
                if (!currentPage.HasValue)
                    return 1;
                else
                    return currentPage;
            }
            set { currentPage = value; }
        }
        #region Lookup References
        /// <summary>
        /// Team dropdown lookup ref.
        /// </summary>
        public Team[] TeamList { get; set; }

        /// <summary>
        /// Status dropdown lookup ref.
        /// </summary>
        public Dictionary<int, string> OfflineStatusOptions = Enum.GetValues(typeof(Enum_OfflineStatus))
            .Cast<Enum_OfflineStatus>().ToDictionary(t => (int)t, t => t.ToString());

        #endregion


        public void InitializeSearchFilter()
        {
            this.SearchFilter = new OfflineSearchFilter();

            this.SearchFilter.Status = Enum_OfflineStatus.Open.GetHashCode();

            //by default look back 14 days ago.
            int result;
            if (!Int32.TryParse(ConfigurationManager.AppSettings["OfflineSearchModel.Default.IntScheduledDaysInterval"], out result))
                result = SCHEDULEDAYSPAST;

            this.SearchFilter.From = DateTime.Now.AddDays(result * -1);
            this.SearchFilter.To = DateTime.Now
                .AddDays(result)
                .Date
                .AddHours(23)
                .AddMinutes(59)
                .AddSeconds(59);
            

        }
       
    }
}