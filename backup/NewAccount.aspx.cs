using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Web.Configuration;
using System.Data;
using BusinessLayer;

namespace MarketingAutomationTool
{
    public partial class NewAccount : System.Web.UI.Page
    {
        public string pConnectionString = WebConfigurationManager.ConnectionStrings["AutoMarketerConnectionString"].ConnectionString.ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Country country = new Country();
                DataTable dtCountries = country.GetCountries(pConnectionString);
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
            bool blnIsAdmin = true;
            string strPassword = misc.GeneratePassword();
            string strPasswordSalt = misc.GeneratePasswordSalt();
            string strEncryptedPassword = misc.GenerateEncryptedPassword(strPassword, strPasswordSalt);

            DataModels.UserLogin userlogin = (DataModels.UserLogin)Session["loggeduser"];

            BusinessLayer.Account acc = new BusinessLayer.Account();
            if (acc.CreateAccount(txtCompanyName.Text.Trim(), txtWebSite.Text.Trim(), txtPhoneNo.Text.Trim(), txtFaxNo.Text.Trim(), txtEmailAddress.Text.Trim(), txtAddress.Text.Trim(), txtCity.Text.Trim(), txtState.Text.Trim(), txtPostalZip.Text.Trim(), int.Parse(ddlCountry.SelectedItem.Value), txtUserName.Text.Trim(), strEncryptedPassword, txtFirstName.Text.Trim(), txtMiddleName.Text.Trim(), txtLastName.Text.Trim(), txtMobileNo.Text.Trim(), txtPhoneNo.Text.Trim(), blnIsAdmin, true, strPasswordSalt, userlogin.UserID, pConnectionString))
            {
                dvMessage.InnerText = "Success! New account created.";
                dvMessage.Attributes.Add("class", "alert alert-success");
                dvMessage.Attributes.Add("style", "display:block");
                ClearFields();
            }
            else
            {
                dvMessage.InnerText = "Error! New account was not created.";
                dvMessage.Attributes.Add("class", "alert alert-danger");
                dvMessage.Attributes.Add("style", "display:block");
            }
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
    }
}