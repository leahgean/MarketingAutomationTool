using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailCampaignService.Business;
using Logger;

namespace EmailCampaignService
{
    public class Process
    {
        private bool run = true;
        public void Start()
        {
            while (run)
            {
                EmailCampaignService.Business.Email_Job_Queue que = new EmailCampaignService.Business.Email_Job_Queue();

                EmailCampaignLogging.WriteLog("Getting Campaign Que", false, LogLevels.ExecutionSteps);

                foreach (EmailCampaignService.Model.Email_Job_Queue item in que.GetPending())
                {
                    ProcessQue(item);
                }
                 
                

                que = null;
                System.Threading.Thread.Sleep(Int32.Parse(ConfigurationManager.AppSettings["INTERVAL_WATCH_MIN"].ToString()) * 60000);
            }
        }

        public void Stop()
        {
            run = false;
        }

        public void ProcessQue(EmailCampaignService.Model.Email_Job_Queue p_que)
        {
            try
            {
                EmailCampaignService.Business.Message bMsg = new EmailCampaignService.Business.Message();
                EmailCampaignService.Model.Message mMsg= bMsg.GetMessage(p_que.MessageId);

                EmailCampaignService.Business.Campaign bCmp = new EmailCampaignService.Business.Campaign();
                EmailCampaignService.Model.Campaign mCmp = bCmp.GetCampaign(p_que.CampaignId);

                if ((!mMsg.Deleted.Value) && (!mCmp.Deleted.Value))
                {
                    if (mCmp.CampaignStatus==(int)ConstantValues.CampaignStatus.Submitted)
                    {
                        if ((mCmp.SendingOption==(int)ConstantValues.SendingOptions.SendNow) || ((mCmp.SendingOption==(int)ConstantValues.SendingOptions.Scheduled)&&mCmp.SendingSchedule<=System.DateTime.UtcNow.AddHours(8)))
                        {
                            EmailCampaignService.Model.Email_Job_Queue_History mEJQH = new EmailCampaignService.Model.Email_Job_Queue_History();
                            EmailCampaignService.Business.Email_Job_Queue_History objEJQH = new EmailCampaignService.Business.Email_Job_Queue_History();
                            EmailCampaignService.Business.Email_Job_Queue objEJQ = new EmailCampaignService.Business.Email_Job_Queue();

                            mEJQH.JobId = p_que.Id;
                            mEJQH.Status = (int)ConstantValues.EmailJobStatus.Sending;
                            mEJQH.CreatedDate = System.DateTime.UtcNow;
                            objEJQH.UpdateJobQueueHistory(mEJQH);

                            objEJQ.UpdateDateSent(p_que.Id);


                            foreach (EmailCampaignService.Model.CampaignContacts cco in mCmp.CampaignContacts)
                            {
                                if (cco.Contact.SubscribedToEmail)
                                {
                                    EmailCampaignService.Business.Mail m = new EmailCampaignService.Business.Mail();

                                    bool isHTMLBody = (mCmp.CampaignFormat == (int)ConstantValues.MessageBodyFormat.HTML);

                                    EmailCampaignService.Model.EmailCampaignsSent pECS = new EmailCampaignService.Model.EmailCampaignsSent();
                                    pECS.AccountUID = mCmp.AccountId;
                                    pECS.CampaignId = mCmp.Id;
                                    pECS.MessageId = mCmp.MessageId.Value;
                                    pECS.ContactID = cco.ContactID;
                                    pECS.CreatedBy = mCmp.CreatedBy;
                                    pECS.CreatedDate = System.DateTime.UtcNow;
                                    pECS.ModifiedBy = null;
                                    pECS.ModifiedDate = null;
                                    pECS.EmailAddress = cco.Contact.EmailAddress;
                                    pECS.EmailSent = true;
                                    pECS.ErrorMessage = string.Empty;
                                    pECS.IPAddress = mCmp.IPAddress;

                                    string sProcessedSubject = ProcessSubject(mCmp.Message.Subject, cco.Contact);

                                    string sProcessedMessageBody = mCmp.Message.MessageBody;
                                    sProcessedMessageBody = AppendEmailOpenedTracker(sProcessedMessageBody,mCmp.CampaignFormat);
                                    sProcessedMessageBody = sProcessedMessageBody.Replace("%%a%%", mCmp.AccountId.ToString());
                                    sProcessedMessageBody = sProcessedMessageBody.Replace("%%c%%", mCmp.CampaignUID.ToString());
                                    sProcessedMessageBody = sProcessedMessageBody.Replace("%%l%%", cco.Contact.ContactID.ToString());

                                    string sSMTPserver = ConfigurationManager.AppSettings["smtpclient"].ToString();
                                    string sPort = ConfigurationManager.AppSettings["port"].ToString();
                                    string sUserName = ConfigurationManager.AppSettings["smtpusername"].ToString();
                                    string sPassword = ConfigurationManager.AppSettings["smtppassword"].ToString();
                                    string sFromAddress = ConfigurationManager.AppSettings["fromaddress"].ToString();

                                    EmailCampaignService.Business.Mail oMail = new EmailCampaignService.Business.Mail();
                                    oMail.SmartASPNetSendEmail(sSMTPserver, int.Parse(sPort), sUserName, sPassword, sFromAddress, cco.Contact.EmailAddress, sProcessedSubject, isHTMLBody, sProcessedMessageBody, System.Net.Mail.MailPriority.Normal, pECS);
                                } 
                            }

                            objEJQH = new EmailCampaignService.Business.Email_Job_Queue_History();

                            mEJQH.JobId = p_que.Id;
                            mEJQH.Status = (int)ConstantValues.EmailJobStatus.Completed;
                            mEJQH.CreatedDate = System.DateTime.UtcNow;
                            objEJQH.UpdateJobQueueHistory(mEJQH);

                            objEJQ.UpdateDateCompleted(p_que.Id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                EmailCampaignLogging.WriteLog(ex, LogLevels.ExceptionsOnly, "Process-ProcessQue",string.Format("JobId:{0}", p_que.Id));
            }
        }

        private string ProcessSubject(string sSubject, EmailCampaignService.Model.Lead dLead)
        {

            EmailCampaignService.Business.Country bCountry = new EmailCampaignService.Business.Country();

            if (sSubject.Contains("%%FirstName%%"))
            {
                sSubject = sSubject.Replace("%%FirstName%%", dLead.FirstName);
            }

            if (sSubject.Contains("%%MiddleName%%"))
            {
                sSubject = sSubject.Replace("%%MiddleName%%", dLead.MiddleName);
            }

            if (sSubject.Contains("%%LastName%%"))
            {
                sSubject = sSubject.Replace("%%LastName%%", dLead.LastName);
            }

            if (sSubject.Contains("%%MobileNumber%%"))
            {
                sSubject = sSubject.Replace("%%MobileNumber%%", dLead.MobileNumber);
            }
            if (sSubject.Contains("%%PhoneNumber%%"))
            {
                sSubject = sSubject.Replace("%%PhoneNumber%%", dLead.PhoneNumber);
            }

            if (sSubject.Contains("%%EmailAddress%%"))
            {
                sSubject = sSubject.Replace("%%EmailAddress%%", dLead.EmailAddress);
            }

            if (sSubject.Contains("%%CompanyName%%"))
            {
                sSubject = sSubject.Replace("%%CompanyName%%", dLead.CompanyName);
            }

            if (sSubject.Contains("%%WebSite%%"))
            {
                sSubject = sSubject.Replace("%%WebSite%%", dLead.WebSite);
            }

            if (sSubject.Contains("%%Position%%"))
            {
                sSubject = sSubject.Replace("%%Position%%", dLead.Position);
            }

            if (sSubject.Contains("%%Title%%"))
            {
                sSubject = sSubject.Replace("%%Title%%", dLead.Title);
            }

            if (sSubject.Contains("%%Gender%%"))
            {
                sSubject = sSubject.Replace("%%Gender%%", dLead.Gender);
            }

            if (sSubject.Contains("%%Address%%"))
            {
                sSubject = sSubject.Replace("%%Address%%", string.Format("{0} {1}", dLead.Address1, dLead.Address2));
            }

            if (sSubject.Contains("%%City%%"))
            {
                sSubject = sSubject.Replace("%%City%%", dLead.City);
            }

            if (sSubject.Contains("%%State%%"))
            {
                sSubject = sSubject.Replace("%%State%%", dLead.State);
            }

            if (sSubject.Contains("%%Country%%"))
            {
                if (dLead.CountryId.HasValue)
                {
                    sSubject = sSubject.Replace("%%Country%%", bCountry.GetCountry(dLead.CountryId.Value));
                }
                else
                {
                    sSubject = sSubject.Replace("%%Country%%", string.Empty);
                }
            }

            if (sSubject.Contains("%%ZipCode%%"))
            {
                sSubject = sSubject.Replace("%%ZipCode%%", dLead.ZipCode);
            }

            return sSubject;
        }

        private string AppendEmailOpenedTracker(string sBody, int msgBodyFormat)
        {

            if (msgBodyFormat.ToString() == ((int)ConstantValues.MessageBodyFormat.HTML).ToString())
            {
                string sEmailsOpenedURL = ConfigurationManager.AppSettings["EmailsOpenedTrackerURL"].ToString().Trim();
                sBody = string.Format("{0}{1}", string.Format("<img src='{0}?a=%%a%%&c=%%c%%&l=%%l%%' width=1 height=1>", sEmailsOpenedURL), sBody);
            }
            return sBody;
        }
    }
}
