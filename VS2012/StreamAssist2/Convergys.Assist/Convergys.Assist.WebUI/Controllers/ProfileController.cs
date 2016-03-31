using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Convergys.Assist.Repository;
using Convergys.Assist.Repository.BusinessModels;
using Convergys.Assist.WebUI.Infrastructure;
using Convergys.Assist.WebUI.Models;

namespace Convergys.Assist.WebUI.Controllers
{
    public class ProfileController : BaseController
    {
        //
        // GET: /Profile/
        private readonly IEmployeeRepository _repoEmployee;

        public ProfileController(IEmployeeRepository repoEmployee)
        {
            this._repoEmployee = repoEmployee;
        }

        public ActionResult Index(EmployeeView empView,
                                  bool? isPwdSuccess,
                                  bool? isTimezoneSuccess)
        {
            //Guard Clause:  In case the cookie containing employeeView is null.
            if (empView == null) return RedirectToAction("Login", "Public");

            ProfileModel model = new ProfileModel();
            if (empView.HrDifference.HasValue)
                model.SelectedTimezone = FormatTimeZone(empView.HrDifference.Value);

            model.PwdChangeOccured = isPwdSuccess.HasValue;
            if (model.PwdChangeOccured)
                model.PwdChangeSuccess = isPwdSuccess.Value;
            model.TZChangeOccured = isTimezoneSuccess.HasValue;
            if (model.TZChangeOccured)
                model.TZChangeSuccess = isTimezoneSuccess.Value;

            model.LoggedOnEmployee = empView;
            model.IsDst = empView.TimeZoneDST.HasValue ? empView.TimeZoneDST.Value : false;
            return View(model);
        }

        [HttpPost]
        public ActionResult SavePassword(EmployeeView empView, ProfileModel model)
        {
            bool success;
            var empViewMod = _repoEmployee.ChangePassword(empView, empView.EmpId, model.OldPwd, model.NewPwd, out success);
            model.ChangeSuccess = success;
            RefreshSecurityCard(empViewMod);
            return RedirectToAction("Index", new { isPwdSuccess = model.ChangeSuccess });
        }

        [HttpPost]
        public ActionResult SaveTimezone(EmployeeView empView, ProfileModel model)
        {
            bool success;

            var empViewMod = _repoEmployee.ChangeTimezone(empView, empView.EmpId, model.SelectedTimezone, model.IsDst, out success);
            model.ChangeSuccess = success;
            RefreshSecurityCard(empViewMod);
            return RedirectToAction("Index", new { isTimezoneSuccess = success });
        }

    }
}
