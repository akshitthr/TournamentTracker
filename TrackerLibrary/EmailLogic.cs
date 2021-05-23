using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace TrackerLibrary
{
    public static class EmailLogic
    {
        public static void SendEmail(string to, string subject, string body)
        {
            SendEmail(new List<string> { to }, new List<string>(), subject, body);
        }
        
        public static void SendEmail(List<string> to, List<string> bcc, string subject, string body)
        {
            MailAddress mailAddress = new MailAddress(GlobalConfig.AppKeyLookup("appEmailId"), GlobalConfig.AppKeyLookup("appEmailName"));

            MailMessage mail = new MailMessage();
            mail.From = mailAddress;
            to.ForEach(x => mail.To.Add(x));
            bcc.ForEach(x => mail.Bcc.Add(x));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(mail);
        }
    }
}
