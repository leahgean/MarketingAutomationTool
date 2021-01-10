using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MarketingAutomationTool.Leads.Reports
{
    public partial class ViewAll : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                grdReportTable.DataSource = GenerateReportTable();
                grdReportTable.DataBind();

            }
        }

        private DataTable GenerateReportTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("ReportName", typeof(string));

            DataRow dtRow = dt.NewRow();
            dtRow["Id"] = 1;
            dtRow["ReportName"] = "Leads Statistics";
            dt.Rows.Add(dtRow);

            dtRow = dt.NewRow();
            dtRow["Id"] = 2;
            dtRow["ReportName"] = "Leads Listing";
            dt.Rows.Add(dtRow);

            dtRow = dt.NewRow();
            dtRow["Id"] = 3;
            dtRow["ReportName"] = "Deleted Leads Listing";
            dt.Rows.Add(dtRow);

            dtRow = dt.NewRow();
            dtRow["Id"] = 4;
            dtRow["ReportName"] = "Unsubscribed Leads Listing";
            dt.Rows.Add(dtRow);

            dtRow = dt.NewRow();
            dtRow["Id"] = 5;
            dtRow["ReportName"] = "Duplicate Leads Listing";
            dt.Rows.Add(dtRow);

            return dt;
        }

        protected void lnkLeads_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Leads/Lead.aspx");
        }

        protected void lnkBreadHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard.aspx");
        }

        protected void grdReportTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch(e.CommandArgument.ToString().Trim())
            {
                case "1":
                    Response.Redirect("~/Leads/Reports/LeadsStatistics.aspx");
                    break;
                case "2":
                    Response.Redirect("~/Leads/Reports/LeadsListing.aspx");
                    break;
                case "3":
                    Response.Redirect("~/Leads/Reports/DeletedLeadsListing.aspx");
                    break;
                case "4":
                    Response.Redirect("~/Leads/Reports/UnsubscribedLeadsListing.aspx");
                    break;
                case "5":
                    Response.Redirect("~/Leads/Reports/DuplicateLeadsListing.aspx");
                    break;

            }
        }
    }
}