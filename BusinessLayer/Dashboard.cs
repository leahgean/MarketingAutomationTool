using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Dashboard
    {
        public DataTable GetDashboardCounts(Guid AccountID)
        {
            DataAccessLayer.Controller.Dashboard cDo = new DataAccessLayer.Controller.Dashboard();
            return cDo.GetDashboardCounts(AccountID);
        }

        public DataTable GetTopFiveLeads(Guid AccountID)
        {
            DataAccessLayer.Controller.Dashboard cDo = new DataAccessLayer.Controller.Dashboard();
            return cDo.GetTopFiveLeads(AccountID);
        }

        public DataTable GetTopFiveCampaigns(Guid AccountID)
        {
            DataAccessLayer.Controller.Dashboard cDo = new DataAccessLayer.Controller.Dashboard();
            return cDo.GetTopFiveCampaigns(AccountID);
        }
    }
}
