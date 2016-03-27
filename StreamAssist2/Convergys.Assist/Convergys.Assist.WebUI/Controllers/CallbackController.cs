using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Convergys.Assist.Repository;
using Convergys.Assist.Repository.BusinessModels;
using Convergys.Assist.Repository.Enums;
using Convergys.Assist.Repository.SearchFilters;
using Convergys.Assist.WebUI.Infrastructure;
using Convergys.Assist.WebUI.Models;
using PagedList;

namespace Convergys.Assist.WebUI.Controllers
{
    [Authorize]
    public class CallbackController : BaseController
    {

        public const int DEFAULTSEARCHPAGESIZE = 10;

        private readonly ICallbackRepository _repoCallback;
        private readonly IEmployeeRepository _repoEmployee;

        public CallbackController(ICallbackRepository repoCallback, IEmployeeRepository repoEmployee)
        {
            this._repoCallback = repoCallback;
            this._repoEmployee = repoEmployee;
        }

        #region Common
        /// <summary>
        /// Can only access if you are assigned or creator or at least teamadmin of team where callback is assigned.
        /// </summary>
        private bool HasViewRights(EmployeeView empView, Callback callback, Team[] teams)
        {
            return (callback.AssignedToEmpId == empView.EmpId || callback.CreatedByEmpId == empView.EmpId)
                || (empView.RoleId.GetHashCode() > Enum_Roles.SupportProfessional.GetHashCode()
                && teams.Any(h => h.TeamId == callback.AssignedToTeamId));
        }

        /// <summary>
        /// Can only access if you are assigned or creator or at least teamadmin of team where callback is assigned.
        /// </summary>
        private bool HasEditRights(EmployeeView empView, Callback callback, Team[] teams)
        {
            return (callback.AssignedToEmpId == empView.EmpId || callback.CreatedByEmpId == empView.EmpId)
                || (empView.RoleId.GetHashCode() > Enum_Roles.ReportAdmin.GetHashCode()
                && teams.Any(h => h.TeamId == callback.AssignedToTeamId));
        }
        #endregion

        #region Search
        private IPagedList<CallbackSearchView> SearchCallbacksPaged(CallbackSearchFilter searchFilter, string sort, int? page)
        {
            var result = _repoCallback.SearchCallbacks(searchFilter);

            switch (sort)
            {
                case "sched": result = result.OrderBy(a => a.AgentCallbackTimeStart); break;
                case "sched_desc": result = result.OrderByDescending(a => a.AgentCallbackTimeStart); break;
                case "assign": result = result.OrderBy(a => a.AssignedToName); break;
                case "assign_desc": result = result.OrderByDescending(a => a.AssignedToName); break;
                case "team": result = result.OrderBy(a => a.TeamName); break;
                case "team_desc": result = result.OrderByDescending(a => a.TeamName); break;
                case "cust": result = result.OrderBy(a => a.CustomerName); break;
                case "cust_desc": result = result.OrderByDescending(a => a.CustomerName); break;
                case "contact": result = result.OrderBy(a => a.Contact1Phone); break;
                case "contact_desc": result = result.OrderByDescending(a => a.Contact1Phone); break;
                case "reason": result = result.OrderBy(a => a.CallbackReasonDescription); break;
                case "reason_desc": result = result.OrderByDescending(a => a.CallbackReasonDescription); break;
                default: result = result.OrderByDescending(a => a.AgentCallbackTimeStart); break;

            }

            int outSize, pageSize;
            if (Int32.TryParse(ConfigurationManager.AppSettings["CallbackController.Search.DefaultPageSize"], out outSize))
                pageSize = outSize;
            else
                pageSize = DEFAULTSEARCHPAGESIZE;

            int pageNumber = (page ?? 1);

            return result.ToPagedList(pageNumber, pageSize);
        }

