using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MarketingAutomationTool.Public.Campaigns
{
    public partial class Beacon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // check if-modified-since header to determine if receiver has requested the image in last 24 hours
            if (checkIfRequested(this.Context.Request))
            {
                //receiver had already requested the image, hence send back a not modified result
                Response.StatusCode = 304;
                Response.SuppressContent = true;
            }
            else
            {
                string accountid = Request.QueryString["a"];
                string campaignid = Request.QueryString["c"];
                string contactid = Request.QueryString["l"];

               try
                {
                    DataModels.CampaignOpened mCO = new DataModels.CampaignOpened();
                    mCO.AccountID = Guid.Parse(accountid);
                    mCO.CampaignID = Guid.Parse(campaignid);
                    mCO.ContactId = Guid.Parse(contactid);
                    mCO.CreatedBy = Guid.Parse(contactid);
                    mCO.CreatedDate = System.DateTime.UtcNow;
                    mCO.IPAddress = Request.UserHostAddress;

                    BusinessLayer.Campaign2 bCam = new BusinessLayer.Campaign2();
                    bCam.InsertEmailsOpened(mCO);
                }
                catch(Exception ex)
                {
                    Logger.Logger.WriteLog(contactid, "Public-Beacon", campaignid, "Error", ex.Message);
                }

                //Send the single pixel gif image as response
                byte[] imgbytes = Convert.FromBase64String("R0lGODlhAQABAIAAANvf7wAAACH5BAEAAAAALAAAAAABAAEAAAICRAEAOw==");
                Response.ContentType = "image/gif";
                Response.AppendHeader("Content-Length", imgbytes.Length.ToString());
                Response.Cache.SetLastModified(DateTime.UtcNow);
                Response.Cache.SetCacheability(HttpCacheability.Public);
                Response.BinaryWrite(imgbytes);
            }

        }

        private bool checkIfRequested(HttpRequest req)
        {
            // check if-modified-since header to check if receiver has already requested the image in last 24 hours
            return req.Headers["If-Modified-Since"] == null ? false : DateTime.Parse(req.Headers["If-Modified-Since"]).AddHours(24) >= DateTime.UtcNow;
        }
    }
}