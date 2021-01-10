using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using MarketingAutomationTool.Utilities;

namespace MarketingAutomationTool.Public
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Public/Login.aspx");
        }

        protected void lnkRetrieve_Click(object sender, EventArgs e)
        {
            UserLogin ulog = new UserLogin();
            DataModels.UserLogin user= ulog.GetUserByUserName(txtUserName.Text.Trim());

            if (user != null)
            {
                if (ulog.ResetHasChangedSystemPassword(user.UserID))
                {
                    Miscellaneous misc = new Miscellaneous();
                    string strPassword = misc.GeneratePassword();
                    string strPasswordSalt = misc.GeneratePasswordSalt();
                    string strEncryptedPassword = misc.GenerateEncryptedPassword(strPassword, strPasswordSalt);
                    string sLoginURL = ConfigurationManager.AppSettings["LoginURL"].ToString();
                    string sChangePasswordURL = ConfigurationManager.AppSettings["ChangePasswordURL"].ToString();
                    string sBody = string.Empty;

                    if (ulog.UpdatePassword(strEncryptedPassword, strPasswordSalt, user.UserID))
                    {
                        sBody = string.Format("Hi {0}," +
                        "<p>Your temporary password to Marketing Automation Tool is:</p>" +
                        "<p>Temporary Password: <b>{1}</b></p>" +
                        "<p>Please click <a href='{2}'>here</a> to login. Please change your password immediately after logging in.</p>" +
                        "<p>&nbsp;</p>"+
                        "<p>&nbsp;</p>"+
                        "<p>Sincerely,</p>"+
                        "<p>Marketing Automation Tool Team</p>",
                   string.Format("{0} {1}", user.FirstName, user.LastName), strPassword, sLoginURL);

                        Mail mail = new Mail();
                        string sSMTPserver = ConfigurationManager.AppSettings["smtpclient"].ToString();
                        string sPort = ConfigurationManager.AppSettings["port"].ToString();
                        string sUserName = ConfigurationManager.AppSettings["smtpusername"].ToString();
                        string sPassword = ConfigurationManager.AppSettings["smtppassword"].ToString();
                        string sFromAddress = ConfigurationManager.AppSettings["fromaddress"].ToString();

                        if (mail.SmartASPNetSendEmail(sSMTPserver, int.Parse(sPort), sUserName, sPassword, sFromAddress, user.EmailAddress.Trim(), "Your Temporary Password", true, sBody, System.Net.Mail.MailPriority.Normal))
                        {
                            ToggleMessage(string.Format("Please check registered email address to see new password and <a href='{0}'>login</a>. Please change temporary password immediately after logging in to secure your account.", sLoginURL), "display:block;", "alert alert-success", (int)ConstantValues.MessageBodyFormat.HTML);
                        }
                        else
                        {
                            ToggleMessage("An error occurred sending email containing new password. Please contact System Administrator.", "display:block;", "alert alert-danger");
                        }

                    }
                    else
                    {
                        ToggleMessage("An error occurred sending new password. Please contact System Administrator.", "display:block;", "alert alert-danger");
                    }
                }
                else
                {
                    ToggleMessage("An error occurred sending new password. Please contact System Administrator.", "display:block;", "alert alert-danger");
                }
            }
            else
            {
                ToggleMessage("UserName not found.", "display:block;", "alert alert-danger");
            }

            

        }

        private void ToggleMessage(string strText, string strDisplay, string strClass, int iMessageBodyFormat = 0)
        {
            dvAlert.Attributes.Add("class", strClass);
            dvAlert.Attributes.Add("style", strDisplay);

            if (iMessageBodyFormat == (int)ConstantValues.MessageBodyFormat.Text)
            {
                dvAlert.InnerText = strText;
            }
            else
            {
                dvAlert.InnerHtml = strText;
            }

        }
    }
}