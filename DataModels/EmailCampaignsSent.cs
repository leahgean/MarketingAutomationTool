using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class EmailCampaignsSent
    {
        public int Id;
        public Guid AccountUID;
        public int CampaignId;
        public int MessageId;
        public Guid ContactID;
        public string EmailAddress;
        public bool? EmailSent;
        public string ErrorMessage;
        public DateTime CreatedDate;
        public Guid CreatedBy;
        public DateTime? ModifiedDate;
        public Guid? ModifiedBy;
        public string IPAddress;
    }
}
