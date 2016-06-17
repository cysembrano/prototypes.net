using FlowFB.Repository;
using FlowFB.Repository.Cache;
using FlowFB.Repository.Filters;
using FlowFB.Repository.Models;
using FlowFB.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowFB.Web.Controllers
{
    [Authorize]
    public class CodesController : Controller
    {
        private readonly IFACodesRepository _repoCodes;

        public CodesController(IFACodesRepository _repoCodes)
        {
            this._repoCodes = _repoCodes;
        }


        //
        // GET: /GL/

        public ActionResult GL()
        {
            var result = _repoCodes.SearchCode(new FAGLCodeFilter() { });
            CodesModel model = new CodesModel() { FACodes = result };
            return View(model);
        }

        //
        // GET: /Taxt/
        public ActionResult Tax()
        {
            var result = _repoCodes.SearchCode(new FATaxCodeFilter() { });
            CodesModel model = new CodesModel() { FACodes = result };
            return View(model);
        }

        //
        // GET: /CostCenter/
        public ActionResult CostCenter()
        {
            var result = _repoCodes.SearchCode(new FACostCenterCodeFilter() { });
            CodesModel model = new CodesModel() { FACodes = result };
            return View(model);
        }
        [HttpPost]
        public string SaveGLCodes(List<MyRow> myRows)
        {
            myRows.Remove(myRows.FirstOrDefault(j => String.IsNullOrEmpty(j.ID) && String.IsNullOrEmpty(j.Code) && String.IsNullOrEmpty(j.Description)));
            string returnText = String.Empty;
            //Add or Edit
            foreach (var ro in myRows)
            {
                if (String.IsNullOrWhiteSpace(ro.ID))
                {
                    var resint = _repoCodes.CreateCode("GLCodes", ro.Code, ro.Description);
                    ro.ID = resint.GetValueOrDefault().ToString();
                    returnText = ro.ID;
                }
                else
                {
                    _repoCodes.EditCode("GLCodes", ro.Code, ro.Description, Int32.Parse(ro.ID));
                }

            }
            //Delete missing
            List<FACode> codes = _repoCodes.SearchCode(new FAGLCodeFilter() { }).ToList();
            foreach(var item in codes)
            {
                bool stay = false;
                foreach(var ro in myRows)
                {
                    int tp;
                    if (Int32.TryParse(ro.ID, out tp) && tp == item.Id)
                    {
                        stay = true;
                        break;
                    }                     
                }
                if(!stay)
                {
                    _repoCodes.DeleteCode("GLCodes", item.Id);
                }
               
            }

            return returnText;

        }

        [HttpPost]
        public string SaveTaxCodes(List<MyRow> myRows)
        {
            myRows.Remove(myRows.FirstOrDefault(j => String.IsNullOrEmpty(j.ID) && String.IsNullOrEmpty(j.Code) && String.IsNullOrEmpty(j.Description)));
            string returnText = String.Empty;
            //Add or Edit
            foreach (var ro in myRows)
            {
                if (String.IsNullOrWhiteSpace(ro.ID))
                {
                    var resint = _repoCodes.CreateCode("TaxCodes", ro.Code, ro.Description);
                    ro.ID = resint.GetValueOrDefault().ToString();
                    returnText = ro.ID;
                }
                else
                {
                    _repoCodes.EditCode("TaxCodes", ro.Code, ro.Description, Int32.Parse(ro.ID));
                }

            }
            //Delete missing
            List<FACode> codes = _repoCodes.SearchCode(new FATaxCodeFilter() { }).ToList();
            foreach (var item in codes)
            {
                bool stay = false;
                foreach (var ro in myRows)
                {
                    int tp;
                    if (Int32.TryParse(ro.ID, out tp) && tp == item.Id)
                    {
                        stay = true;
                        break;
                    }
                }
                if (!stay)
                {
                    _repoCodes.DeleteCode("TaxCodes", item.Id);
                }

            }

            return returnText;

        }

        [HttpPost]
        public string SaveCostCenterCodes(List<MyRow> myRows)
        {
            myRows.Remove(myRows.FirstOrDefault(j => String.IsNullOrEmpty(j.ID) && String.IsNullOrEmpty(j.Code) && String.IsNullOrEmpty(j.Description)));
            string returnText = String.Empty;
            //Add or Edit
            foreach (var ro in myRows)
            {
                if (String.IsNullOrWhiteSpace(ro.ID))
                {
                    var resint = _repoCodes.CreateCode("CostCenter", ro.Code, ro.Description);
                    ro.ID = resint.GetValueOrDefault().ToString();
                    returnText = ro.ID;
                }
                else
                {
                    _repoCodes.EditCode("CostCenter", ro.Code, ro.Description, Int32.Parse(ro.ID));
                }

            }
            //Delete missing
            List<FACode> codes = _repoCodes.SearchCode(new FACostCenterCodeFilter() { }).ToList();
            foreach (var item in codes)
            {
                bool stay = false;
                foreach (var ro in myRows)
                {
                    int tp;
                    if (Int32.TryParse(ro.ID, out tp) && tp == item.Id)
                    {
                        stay = true;
                        break;
                    }
                }
                if (!stay)
                {
                    _repoCodes.DeleteCode("CostCenter", item.Id);
                }

            }

            return returnText;

        }
    }
}
