using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convergys.Assist.Repository.Enums;

namespace Convergys.Assist.Repository.BusinessModels
{
    public class OfflineEvent
    {
        public int Id { get; set; }
        public decimal? LogId { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }

        public int? Status { get; set; }
        public Enum_OfflineEventStatus StatusType
        {
            get
            {
                return (Enum_OfflineEventStatus)Enum.ToObject(typeof(Enum_OfflineEventStatus), this.Status);
            }
        }

        public DateTime? CreationDate { get; set; }
        public string Comments { get; set; }

        public TimeSpan GetElapsedTime()
        {
            return End.GetValueOrDefault() - Start.GetValueOrDefault(); 
        }
    }
}
