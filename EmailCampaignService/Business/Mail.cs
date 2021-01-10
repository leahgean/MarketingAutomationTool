using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Logger;

namespace EmailCampaignService.Business
{
   public class Mail
    {
        public bool SendEmailCampaign(string smtpclient, int port, string username, string password, string from, string to, string subject, bool ishtmlbody, string body, MailPriority mailPriority, EmailCampaignService.Model.EmailCampaignsSent pECS)
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

            EmailCampaignService.Business.Campaign oCam = new EmailCampaignService.Business.Campaign();

            try
            {
                smtpClient.Send(mailMessage);
                pECS.EmailSent = true;
                pECS.ErrorMessage = null;
                oCam.InsertEmailCampaignsSent(pECS);
                return true;
            }
            catch (Exception ex)
            {
                pECS.EmailSent = false;
                pECS.ErrorMessage = ex.Message;
                oCam.InsertEmailCampaignsSent(pECS);
                EmailCampaignLogging.WriteLog(ex,LogLevels.ExceptionsOnly, "Mail-SendEmailCampaign", string.Format("Email Recipient: {0}",to));
                return false;
            }
        }

        public bool SmartASPNetSendEmail(string smtpclient, int port, string username, string password, string from, string to, string subject, bool ishtmlbody, string body, MailPriority mailPriority, EmailCampaignService.Model.EmailCampaignsSent pECS)
        {
            if (smtpclient == "smtp.gmail.com")
            {
                return SendEmailCampaign(smtpclient, port, username, password, from, to, subject, ishtmlbody, body, mailPriority, pECS);
            }
            else
            {
                EmailCampaignService.Business.Campaign oCam = new EmailCampaignService.Business.Campaign();
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

                    pECS.EmailSent = true;
                    pECS.ErrorMessage = null;
                    oCam.InsertEmailCampaignsSent(pECS);

                    return true;
                }
                catch (Exception ex)
                {
                    EmailCampaignLogging.WriteLog(ex,LogLevels.ExceptionsOnly, "Mail-SmartASPNetSendEmail", string.Format("Email Recipient: {0}", to));

                    pECS.EmailSent = false;
                    pECS.ErrorMessage = ex.Message;
                    oCam.InsertEmailCampaignsSent(pECS);

                    return false;
                    throw ex;
                }
            }
        }
    }
}
