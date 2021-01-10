using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using MarketingAutomationTool.Leads.DataSets;
using Microsoft.Reporting.WebForms;

namespace MarketingAutomationTool.Leads.Reports
{
    public partial class NewLeadsStatistics : System.Web.UI.Page
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
                BusinessLayer.Lead objLeads = new BusinessLayer.Lead();
                DataTable dtSources=objLeads.GetSource(userlogin.AccountID);

                ddlSource.DataSource = dtSources;
                ddlSource.DataTextField = "Text";
                ddlSource.DataValueField = "Value";
                ddlSource.DataBind();

                ddlSource.Items.Insert(0, new ListItem("All Sources",string.Empty));

                
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

                GenerateReport(dtStartDate,dtEndDate);
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

        private void GenerateReport(DateTime? dtStartDate,DateTime? dtEndDate)
        {
            LeadReport oLR = new LeadReport();
            
            string sSource;
            int? iType = null;
            int iTypeValue;
            string sType = string.Empty;
            int? iStatus = null;
            int iStatusValue;
            string sStatus = string.Empty;
            int iTotalLeadCount = 0;

            sSource = ddlSource.SelectedItem.Value;
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

            DS_NewLeadsStatistics ds = new DS_NewLeadsStatistics();
            DataTable dt = oLR.NewLeadsStatistics(dtStartDate, dtEndDate, sSource, iType, iStatus, userlogin.AccountID, out iTotalLeadCount);

            DataTable dt1 = new DataTable();
            dt1.Columns.Add(new DataColumn("Date", typeof(DateTime)));
            dt1.Columns.Add(new DataColumn("Count", typeof(Int32)));

            DataTable dt2 = new DataTable();
            dt2.Columns.Add(new DataColumn("Date", typeof(DateTime)));
            dt2.Columns.Add(new DataColumn("Count", typeof(Int32)));

            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
               if ((i%2)==0)
                {
                    DataRow dt2Row = dt2.NewRow();
                    dt2Row["Date"] = dr["DATE"];
                    dt2Row["Count"] = dr["COUNT"];
                    dt2.Rows.Add(dt2Row);
                }
               else
                {
                    DataRow dt1Row = dt1.NewRow();
                    dt1Row["Date"] = dr["DATE"];
                    dt1Row["Count"] = dr["COUNT"];
                    dt1.Rows.Add(dt1Row);
                }
                i++;
            }

            DataTable dt3 = new DataTable();
            dt3.Columns.Add("StartDate", typeof(string));
            dt3.Columns.Add("EndDate", typeof(string));
            dt3.Columns.Add("Source", typeof(string));
            dt3.Columns.Add("Type", typeof(string));
            dt3.Columns.Add("Status", typeof(string));
            dt3.Columns.Add("GenerateTime", typeof(string));

            DataRow dtRow3 = dt3.NewRow();
            dtRow3["StartDate"] = string.Format("{0:dd MMM yyyy}", dtStartDate);
            dtRow3["EndDate"] = string.Format("{0:dd MMM yyyy}", dtEndDate);
            dtRow3["Source"] =  GetSource(sSource);
            dtRow3["Type"] =  GetType(sType);
            dtRow3["Status"] =  GetStatus(sStatus);
            dtRow3["GenerateTime"] = string.Format("{0:dd MMM yyyy hh:mm:ss tt}", System.DateTime.UtcNow.AddHours(8));

            dt3.Rows.Add(dtRow3);

            DataTable dt4 = new DataTable();
            dt4.Columns.Add("Count", typeof(int));

            DataRow dtRow4 = dt4.NewRow();
            dtRow4["Count"] = iTotalLeadCount;
            dt4.Rows.Add(dtRow4);

            ds.Tables["tblStatistics"].Merge(dt1);
            ds.Tables["tblStatistics2"].Merge(dt2);
            ds.Tables["tblParameters"].Merge(dt3);
            ds.Tables["tblTotal"].Merge(dt4);
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../ReportLibrary/LeadsStatistics.rdlc");

            ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables["tblStatistics"]);
            ReportDataSource datasource2 = new ReportDataSource("DataSet2", ds.Tables["tblStatistics2"]);
            ReportDataSource datasource3 = new ReportDataSource("DataSet3", ds.Tables["tblParameters"]);
            ReportDataSource datasource4 = new ReportDataSource("DataSet4", ds.Tables["tblTotal"]);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            ReportViewer1.LocalReport.DataSources.Add(datasource2);
            ReportViewer1.LocalReport.DataSources.Add(datasource3);
            ReportViewer1.LocalReport.DataSources.Add(datasource4);
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

        private  string GetType(string stype)
        {
            switch(stype)
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