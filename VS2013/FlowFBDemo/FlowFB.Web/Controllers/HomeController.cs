using FlowFB.Logging;
using FlowFB.Repository;
using FlowFB.Repository.Cache;
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

        [Authorize]
        public ActionResult Setup()
        {
            SetupModel model = new SetupModel();
            return View(model);
            
        }

        [Authorize]
        public JsonResult SetupProjectUpdate(string AP, string GL, string Tax, string Cost)
        {
            ProjectCache.ProjectData["APIInvoice"] = Int32.Parse(AP);
            ProjectCache.ProjectData["CostCenter"] = Int32.Parse(Cost);
            ProjectCache.ProjectData["GLCodes"] = Int32.Parse(GL);
            ProjectCache.ProjectData["TaxCodes"] = Int32.Parse(Tax);

            return Json(String.Format("{0} {1} {2} {3}", AP, GL, Tax, Cost), JsonRequestBehavior.AllowGet);
        }

    }
}
