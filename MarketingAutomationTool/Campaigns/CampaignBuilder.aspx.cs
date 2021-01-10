using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace MarketingAutomationTool.Campaigns
{
    public partial class CampaignBuilder : System.Web.UI.Page
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

          hdnbouncename.Value =  ConfigurationManager.AppSettings["bouncename"].ToString().Trim();
          hdnemailaddress.Value =  ConfigurationManager.AppSettings["bounceaddress"].ToString().Trim();

            if (!Page.IsPostBack)
            {
                PopulateDefaultValues();
                PopulateDatabaseFieldDropDown();
            }
        }

        private void PopulateDefaultValues()
        {
            txtCampaignName.Text = string.Format("Campaign created by {0} on {1}", userlogin.FirstName, string.Format("{0:dd MMM yyyy hh:mm:ss tt}", System.DateTime.Now));
            txtSenderName.Text = string.Format("{0} {1}", userlogin.FirstName, userlogin.LastName);
            txtSenderEmail.Text = userlogin.EmailAddress;
        }

        private void PopulateDatabaseFieldDropDown()
        {
            ddlDatabaseField.Items.Add(new ListItem("Please select database field to add to Subject line", string.Empty));
            ddlDatabaseField.Items.Add(new ListItem("First Name", "%%FirstName%%"));
            ddlDatabaseField.Items.Add(new ListItem("Middle Name", "%%MiddleName%%"));
            ddlDatabaseField.Items.Add(new ListItem("Last Name", "%%LastName%%"));
            ddlDatabaseField.Items.Add(new ListItem("Mobile Number", "%%MobileNumber%%"));
            ddlDatabaseField.Items.Add(new ListItem("Phone Number", "%%PhoneNumber%%"));
            ddlDatabaseField.Items.Add(new ListItem("Email Address", "%%EmailAddress%%"));
            ddlDatabaseField.Items.Add(new ListItem("CompanyName", "%%CompanyName%%"));
            ddlDatabaseField.Items.Add(new ListItem("WebSite", "%%WebSite%%"));
            ddlDatabaseField.Items.Add(new ListItem("Position", "%%Position%%"));
            ddlDatabaseField.Items.Add(new ListItem("Title", "%%Title%%"));
            ddlDatabaseField.Items.Add(new ListItem("Gender", "%%Gender%%"));
            ddlDatabaseField.Items.Add(new ListItem("Address", "%%Address%%"));
            ddlDatabaseField.Items.Add(new ListItem("City", "%%City%%"));
            ddlDatabaseField.Items.Add(new ListItem("State", "%%State%%"));
            ddlDatabaseField.Items.Add(new ListItem("Country", "%%Country%%"));
            ddlDatabaseField.Items.Add(new ListItem("ZipCode", "%%ZipCode%%"));
        }

        protected void lnkBreadHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard.aspx");
        }

        protected void lnkSaveAsDraft_Click(object sender, EventArgs e)
        {

        }

        protected void txtBody_HtmlEditorExtender_ImageUploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
        {
            string fullpath = Server.MapPath("~/Public/Campaigns/Images/") + e.FileName;
            txtBody_HtmlEditorExtender.AjaxFileUpload.SaveAs(fullpath);
            e.PostedUrl = Page.ResolveUrl("~/Public/Campaigns/Images/" + e.FileName);
        }
    }
}