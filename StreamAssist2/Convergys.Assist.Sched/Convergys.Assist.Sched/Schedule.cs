using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convergys.Assist.Sched
{
    public class Schedule
    {
        public int Id { get; set; }
        public string EmpId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string ScheduleDetail { get; set; }
        public DateTime AdjScheduleDateStart { get; set; }
        public DateTime AdjScheduleDateEnd { get; set; }
    }
}
