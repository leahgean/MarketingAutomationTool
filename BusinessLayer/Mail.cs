using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
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
                Logger.Logger.WriteLog(to, "Mail-SendEmail", string.Empty, "Error", ex.Message);
                return false;
            }
        }

        public bool SmartASPNetSendEmail(string smtpclient, int port, string username, string password, string from, string to, string subject, bool ishtmlbody, string body, MailPriority mailPriority)
        {
            if (smtpclient == "smtp.gmail.com")
            {
                return SendEmail(smtpclient, port, username, password, from, to, subject, ishtmlbody, body, mailPriority);
            }
            else
            {
                try
                {
                    MailMessage m = new MailMessage();
                    SmtpClient sc = new SmtpClient();
                    m.From = new MailAddress(from);
                    m.To.Add(to);
                    m.Subject = subject;
                    m.Body = body;
                    m.IsBodyHtml = ishtmlbody;
                    sc.Host = smtpclient;
                    sc.Port = port;
                    sc.Credentials = new System.Net.NetworkCredential(from, password);
                    sc.EnableSsl = false;
                    sc.Send(m);
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.Logger.WriteLog(to, "Mail-SmartASPNetSendEmail", string.Empty, "Error", ex.Message);
                    return false;
                    throw ex;
                }
            }
        }
    }
}
