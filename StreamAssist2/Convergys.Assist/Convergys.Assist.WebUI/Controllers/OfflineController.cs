using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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
    public class OfflineController : BaseController
    {

        public const int DEFAULTSEARCHPAGESIZE = 10;

        private readonly IOfflineRepository _repoOffline;
        private readonly IEmployeeRepository _repoEmployee;

        public OfflineController(IOfflineRepository _repoOffline, IEmployeeRepository repoEmployee)
        {
            this._repoOffline = _repoOffline;
            this._repoEmployee = repoEmployee;
        }

        #region Common
        /// <summary>
        /// Can only access if you are assigned or creator or at least teamadmin of team where callback is assigned.
        /// </summary>
        private bool HasViewRights(EmployeeView empView, OfflineSearchView offline, Team[] teams)
        {
            return (offline.EmpId == empView.EmpId)
                || (empView.RoleId.GetHashCode() > Enum_Roles.SupportProfessional.GetHashCode()
                && teams.Any(h => h.TeamId == offline.TeamId));
        }

        private bool HasEditRights(EmployeeView empView, OfflineSearchView offline, Team[] teams)
        {
            return (offline.EmpId == empView.EmpId)
                || (empView.RoleId.GetHashCode() > Enum_Roles.ReportAdmin.GetHashCode()
                && teams.Any(h => h.TeamId == offline.TeamId));
        }

        public ActionResult GetActivityTypesByTeam(EmployeeView empView, int? id, int selected = -1, bool addOne = true)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            StringBuilder sb = new StringBuilder();
            if (id.HasValue)
            {
                var activityList = _repoOffline.GetOfflineActivityByTeam(empView, id.Value);
                if (activityList.Length != 0)
                {
                    if(addOne) sb.Append("<option value=\"-1\">Select Activity Type</option>");
                    foreach (var res in activityList)
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

        public ActionResult GetContactTypesByTeam(EmployeeView empView, int? id, int selected = -1, bool addOne = true)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            StringBuilder sb = new StringBuilder();
            if (id.HasValue)
            {
                var ContactList = _repoOffline.GetOfflineContactByTeam(empView, id.Value);
                if (ContactList.Length != 0)
                {
                    if(addOne) sb.Append("<option value=\"-1\">Select Contact Type</option>");
                    foreach (var res in ContactList)
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

        #region Search
        private IPagedList<OfflineSearchView> SearchOfflinePaged(OfflineSearchFilter searchFilter, string sort, int? page)
        {
            var result = _repoOffline.SearchOfflineActivities(searchFilter);

            switch (sort)
            {
                case "cdate": result = result.OrderBy(a => a.CreationDate); break;
                case "cdate_desc": result = result.OrderByDescending(a => a.CreationDate); break;
                case "assign": result = result.OrderBy(a => a.FirstName); break;
                case "assign_desc": result = result.OrderByDescending(a => a.FirstName); break;
                case "team": result = result.OrderBy(a => a.Team); break;
                case "team_desc": result = result.OrderByDescending(a => a.Team); break;
                case "case": result = result.OrderBy(a => a.CaseIdentity); break;
                case "case_desc": result = result.OrderByDescending(a => a.CaseIdentity); break;
                case "contact": result = result.OrderBy(a => a.OfflineContactType); break;
                case "contact_desc": result = result.OrderByDescending(a => a.OfflineContactType); break;
                case "activity": result = result.OrderBy(a => a.OfflineActivityType); break;
                case "activity_desc": result = result.OrderByDescending(a => a.OfflineActivityType); break;
                default: result = result.OrderByDescending(a => a.CreationDate); break;

            }

            int outSize, pageSize;
            if (Int32.TryParse(ConfigurationManager.AppSettings["OfflineController.Search.DefaultPageSize"], out outSize))
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

            OfflineSearchModel model = new OfflineSearchModel();
            model.LoggedOnEmployee = empView;
            if (!page.HasValue)
            {
                model.InitializeSearchFilter();
                model.SearchFilter.TeamId = model.LoggedOnEmployee.TeamId.GetValueOrDefault();
                model.SearchFilter.EmpId = model.LoggedOnEmployee.EmpId;
            }
            else
            {
                model.SearchFilter = new OfflineSearchFilter()
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
            model.SearchResults = SearchOfflinePaged(model.SearchFilter, sort, page);
            model.CurrentPage = page.HasValue ? page.Value : 1;


            return View(model);
        }


        [HttpPost]
        [ActionName("Search")]
        public ActionResult Search_Post(EmployeeView empView, OfflineSearchModel model)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            model.LoggedOnEmployee = empView;

            if (model.SearchFilter.EmpId == -1) model.SearchFilter.EmpId = null;

            model.TeamList = _repoEmployee.ViewTeams(empView);
            model.SearchFilter.TeamIds = model.TeamList.Select(b => b.TeamId).ToList();
            model.SearchResults = SearchOfflinePaged(model.SearchFilter, string.Empty, null);


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

        /// <param name="id">Offline Log Id</param>
        public ActionResult ChangeOfflineStatus(EmployeeView empView,
                                                 int? id,
                                                 Enum_OfflineStatus status,
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

            var found = _repoOffline.SearchOfflineActivityById(empView, id.Value);
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

            _repoOffline.SetOfflineStatus(empView, id.Value, status);
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

        /// <param name="id">Offline Log Id</param>
        public ActionResult DeleteOfflineFromSearch(EmployeeView empView,
                                                    int? id, int? page,
                                                    string sort, string key,
                                                    int? stat, DateTime? sTo,
                                                    DateTime? sFr, int? tm,
                                                    decimal? emp)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            if (!id.HasValue)
                return RedirectToAction("Search", new { empView = empView });

            var found = _repoOffline.SearchOfflineActivityById(empView, id.Value);
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

            _repoOffline.DeleteOffline(empView, id.Value);
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

            var foundOffline = _repoOffline.SearchOfflineActivityById(empView, id.Value);
            var foundOfflineEvents = _repoOffline.GetOfflineEventsByLog(empView, id.Value);

            OfflineShowModel model = new OfflineShowModel(foundOffline);
            model.LoggedOnEmployee = empView;
            model.TeamList = _repoEmployee.ViewTeams(empView);
            model.ActionTypeId = EActionType.View.GetHashCode();
            model.OfflineEvents = foundOfflineEvents;

            //BEGIN:  Set Hidden field value to JSON
            var list = new List<object>();
            foreach (var ev in foundOfflineEvents)
            {
                list.Add(OfflineEventToJson(ev).Data);
            }
            var serializer = new JavaScriptSerializer();
            model.JSONOfflineEvents = serializer.Serialize(list.ToArray());
            //END

            if (!HasViewRights(model.LoggedOnEmployee, model.Offline, model.TeamList))
                return View("AccessInvalid");

            return View("Show", model);
        }

        public ActionResult Create(EmployeeView empView)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            OfflineShowModel model = new OfflineShowModel();
            model.LoggedOnEmployee = empView;
            model.Offline = new OfflineSearchView();
            model.Offline.Team = empView.Team;
            model.Offline.TeamId = empView.TeamId.GetValueOrDefault();
            model.Offline.EmpId = empView.EmpId;
            model.Offline.FirstName = empView.FirstName;
            model.Offline.Status = Enum_OfflineStatus.Open.GetHashCode();

            model.ActionTypeId = EActionType.Create.GetHashCode();
            return View("Show", model);
        }

        public ActionResult Edit(EmployeeView empView, int? id, bool gotoView = false)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            if (!id.HasValue)
                return RedirectToAction("Create", new { empView = empView });

            OfflineShowModel model = new OfflineShowModel();
            model.LoggedOnEmployee = empView;           
            model.TeamList = _repoEmployee.ViewTeams(empView);

            var offSearchView = _repoOffline.SearchOfflineActivityById(empView, id.GetValueOrDefault());
            model.Offline = offSearchView;

            var offSearchEvent = _repoOffline.GetOfflineEventsByLog(empView, id.GetValueOrDefault());
            model.OfflineEvents = offSearchEvent;
            
            //BEGIN:  Set Hidden field value to JSON
            var list = new List<object>();
            foreach (var ev in offSearchEvent)
            {
                list.Add(OfflineEventToJson(ev).Data);
            }
            var serializer = new JavaScriptSerializer();
            model.JSONOfflineEvents = serializer.Serialize(list.ToArray());    
            //END

            model.ActionTypeId = EActionType.Edit.GetHashCode();

            if (!HasEditRights(empView,model.Offline,model.TeamList))
                return View("AccessInvalid");

            if (gotoView)
                return RedirectToAction("View", new { id = model.Offline.OfflineLogId });
            else
                return View("Show", model);
        }

        [HttpPost]
        public ActionResult Submit(EmployeeView empView, OfflineShowModel model)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            int offlineId = -1;
            bool redirectToView = false;
            if (model.ActionType == EActionType.Create)
                offlineId = _repoOffline.AddOfflineLog(empView, model.Offline);
            if (model.ActionType == EActionType.Edit)
            {
                offlineId = _repoOffline.EditOfflineLog(empView, model.Offline);
                if(offlineId > 0)
                    redirectToView = true;
            }

            return RedirectToAction("Edit", new { id = offlineId, gotoView = redirectToView });
        }

        public ActionResult SaveEvent(EmployeeView empView, int? logid, TimeSpan elapsedTime, string status, string comments, DateTime endTime)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            OfflineEvent offEvent = new OfflineEvent()
            {
                Comments = comments,
                End = endTime,
                Start = endTime - elapsedTime,
                Status = Enum.Parse(typeof(Enum_OfflineEventStatus), status).GetHashCode(),
                LogId = Convert.ToDecimal(logid)
            };
            offEvent.Id = _repoOffline.SaveOfflineEvent(empView, offEvent);
            return OfflineEventToJson(offEvent);
        }

        public ActionResult DeleteEvent(EmployeeView empView, int logId, int eventid)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            _repoOffline.DeleteOfflineEvent(empView, eventid);
            var offEvents = _repoOffline.GetOfflineEventsByLog(empView,logId);
            var list = new List<object>();
            foreach (var ev in offEvents)
            {
                list.Add(OfflineEventToJson(ev).Data);
            }
            return Json(list.ToArray());  

        }

        private JsonResult OfflineEventToJson(OfflineEvent offEvent)
        {
            var strElapsedTime = offEvent.GetElapsedTime().ToString(@"hh\:mm\:ss");
            return Json(new
            {
                endTime = offEvent.End,
                comments = offEvent.Comments,
                elapsedTime = strElapsedTime,
                status = offEvent.StatusType.ToString(),
                logid = offEvent.LogId,
                eventid = offEvent.Id
            });
        }

        #endregion

        #region Setting
        /// <param name="id">Team Id</param>
        public ActionResult Settings(EmployeeView empView, int? id)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            //If not Team Admin. Send him to Access Invalid page.
            if (!IsAuthorized(empView, Enum_Roles.TeamAdmin)) return View("AccessInvalid");

            OfflineSettingsModel model = new OfflineSettingsModel();
            model.LoggedOnEmployee = empView;
            model.TeamList = _repoEmployee.ViewTeams(empView);
            model.TeamSelectedId = id.HasValue ? id : empView.TeamId.Value;

            UpdateModel(model);

            return View(model);
        }


        /// <param name="id">Team Id</param>
        public ActionResult AddContactType(EmployeeView empView, int? id, string ContactType)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            //If not Team Admin. Send him to Access Invalid page.
            if (!IsAuthorized(empView, Enum_Roles.TeamAdmin)) return View("AccessInvalid");

            if (!id.HasValue) throw new ArgumentNullException("Team Id can not be null.");

            if (String.IsNullOrWhiteSpace(ContactType))
                return Settings(empView, id);

            OfflineContact contact = new OfflineContact();
            contact.Description = ContactType;
            contact.TeamId = id;

            var returnedId = _repoOffline.AddContactType(empView, contact);
            return RedirectToAction("Settings", new { id = id });

        }

        /// <param name="id">Team Id</param>
        public ActionResult DeleteContactType(EmployeeView empView, int? id, int? ContactTypeId)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            //If not Team Admin. Send him to Access Invalid page.
            if (!IsAuthorized(empView, Enum_Roles.TeamAdmin)) return View("AccessInvalid");

            if (!id.HasValue) throw new ArgumentNullException("Team Id can not be null.");
            if (!ContactTypeId.HasValue) throw new ArgumentNullException("ContactType Id can not be null.");

            _repoOffline.DeleteContactType(empView, ContactTypeId.Value);

            return RedirectToAction("Settings", new { id = id });
        }

        /// <param name="id">Team Id</param>
        public ActionResult SaveContactType(EmployeeView empView, int? id, int? ContactTypeId, string ContactType)
        {
            if (empView == null) return RedirectToAction("Login", "Public");
            
            //If not Team Admin. Send him to Access Invalid page.
            if (!IsAuthorized(empView, Enum_Roles.TeamAdmin)) return View("AccessInvalid");

            if (!id.HasValue) throw new ArgumentNullException("Team Id can not be null.");
            if (!ContactTypeId.HasValue) throw new ArgumentNullException("Contact Type Id can not be null.");

            if (String.IsNullOrWhiteSpace(ContactType))
                return Settings(empView, id);

            _repoOffline.SaveContactType(empView, ContactTypeId.Value, ContactType);

            return RedirectToAction("Settings", new { id = id });

        }

        /// <param name="id">Team Id</param>
        public ActionResult AddActivityType(EmployeeView empView, int? id, string ActivityType)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            //If not Team Admin. Send him to Access Invalid page.
            if (!IsAuthorized(empView, Enum_Roles.TeamAdmin)) return View("AccessInvalid");

            if (!id.HasValue) throw new ArgumentNullException("Team Id can not be null.");

            if (String.IsNullOrWhiteSpace(ActivityType))
                return Settings(empView, id);

            OfflineActivity activity = new OfflineActivity();
            activity.Description = ActivityType;
            activity.TeamId = id;

            var returnedId = _repoOffline.AddActivityType(empView, activity);
            return RedirectToAction("Settings", new { id = id });

        }

        /// <param name="id">Team Id</param>
        public ActionResult DeleteActivityType(EmployeeView empView, int? id, int? ActivityTypeId)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            //If not Team Admin. Send him to Access Invalid page.
            if (!IsAuthorized(empView, Enum_Roles.TeamAdmin)) return View("AccessInvalid");

            if (!id.HasValue) throw new ArgumentNullException("Team Id can not be null.");
            if (!ActivityTypeId.HasValue) throw new ArgumentNullException("ActivityType Id can not be null.");

            _repoOffline.DeleteActivityType(empView, ActivityTypeId.Value);

            return RedirectToAction("Settings", new { id = id });
        }

        /// <param name="id">Team Id</param>
        public ActionResult SaveActivityType(EmployeeView empView, int? id, int? ActivityTypeId, string ActivityType)
        {
            if (empView == null) return RedirectToAction("Login", "Public");

            //If not Team Admin. Send him to Access Invalid page.
            if (!IsAuthorized(empView, Enum_Roles.TeamAdmin)) return View("AccessInvalid");

            if (!id.HasValue) throw new ArgumentNullException("Team Id can not be null.");

            if (!ActivityTypeId.HasValue) throw new ArgumentNullException("Activity Type Id can not be null.");

            if (String.IsNullOrWhiteSpace(ActivityType))
                return Settings(empView, id);

            _repoOffline.SaveActivityType(empView, ActivityTypeId.Value, ActivityType);

            return RedirectToAction("Settings", new { id = id });

        }

        #endregion

    }
}
