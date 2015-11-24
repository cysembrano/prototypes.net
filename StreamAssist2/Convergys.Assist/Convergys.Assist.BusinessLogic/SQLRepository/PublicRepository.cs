using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convergys.Assist.Data;
using Convergys.Assist.Repository;
using Convergys.Assist.Repository.BusinessModels;
using Convergys.Assist.BusinessLogic.Extensions;
using Convergys.Assist.Shared.Util;
using System.Configuration;
using System.Net.Mail;

namespace Convergys.Assist.BusinessLogic.SQLRepository
{
    public class PublicRepository : IPublicRepository
    {
        public EmployeeView LoginEmployee(string emailAddress, string clearPassword)
        {
            using (SADataModel dbContext = new SADataModel())
            {
               

                var encrypted = Crypto.Encrypt(clearPassword);
                var emp = dbContext.CAEmployeeView.FirstOrDefault(b =>
                          b.EmailAddress.Trim().ToLower() == emailAddress.Trim().ToLower() &&
                          b.NewPwd == encrypted);

                if (emp == null)
                    return null;

                return emp.ToEmployeeView();
            }
        }


        public bool SendNewPassword(string emailAddress, string htmlTemplate, string url)
        {
            using (SADataModel dbContext = new SADataModel())
            {
                var empView = dbContext.CAEmployeeView.FirstOrDefault(b =>
                          b.EmailAddress.Trim().ToLower() == emailAddress.Trim().ToLower());
                
                //Check user Exists
                if (empView == null)
                    return false;

                //Replace password
                string newPassword = Crypto.Encrypt(Crypto.GeneratePassword(8));
                

                var emp = dbContext.CAEmployee.FirstOrDefault(d => d.EmpId == empView.EmpId);
                foreach(var pass in emp.CAPassword)
                {
                    pass.OldPwd = newPassword;
                    pass.NewPwd = newPassword;
                    pass.DateChanged = DateTime.Now;
                }

                //Save New Password
                if (dbContext.SaveChanges() < 1)
                    return false;

                //Change EmployeeView Object Password to new password
                var empViewObj = empView.ToEmployeeView();
                empViewObj.NewPwd = newPassword;


                EmailNewPassword(empViewObj, htmlTemplate, url);
                return true;
            }

        }

        private void EmailNewPassword(EmployeeView empView, string htmlTemplate, string url)
        {
            var config = (SmtpSettings)ConfigurationManager.GetSection("SmtpSettings");
            var message = new MailMessage();

            string To = empView.EmailAddress;
            message.To.Add(new MailAddress(To));
            message.Subject = "Assist Send Password";
            
            //Assign Body
            string body = string.Empty;
            if (!string.IsNullOrWhiteSpace(htmlTemplate))
                body = htmlTemplate;
            else
                body = @"<html xmlns='http://www.w3.org/1999/xhtml'><head><title></title><style>td{ border: 1px solid black; }</style></head><body><br /><h2>Assist Password</h2><br /><div style = 'border-top:3px solid #22BCE5'>&nbsp;</div><span style = 'font-family:Arial;font-size:10pt'>Hello <b>{FirstName}</b>,<br /><br /><p>Your account details for the Convergys Assist website are as follows:</p><br /><table cellspacing='0'><tr><td>Email Address</td><td>{EmailAddress}</td></tr><tr><td>Password</td><td>{Password}</td></tr><tr><td>URL</td><td><a style = 'color:#22BCE5' href = '{URL}'>{URL}</a></td></tr></table><br /><br />Regards,<br />Convergys Assist Administrator</span></body></html>";

            body = body.Replace("{FirstName}", empView.FirstName);
            body = body.Replace("{EmailAddress}", empView.EmailAddress);
            body = body.Replace("{Password}", Crypto.Decrypt(empView.NewPwd));
            body = body.Replace("{URL}", url);
            message.Body = body;

            message.IsBodyHtml = true;
            EmailSender.SendEmail(config, message);
        }
    }
}
