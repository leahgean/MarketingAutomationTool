using System;
using System.Web.UI;
using BusinessLayer;
using System.Web.UI.WebControls;
using System.Data;

namespace MarketingAutomationTool.MyAccount
{
    public partial class Users : System.Web.UI.Page
    {


        delegate void UsersPagerMethod(string curPage);

        private DataModels.UserLogin userlogin
        {
            get
            {
                if (ViewState["loggeduser"]!= null)
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

        public bool IsSuperAdminUser
        {
            get { return userlogin.IsSuperAdminUser; }
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
                //List<DataModels.UserModel> userlist= usr.GetUsersByAccountId(Pager.PageNum, Pager.MaxRows, Pager.SortBy, Pager.SortDirection, userlogin.AccountID, out iMinItem, out iMaxItem, out iTotalRows);
                //grdUsers.DataSource = userlist;
                Pager.PageNum = 1;
                Refresh(Pager.PageNum.ToString());
            }
        }

        private void BindData(Guid accountid, out int iMinItem, out int iMaxItem, out int iTotalRows)
        {
            User usr = new User();
            DataTable dt = usr.GetUsersByAccountId(Pager.PageNum, Pager.CurMaxRows, Pager.SortBy, Pager.SortDirection, accountid, txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtUserName.Text.Trim(), out iMinItem, out iMaxItem, out iTotalRows);
            grdUsers.DataSource = dt;
            grdUsers.DataBind();
            grdUsers.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            Pager.PageNum = 1;
            Refresh(Pager.PageNum.ToString());

        }

        public void Refresh(string curpage)
        {
            int iMinItem, iMaxItem, iTotalRows = 0;
            BindData(userlogin.AccountID, out iMinItem, out iMaxItem, out iTotalRows);
            SetPager(Convert.ToInt32(curpage), "Id", "ASC", iTotalRows, iMinItem, iMaxItem);
        }

        protected void grdUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName== "SELECT")
            {
                Response.Redirect(string.Format("~/MyAccount/UserDetails.aspx?c=3&userid={0}", e.CommandArgument.ToString()));
            }
        }

        protected void lnkBreadHome_Click(object sender, EventArgs e)
        {
            //Session["maintab"] = "HOME";
            Response.Redirect("~/Dashboard.aspx");
        }
    }
}