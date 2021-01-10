using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class ConstantValues
    {
        public enum ContactAction
        {
            SUBSCRIBE = 1,
            UNSUBSCRIBE = 2,
            UPDATECONTACTDETAILS = 3,
            DELETE = 4
        }

        public enum CampaignStatus
        {
            Draft = 0,
            Submitted = 1
        }
    }
    
}
