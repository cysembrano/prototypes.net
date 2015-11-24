using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Convergys.Assist.Logging;
using Convergys.Assist.Repository.BusinessModels;
using Convergys.Assist.Repository.Enums;
using Newtonsoft.Json;

namespace Convergys.Assist.WebUI.Infrastructure
{
    public abstract class BaseController : Controller
    {
        protected bool IsAuthorized(EmployeeView securityCard, Enum_Roles minimumRole)
        {
            if (securityCard.RoleId.GetValueOrDefault() < minimumRole.GetHashCode())
            {
                StackFrame frame = new StackFrame(1);
                Log4NetManager.Instance.Warn(
                    this.GetType(),
                    string.Format("{0}: Employee ({1}){2} Unauthorized.", frame.GetMethod().Name, securityCard.EmpId, securityCard.FirstName)
                    );

                return false;
            }

            return true;
        }

        protected override IAsyncResult BeginExecute(System.Web.Routing.RequestContext requestContext, AsyncCallback callback, object state)
        {
            var culture = requestContext.HttpContext.Request.QueryString["lang"];

            //Check if lang is present
            if (string.IsNullOrWhiteSpace(culture))
                return base.BeginExecute(requestContext, callback, state);

            //Check if lang is valid
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo(culture);
            if (cultureInfo == null)
                return base.BeginExecute(requestContext, callback, state);

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;



            return base.BeginExecute(requestContext, callback, state);
        }

        protected void RefreshSecurityCard(EmployeeView empView)
        {
            /// In order to pickup the settings from config, we create a default cookie and use its values to create a 
            /// new one.
            var cookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            var ticket = FormsAuthentication.Decrypt(cookie.Value);

            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration,
                ticket.IsPersistent, JsonConvert.SerializeObject(empView), ticket.CookiePath);
            var encTicket = FormsAuthentication.Encrypt(newTicket);

            /// Use existing cookie. Could create new one but would have to copy settings over...
            HttpContext.Response.Cookies[FormsAuthentication.FormsCookieName].Value = encTicket;

        }

        protected string FormatTimeZone(int hr, int min)
        {
            if (hr == 0)
                return "0.00"; //UTC

            return String.Format("{0}.{1}", hr, min.ToString() == "0" ? "00" : min.ToString());
        }

        protected string FormatTimeZone(string timezone)
        {
            string[] utcSplit = timezone.Split('.');
            int hr = int.Parse(utcSplit[0]);
            var min = int.Parse(utcSplit[1]);

            return FormatTimeZone(hr, min);
        }

        protected string FormatTimeZone(decimal timezone)
        {
            return FormatTimeZone(timezone.ToString("#0.00"));
        }
    }
}
