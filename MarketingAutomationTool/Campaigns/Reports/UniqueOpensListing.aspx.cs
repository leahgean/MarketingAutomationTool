using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using MarketingAutomationTool.Campaigns.DataSets;
using Microsoft.Reporting.WebForms;

namespace MarketingAutomationTool.Campaigns.Reports
{
    public partial class UnqiueOpensListing : System.Web.UI.Page
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
                BusinessLayer.Campaign objCampaign = new BusinessLayer.Campaign();
                DataTable dtSubmittedCampaigns = objCampaign.GetCampaignReportGetAllSubmittedCampaigns(userlogin.AccountID);

                ddlCampaign.DataSource = dtSubmittedCampaigns;
                ddlCampaign.DataTextField = "CampaignName";
                ddlCampaign.DataValueField = "CampaignId";
                ddlCampaign.DataBind();

                ddlCampaign.Items.Insert(0, new ListItem("All Submitted Campaigns", string.Empty));

                DateTime? dtStartDate = null;
                DateTime? dtEndDate = null;
                DateTime dtStartDateValue;
                DateTime dtEndDateValue;

                if (DateTime.TryParse(System.DateTime.UtcNow.AddHours(8).AddDays(-7).ToString(), out dtStartDateValue))
                {
                    dtStartDate = dtStartDateValue;
                }

                if (DateTime.TryParse(System.DateTime.UtcNow.AddHours(8).ToString(), out dtEndDateValue))
                {
                    dtEndDate = dtEndDateValue;
                }

                GenerateReport(dtStartDate, dtEndDate);

            }

        }

        private void GenerateReport(DateTime? dtStartDate, DateTime? dtEndDate)
        {
            Campaign oCR = new Campaign();
            Campaign2 oCR2 = new Campaign2();

            int? CampaignId = null;
            int iCampaignId;

            if (!string.IsNullOrEmpty(ddlCampaign.SelectedItem.Value))
            {
                if (int.TryParse(ddlCampaign.SelectedItem.Value.Trim(), out iCampaignId))
                {
                    CampaignId = iCampaignId;
                }
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("StartDate", typeof(string));
            dt.Columns.Add("EndDate", typeof(string));
            dt.Columns.Add("CampaignName", typeof(string));
            dt.Columns.Add("GenerateTime", typeof(string));

            DataRow dtRow = dt.NewRow();
            dtRow["StartDate"] = string.Format("{0:dd MMM yyyy}", dtStartDate);
            dtRow["EndDate"] = string.Format("{0:dd MMM yyyy}", dtEndDate);

            if (CampaignId != null)
            {
                dtRow["CampaignName"] = oCR2.GetCampaignName(CampaignId.Value, userlogin.AccountID, userlogin.UserID);
            }
            else
            {
                dtRow["CampaignName"] = "All Submitted Campaigns";
            }

            dtRow["GenerateTime"] = string.Format("{0:dd MMM yyyy hh:mm:ss tt}", System.DateTime.UtcNow.AddHours(8));
            dt.Rows.Add(dtRow);

            DS_UnqiueOpensListing ds = new DS_UnqiueOpensListing();
            DataTable dt2 = oCR.GetCampaignReportUniqueOpensListing(userlogin.AccountID, dtStartDate, dtEndDate, CampaignId);


            ds.Tables["tblParameters"].Merge(dt);
            ds.Tables["tblOpens"].Merge(dt2);
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../ReportsLibrary/UniqueOpensListing.rdlc");

            ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables["tblParameters"]);
            ReportDataSource datasource2 = new ReportDataSource("DataSet2", ds.Tables["tblOpens"]);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            ReportViewer1.LocalReport.DataSources.Add(datasource2);
            ReportViewer1.LocalReport.Refresh();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            DateTime? dtStartDate = null;
            DateTime? dtEndDate = null;
            DateTime dtStartDateValue;
            DateTime dtEndDateValue;

            if (DateTime.TryParse(System.DateTime.UtcNow.AddHours(8).AddDays(-7).ToString(), out dtStartDateValue))
            {
                dtStartDate = dtStartDateValue;
            }

            if (DateTime.TryParse(System.DateTime.UtcNow.AddHours(8).ToString(), out dtEndDateValue))
            {
                dtEndDate = dtEndDateValue;
            }

            GenerateReport(dtStartDate, dtEndDate);

        }

        protected void lnkBreadHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard.aspx");
        }

        protected void lnkCampaigns_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Campaigns/Dashboard.aspx");
        }

        protected void lnkCampaignReports_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Campaigns/ViewAll.aspx");

        }
    }
}