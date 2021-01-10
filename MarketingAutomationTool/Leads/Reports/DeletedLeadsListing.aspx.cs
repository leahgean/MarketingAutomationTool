using BusinessLayer;
using MarketingAutomationTool.Leads.DataSets;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MarketingAutomationTool.Leads.Reports
{
    public partial class DeletedLeadsListing : System.Web.UI.Page
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

        protected void lnkLeads_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Leads/Lead.aspx");
        }

        protected void lnkBreadHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard.aspx");
        }

       
        protected void lnkReports_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Leads/Reports/ViewAll.aspx");
        }

        private void GenerateReport(DateTime? dtStartDate, DateTime? dtEndDate)
        {
            LeadReport oLR = new LeadReport();

            
            int? iType = null;
            int iTypeValue;
            string sType = string.Empty;
            int? iStatus = null;
            int iStatusValue;
            string sStatus = string.Empty;
            int iTotalLeadCount = 0;
        
            sType = ddlType.SelectedItem.Value.Trim();

            if (int.TryParse(sType, out iTypeValue))
            {
                iType = iTypeValue;
            }

            sStatus = hdnStatus.Value.Trim();

            if (int.TryParse(sStatus, out iStatusValue))
            {
                iStatus = iStatusValue;
            }

            DS_LeadsListing ds = new DS_LeadsListing();
            DataTable dt = oLR.DeletedLeadsListing(dtStartDate, dtEndDate, iType, iStatus, userlogin.AccountID, out iTotalLeadCount);

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("StartDate", typeof(string));
            dt2.Columns.Add("EndDate", typeof(string));
            dt2.Columns.Add("Type", typeof(string));
            dt2.Columns.Add("Status", typeof(string));
            dt2.Columns.Add("GenerateTime", typeof(string));

            DataRow dtRow2 = dt2.NewRow();
            dtRow2["StartDate"] = string.Format("{0:dd MMM yyyy}", dtStartDate);
            dtRow2["EndDate"] = string.Format("{0:dd MMM yyyy}", dtEndDate);
            dtRow2["Type"] =  GetType(sType);
            dtRow2["Status"] = GetStatus(sStatus);
            dtRow2["GenerateTime"] = string.Format("{0:dd MMM yyyy hh:mm:ss tt}", System.DateTime.UtcNow.AddHours(8));
            dt2.Rows.Add(dtRow2);

            DataTable dt3 = new DataTable();
            dt3.Columns.Add("Count", typeof(int));

            DataRow dtRow3 = dt3.NewRow();
            dtRow3["Count"] = iTotalLeadCount;
            dt3.Rows.Add(dtRow3);

            ds.Tables["tblLeadsListing"].Merge(dt);
            ds.Tables["tblParameters"].Merge(dt2);
            ds.Tables["tblTotal"].Merge(dt3);
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../ReportLibrary/DeletedLeadsListing.rdlc");

            ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables["tblLeadsListing"]);
            ReportDataSource datasource2 = new ReportDataSource("DataSet2", ds.Tables["tblParameters"]);
            ReportDataSource datasource3 = new ReportDataSource("DataSet3", ds.Tables["tblTotal"]);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            ReportViewer1.LocalReport.DataSources.Add(datasource2);
            ReportViewer1.LocalReport.DataSources.Add(datasource3);
            ReportViewer1.LocalReport.Refresh();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            DateTime? dtStartDate = null;
            DateTime? dtEndDate = null;
            DateTime dtStartDateValue;
            DateTime dtEndDateValue;

            if (DateTime.TryParse(txtStartDate.Text.Trim(), out dtStartDateValue))
            {
                dtStartDate = dtStartDateValue;
            }

            if (DateTime.TryParse(txtEndDate.Text.Trim(), out dtEndDateValue))
            {
                dtEndDate = dtEndDateValue;
            }
            GenerateReport(dtStartDate, dtEndDate);
        }

        private string GetSource(string sSource)
        {
            if (string.IsNullOrEmpty(sSource)) return "All";
            else return sSource;
        }

        private string GetType(string stype)
        {
            switch (stype)
            {
                case "1":
                    return "Lead";
                case "2":
                    return "Contact";
                default:
                    return "All";
            }
        }


        private string GetStatus(string sstatus)
        {
            switch (sstatus)
            {
                case "1":
                    return "Not Confirmed";
                case "2":
                    return "Confirmed";
                case "3":
                    return "Active";
                case "4":
                    return "Inactive";
                default:
                    return "All";
            }
        }
    }
}