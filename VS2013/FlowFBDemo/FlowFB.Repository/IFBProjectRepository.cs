using FlowFB.Repository.Filters;
using FlowFB.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowFB.Repository
{
    public interface IFBProjectRepository
    {
        IEnumerable<FBProject> SearchFBProjects(FBProjectFilter filter);

    }
}
