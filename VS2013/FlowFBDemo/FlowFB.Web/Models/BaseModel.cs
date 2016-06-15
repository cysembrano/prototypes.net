using FlowFB.Web.Infrastructure.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowFB.Web.Models
{
    public abstract class BaseModel
    {
        public Dictionary<string, int> Projects = ProjectCache.ProjectData;
    }
}
