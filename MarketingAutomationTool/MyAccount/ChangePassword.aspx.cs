using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MarketingAutomationTool.MyAccount
{
    public partial class ChangePassword : System.Web.UI.Page
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

        public string LoggedUserId
        {
            get
            {
                if (userlogin != null)
                {
                    return userlogin.UserID.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            
        }

        public string UserName;
        protected void Page_Load(object sender, EventArgs e)
        {
            userlogin = (DataModels.UserLogin)Session["loggeduser"];
            if (userlogin == null)
                Response.Redirect("~/Public/Login.aspx");

            UserName = userlogin.UserName;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        { 

            BusinessLayer.Miscellaneous misc = new BusinessLayer.Miscellaneous();
            string sOldPassword = misc.GenerateEncryptedPassword(txtOldPassword.Text.Trim(), userlogin.PasswordSalt);
            string sNewPassword = string.Empty;
            string sNewPasswordSalt = string.Empty;

            if (sOldPassword == userlogin.SavedPassword)
            {
                if (!BusinessLayer.Extensions.IsPasswordString(txtNewPassword.Text))
                {
                    ToggleMessage("Password must be at least 8 and at most 10 characters long. It must have at least one uppercase letter, one lowercase letter, one number and at least one of these special characters @ $ ! % * ? &", "display:block", "alert alert-danger");

                }
                else
                {
                    if (!txtNewPassword.Text.Equals(txtConfirmNewPassword.Text))
                    {
                        ToggleMessage("New Password does not match with Confirm New Password value.", "display:block", "alert alert-danger");
                    }
                    else
                    {
                        sNewPasswordSalt = misc.GeneratePasswordSalt();
                        sNewPassword = misc.GenerateEncryptedPassword(txtNewPassword.Text.Trim(), sNewPasswordSalt);

                        BusinessLayer.UserLogin bUserLogin = new BusinessLayer.UserLogin();

                        if (bUserLogin.UpdatePassword(sNewPassword, sNewPasswordSalt, userlogin.UserID))
                        {
                            if (!userlogin.HasChangedSystemPassword)
                            {
                                bUserLogin.SetSystemPasswordChanged(userlogin.UserID);
                            }
                            ToggleMessage("Success! Password changed.", "display:block", "alert alert-success");
                        }
                        else
                        {
                            ToggleMessage("Failed! Password was not changed.", "display:block", "alert alert-danger");
                        }
                    }
                }
            }
            else
            {

                ToggleMessage("Old Password is incorrect. Please enter correct password.", "display:block", "alert alert-danger");
            }

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtOldPassword.Text = string.Empty;
            txtNewPassword.Text = string.Empty;
            txtConfirmNewPassword.Text = string.Empty;
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
    }
}