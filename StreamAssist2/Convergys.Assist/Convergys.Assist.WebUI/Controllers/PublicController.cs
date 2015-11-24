using System.Web.Mvc;
using System.Linq;
using System.Web.Security;
using Convergys.Assist.Logging;
using System.Collections.Generic;
using Convergys.Assist.WebUI.Infrastructure;
using Convergys.Assist.Repository;
using Convergys.Assist.Repository.BusinessModels;

namespace Convergys.Assist.WebUI.Controllers
{
    public class PublicController : BaseController
    {
        private readonly IPublicRepository _repoPublic;

        public PublicController(IPublicRepository repoPublic)
        {
            this._repoPublic = repoPublic;
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection formCollection)
        {
            if (!ModelState.IsValid)
                return View();

            string emailAddress = formCollection.Get("EmailAddress");
            string password = formCollection.Get("Password");
            string redirectUrl = string.IsNullOrWhiteSpace(formCollection.Get("ReturnUrl")) ? "/Home/Index" : formCollection.Get("ReturnUrl");    
            bool rememberMe = formCollection.Get("RememberMe").ToUpper() == "TRUE,FALSE";

            var isLogin = formCollection.AllKeys.Contains("action:Login");
            if (isLogin)
            {
                //Login Button was clicked.
                if (string.IsNullOrWhiteSpace(password))
                {
                    SetMessage(message: "Authentication Failed", isError: true);
                    return View();
                }

                //Authenticate
                var empView = Authenticate(emailAddress, password);
                if (empView == null)
                {
                    //No employee found.
                    SetMessage(message: "Authentication Failed", isError: true);
                    return View();
                }

                //Log Success
                Log4NetManager.Instance.Info(
                                                this.GetType()
                                               ,string.Format(
                                                                "Login(FormCollection formCollection): Login by ({0}){1}."
                                                                ,empView.EmpId
                                                                ,empView.FirstName
                                                              )
                                            );
                //Store EmployeeView to FormsAuthentication
                StoreToAuthToken(empView, rememberMe);
                return Redirect(redirectUrl);
            }
            else
            {
                SendPassword(emailAddress);
                SetMessage(message: "Password sent to " + emailAddress);

            }

            return View();
        }

        public ActionResult LogOff(EmployeeView empView)
        {
            FormsAuthentication.SignOut();
            //Log Signout
            Log4NetManager.Instance.Info(
                                this.GetType()
                               , string.Format(
                                                "LogOff(EmployeeView empView): Logoff by ({0}){1}."
                                                , empView.EmpId
                                                , empView.FirstName
                                              )
                            );

            return RedirectToAction("Index", "Home");
        }

        [NonAction]
        private EmployeeView Authenticate(string emailAddress, string clearPassword)
        {
            EmployeeView model = null;
            model = _repoPublic.LoginEmployee(emailAddress, clearPassword);                
            return model;
        }
        [NonAction]
        private void StoreToAuthToken(EmployeeView model, bool rememberMe)
        {
            Response.SetAuthCookie<EmployeeView>(model.FirstName, rememberMe, model);
        }
        [NonAction]
        private void SendPassword(string emailAddress)
        {
            _repoPublic.SendNewPassword(emailAddress, Resources.Content.Assist_EmailBody_NewPassword, HttpContext.Request.Url.AbsoluteUri);
        }
        [NonAction]
        private void SetMessage(string message, bool isError = false)
        {
            ViewBag.Message = message;
            ViewBag.Error = isError;
        }

    }
}
