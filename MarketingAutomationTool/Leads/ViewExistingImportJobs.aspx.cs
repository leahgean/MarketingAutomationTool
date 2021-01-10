using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

namespace MarketingAutomationTool.Leads
{
    public partial class ViewExistingImportJobs : System.Web.UI.Page
    {
        delegate void UsersPagerMethod(string curPage);

        public DataModels.UserLogin userlogin
        {
            get
            {
                if (ViewState["userlogin"] != null)
                {
                    return (DataModels.UserLogin)ViewState["userlogin"];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ViewState["userlogin"] = (DataModels.UserLogin)value;
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
                Refresh(Pager.PageNum.ToString());
            }
        }

        protected void lnkBreadHome_Click(object sender, EventArgs e)
        {
            //Session["maintab"] = "HOME";
            Response.Redirect("~/Dashboard.aspx");
        }

        public void Refresh(string curpage)
        {
            int iMinItem, iMaxItem, iTotalRows = 0;
            BindData(false, out iMinItem, out iMaxItem, out iTotalRows);
            SetPager(Convert.ToInt32(curpage), "DateCreated", "DESC", iTotalRows, iMinItem, iMaxItem);
        }

        private void BindData(bool isDeleted, out int iMinItem, out int iMaxItem, out int iTotalRows)
        {
            LeadImport cList = new LeadImport();
            DataTable dt = cList.GetExistingImports(userlogin.AccountID, Pager.PageNum, Pager.CurMaxRows, "DATECREATED", "DESC", out iMinItem, out iMaxItem, out iTotalRows);
            grdList.DataSource = dt;
            grdList.DataBind();
        }

        private void SetPager(int curpage, string sortby, string sortdir, int totalrows, int minitem, int maxitem)
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
                Pager.MaxPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalrows) / Convert.ToDecimal(Pager.CurMaxRows)));
            }
            else
            {
                Pager.Visible = false;
            }
        }

        protected void grdList_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}