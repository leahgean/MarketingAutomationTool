using System;
using System.Web;
using BusinessLayer;
using System.Web.Security;

namespace MarketingAutomationTool
{
    public partial class MATSite : System.Web.UI.MasterPage
    {
        public string isHomeActive;
        public string isLeadActive;
        protected void Page_Load(object sender, EventArgs e)
        {
            DataModels.UserLogin userlogin = (DataModels.UserLogin)Session["loggeduser"];
            if (userlogin == null)
                Response.Redirect("~/Public/Login.aspx");

            string path = HttpContext.Current.Request.Url.AbsolutePath;
            string[] pathArr=path.Split('/');
            string tab = pathArr[1].Trim().ToUpper();

            switch(tab)
            {
                case "LEADS":
                    lnkLeads.CssClass = "nav-link active";
                    break;
                case "CAMPAIGNS":
                    lnkCampaigns.CssClass = "nav-link active";
                    break;
                default:
                    lnkHome.CssClass = "nav-link active";
                    break;
            }


            //if (Session["maintab"] != null)
            //{
            //    if (Session["maintab"].ToString() == "HOME")
            //    {
            //        lnkHome.CssClass = "nav-link active";
            //    }
            //    else if (Session["maintab"].ToString() == "LEADS")
            //    {
            //        lnkLeads.CssClass = "nav-link active";
            //    }

            //}
            //else
            //{
            //    lnkHome.CssClass = "nav-link active";
            //}

            lblLoggedUser.Text = userlogin.FirstName;
            lnkSwitchBack.Visible = Session["adminuser"] != null;
            spSwitchBackSeparator.Visible = Session["adminuser"] != null;


        }

        protected void lnkMyAccount_Click(object sender, EventArgs e)
        {
            //Session["maintab"] = "HOME";
            Response.Redirect("~/MyAccount/MyAccount.aspx");
        }

        protected void lnkSignOut_Click(object sender, EventArgs e)
        {
            if (Session["adminuser"] != null)
            {
                Session["loggeduser"] = Session["adminuser"];
                Session["adminuser"] = null;
            }

            DataModels.UserLogin userlogin = (DataModels.UserLogin)Session["loggeduser"];
    

            UserLogin uLogout = new UserLogin();
            if (Session["uniquesessionid"]!= null)
                uLogout.UpdateUserAccess(Session["uniquesessionid"].ToString(), userlogin.UserID);
            HttpContext.Current.Session.Clear();
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage("&Logout=true");
        }

        protected void lnkSwitchBack_Click(object sender, EventArgs e)
        {
            Session["loggeduser"] = Session["adminuser"];
            Session["adminuser"] = null;
            Response.Redirect("~/MyAccount/Accounts.aspx");
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard.aspx");
        }

        protected void lnkLeads_Click(object sender, EventArgs e)
        {
            Session["LeadActiveList"] = ((int)Utilities.ConstantValues.LeadActiveList.Shortcuts).ToString();
            Response.Redirect("~/Leads/Lead.aspx");
        }

        protected void lnkCampaigns_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Campaigns/Dashboard.aspx");
        }
    }
}