using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MarketingAutomationTool.Campaigns.Reports
{
    public partial class ViewAll : System.Web.UI.Page
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

            if (!Page.IsPostBack)
            {

                grdCampaignsReports.DataSource = GenerateReportTable();
                grdCampaignsReports.DataBind();

            }
        }

        private DataTable GenerateReportTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("ReportName", typeof(string));

            DataRow dtRow = dt.NewRow();
            dtRow["Id"] = 1;
            dtRow["ReportName"] = "Email Campaign Statistics";
            dt.Rows.Add(dtRow);

            dtRow = dt.NewRow();
            dtRow["Id"] = 3;
            dtRow["ReportName"] = "Emails Sent Listing";
            dt.Rows.Add(dtRow);

            dtRow = dt.NewRow();
            dtRow["Id"] = 3;
            dtRow["ReportName"] = "Total Opens Listing";
            dt.Rows.Add(dtRow);

            dtRow = dt.NewRow();
            dtRow["Id"] = 4;
            dtRow["ReportName"] = "Unique Opens Listing";
            dt.Rows.Add(dtRow);

            dtRow = dt.NewRow();
            dtRow["Id"] = 5;
            dtRow["ReportNme"] = "Total Clickthroughs Listing";
            dt.Rows.Add(dtRow);

            dtRow = dt.NewRow();
            dtRow["Id"] = 6;
            dtRow["ReportName"] = "Unique Clickthroughs Listing";
            dt.Rows.Add(dtRow);

            return dt;
        }

        protected void grdCampaignsReports_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string sReportId = e.CommandArgument.ToString().Trim();

            if (sReportId=="1")
            {
                Response.Redirect("~/Campaigns/Reports/EmailStats.aspx");
            }
            else if (sReportId == "2")
            {
                Response.Redirect("~/Campaigns/Reports/EmailsSentListing.aspx");
            }
            else if (sReportId == "3")
            {
                Response.Redirect("~/Campaigns/Reports/TotalOpensListing.aspx");
            }
            else if (sReportId == "4")
            {
                Response.Redirect("~/Campaigns/Reports/UniqueOpensListing.aspx");
            }
            else if (sReportId == "5")
            {
                Response.Redirect("~/Campaigns/Reports/TotalClickthroughsListing.aspx");
            }
            else if (sReportId == "6")
            {
                Response.Redirect("~/Campaigns/Reports/UniqueClickthroughsListing.aspx");
            }
        }

        protected void lnkBreadHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard.aspx");
        }

        protected void lnkCampaigns_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Campaigns/Dashboard.aspx");
        }
    }
}