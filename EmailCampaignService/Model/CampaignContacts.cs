using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailCampaignService.Model
{
    public class CampaignContacts
    {
        public int Id;
        public Guid AccountId;
        public int CampaignID;
        public Guid ContactID;
        public int? CampaignScore;
        public Guid CreatedBy;
        public DateTime? CreatedDate;
        public Guid? ModifiedBy;
        public DateTime? ModifiedDate;
        public Lead Contact;
    }
}
