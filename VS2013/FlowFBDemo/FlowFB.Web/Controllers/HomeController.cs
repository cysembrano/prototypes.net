using FlowFB.Logging;
using FlowFB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowFB.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFBProjectRepository _repoProject;

        public HomeController(IFBProjectRepository repoProject)
        {
            this._repoProject = repoProject;
        }

        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {

            var result = _repoProject.SearchFBProjects(null);
            string msg = String.Empty;
            foreach (var r in result)
            {
                msg += "|" + r.ProjectName;
            }
            ViewBag.Message = msg;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
