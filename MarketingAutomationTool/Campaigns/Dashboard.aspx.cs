using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace MarketingAutomationTool.Campaigns
{
    public partial class Dashboard : System.Web.UI.Page
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

            BindBarChart(userlogin.AccountID);
            BindBarChart_UniqueStats(userlogin.AccountID);

            if (!Page.IsPostBack)
            {
                PopulateCampaigns();
            }
        }

        private void PopulateCampaigns()
        {
            BusinessLayer.Dashboard db = new BusinessLayer.Dashboard();
            DataTable dt = db.GetTopFiveCampaigns(userlogin.AccountID);

            grdCampaigns.DataSource = dt;
            grdCampaigns.DataBind();
        }

        protected void lnkBreadHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard.aspx");
        }

        protected void lnkViewAll_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Campaigns/ViewAll.aspx");
        }

        protected void grdCampaigns_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper().Trim())
            {
                case "SELECT":
                    string sCommandArg = e.CommandArgument.ToString();
                    string[] sCommArgArr = sCommandArg.Split('|');

                    Response.Redirect(string.Format("/Campaigns/CampaignBuilder2.aspx?c={0}&uid={1}", sCommArgArr[0].Trim(), sCommArgArr[1].Trim()));
                    break;
            }
        }


        private void BindBarChart(Guid pAccountID)
        {

            BusinessLayer.Campaign objCampaign = new BusinessLayer.Campaign();
            DataTable dt = objCampaign.GetCampaignStats(pAccountID);


            
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    List<string> data = (from p in dt.AsEnumerable()
                                         select p.Field<string>("Name")).Distinct().ToList();

                    cCampaignStat.Visible = true;
                    foreach (string d in data)
                    {

                        //Get the MonthName for each Item.
                        string[] x = (from p in dt.AsEnumerable()
                                   where p.Field<string>("Name") == d
                                   orderby p.Field<DateTime>("Date") ascending, p.Field<int>("Order") ascending
                                   select p.Field<string>("MonthName")).ToArray();

                        //Get the Count for each Item.
                        int[] y = (from p in dt.AsEnumerable()
                                   where p.Field<string>("Name") == d
                                   orderby p.Field<DateTime>("Date") ascending, p.Field<int>("Order") ascending
                                   select p.Field<int>("Count")).ToArray();

                        //Add Series to the Chart.
                        cCampaignStat.Series.Add(new Series(d));
                        cCampaignStat.Series[d].IsValueShownAsLabel = true;
                        cCampaignStat.Series[d].ChartType = SeriesChartType.Column;
                        cCampaignStat.Series[d].Points.DataBindXY(x, y);
                    }

                    cCampaignStat.Legends[0].Enabled = true;
                    cCampaignStat.ChartAreas[0].AxisX.Title = "Month";
                    cCampaignStat.ChartAreas[0].AxisY.Title = "Count";
                }
                else
                {
                    cCampaignStat.Visible = false;
                }
            }
            else
            {
                cCampaignStat.Visible = false;
            }
        }


        private void BindBarChart_UniqueStats(Guid pAccountID)
        {

            BusinessLayer.Campaign objCampaign = new BusinessLayer.Campaign();
            DataTable dt = objCampaign.GetCampaignUniqueStats(pAccountID);



            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    List<string> data = (from p in dt.AsEnumerable()
                                         select p.Field<string>("Name")).Distinct().ToList();

                    cCampaignStatUnqiue.Visible = true;
                    foreach (string d in data)
                    {

                        //Get the MonthName for each Item.
                        string[] x = (from p in dt.AsEnumerable()
                                      where p.Field<string>("Name") == d
                                      orderby p.Field<DateTime>("Date") ascending, p.Field<int>("Order") ascending
                                      select p.Field<string>("MonthName")).ToArray();

                        //Get the Count for each Item.
                        int[] y = (from p in dt.AsEnumerable()
                                   where p.Field<string>("Name") == d
                                   orderby p.Field<DateTime>("Date") ascending, p.Field<int>("Order") ascending
                                   select p.Field<int>("Count")).ToArray();

                        //Add Series to the Chart.
                        cCampaignStatUnqiue.Series.Add(new Series(d));
                        cCampaignStatUnqiue.Series[d].IsValueShownAsLabel = true;
                        cCampaignStatUnqiue.Series[d].ChartType = SeriesChartType.Column;
                        cCampaignStatUnqiue.Series[d].Points.DataBindXY(x, y);
                    }

                    cCampaignStat.Legends[0].Enabled = true;
                    cCampaignStat.ChartAreas[0].AxisX.Title = "Month";
                    cCampaignStat.ChartAreas[0].AxisY.Title = "Count";
                }
                else
                {
                    cCampaignStatUnqiue.Visible = false;
                }
            }
            else
            {
                cCampaignStatUnqiue.Visible = false;
            }
        }


    }
}