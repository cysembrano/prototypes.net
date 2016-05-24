using FlowFB.Repository;
using FlowFB.Repository.Filters;
using FlowFB.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowFB.Web.Controllers
{
    [Authorize]
    public class JournalController : BaseController
    {
        //
        // GET: /Journal/

        private readonly IFAPurchaseRepository _repoPurchase;

        public JournalController(IFAPurchaseRepository repoPurchase)
        {
            this._repoPurchase = repoPurchase;
        }
        
        public ActionResult Purchases(int? projectId)
        {
            FAPurchaseFilter filter = null;
            if(projectId.HasValue)
            {
                filter = new FAPurchaseFilter();
                filter.ProjectID = projectId.Value;
            }
            var result = this._repoPurchase.SearchFAPurchase(filter);

            PurchasesModel model = new PurchasesModel();
            model.Purchases = result;
            return View(model);
        }

        public ActionResult Purchase(int? purchaseId)
        {
            return View();
        }


    }
}
