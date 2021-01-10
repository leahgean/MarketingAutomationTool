using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Configuration;

namespace MarketingAutomationTool.MyAccount
{
    public partial class NewAccount : System.Web.UI.Page
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

            if (!userlogin.IsSuperAdminUser)
                Response.Redirect("~/Public/Login.aspx");

            if (!Page.IsPostBack)
            {
                Country country = new Country();
                DataTable dtCountries = country.GetCountries();
                ddlCountry.DataSource = dtCountries;
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataBind();

                ddlCountry.Items.Insert(0, new ListItem("Please select country", string.Empty));
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Miscellaneous misc = new Miscellaneous();
            bool blnIsAdminUser = true;
            bool blnIsActiveUser = true;
            bool blnIsOwnerUser = true;
            bool blnIsSuperAdminUser = false;
            bool blnIsActiveAccount = true;
            bool blnIsSuperAdminAccount = false;
            string strPassword = misc.GeneratePassword();
            string strPasswordSalt = misc.GeneratePasswordSalt();
            string strEncryptedPassword = misc.GenerateEncryptedPassword(strPassword, strPasswordSalt);

            BusinessLayer.Account acc = new BusinessLayer.Account();
            BusinessLayer.User usr = new BusinessLayer.User();
            if (usr.UniqueUserName(txtUserName.Text.Trim()) == 0)
            {
                if (acc.UniqueEmail(txtEmailAddress.Text.Trim()))
                {
                    if (acc.CreateAccount(txtCompanyName.Text.Trim(), txtWebSite.Text.Trim(), txtPhoneNo.Text.Trim(), txtFaxNo.Text.Trim(), txtEmailAddress.Text.Trim(), txtAddress.Text.Trim(), txtCity.Text.Trim(), txtState.Text.Trim(), txtPostalZip.Text.Trim(), Convert.ToInt32(ddlCountry.SelectedItem.Value), txtUserName.Text.Trim(), strEncryptedPassword, txtFirstName.Text.Trim(), txtMiddleName.Text.Trim(), txtLastName.Text.Trim(), txtMobileNo.Text.Trim(), blnIsAdminUser, blnIsActiveUser, blnIsOwnerUser, blnIsSuperAdminUser, strPasswordSalt, userlogin.UserID, blnIsActiveAccount, blnIsSuperAdminAccount, Request.UserHostAddress))
                    {

                        Mail mail = new Mail();
                        string sLoginURL = ConfigurationManager.AppSettings["LoginURL"].ToString();
                        string sBody = string.Format("Dear {0}," +
                            "<p>Please use following user name and password to login to Marketing Automation Tool:</p>" +
                            "<p>User Name: <b>{1}</b></p>" +
                            "<p>Password: <b>{2}</b></p>" +
                            "<p>Please use your user name and this password to log to the system - <a href='{3}'>{3}</a>.</p>" +
                            "<p>For security purposes, please change system generated password once you have logged in.</p>" +
                            "<p>&nbsp;</p>" +
                            "<p>&nbsp;</p>" +
                            "<p>Sincerely,</p>" +
                            "<p>Marketing Automation Tool Team</p>",
                            txtFirstName.Text.Trim(), txtUserName.Text.Trim(), strPassword, sLoginURL);

                        string sSMTPserver = ConfigurationManager.AppSettings["smtpclient"].ToString();
                        string sPort = ConfigurationManager.AppSettings["port"].ToString();
                        string sUserName = ConfigurationManager.AppSettings["smtpusername"].ToString();
                        string sPassword = ConfigurationManager.AppSettings["smtppassword"].ToString();
                        string sFromAddress = ConfigurationManager.AppSettings["fromaddress"].ToString();

                        if (mail.SendEmail(sSMTPserver, int.Parse(sPort), sUserName, sPassword, sFromAddress, txtEmailAddress.Text.Trim(), "Your access to our system", true, sBody, System.Net.Mail.MailPriority.Normal))
                        {
                            ToggleMessage("Success! New account created.", "display:block", "alert alert-success");
                            ClearFields();
                        }
                        else
                        {
                            ToggleMessage("Warning! New account was created but email notification was not sent.", "display:block", "alert alert-danger");
                        }
                    }
                    else
                    {
                        ToggleMessage("Error! New account was not created.", "display:block", "alert alert-danger");
                    }
                }
                else
                {
                    ToggleMessage("Error! An account has already been created for this Email Address.", "display:block", "alert alert-danger");
                }
            }
            else
            {
                ToggleMessage("Error! User Name already exists. Please enter a unique User Name.", "display:block", "alert alert-danger");
            }
        }

        private void ToggleMessage(string strText, string strDisplay, string strClass)
        {
            dvMessage.Attributes.Add("class", strClass);
            dvMessage.Attributes.Add("style", strDisplay);
            dvMessage.InnerText = strText;
        }

        private void ClearFields()
        {
            txtUserName.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtMiddleName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtPhoneNo.Text = string.Empty;
            txtFaxNo.Text = string.Empty;
            txtMobileNo.Text = string.Empty;
            txtEmailAddress.Text = string.Empty;
            txtCompanyName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtState.Text = string.Empty;
            txtPostalZip.Text = string.Empty;
            ddlCountry.SelectedIndex = 0;
            txtWebSite.Text = string.Empty;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
            dvMessage.InnerText = "";
            dvMessage.Attributes.Remove("class");
            dvMessage.Attributes.Add("style", "display:none;");
        }

        protected void lnkBreadHome_Click(object sender, EventArgs e)
        {
            //Session["maintab"] = "HOME";
            Response.Redirect("~/Dashboard.aspx");
        }
    }
}