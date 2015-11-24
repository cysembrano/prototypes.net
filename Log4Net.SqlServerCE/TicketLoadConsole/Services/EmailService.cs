using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using TicketLoadConsole.Entities;
using TicketLoadConsole.Util;
using System.Net.Mail;
using System.Data;

namespace TicketLoadConsole.Services
{
    public class ESurveyEmail
    {
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public static class EmailService
    {
        private static ESurveyEmail GetEmail(ESurveyTicket Ticket, ContractMetadata Contract)
        {
            if (Ticket == null || Contract == null)
            {
                Log4NetManager.Instance.Error(typeof(EmailService), "GetEmail: Parameter Ticket or Contract is null");
                return null;
            }

            var returnObj = new ESurveyEmail();
            switch (Ticket.Cust_Language)
            {
                case "DUTCH":
                    returnObj.Subject = @"Thomson/Speedtouch Klanttevredenheidsonderzoek";
                    returnObj.Body = Properties.Resources.ESurveyMessageDutch;
                    break;
                case "FRENCH":
                    returnObj.Subject = @"Enquête de Qualité Thomson/Speedtouch";
                    returnObj.Body = Properties.Resources.ESurveyMessageFrench;
                    break;
                case "GERMAN":
                    returnObj.Subject = @"Thomson/Speedtouch Qualitätsumfrage";
                    returnObj.Body = Properties.Resources.ESurveyMessageGerman;
                    break;
                default: //ENGLISH
                    returnObj.Subject = @"Thomson/Speedtouch Quality survey";
                    returnObj.Body = Properties.Resources.ESurveyMessageEnglish;
                    break;
            }

            returnObj.Body = returnObj.Body.Replace("{CURRENTDATE}", DateTime.Now.ToString("dd-MMM-yyyy"));
            returnObj.Body = returnObj.Body.Replace("{CUSTFNAME}", Ticket.Customer_FName);
            returnObj.Body = returnObj.Body.Replace("{CUSTLNAME}", Ticket.Customer_LName);
            returnObj.Body = returnObj.Body.Replace("{EMAILDATE}", Ticket.Date_Svc_Start.GetValueOrDefault().ToString("dd-MMM-yyyy"));
            returnObj.Body = returnObj.Body.Replace("{PRODUCTCODE}", Ticket.Product_Code);
            returnObj.Body = returnObj.Body.Replace("{CASEID}", Ticket.Ticket_Id);
            returnObj.Body = returnObj.Body.Replace("{URL}", Contract.URL);
            returnObj.Body = returnObj.Body.Replace("{URLAPP}", Contract.URL_App);
            returnObj.Body = returnObj.Body.Replace("{DOCID}", Ticket.Doc_Id.GetValueOrDefault().ToString());
            returnObj.Body = returnObj.Body.Replace("{CUSTEMAIL5}", Ticket.Email.Substring(0,5));

            return returnObj;
        }

        public static void SendEmailSurvey(ESurveyTicket Ticket, ContractMetadata Contract, bool useOverrideEmail = false)
        {
            Log4NetManager.Instance.Info(typeof(EmailService), "SendEmailSurvey(t,c): Start");
            Log4NetManager.Instance.Info(typeof(EmailService), String.Format("SendEmailSurvey(t,c): Language-{0}",Ticket.Cust_Language));
            var Email = GetEmail(Ticket, Contract);
            SendEmailSurvey(Email.Body, Email.Subject, Ticket.Email, Contract.Reply_To_Email, Contract.From_Email, useOverrideEmail);
            Log4NetManager.Instance.Info(typeof(EmailService), "SendEmailSurvey(t,c): End");
        }

        public static void SendEmailSurvey(string HtmlMsgBody, string Subject, string ToEmailAddress, string FromEmailAddress, string FromName, bool useOverrideEmail = false)
        {
            Log4NetManager.Instance.Info(typeof(EmailService), "SendEmailSurvey: Start");

            var config = (SmtpSettings)ConfigurationManager.GetSection("SmtpSettings");
            var message = new MailMessage();

            string toEmail = string.Empty;
            if (useOverrideEmail)
            {
                //If app config has value use that value else use hard coded (ON TEST MODE)
                var overrideEmail = ConfigurationManager.AppSettings["Prog.Test.OverrideToEmail"];
                toEmail = String.IsNullOrWhiteSpace(overrideEmail) ? "cythecy3@gmail.com" : overrideEmail;
            }
            else
            {
                //(ON DEFAULT MODE)
                toEmail = ToEmailAddress;
            }
            message.To.Add(new MailAddress(toEmail));
            message.From = new MailAddress(FromEmailAddress, FromName);
            message.Subject = Subject;

            ////Assign Body
            message.Body = HtmlMsgBody;

            message.IsBodyHtml = true;
            Log4NetManager.Instance.Info(typeof(EmailService), String.Format("SendEmailSurvey: Sending Email To-{0}", ToEmailAddress));
            try
            {
                EmailSender.SendEmail(config, message);
            }
            catch(Exception ex)
            {
                Log4NetManager.Instance.Error(typeof(EmailService), ex);
            }

            Log4NetManager.Instance.Info(typeof(EmailService), "SendEmailSurvey: End");
        }
    }
}
