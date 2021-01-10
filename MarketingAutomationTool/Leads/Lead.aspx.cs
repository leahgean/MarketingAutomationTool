using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace MarketingAutomationTool.Leads
{
    public partial class Lead : System.Web.UI.Page
    {
        delegate void UsersPagerMethod(string curPage);

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

            UsersPagerMethod userpager = new UsersPagerMethod(Refresh);
            Pager.CallingPageMethod = userpager;
           
            BindBarChart(userlogin.AccountID);
            BindPieChart(userlogin.AccountID);

            if (!Page.IsPostBack)
            {
                Pager.PageNum = 1;
                Pager.SortBy = "CreatedDate";
                Pager.SortDirection = "DESC";
                Refresh(Pager.PageNum.ToString());
            }
        }

        public void Refresh(string curpage)
        {
            int iMinItem, iMaxItem, iTotalRows = 0;
            int iPageNum = Convert.ToInt32(curpage);
            int iNewPageNum = iPageNum;
            decimal dMaxPages = 0;
            BindData(userlogin.AccountID, userlogin.UserID, Pager.CurMaxRows,Pager.SortBy, Pager.SortDirection, iPageNum, out iNewPageNum, out iMinItem, out iMaxItem, out iTotalRows, out dMaxPages);
            SetPager(iNewPageNum, "CreatedDate", "DESC", iTotalRows, iMinItem, iMaxItem, dMaxPages);
                
        }

        private void BindData(Guid AccountID, Guid CreatedBy, int MaxRows, string SortBy, string SortDirection, int PageNum, out int NewPageNum, out int MinItem, out int MaxItem, out int TotalRows, out decimal MaxPages)
        {
            BusinessLayer.Lead objlead = new BusinessLayer.Lead();
            DataTable dt = objlead.GetRecentLeads(AccountID, CreatedBy, MaxRows, SortBy, SortDirection, PageNum, out NewPageNum, out MinItem, out MaxItem, out TotalRows, out MaxPages);
            grdContacts.DataSource = dt;
            grdContacts.DataBind();
        }

        private void SetPager(int curpage, string sortby, string sortdir, int totalrows, int minitem, int maxitem, decimal maxpages)
        {
            if (totalrows > 0)
            {
                Pager.Visible = true;
                Pager.PageNum = curpage;
                Pager.SortBy = sortby;
                Pager.SortDirection = sortdir;
                Pager.TotalRows = totalrows;
                Pager.MinItem = minitem;
                Pager.MaxItem = maxitem;
                Pager.TotalRows = totalrows;
                Pager.MaxPages = Convert.ToInt32(maxpages);//Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalrows) / Convert.ToDecimal(Pager.CurMaxRows)));
            }
            else
            {
                Pager.Visible = false;
            }
        }

        private void BindPieChart(Guid pAccountID)
        {
            BusinessLayer.Lead objlead = new BusinessLayer.Lead();
            decimal dtotal = 0;

            /*Get Data*/
            DataTable dt = objlead.GetContactPerCountry(pAccountID);
            if (dt!=null)
            {
                if (dt.Rows.Count > 0)
                {
                    cContactPerCountry.Visible = true;

                    foreach (DataRow dr in dt.Rows)
                    {
                        dtotal += decimal.Parse(dr["Count"].ToString());
                    }

                    string[] XPointMember = new string[dt.Rows.Count];
                    decimal[] YPointMember = new decimal[dt.Rows.Count];

                    for (int count = 0; count < dt.Rows.Count; count++)
                    {
                        XPointMember[count] = dt.Rows[count]["Country"].ToString();
                        YPointMember[count] = Math.Round((decimal.Parse(dt.Rows[count]["Count"].ToString()) / dtotal), 2) * 100;
                    }

                    cContactPerCountry.Series[0].Points.DataBindXY(XPointMember, YPointMember);

                    cContactPerCountry.Series[0].BorderWidth = 10;
                    cContactPerCountry.Series[0].ChartType = SeriesChartType.Pie;

                    /*Add Legend*/
                    cContactPerCountry.Legends.Add(new Legend());
                    cContactPerCountry.Legends[0].Docking = Docking.Bottom;
                    cContactPerCountry.Legends[0].IsDockedInsideChartArea = false;
                    ElementPosition pos = new ElementPosition();
                    cContactPerCountry.Legends[0].Alignment = StringAlignment.Center;

                    foreach (Series charts in cContactPerCountry.Series)
                    {
                        int iPointIndex = 0;

                        foreach (DataPoint point in charts.Points)
                        {
                            point.Color = SetColor(iPointIndex);
                            point.Label = string.Format("{0}%", point.YValues[0]);
                            point.LegendText = point.AxisLabel;
                            iPointIndex++;
                        }
                    }

                }
                else
                {
                    cContactPerCountry.Visible = false;
                }
            }
            else
            {
                cContactPerCountry.Visible = false;
            }

        }

        private Color SetColor(int iPointIndex)
        {
            int iModResult = (iPointIndex % 10);
            switch (iModResult)
            {
                case 1: return Color.FromArgb(192, 80, 78); 
                case 2: return Color.FromArgb(155, 187, 88);
                case 3: return Color.FromArgb(35, 191, 170);
                case 4: return Color.FromArgb(128, 100, 161);
                case 5: return Color.FromArgb(74, 172, 197);
                case 6: return Color.FromArgb(116, 186, 92);
                case 7: return Color.FromArgb(247, 150, 71);
                case 8: return Color.FromArgb(172, 47, 92);
                case 9: return Color.FromArgb(160, 207, 98);
                default: return Color.FromArgb(79, 129, 188);
            }
        }

       


        private void BindBarChart(Guid pAccountID)
        {
           
            BusinessLayer.Lead objlead = new BusinessLayer.Lead();
            DataTable dt = objlead.GetLeadTotalByMonth(pAccountID);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    cContactProgress.Visible = true;
                    Series series = new Series();

                    foreach (DataRow dr in dt.Rows)
                    {
                        int y = (int)dr["COUNT"];
                        series.Points.AddXY(dr["MONTH"].ToString(), y);
                    }

                    series.Color = SetColor(0);
                    cContactProgress.Series.Add(series);
                    cContactProgress.ChartAreas[0].AxisX.Title = "Month";
                    cContactProgress.ChartAreas[0].AxisY.Title = "Number of Leads";
                }
                else
                {
                    cContactProgress.Visible = false;
                }
            }
            else
            {
                cContactProgress.Visible = false;
            }
        }

        protected void lnkBreadHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard.aspx");
        }
        
        protected void grdContacts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "SELECT":
                    Response.Redirect(string.Format("~/Leads/EditLead.aspx?Guid={0}", e.CommandArgument.ToString()));
                    break;
            }

        }
        
    }
}