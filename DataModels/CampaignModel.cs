using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
   public class CampaignModel
    {
        public int Id;
        public Guid CampaignUID;
        public Guid AccountId;
        public string CampaignName;
        public int CampaignType;
        public int CampaignFormat;
        public string CampaignDescription;
        public int? MessageId;
        public bool? UseBounceAddressInFromField;
        public bool? HideInSearch;
        public int CampaignStatus;
        public int? TemplateId;
        public DateTime CreatedDate;
        public Guid CreatedBy;
        public DateTime? ModifiedDate;
        public Guid? ModifiedBy;
        public bool? Deleted;
        public DateTime? DeletedDate;
        public Guid? DeletedBy;
        public int? SendingOption;
        public DateTime? SendingSchedule;
        public int? SearchID;
        public string ExcludeList;
        public bool EnableScoring;
        public MessageModel Message;
        public List<CampaignContacts> CampaignContacts;
        public string IPAddress;
        public DateTime? SubmittedDate;
        public Guid? SubmittedBy;
    }
}
