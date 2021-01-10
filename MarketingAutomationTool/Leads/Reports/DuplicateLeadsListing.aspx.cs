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
    public partial class DuplicateLeadsListing : System.Web.UI.Page
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
                GenerateReport();
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

        private void GenerateReport()
        {
            LeadReport oLR = new LeadReport();

            
            int iTotalLeadCount = 0;

            DS_LeadsListing ds = new DS_LeadsListing();
            DataTable dt = oLR.DuplicateLeadsListing(userlogin.AccountID, out iTotalLeadCount);

            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Count", typeof(int));
            dt1.Columns.Add("GenerateTime", typeof(string));

            DataRow dtRow1 = dt1.NewRow();
            dtRow1["Count"] = iTotalLeadCount;
            dtRow1["GenerateTime"] = string.Format("{0:dd MMM yyyy hh:mm:ss tt}", System.DateTime.UtcNow.AddHours(8));
            dt1.Rows.Add(dtRow1);

            ds.Tables["tblLeadsListing"].Merge(dt);
            ds.Tables["tblTotal"].Merge(dt1);
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../ReportLibrary/DuplicateLeadsListing.rdlc");

            ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables["tblLeadsListing"]);
            ReportDataSource datasource2 = new ReportDataSource("DataSet2", ds.Tables["tblTotal"]);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            ReportViewer1.LocalReport.DataSources.Add(datasource2);
            ReportViewer1.LocalReport.Refresh();
        }

        

        


        
    }
}