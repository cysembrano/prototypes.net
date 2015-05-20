using Messageboards.Web.Models;
using Messageboards.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Messageboards.Web.Controllers
{
    public class HomeController : Controller
    {
        private IMailService _mail;
        public HomeController(IMailService mail)
        {
            _mail = mail;
        }
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModel model)
        {
            var msg = string.Format("Comment From: {1}{0}Email:{2}{0}Website:{3}{0}Comment{4}{0}", 
                Environment.NewLine, 
                model.Name, 
                model.Email,
                model.Website,
                model.Comment
                );


            if (_mail.SendMail("noreply@yourdomain.com",
                "foo@yourdomain.com",
                "Web contact",
                msg))
            {
                ViewBag.MailSent = true;
            }
            return View();
        }

        [Authorize]
        public ActionResult MyMessages()
        {
            return View();
        }
    }
}
