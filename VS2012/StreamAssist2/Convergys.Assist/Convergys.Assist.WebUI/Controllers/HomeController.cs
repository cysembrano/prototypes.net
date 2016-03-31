using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Convergys.Assist.Repository;
using Convergys.Assist.Repository.BusinessModels;
using Convergys.Assist.Repository.SearchFilters;
using Convergys.Assist.WebUI.Infrastructure;
using Convergys.Assist.WebUI.Models;
using Newtonsoft.Json;

namespace Convergys.Assist.WebUI.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ICallbackRepository _repoCallback;
        private readonly IOfflineRepository _repoOffline;

        public HomeController(ICallbackRepository repoCallback, IOfflineRepository repoOffline)
        {
            this._repoCallback = repoCallback;
            this._repoOffline = repoOffline;
        }

        public ActionResult Index(EmployeeView empView)
        {
            //Guard Clause:  In case the cookie containing employeeView is null.
            if (empView == null) return RedirectToAction("Login", "Public");
            
            HomeModel model = new HomeModel();
            model.LoggedOnEmployee = empView;
            CallbackSearchFilter search = new CallbackSearchFilter();
            search.EmpId = empView.EmpId;
            search.From = model.FromDate;
            search.To = model.ToDate;
            search.TeamId = model.Team;
            search.Status = 1;
            var callbacks = _repoCallback.SearchCallbacks(search);
            model.CallbackCount = callbacks.Count();

            OfflineSearchFilter offSearch = new OfflineSearchFilter();
            offSearch.EmpId = empView.EmpId;
            offSearch.From = model.OfflineFromDate;
            offSearch.To = model.ToDate;
            offSearch.TeamId = model.Team;
            offSearch.Status = 1;
            var offlines = _repoOffline.SearchOfflineActivities(offSearch);
            model.OfflineCount = offlines.Count();
            return View(model);
        }

        

    }
}
