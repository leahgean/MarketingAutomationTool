using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailCampaignService.Model
{
    public class Email_Job_Queue
    {
        public int Id;
        public int MessageId;
        public int CampaignId;
        public Guid	CreatedBy;
        public DateTime	CreatedDate;
        public string IPAddress;
        public DateTime? Schedule;
        public bool? bUpdate;
        public DateTime? datesent;
        public DateTime? datecompleted;
    }
}
