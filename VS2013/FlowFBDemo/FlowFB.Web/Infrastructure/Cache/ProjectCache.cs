using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowFB.Web.Infrastructure.Cache
{
    public static class ProjectCache
    {
        public static Dictionary<string, int> ProjectData { get; set; }
    }
}