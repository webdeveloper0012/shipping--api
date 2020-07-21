using System.Configuration;
using System.Net.Mail;
using System.Web;

namespace WPPDataModel.ShippingSystem.DataStructure
{
    public class WPPErrorHandler
    {
        public static void EmergencyEmail(string pSubject, string pMessage)
        {
            return;
            SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["ErrorEmailSmtpServer"]);
            MailMessage email = new MailMessage();
            email.Subject = pSubject;
            email.Priority = MailPriority.High;
            email.Body = pMessage;
            email.From = new MailAddress(ConfigurationManager.AppSettings["ErrorEmailFrom"], HttpContext.Current.Server.MachineName);
            email.To.Add(ConfigurationManager.AppSettings["EmergencyEmailTo"]);
            try
            {
                smtp.Send(email);
            }
            catch { }
        }

        public static void PerformanceEmail(string pSubject, string pMessage)
        {
            return;
            SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["ErrorEmailSmtpServer"]);
            MailMessage email = new MailMessage();
            email.Subject = pSubject;
            email.Priority = MailPriority.High;
            email.Body = pMessage;
            email.From = new MailAddress(ConfigurationManager.AppSettings["ErrorEmailFrom"], HttpContext.Current.Server.MachineName);
            email.To.Add(ConfigurationManager.AppSettings["PerformanceEmailTo"]);
            try
            {
                smtp.Send(email);
            }
            catch { }
        }

    }
}
