using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModels;
using BusinessLayer;

namespace MarketingAutomationTool.Public.Campaigns
{
    public partial class Clickthroughs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string accountid = Request.QueryString["a"];
            string campaignid = Request.QueryString["c"];
            string contactid = Request.QueryString["l"];
            string link = Request.QueryString["li"];

            try
            {
                DataModels.CampaignClickthroughs mCC = new CampaignClickthroughs();
                mCC.AccountID = Guid.Parse(accountid);
                mCC.CampaignID = Guid.Parse(campaignid);
                mCC.ContactId = Guid.Parse(contactid);
                mCC.Link = link;
                mCC.CreatedBy = Guid.Parse(contactid);
                mCC.CreatedDate = System.DateTime.UtcNow;
                mCC.IPAddress = Request.UserHostAddress;

                BusinessLayer.Campaign2 bCam = new BusinessLayer.Campaign2();
                bCam.InsertClickthroughs(mCC);
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(contactid, "Public-Clickthroughs", campaignid, "Error", ex.Message);
            }
            Response.Redirect(link);
        }
    }
}