using FlowFB.Logging;
using FlowFB.Repository;
using FlowFB.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowFB.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IFBProjectRepository _repoProject;

        public HomeController(IFBProjectRepository repoProject)
        {
            this._repoProject = repoProject;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Projects()
        {
            ProjectsModel model = new ProjectsModel();
            model.Projects = _repoProject.SearchFBProjects(null);
            return View(model);
        }



    }
}
