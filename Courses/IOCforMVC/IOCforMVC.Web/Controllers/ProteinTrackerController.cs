using IOCforMVC.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IOCforMVC.Web.Controllers
{
    public class ProteinTrackerController : Controller
    {
        private IProteinTrackingService _proteinTrackingService;

        public ProteinTrackerController(IProteinTrackingService proteinTrackingService)
        {
            this._proteinTrackingService = proteinTrackingService;
        }

        //
        // GET: /ProteinTracker/

        public ActionResult Index()
        {
            ViewBag.Total = _proteinTrackingService.Total;
            ViewBag.Goal = _proteinTrackingService.Goal;
            return View();
        }

        public ActionResult AddProtein(int? amount)
        {
            _proteinTrackingService.AddProtein(amount.GetValueOrDefault());
            ViewBag.Goal = _proteinTrackingService.Goal;
            ViewBag.Total = _proteinTrackingService.Total;
            return View("Index");

        }

    }
}
