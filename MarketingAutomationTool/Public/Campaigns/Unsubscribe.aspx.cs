using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModels;
using BusinessLayer;
using MarketingAutomationTool.Utilities;

namespace MarketingAutomationTool.Public.Campaigns
{
    public partial class Unsubscribe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string accountid = Request.QueryString["a"];
            string campaignid = Request.QueryString["c"];
            string contactid = Request.QueryString["l"];

            try
            {
                DataModels.CampaignUnsubscribes mCU = new CampaignUnsubscribes();
                mCU.AccountID = Guid.Parse(accountid);
                mCU.CampaignID = Guid.Parse(campaignid);
                mCU.ContactId = Guid.Parse(contactid);
                mCU.CreatedBy = Guid.Parse(contactid);
                mCU.CreatedDate = System.DateTime.UtcNow;
                mCU.IPAddress = Request.UserHostAddress;

                BusinessLayer.Campaign2 bCam = new BusinessLayer.Campaign2();
                bCam.InsertUnsubscribe(mCU);


                BusinessLayer.Lead bLead = new BusinessLayer.Lead();
                DataModels.Lead mLead = new DataModels.Lead();

                mLead=bLead.SelectLead(Guid.Parse(contactid), Guid.Parse(accountid));

                int? iCampaignId = bCam.SelectCampaignId(Guid.Parse(accountid), Guid.Parse(campaignid), Guid.Parse(contactid));
                int? iMessageId = bCam.SelectMessageId(Guid.Parse(accountid), Guid.Parse(campaignid), Guid.Parse(contactid));
                if (bLead.Unsubscribe(Guid.Parse(accountid), Guid.Parse(contactid), iCampaignId.Value, iMessageId.Value, (int)ConstantValues.ContactAction.UNSUBSCRIBE, Guid.Parse(contactid), Request.UserHostAddress, "EMAILCAMPAIGN", true, mLead.EmailAddress, mLead.CountryId, mLead.City, mLead.State))
                {
                    Account a = new Account();
                    string sAccountName = a.GetAccountName(Guid.Parse(accountid), Guid.Parse(contactid));

                    ToggleMessage(string.Format("Unsubscribe successful. You will no longer receive email notifications from {0}.",sAccountName,accountid,contactid), "display:block;", "alert alert-success", (int)Utilities.ConstantValues.MessageBodyFormat.HTML);
                }
                else
                {

                    Account a = new Account();
                    string sAccountEmail = a.GetAccountName(Guid.Parse(accountid), Guid.Parse(contactid));
                    ToggleMessage(string.Format("An error occurred during unsubscribe. Please email {0} to request to be unsubscribed from email notifications.", sAccountEmail), "display:block;", "alert alert-error", (int)Utilities.ConstantValues.MessageBodyFormat.Text);
                }  
            }
            catch(Exception ex)
            {
                Logger.Logger.WriteLog(contactid, "Public-Unsubscribe", campaignid, "Error", ex.Message);
            }
        }

        private void ToggleMessage(string strText, string strDisplay, string strClass, int pFormat)
        {
            dvAlert.Attributes.Add("class", strClass);
            dvAlert.Attributes.Add("style", strDisplay);
            if (pFormat==(int)Utilities.ConstantValues.MessageBodyFormat.HTML)
            {
                dvAlert.InnerHtml = strText;
            }
           else
            {
                dvAlert.InnerText = strText;
            }
            
        }
    }
}