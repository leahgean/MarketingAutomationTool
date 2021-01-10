using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class CampaignOpened
    {
        public int ID;
        public Guid AccountID;
        public Guid CampaignID;
        public Guid ContactId;
        public DateTime CreatedDate;
        public Guid CreatedBy;
        public string IPAddress;
    }
}
