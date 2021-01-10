using EmailCampaignService.Data;
using Logger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailCampaignService.Business
{
    public class Email_Job_Queue_History
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AutoMarketerConnectionString"].ConnectionString.ToString();
        public bool UpdateJobQueueHistory(EmailCampaignService.Model.Email_Job_Queue_History pJobHistory)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext(connectionString);

            try
            {

                EmailCampaignService.Data.Email_Job_Queue_History oEJQH = new EmailCampaignService.Data.Email_Job_Queue_History();
                oEJQH.JobId = pJobHistory.JobId;
                oEJQH.STATUS = byte.Parse(pJobHistory.Status.ToString());
                oEJQH.CreatedDate = pJobHistory.CreatedDate;
                dc.Email_Job_Queue_Histories.InsertOnSubmit(oEJQH);
                dc.SubmitChanges();
            }
            catch (Exception ex)
            {
                EmailCampaignLogging.WriteLog(ex, LogLevels.ExceptionsOnly, "Email_Job_Queue_History-UpdateJobQueueHistory", string.Format("JobId:{0}", pJobHistory.JobId));
                return false;
            }
            return true;
        }
    }
}