        [HttpGet]
        [ActionName("Search")]
        public ActionResult Search_Get(EmployeeView empView,
                                       int? page,
                                       string sort,
                                       string key,
                                       int? stat,
                                       DateTime? sTo,
                                       DateTime? sFr,
                                       int? tm,
                                       decimal? emp
                                       )
        {

            if (empView == null) return RedirectToAction("Login", "Public");

            CallbackSearchModel model = new CallbackSearchModel();
            model.LoggedOnEmployee = empView;
            if (!page.HasValue)
            {
                model.InitializeSearchFilter();
                model.SearchFilter.TeamId = model.LoggedOnEmployee.TeamId.GetValueOrDefault();
                model.SearchFilter.EmpId = model.LoggedOnEmployee.EmpId;
            }
            else
            {
                model.SearchFilter = new CallbackSearchFilter()
                {
                    Keyword = key,
                    TeamId = tm,
                    Status = stat,
                    EmpId = emp,
                    From = sFr,
                    To = sTo
                };
            }

            if (model.SearchFilter.EmpId == -1) model.SearchFilter.EmpId = null;


            model.TeamList = _repoEmployee.ViewTeams(empView);
            model.SearchFilter.TeamIds = model.TeamList.Select(b => b.TeamId).ToList();
            model.SearchResults = SearchCallbacksPaged(model.SearchFilter, sort, page);
            model.CurrentPage = page.HasValue ? page.Value : 1;


            return View(model);
        }


        [HttpPost]
        [ActionName("Search")]
        public ActionResult Search_Post(EmployeeView empView, CallbackSearchModel model)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            model.LoggedOnEmployee = empView;

            if (model.SearchFilter.EmpId == -1) model.SearchFilter.EmpId = null;

            model.TeamList = _repoEmployee.ViewTeams(empView);
            model.SearchFilter.TeamIds = model.TeamList.Select(b => b.TeamId).ToList();
            model.SearchResults = SearchCallbacksPaged(model.SearchFilter, string.Empty, null);


            return View(model);
        }

