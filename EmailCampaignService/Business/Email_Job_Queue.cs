using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailCampaignService.Data;
using Logger;


namespace EmailCampaignService.Business
{
    public class Email_Job_Queue
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AutoMarketerConnectionString"].ConnectionString.ToString();


        public List<EmailCampaignService.Model.Email_Job_Queue> GetPending()
        {
            DataClasses1DataContext dc = new DataClasses1DataContext(connectionString);
            List<EmailCampaignService.Model.Email_Job_Queue> lstEmail_Job_Queue = new List<EmailCampaignService.Model.Email_Job_Queue>();
            foreach (EmailCampaignService.Data.Email_Job_Queue item in dc.Email_Job_Queues.Where(e => e.Email_Job_Queue_Histories.OrderByDescending(x => x.STATUS).First().STATUS == (int)ConstantValues.EmailJobStatus.Pending))
            {
                EmailCampaignService.Model.Email_Job_Queue emailjobqueue = new EmailCampaignService.Model.Email_Job_Queue();
                emailjobqueue.Id=item.Id;
                emailjobqueue.MessageId = item.MessageId;
                emailjobqueue.CampaignId = item.CampaignId;
                emailjobqueue.CreatedBy = item.CreatedBy;
                emailjobqueue.CreatedDate = item.CreatedDate;
                emailjobqueue.IPAddress = item.IPAddress;
                emailjobqueue.Schedule = item.Schedule;
                emailjobqueue.bUpdate = item.bUpdate;
                emailjobqueue.datesent = item.datesent;
                emailjobqueue.datecompleted = item.datecompleted;
                lstEmail_Job_Queue.Add(emailjobqueue);
            }

            return lstEmail_Job_Queue;
        }

        public bool UpdateDateSent(int jobid)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext(connectionString);
            try
            {
                dc.Email_Job_Queues.FirstOrDefault(e => e.Id == jobid).datesent= System.DateTime.UtcNow;
                dc.SubmitChanges();
            }
            catch (Exception ex)
            {
                EmailCampaignLogging.WriteLog(ex, LogLevels.ExceptionsOnly, "Email_Job_Queue-UpdateDateSent", string.Format("JobId:{0}", jobid));
                return false;
            }
            return true;
        }

        public bool UpdateDateCompleted(int jobid)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext(connectionString);
            try
            {
                dc.Email_Job_Queues.FirstOrDefault(e => e.Id == jobid).datecompleted = System.DateTime.UtcNow;
                dc.SubmitChanges();
            }
            catch (Exception ex)
            {
                EmailCampaignLogging.WriteLog(ex, LogLevels.ExceptionsOnly, "Email_Job_Queue-UpdateDateCompleted", string.Format("JobId:{0}", jobid));
                return false;
            }
            return true;
        }


    }
}
