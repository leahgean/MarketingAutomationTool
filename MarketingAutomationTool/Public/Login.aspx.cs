using System;
using System.Web;
using System.Web.UI;
using System.Web.Security;

namespace MarketingAutomationTool.Public
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Clear();

            if (Request.QueryString["Logout"] == "true")
            {
                System.Web.Security.FormsAuthentication.SignOut();
                HttpContext.Current.Session.Clear();
                ToggleMessage("You have opted to sign out. Please login to log back in again.", "display:block;", "alert alert-success");
            }

            if (!Page.IsPostBack)
            {
                txtLoginName.Attributes.Add("required", "");
                txtLoginName.Attributes.Add("autofocus", "");
                txtPassword.Attributes.Add("required", "");
            }
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            string sEnteredPassword = string.Empty;

            BusinessLayer.UserLogin ulogin = new BusinessLayer.UserLogin();
            
            DataModels.UserLogin user = new DataModels.UserLogin();
            user = ulogin.GetUserByUserName(txtLoginName.Text.Trim());

            dvAlert.Attributes.Add("style", "display:block;");
            dvAlert.InnerText = string.Empty;

            if (user != null)
            {
                BusinessLayer.Account acc = new BusinessLayer.Account();
                DataModels.AccountModel accModel = new DataModels.AccountModel();
                accModel = acc.GetAccountDetails(user.AccountID);

                if ((user.IsActive) && (accModel.IsActive))
                {
                    BusinessLayer.Miscellaneous misc = new BusinessLayer.Miscellaneous();
                    sEnteredPassword = misc.GenerateEncryptedPassword(txtPassword.Text.Trim(), user.PasswordSalt);

                    Session["loggeduser"] = null;

                    if (sEnteredPassword == user.SavedPassword)
                    {
                        Session["loggeduser"] = user;
                        Session["adminuser"] = null;
                        Session["uniquesessionid"] = Guid.NewGuid();
                        ulogin.LogUserAccess(user.UserID, this.Request.UserHostAddress, Session["uniquesessionid"].ToString());

                        FormsAuthentication.SignOut();
                        FormsAuthentication.SetAuthCookie(txtLoginName.Text.ToString(), false);


                        if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                        {
                            //string s = Request.QueryString["ReturnUrl"].ToString();
                            Response.Redirect("~/Dashboard.aspx");
                        }
                        else
                        {
                            FormsAuthentication.RedirectFromLoginPage(txtLoginName.Text.Trim(), false);
                        }
                    }
                    else
                    {
                        ToggleMessage("Password is invalid.", "display:block;", "alert alert-danger");
                    }
                }
                else
                {
                    ToggleMessage("Invalid login. Please contact System Administrator.", "display:block;", "alert alert-danger");
                }
            }
            else
            {
                ToggleMessage("Username does not exist.", "display:block;", "alert alert-danger");
            }
        }

        private void ToggleMessage(string strText, string strDisplay, string strClass)
        {
            dvAlert.Attributes.Add("class", strClass);
            dvAlert.Attributes.Add("style", strDisplay);
            dvAlert.InnerText = strText;
        }

        protected void lnkForgotUserName_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgotUserName.aspx");
        }

        protected void lnkForgotPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPassword.aspx");
        }
    }
}