using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailCampaignService.Model
{
    public class Message
    {
        public int Id;
        public Guid MessageUID;
        public Guid AccountId;
        public int MessageFormat;
        public string Entity;
        public string Subject;
        public string SenderName;
        public string SenderEmail;
        public string MessageBody;
        public DateTime CreatedDate;
        public Guid CreatedBy;
        public DateTime? ModifiedDate;
        public Guid? ModifiedBy;
        public bool? Deleted;
        public DateTime? DeletedDate;
        public Guid? DeletedBy;
    }
}
