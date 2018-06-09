using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MailHepler
    {
        public void SendMail(string toEmailAddress, string subject, string content)
        {
            var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
            var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var sMTPHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var sMTPPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();
            var enableSSL = bool.Parse(ConfigurationManager.AppSettings["EnableSSL"].ToString());

            MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(toEmailAddress));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = content;

            var client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
            client.Host = sMTPHost;
            client.Port = !string.IsNullOrEmpty(sMTPPort) ? int.Parse(sMTPPort) : 0;
            client.EnableSsl = enableSSL;
            client.Send(message);
        }
    }
}
