using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MarketingAutomationTool.UserControls
{
    public partial class NewEmailCampaign : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkNewEmailCampaign_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Campaigns/CampaignBuilder2.aspx");
        }
    }
}