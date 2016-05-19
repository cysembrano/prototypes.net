using FlowFB.Data;
using FlowFB.Repository;
using FlowFB.Repository.Filters;
using FlowFB.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlowFB.BusinessLogic.Extensions;

namespace FlowFB.BusinessLogic.SQLRepository
{
    public class FBProjectRepository : IFBProjectRepository
    {
        public IEnumerable<FBProject> SearchFBProjects(FBProjectFilter filter)
        {

            FBProject[] fbProjects = new FBProject[] { };
            using(FlowFBEntities context = new FlowFBEntities())
            {
                if (filter == null)
                    fbProjects = context.FFBA_Projects.AsQueryable().ToFBProjects();

            }

            return fbProjects;
        }
    }
}
