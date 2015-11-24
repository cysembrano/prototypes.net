using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Mail;
using Convergys.Assist.Logging;

namespace Convergys.Assist.Shared.Util
{
    public class SmtpSettings : ConfigurationSection
    {
        [ConfigurationProperty("SmtpConfig", IsRequired = true)]
        public SmtpConfig SmtpConfig
        {
            get { return (SmtpConfig)base["SmtpConfig"]; }
        }
    }
    public sealed class SmtpConfig : ConfigurationElement
    {
        [ConfigurationProperty("SMTPServerAddress", IsRequired = true)]
        public string SMTPServerAddress
        {
            get { return (string)this["SMTPServerAddress"]; }
        }
        [ConfigurationProperty("SMTPServerPort")]
        public int? SMTPServerPort
        {
            get { return (int?)this["SMTPServerPort"]; }
        }
        [ConfigurationProperty("SMTPSenderEmailAddress", IsRequired = true)]
        public string SMTPSenderEmailAddress
        {
            get { return (string)this["SMTPSenderEmailAddress"]; }
        }
        [ConfigurationProperty("SMTPSenderName")]
        public string SMTPSenderName
        {
            get { return (string)this["SMTPSenderName"]; }
        }
    }

    public static class EmailSender
    {
        public static void SendEmail(SmtpSettings settings, MailMessage message)
        {
            //Guard Clauses
            if (settings == null)
            {
                Log4NetManager.Instance.Error(typeof(EmailSender), "SendEmail Failed: SmtpSettings is null");
                throw new ArgumentNullException("settings");
            }
            if (message == null)
            {
                Log4NetManager.Instance.Error(typeof(EmailSender), "SendEmail Failed: MailMessage is null");
                throw new ArgumentNullException("message");
            }

            int port = settings.SmtpConfig.SMTPServerPort.HasValue ? settings.SmtpConfig.SMTPServerPort.Value : 25;
            SmtpClient smtpClient = new SmtpClient(settings.SmtpConfig.SMTPServerAddress, port);
            
            //smtpClient.Credentials = new System.Net.NetworkCredential("info@MyWebsiteDomainName.com", "myIDPassword");
            //smtpClient.UseDefaultCredentials = true;
            //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtpClient.EnableSsl = true;

            //Setting From , To and CC
            message.From = new MailAddress(settings.SmtpConfig.SMTPSenderEmailAddress, settings.SmtpConfig.SMTPSenderName);
            try
            {
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                Log4NetManager.Instance.ErrorFormat(typeof(EmailSender), "Exception {0} \n Inner Exception {1}", ex.ToString(), ex.InnerException.ToString());
                throw;
            }
        }
    }
}
