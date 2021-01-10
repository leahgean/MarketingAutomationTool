using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MarketingAutomationTool.Leads.UserControls
{
    public partial class Lead : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            Session["LeadActiveList"] = ((int)Utilities.ConstantValues.LeadActiveList.Shortcuts).ToString();
            Session["SearchType"] = null;
            Response.Redirect("~/Leads/SimplifiedSearch.aspx");
        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            Session["LeadActiveList"] = ((int)Utilities.ConstantValues.LeadActiveList.Shortcuts).ToString();
            Session["SearchType"] = "EXPORT";
            Response.Redirect("~/Leads/SimplifiedSearch.aspx");
        }

        protected void lnkNewLead_Click(object sender, EventArgs e)
        {
            Session["LeadActiveList"] = ((int)Utilities.ConstantValues.LeadActiveList.Shortcuts).ToString();
            Response.Redirect("~/Leads/AddLead.aspx");
        }

        protected void lnkImportLeads_Click(object sender, EventArgs e)
        {
            Session["LeadActiveList"] = ((int)Utilities.ConstantValues.LeadActiveList.Shortcuts).ToString();
            Response.Redirect("~/Leads/ImportLeads.aspx");
        }

        protected void lnkViewExistingImportJob_Click(object sender, EventArgs e)
        {
            Session["LeadActiveList"] = ((int)Utilities.ConstantValues.LeadActiveList.Shortcuts).ToString();
            Response.Redirect("~/Leads/ViewExistingImportJobs.aspx");
        }

        protected void lnkNewLeadsStatistics_Click(object sender, EventArgs e)
        {
            Session["LeadActiveList"] = ((int)Utilities.ConstantValues.LeadActiveList.Shortcuts).ToString();
            Response.Redirect("~/Leads/Reports/LeadsStatistics.aspx");
        }

        protected void lnkViewAllReports_Click(object sender, EventArgs e)
        {
            Session["LeadActiveList"] = ((int)Utilities.ConstantValues.LeadActiveList.Shortcuts).ToString();
            Response.Redirect("~/Leads/Reports/ViewAll.aspx");
        }

        protected void lnkLeadsListing_Click(object sender, EventArgs e)
        {
            Session["LeadActiveList"] = ((int)Utilities.ConstantValues.LeadActiveList.Shortcuts).ToString();
            Response.Redirect("~/Leads/Reports/LeadsListing.aspx");
        }

        protected void lnkDeletedLeads_Click(object sender, EventArgs e)
        {
            Session["LeadActiveList"] = ((int)Utilities.ConstantValues.LeadActiveList.Shortcuts).ToString();
            Response.Redirect("~/Leads/Reports/DeletedLeadsListing.aspx");
        }

        protected void lnkUnsubscribedLeads_Click(object sender, EventArgs e)
        {
            Session["LeadActiveList"] = ((int)Utilities.ConstantValues.LeadActiveList.Shortcuts).ToString();
            Response.Redirect("~/Leads/Reports/UnsubscribedLeadsListing.aspx");
        }

        protected void lnkDuplicateLeads_Click(object sender, EventArgs e)
        {
            Session["LeadActiveList"] = ((int)Utilities.ConstantValues.LeadActiveList.Shortcuts).ToString();
            Response.Redirect("~/Leads/Reports/DuplicateLeadsListing.aspx");
        }

        protected void lnkFacebook_Click(object sender, EventArgs e)
        {
            Session["LeadActiveList"] = ((int)Utilities.ConstantValues.LeadActiveList.Shortcuts).ToString();
            Session["FBAction"] = "IMPORT";
            Response.Redirect("../MyAccount/FacebookLogin.aspx");
        }
    }
}