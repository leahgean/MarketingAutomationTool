using EmailCampaignService.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailCampaignService.Business
{
    public class Message
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AutoMarketerConnectionString"].ConnectionString.ToString();
        public EmailCampaignService.Model.Message GetMessage(int messageId)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext(connectionString);

            EmailCampaignService.Data.Message mObj = dc.Messages.FirstOrDefault(e => e.Id == messageId);
            EmailCampaignService.Model.Message msg = new EmailCampaignService.Model.Message();


            msg.Id = mObj.Id;
            msg.MessageUID = mObj.MessageUID;
            msg.AccountId = mObj.AccountId;
            msg.MessageFormat = mObj.MessageFormat;
            msg.Entity = mObj.Entity;
            msg.Subject = mObj.Subject;
            msg.SenderName = mObj.SenderName;
            msg.SenderEmail = mObj.SenderEmail;
            msg.MessageBody = mObj.MessageBody;
            msg.CreatedDate = mObj.CreatedDate;
            msg.CreatedBy = mObj.CreatedBy;
            msg.ModifiedDate = mObj.ModifiedDate;
            msg.ModifiedBy = mObj.ModifiedBy;
            msg.Deleted = mObj.Deleted;
            msg.DeletedDate = mObj.DeletedDate;
            msg.DeletedBy = mObj.DeletedBy;
            return msg;

        }
    }
}
