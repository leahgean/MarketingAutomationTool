using System;
using System.Web.UI;
using System.Configuration;
using BusinessLayer;
using System.Data;

namespace MarketingAutomationTool
{
    public partial class Dashboard : System.Web.UI.Page
    {

        private DataModels.UserLogin userlogin
        {
            get
            {
                if (ViewState["loggeduser"] != null)
                {
                    return (DataModels.UserLogin)ViewState["loggeduser"];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ViewState["loggeduser"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            userlogin = (DataModels.UserLogin)Session["loggeduser"];
            if (userlogin == null)
                Response.Redirect("~/Public/Login.aspx");

            if (!Page.IsPostBack)
            {
                string sChangePasswordURL = ConfigurationManager.AppSettings["ChangePasswordURL"].ToString();
                BusinessLayer.UserLogin ul = new BusinessLayer.UserLogin();
                userlogin.HasChangedSystemPassword = ul.GetHasChangedSystemPassword(userlogin.UserID);

                if (!userlogin.HasChangedSystemPassword)
                {
                    ToggleMessage(string.Format("For security purposes, please change system generated password. Click <a href='{0}?c=3'>here</a> to change password.",sChangePasswordURL), "display:block", "alert alert-danger");
                }
                else
                {
                    ToggleMessage(string.Empty, "display:none", string.Empty);
                }

                PopulateCounts();
                PopulateLeads();
                PopulateCampaigns();
            }

        }

        private void PopulateCounts()
        {
            BusinessLayer.Dashboard db = new BusinessLayer.Dashboard();
            DataTable dt=db.GetDashboardCounts(userlogin.AccountID);

            foreach(DataRow dr in dt.Rows)
            {
                lblTotalLeads.Text=dr["TotalLeads"].ToString();
                lblNewLeads.Text = dr["NewLeads"].ToString();
                lblEmailsSent.Text = dr["EmailsSent"].ToString();
                lblUniqueOpens.Text = dr["UniqueOpens"].ToString();
                lblTotalOpens.Text = dr["TotalOpens"].ToString();
                lblUniqueClicks.Text = dr["UniqueClicks"].ToString();
                lblTotalClicks.Text = dr["TotalClicks"].ToString();
                lblUnsubscribe.Text = dr["Unsubscribe"].ToString();
            } 
        }

        private void PopulateLeads()
        {
            BusinessLayer.Dashboard db = new BusinessLayer.Dashboard();
            DataTable dt = db.GetTopFiveLeads(userlogin.AccountID);

            grdLeads.DataSource = dt;
            grdLeads.DataBind();
        }

        private void PopulateCampaigns()
        {
            BusinessLayer.Dashboard db = new BusinessLayer.Dashboard();
            DataTable dt = db.GetTopFiveCampaigns(userlogin.AccountID);

            grdCampaigns.DataSource = dt;
            grdCampaigns.DataBind();
        }

        private void ToggleMessage(string strText, string strDisplay, string strClass)
        {
            dvMessage.Attributes.Add("class", strClass);
            dvMessage.Attributes.Add("style", strDisplay);
            dvMessage.InnerHtml = strText;
        }

        protected void btnViewAllCampaigns_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Campaigns/ViewAll.aspx");
        }

        protected void btnViewLeads_Click(object sender, EventArgs e)
        {
            Session["SearchType"] = "VIEWALL";
            Response.Redirect("/Leads/SimplifiedSearch.aspx");
        }

        protected void grdLeads_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper().Trim())
            {
                case "SELECT":
                    Response.Redirect("/Leads/EditLead.aspx?Guid=" + e.CommandArgument);
                    break;
            }
        }

        protected void grdCampaigns_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper().Trim())
            {
                case "SELECT":
                    string sCommandArg = e.CommandArgument.ToString();
                    string[] sCommArgArr= sCommandArg.Split('|');

                    Response.Redirect(string.Format("/Campaigns/CampaignBuilder2.aspx?c={0}&uid={1}", sCommArgArr[0].Trim(), sCommArgArr[1].Trim()));
                    break;
            }

        }
    }
}