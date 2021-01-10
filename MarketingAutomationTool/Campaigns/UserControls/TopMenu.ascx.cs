using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MarketingAutomationTool.Campaigns.UserControls
{
    public partial class TopMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkStatsByDate_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Campaigns/Reports/EmailStats.aspx");
        }

        protected void lnkViewAllReports_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Campaigns/Reports/ViewAll.aspx");
        }
    

        protected void lnkCreateNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Campaigns/CampaignBuilder2.aspx");
        }

        protected void lnkBrowse_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Campaigns/ViewAll.aspx");
        }

        protected void lnkEmailsSentListing_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Campaigns/Reports/EmailsSentListing.aspx");
        }

        protected void lnkTotalOpensListing_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Campaigns/Reports/TotalOpensListing.aspx");
        }

        protected void lnkUniqueOpensListing_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Campaigns/Reports/UniqueOpensListing.aspx");
        }

        protected void lnkTotalClicksListing_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Campaigns/Reports/TotalClickthroughsListing.aspx");
        }

        protected void lnkUnqiueClicksListing_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Campaigns/Reports/UniqueClickthroughsListing.aspx");

        }
    }
}