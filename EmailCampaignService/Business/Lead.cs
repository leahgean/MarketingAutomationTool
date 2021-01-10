using EmailCampaignService.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailCampaignService.Business
{
    public class Lead
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AutoMarketerConnectionString"].ConnectionString.ToString();
        public EmailCampaignService.Model.Lead SelectLead(Guid LeadGuid, Guid AccountID)
        {
            EmailCampaignService.Model.Lead mLead = new EmailCampaignService.Model.Lead();
            DataClasses1DataContext dc = new DataClasses1DataContext(connectionString);
            EmailCampaignService.Data.Contact oContact=  dc.Contacts.FirstOrDefault(e => e.ContactID == LeadGuid && e.AccountID == AccountID);


            return mLead;
        }
    }
}
