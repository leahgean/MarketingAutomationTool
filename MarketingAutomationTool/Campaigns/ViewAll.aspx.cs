using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MarketingAutomationTool.Campaigns
{
    public partial class ViewAll : System.Web.UI.Page
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

            if (!Page.IsPostBack)
            {
                Pager.PageNum = 1;
                Pager.SortBy = "C.Id";
                Pager.SortDirection = "DESC";
                Refresh(Pager.PageNum.ToString());
            }
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
        public void Refresh(string curpage)
        {
            int iMinItem, iMaxItem, iTotalRows = 0;
            int iSearchId = 0;
            int iPageNum = Convert.ToInt32(curpage);
            int iNewPageNum = iPageNum;
            decimal dMaxPages = 0;
            BindData(iSearchId, iPageNum, out iNewPageNum, out iMinItem, out iMaxItem, out iTotalRows, out dMaxPages);
            SetPager(iNewPageNum, Pager.SortBy, Pager.SortDirection, iTotalRows, iMinItem, iMaxItem, dMaxPages);   
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

        private void BindData(int iSearchId, int iPageNum, out int iNewPageNum, out int iMinItem, out int iMaxItem, out int iTotalRows, out decimal dMaxPages)
        {
            BusinessLayer.Campaign oCam = new BusinessLayer.Campaign();
            DataTable dt = oCam.GetAllCampaigns(userlogin.AccountID,null,null,userlogin.UserID, Pager.CurMaxRows, Pager.SortBy, Pager.SortDirection,iPageNum, out iNewPageNum, out iMinItem, out iMaxItem, out iTotalRows, out dMaxPages);
            grdCampaigns.DataSource = dt;
            grdCampaigns.DataBind();
        }

        protected void lnkBreadHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard.aspx");
        }

        protected void lnkCampaigns_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Campaigns/Dashboard.aspx");
        }
    }
}