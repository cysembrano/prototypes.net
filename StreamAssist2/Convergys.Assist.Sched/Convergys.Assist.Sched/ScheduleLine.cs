using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convergys.Assist.Sched
{
    public class ScheduleLine
    {
        public int ScheduleId { get; set; }
        public string Activity { get; set; }
        public string ActivityStart { get; set; }
        public string ActivityEnd { get; set; }
    }
}