        [HttpGet]
        public ActionResult GetEmployeeTeams(EmployeeView empView, int? id, int? EmpId, bool anyOption = true)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            StringBuilder sb = new StringBuilder();
            if (anyOption) sb.Append("<option value='-1'>Any Employee</option>");
            if (id.HasValue)
            {
                var empResult = _repoEmployee.ViewEmployeesByTeam(empView, id);

                foreach (var item in empResult)
                {
                    //Set the loggedin guy as the default
                    if (!EmpId.HasValue && String.Compare(item.EmpId.ToString("c"), empView.EmpId.ToString("c")) == 0)
                    {
                        sb.Append(string.Format("<option selected='selected' value='{0}'>{1}</option>", item.EmpId, item.FirstName));
                        continue;
                    }//Set on post back set the selected guy.
                    else if (EmpId.HasValue && String.Compare(item.EmpId.ToString("c"), EmpId.Value.ToString("c")) == 0)
                    {
                        sb.Append(string.Format("<option selected='selected' value='{0}'>{1}</option>", item.EmpId, item.FirstName));
                        continue;
                    }//otherwise just create an option tag for everyone.
                    sb.Append(string.Format("<option value='{0}'>{1}</option>", item.EmpId, item.FirstName));
                }
            }
            return Content(sb.ToString());
        }


        /// <param name="id">Callback Id</param>
        public ActionResult ChangeCallbackStatus(EmployeeView empView,
                                                 int? id,
                                                 Enum_CallbackStatus status,
                                                 int? page,
                                                 string sort,
                                                 string key,
                                                 int? stat,
                                                 DateTime? sTo,
                                                 DateTime? sFr,
                                                 int? tm,
                                                 decimal? emp)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            if (!id.HasValue)
                return RedirectToAction("Search", new { empView = empView });

            var found = _repoCallback.ViewCallback(empView, id.Value);
            var teamlist = _repoEmployee.ViewTeams(empView);

            if (found != null)
            {
                if (!HasEditRights(empView, found, teamlist))
                    return View("AccessInvalid");
            }
            else
            {
                return RedirectToAction("Search", new { empView = empView });
            }

            _repoCallback.SetCallbackStatus(empView, id.Value, status);
            return RedirectToAction("Search", new
            {
                empView = empView,
                stat = stat,
                page = page,
                sort = sort,
                key = key,
                sTo = sTo,
                sFr = sFr,
                tm = tm,
                emp = emp
            });


        }

        /// <param name="id">Callback Id</param>
        public ActionResult DeleteCallbackFromSearch(EmployeeView empView,
                                                    int? id, int? page,
                                                    string sort,string key,
                                                    int? stat,DateTime? sTo,
                                                    DateTime? sFr, int? tm,
                                                    decimal? emp)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            if (!id.HasValue)
                return RedirectToAction("Search", new { empView = empView });

            var found = _repoCallback.ViewCallback(empView, id.Value);
            var teamlist = _repoEmployee.ViewTeams(empView);

            if (found != null)
            {
                if (!HasEditRights(empView, found, teamlist))
                    return View("AccessInvalid");
            }
            else
            {
                return RedirectToAction("Search", new { empView = empView });
            }

            _repoCallback.DeleteCallback(empView, id.Value);
            return RedirectToAction("Search", new
            {
                empView = empView,
                stat = stat,
                page = page,
                sort = sort,
                key = key,
                sTo = sTo,
                sFr = sFr,
                tm = tm,
                emp = emp
            });


        }
        #endregion

        #region Show


        public ActionResult View(EmployeeView empView, int? id)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            if (!id.HasValue)
                return RedirectToAction("Create", new { empView = empView });

            var foundCallback = _repoCallback.ViewCallback(empView, id.Value);

            CallbackShowModel model = new CallbackShowModel();
            model.Callback = foundCallback;
            model.LoggedOnEmployee = empView;
            model.TeamList = _repoEmployee.ViewTeams(empView);
            model.ActionTypeId = EActionType.View.GetHashCode();

            if (!HasViewRights(model.LoggedOnEmployee, model.Callback, model.TeamList))
                return View("AccessInvalid");

            return View("Show", model);
        }

        public ActionResult Create(EmployeeView empView)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            CallbackShowModel model = new CallbackShowModel();
            model.Callback = new Callback();
            model.LoggedOnEmployee = empView;
            model.TeamList = _repoEmployee.ViewTeams(empView);
            model.AgentSelectedTimezone = model.LoggedOnEmployee.HrDifference.Value.ToString("0.00");
            model.CustSelectedTimezone = model.LoggedOnEmployee.HrDifference.Value.ToString("0.00");

            model.Callback.AssignedToTeamId = Convert.ToDecimal(empView.TeamId);
            model.Callback.AssignedToTeamName = empView.Team;
            model.Callback.AssignedToEmpId = empView.EmpId;
            model.Callback.AssignedToEmpName = empView.FirstName;
            model.Callback.CreatedByEmpId = empView.EmpId;
            model.Callback.CreatedByEmpName = empView.FirstName;

            model.ActionTypeId = EActionType.Create.GetHashCode();
            return View("Show", model);
        }

        public ActionResult Edit(EmployeeView empView, int? id)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            if (!id.HasValue)
                return RedirectToAction("Create", new { empView = empView });

            var foundCallback = _repoCallback.ViewCallback(empView, id.Value);

            CallbackShowModel model = new CallbackShowModel();
            model.Callback = foundCallback;
            model.LoggedOnEmployee = empView;
            model.TeamList = _repoEmployee.ViewTeams(empView);
            model.ActionTypeId = EActionType.Edit.GetHashCode();
            model.AgentSelectedTimezone = GetCallbackTimezone(foundCallback.AgentCallbackTimeStart);
            model.CustSelectedTimezone = GetCallbackTimezone(foundCallback.CustomerCallbackTimeStart);

            if (!HasEditRights(model.LoggedOnEmployee, model.Callback, model.TeamList))
                return View("AccessInvalid");

            return View("Show", model);
        }

        [HttpPost]
        public ActionResult Submit(EmployeeView empView, CallbackShowModel model)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            model.LoggedOnEmployee = empView;
            model.TeamList = _repoEmployee.ViewTeams(empView);
            model.Callback.AgentCallbackTimeStart = SetCallbackStartTime(model.Callback.AgentCallbackTimeStart, model.AgentSelectedTimezone);
            model.Callback.CustomerCallbackTimeStart = SetCallbackStartTime(model.Callback.CustomerCallbackTimeStart, model.CustSelectedTimezone);

            int callbackId = -1;
            if (model.ActionType == EActionType.Create)
                callbackId = _repoCallback.AddCallback(empView, model.Callback);
            if (model.ActionType == EActionType.Edit)
                callbackId = _repoCallback.EditCallback(empView, model.Callback);

            return RedirectToAction("View", new { id = callbackId });
        }

        private DateTimeOffset? SetCallbackStartTime(DateTimeOffset? dt, string utcOffset)
        {
            if (!dt.HasValue)
                return null;

            string[] utcSplit = utcOffset.Split('.');
            int hrs = int.Parse(utcSplit[0]);
            var min = int.Parse(utcSplit[1]);

            var dateTimeTemp = dt.Value.DateTime;
            return new DateTimeOffset(dateTimeTemp, new TimeSpan(hrs, min, 0));

        }

        private string GetCallbackTimezone(DateTimeOffset? dt)
        {
            if (!dt.HasValue)
                return null;
            return FormatTimeZone(dt.Value.Offset.Hours, dt.Value.Offset.Minutes);
        }

        public ActionResult GetReasonListByTeamShow(EmployeeView empView, int? id, int selected = -1)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            StringBuilder sb = new StringBuilder();
            if (id.HasValue)
            {
                var reasonList = _repoCallback.GetCallbackReasonsByTeam(empView, id.Value);
                if (reasonList.Length != 0)
                {
                    sb.Append("<option value=\"-1\">Select Reason</option>");
                    foreach (var res in reasonList)
                    {
                        if (selected == res.Id)
                            sb.Append(string.Format("<option value=\"{0}\" selected>{1}</option>", res.Id, res.Description));
                        else
                            sb.Append(string.Format("<option value=\"{0}\">{1}</option>", res.Id, res.Description));
                    }
                }
            }
            return Content(sb.ToString());
        }
        #endregion

        #region Settings

        /// <param name="id">Team Id</param>
        public ActionResult GetReasonListByTeamSettings(EmployeeView empView, int? id)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            StringBuilder sb = new StringBuilder();
            if (id.HasValue)
            {
                var reasonList = _repoCallback.GetCallbackReasonsByTeam(empView, id.Value);
                if (reasonList.Length != 0)
                {
                    foreach (var res in reasonList)
                    {
                        sb.Append(string.Format("<option value=\"{0}\">{1}</option>", res.Id, res.Description));
                    }
                }
            }
            return Content(sb.ToString());
        }

        /// <param name="id">Team Id</param>
        public ActionResult Settings(EmployeeView empView, int? id)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            //If not Team Admin. Send him to Access Invalid page.
            if (!IsAuthorized(empView, Enum_Roles.TeamAdmin)) return View("AccessInvalid");

            CallbackSettingsModel model = new CallbackSettingsModel();
            model.LoggedOnEmployee = empView;
            model.TeamList = _repoEmployee.ViewTeams(empView);
            model.TeamSelectedId = id.HasValue ? id : empView.TeamId.Value;

            UpdateModel(model);

            return View(model);
        }

        /// <param name="id">Team Id</param>
        public ActionResult AddReason(EmployeeView empView, int? id, string reason)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            if (!id.HasValue) throw new ArgumentNullException("Team Id can not be null.");

            if (String.IsNullOrWhiteSpace(reason))
                return Settings(empView, id);

            CallbackReason callbackReason = new CallbackReason();
            callbackReason.CreateDate = DateTime.Now;
            callbackReason.Description = reason;
            callbackReason.TeamId = id;
            callbackReason.ModifiedDate = null;
            callbackReason.ModifiedBy = empView.EmpId;

            var returnedId = _repoCallback.AddReason(empView, callbackReason);
            return RedirectToAction("Settings", new { id = id });

        }

        /// <param name="id">Team Id</param>
        public ActionResult DeleteReason(EmployeeView empView, int? id, int? reasonId)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            if (!id.HasValue) throw new ArgumentNullException("Team Id can not be null.");
            if (!reasonId.HasValue) throw new ArgumentNullException("Reason Id can not be null.");

            _repoCallback.DeleteReason(empView, reasonId.Value);

            return RedirectToAction("Settings", new { id = id });
        }

        /// <param name="id">Team Id</param>
        public ActionResult SaveReason(EmployeeView empView, int? id, int? reasonId, string reason)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            if (!id.HasValue) throw new ArgumentNullException("Team Id can not be null.");
            if (!reasonId.HasValue) throw new ArgumentNullException("Reason Id can not be null.");

            if (String.IsNullOrWhiteSpace(reason))
                return Settings(empView, id);

            _repoCallback.SaveReason(empView, reasonId.Value, reason);

            return RedirectToAction("Settings", new { id = id });

        }

        #endregion
    }
}
