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
    public partial class ForgotUserName : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }

        }

        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Public/Login.aspx");
        }

        protected void lnkRetrieve_Click(object sender, EventArgs e)
        {
            UserLogin ulog = new UserLogin();
            List<DataModels.UserLogin> ulogin = new List<DataModels.UserLogin>();
            ulogin=ulog.GetUserNameByEmailAddress(txtEmailAddress.Text.Trim());
            string sBody = string.Empty;
            string sLoginURL = ConfigurationManager.AppSettings["LoginURL"].ToString();

            if (ulogin!=null)
            {
                Mail mail = new Mail();

                if (ulogin.Count == 1)
                {    
                    sBody = string.Format("Hi {0}," +
                    "<p>Your username to Marketing Automation Tool is:</p>" +
                    "<p>User Name: <b>{1}</b></p>"+
                    "<p>Please click <a href='{2}'>here</a> to login.</p>"+
                    "<p>&nbsp;</p>" +
                    "<p>&nbsp;</p>" +
                    "<p>Sincerely,</p>" +
                    "<p>Marketing Automation Tool Team</p>",
                    string.Format("{0} {1}",ulogin[0].FirstName, ulogin[0].LastName),ulogin[0].UserName,sLoginURL);
                }
                else
                {

                    sBody = string.Format("Hi," +
                                        "<p>Your usernames to Marketing Automation Tool are:</p>" +
                                        "<p>User Names:</p>");

                    string sUserNames = string.Empty;
                    foreach (DataModels.UserLogin item in ulogin)
                    {
                        sUserNames = sUserNames + string.Format("<ul>{0}</ul>", item.UserName);
                    }

                    sBody = string.Concat(sBody, sUserNames);
                    sBody = string.Concat(sBody, string.Format("<p>Please click <a href='{0}'>here</a> to login.</p>"+
                        "<p>&nbsp;</p>" +
                        "<p>&nbsp;</p>" +
                        "<p>Sincerely,</p>" +
                        "<p>Marketing Automation Tool Team</p>", sLoginURL));

                }
                
                string sSMTPserver = ConfigurationManager.AppSettings["smtpclient"].ToString();
                string sPort = ConfigurationManager.AppSettings["port"].ToString();
                string sUserName = ConfigurationManager.AppSettings["smtpusername"].ToString();
                string sPassword = ConfigurationManager.AppSettings["smtppassword"].ToString();
                string sFromAddress = ConfigurationManager.AppSettings["fromaddress"].ToString();

                if (mail.SmartASPNetSendEmail(sSMTPserver, int.Parse(sPort), sUserName, sPassword, sFromAddress, txtEmailAddress.Text.Trim(), "Your Username", true, sBody, System.Net.Mail.MailPriority.Normal))
                {
                    ToggleMessage(string.Format("An email has been sent containing your username. Please check your email to see username and click <a href='{0}'>here</a> to login.", sLoginURL), "display:block;", "alert alert-success",(int)ConstantValues.MessageBodyFormat.HTML);
                }
                else
                {
                    ToggleMessage("An error occurred sending email containing your username. Please contact System Administrator.", "display:block;", "alert alert-danger");
                }
            }
            else
            {
                ToggleMessage("Email address not found.", "display:block;", "alert alert-danger");
            }
        }

        private void ToggleMessage(string strText, string strDisplay, string strClass, int iMessageBodyFormat=0)
        {
            dvAlert.Attributes.Add("class", strClass);
            dvAlert.Attributes.Add("style", strDisplay);

            if (iMessageBodyFormat==(int)ConstantValues.MessageBodyFormat.Text)
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