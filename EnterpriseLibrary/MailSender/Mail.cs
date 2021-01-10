using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace EnterpriseLibrary.MailSender
{
    public class Mail
    {
        public bool SendEmail(string smtpclient, int port, string username, string password, string from, string to, string subject, bool ishtmlbody, string body, MailPriority mailPriority)
        {
            SmtpClient smtpClient = new SmtpClient(smtpclient, port);
            smtpClient.EnableSsl = true;

            smtpClient.Credentials = new System.Net.NetworkCredential(username, password);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            MailMessage mailMessage = new MailMessage(from, to);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = ishtmlbody;
            mailMessage.Priority = mailPriority;

            try
            {
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
