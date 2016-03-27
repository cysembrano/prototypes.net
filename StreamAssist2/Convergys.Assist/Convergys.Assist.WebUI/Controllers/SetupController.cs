using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Convergys.Assist.Repository;
using Convergys.Assist.Repository.BusinessModels;
using Convergys.Assist.Repository.Enums;
using Convergys.Assist.WebUI.Infrastructure;
using Convergys.Assist.WebUI.Models;

namespace Convergys.Assist.WebUI.Controllers
{
    public class SetupController : BaseController
    {
        private readonly IEmployeeRepository _repoEmployee;

        public SetupController(IEmployeeRepository repoEmployee)
        {
            this._repoEmployee = repoEmployee;
        }

        public ActionResult Index(EmployeeView empView, bool? isRoleChangeSuccess)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            //If not Team Admin. Send him to Access Invalid page.
            if (!IsAuthorized(empView, Enum_Roles.TeamAdmin)) return View("AccessInvalid");

            SetupModel model = new SetupModel();
            model.LoggedOnEmployee = empView;
            model.RoleChangeOccured = isRoleChangeSuccess.HasValue;
            if (model.RoleChangeOccured)
                model.RoleChangeSuccess = isRoleChangeSuccess.Value;
            model.TeamList = _repoEmployee.ViewTeams(empView);
            model.SelectedTeamId = empView.TeamId.Value;
            return View(model);
        }


        /// <param name="id">Team Id</param>
        public ActionResult GetEmployeeListByTeam(EmployeeView empView, int? id)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            StringBuilder sb = new StringBuilder();
            if (id.HasValue)
            {
                var empList = _repoEmployee.ViewEmployeesByTeamWithRole(empView, id.Value);
                if (empList.Length != 0)
                {
                    sb.Append("<option value=\"\">Select Employee</option>");
                    foreach (var res in empList)
                    {
                        sb.Append(string.Format("<option value=\"{0}\">{1}</option>", res.EmpId + "|" + res.RoleId,  res.FirstName));
                    }
                }
            }
            return Content(sb.ToString());
        }

        [HttpPost]
        public ActionResult SaveEmployeeRole(EmployeeView empView, SetupModel model)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            //If not Team Admin. Send him to Access Invalid page.
            if (!IsAuthorized(empView, Enum_Roles.TeamAdmin)) return View("AccessInvalid");
            decimal empId = decimal.Parse(model.RoleChangeSelectedEmp.Split('|')[0]);
            Enum_Roles role = (Enum_Roles)Enum.Parse(typeof(Enum_Roles), model.RoleChangeSelectedRole.ToString());
            bool success;
            _repoEmployee.ChangeRole(empView, empId, role, out success);

            return RedirectToAction("Index", new { isRoleChangeSuccess = success });
        }
    }
}
