using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
using DataModels;
using System.Collections.Generic;

namespace MarketingAutomationTool.MyAccount
{
    public partial class MyAccount : System.Web.UI.Page
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
            {
                Response.Redirect("~/Public/Login.aspx");
            }

            if (!Page.IsPostBack)
            {

                PopulateCountries();
                PopulateFields(userlogin.AccountID);
                
            }

        }

        private void PopulateCountries()
        {
            Country country = new Country();
            DataTable dtCountries = country.GetCountries();
            ddlCountry.DataSource = dtCountries;
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "CountryId";
            ddlCountry.DataBind();

            ddlCountry.Items.Insert(0, new ListItem("Please select country", string.Empty));
        }

        private void PopulateFields(Guid accountid)
        {
            Account acc = new Account();
            AccountModel accmodel = acc.GetAccountDetails(accountid);
            lblCompanyName.InnerText = accmodel.AccountName;
            txtRegistrationNo.Text = accmodel.RegistrationNumber;
            txtAddress.Text = accmodel.Address;
            txtCity.Text = accmodel.City;
            txtState.Text = accmodel.State;
            txtPostCode.Text = accmodel.ZipCode;
            ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(accmodel.CountryId.ToString()));
            txtWebSite.Text = accmodel.CompanyWebsite;
            txtBillingContactName.Text = accmodel.Name_Finances;
            txtBillingContactEmailAddress.Text = accmodel.Email_Finances;
            txtMarketingContactName.Text = accmodel.Name_Marketing;
            txtMarketingContanctEmailAddress.Text = accmodel.Email_Marketing;
            txtTechnicalContactName.Text = accmodel.Name_Technical;
            txtTechnicalContactEmailAddress.Text = accmodel.Email_Technical;
            txtAdminContactName.Text = accmodel.Name_Admin;
            txtAdminContactEmailAddress.Text = accmodel.Email_Admin;
            lblPersonalizedURL.InnerText = accmodel.PersonalizedURL;

            UserSettings usrset = new UserSettings();
            List<UserSettingsModel> lstTestEmailAddresses = usrset.GetUserAccountSettings(accountid, Constants.SettingCategoryEmail.ToString());

            if (lstTestEmailAddresses!= null)
            {
                foreach (UserSettingsModel usraccModel in lstTestEmailAddresses)
                {
                    if (usraccModel.Sort_Order == 1)
                    {
                        txtTestEmailAddress1.Text = usraccModel.Setting_Value;
                    }
                    if (usraccModel.Sort_Order == 2)
                    {
                        txtTestEmailAddress2.Text = usraccModel.Setting_Value;
                    }
                    if (usraccModel.Sort_Order == 3)
                    {
                        txtTestEmailAddress3.Text = usraccModel.Setting_Value;
                    }
                }
            }

            List<UserSettingsModel> lstTestMobileNos = usrset.GetUserAccountSettings(accountid, Constants.SettingCategorySMS.ToString());

            if (lstTestMobileNos!=null)
            {
                foreach (UserSettingsModel usraccModel in lstTestMobileNos)
                {
                    if (usraccModel.Sort_Order == 1)
                    {
                        txtTestMobile1.Text = usraccModel.Setting_Value;
                    }
                    if (usraccModel.Sort_Order == 2)
                    {
                        txtTestMobile2.Text = usraccModel.Setting_Value;
                    }
                    if (usraccModel.Sort_Order == 3)
                    {
                        txtTestMobile3.Text = usraccModel.Setting_Value;
                    }
                }
            }

            User usr = new User();
            DataModels.UserModel usrModel= usr.GetAccountOwnerByAccountId(accountid);

            lblCreatorOwnerFirstName.InnerText = usrModel.FirstName;
            lblCreatorOwnerLastName.InnerText = usrModel.LastName;
            lblCreatorOwnerAddress.InnerText = usrModel.Address;
            lblCreatorOwnerPosition.InnerText = usrModel.Position;
            lblCreatorOwnerEmailAddress.InnerText = usrModel.EmailAddress;
            lblCreatorOwnerMobileNo.InnerText = usrModel.MobileNumber;
            lblCreatorOwnerPhoneNumber.InnerText = usrModel.PhoneNumber;
            lblCreatorOwnerUserName.InnerText = usrModel.UserName;
            lblLastLoginDate.InnerText = string.Format("{0:dd MMM yyyy hh:mm:ss tt}",usrModel.LastLoginDate);

        }


        public bool AddUpdateUserSettings()
        {
            bool blnResult = true;
            UserSettings usr = new UserSettings();

            blnResult =  blnResult && usr.AddUpdateUserSettings(userlogin.AccountID, userlogin.UserID, Constants.SettingCategoryEmail.ToString(), "TestEmail1", txtTestEmailAddress1.Text.Trim(), 1, false);
            blnResult = blnResult && usr.AddUpdateUserSettings(userlogin.AccountID, userlogin.UserID, Constants.SettingCategoryEmail.ToString(), "TestEmail2", txtTestEmailAddress2.Text.Trim(), 2, false);
            blnResult = blnResult && usr.AddUpdateUserSettings(userlogin.AccountID, userlogin.UserID, Constants.SettingCategoryEmail.ToString(), "TestEmail3", txtTestEmailAddress3.Text.Trim(), 3, false);
            blnResult = blnResult && usr.AddUpdateUserSettings(userlogin.AccountID, userlogin.UserID, Constants.SettingCategorySMS.ToString(), "TestMobile1", txtTestMobile1.Text.Trim(), 1, false);
            blnResult = blnResult && usr.AddUpdateUserSettings(userlogin.AccountID, userlogin.UserID, Constants.SettingCategorySMS.ToString(), "TestMobile2", txtTestMobile2.Text.Trim(), 2, false);
            blnResult = blnResult && usr.AddUpdateUserSettings(userlogin.AccountID, userlogin.UserID, Constants.SettingCategorySMS.ToString(), "TestMobile3", txtTestMobile3.Text.Trim(), 3, false);

            return blnResult;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Account acc = new Account();
            UserSettings usr = new UserSettings();
            if (acc.UpdateAccountDetail(txtRegistrationNo.Text.Trim(), txtWebSite.Text.Trim(), txtAddress.Text.Trim(), txtCity.Text.Trim(), txtState.Text.Trim(), txtPostCode.Text.Trim(), int.Parse(ddlCountry.SelectedItem.Value), userlogin.UserID, userlogin.AccountID, txtAdminContactEmailAddress.Text.Trim(), txtBillingContactEmailAddress.Text.Trim(), txtTechnicalContactEmailAddress.Text.Trim(), txtMarketingContanctEmailAddress.Text.Trim(), txtAdminContactName.Text.Trim(), txtBillingContactName.Text.Trim(), txtTechnicalContactName.Text.Trim(), txtMarketingContactName.Text.Trim()))
            {
                if (AddUpdateUserSettings())
                {
                    ToggleMessage("Success! Account was updated.", "display:block", "alert alert-success");
                }
                else
                {
                    ToggleMessage("Error updating Preferences.", "display:block", "alert alert-danger");
                }
            }
            else
            {
                ToggleMessage("Error! Account was not updated.", "display:block", "alert alert-danger");
            }

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            PopulateFields(userlogin.AccountID);
        }

        private void ToggleMessage(string strText, string strDisplay, string strClass)
        {
            dvMessage.Attributes.Add("class", strClass);
            dvMessage.Attributes.Add("style", strDisplay);
            dvMessage.InnerText = strText;
        }

        protected void lnkBreadHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard.aspx");
        }

        protected void lnkViewUsers_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/MyAccount/Users?c=3&accountid={0}",userlogin.AccountID));
        }
    }
}