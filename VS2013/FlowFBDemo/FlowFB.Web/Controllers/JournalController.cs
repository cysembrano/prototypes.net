using FlowFB.Logging;
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
        
        public ActionResult Purchases(int? projectId, int? statusId)
        {
            FAPurchaseFilter filter = null;
            if(projectId.HasValue)
            {
                filter = new FAPurchaseFilter();
                filter.ProjectID = projectId.Value;
                filter.Status = statusId;
            }
            var result = this._repoPurchase.SearchFAPurchases(filter);

            PurchasesModel model = new PurchasesModel();
            model.Purchases = result;
            return View(model);
        }

        public ActionResult Purchase(int? purchaseId)
        {
            var result = this._repoPurchase.SearchFAPurchase(purchaseId.GetValueOrDefault());
            PurchaseModel model = new PurchaseModel();
            model.Purchase = result;
            return View(model);
        }

        [HttpPost()]
        [ActionName("Purchase")]
        public ActionResult Purchase_Post()
         {
            PurchaseModel model = new PurchaseModel();
            UpdateModel(model);

            try
            {
                _repoPurchase.SaveFAPurchaseComment(model.Purchase.PurchaseID, model.Purchase.Comments);

                ViewBag.Message = "Purchase change has been saved.";
            }
            catch(Exception e)
            {
                Log4NetManager.Instance.Error(this.GetType(), e);
            }


            return View(model);
        }


    }
}
