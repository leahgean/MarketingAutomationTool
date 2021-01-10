using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;

namespace MarketingAutomationTool.MyAccount
{
    public partial class Accounts : System.Web.UI.Page
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

            if (!userlogin.IsSuperAdminUser)
                Response.Redirect("~/Public/Login.aspx");

            UsersPagerMethod userpager = new UsersPagerMethod(Refresh);
            Pager.CallingPageMethod = userpager;

            if (!Page.IsPostBack)
            {
                Pager.PageNum = 1;
                Refresh(Pager.PageNum.ToString());
            }

        }

        public void Refresh(string curpage)
        {
            
            int iMinItem, iMaxItem, iTotalRows = 0;
            BindData(out iMinItem, out iMaxItem, out iTotalRows);
            SetPager(Convert.ToInt32(curpage), "Id", "ASC", iTotalRows, iMinItem, iMaxItem);
        }

        private void BindData(out int iMinItem, out int iMaxItem, out int iTotalRows)
        {
            Account acc = new Account();
            DataTable dt = acc.GetAccounts(Pager.PageNum, Pager.CurMaxRows, Pager.SortBy, Pager.SortDirection, txtAccountName.Text.Trim(), ddlStatus.SelectedItem.Value.Trim(), out iMinItem, out iMaxItem, out iTotalRows);
            grdAccounts.DataSource = dt;
            grdAccounts.DataBind();
            grdAccounts.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        protected void grdAccounts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "SELECT":
                    Response.Redirect(string.Format("~/MyAccount/AccountDetails.aspx?c=2&accountid={0}", e.CommandArgument.ToString()));
                    break;
                case "USAGE":
                    Response.Redirect(string.Format("~/MyAccount/Usage.aspx?c=2&accountid={0}", e.CommandArgument.ToString()));
                    break;
                case "LOGINASCLIENT":
                    LoginAsClient(Guid.Parse(e.CommandArgument.ToString().Trim()));
                    break;
            }
        }

        private void LoginAsClient(Guid accountid)
        {

            if (userlogin.AccountID == accountid)
            {
                ToggleMessage("You are already logged as this account.", "display:block", "alert alert-danger");
            }
            else
            {

                BusinessLayer.UserLogin ulogin = new BusinessLayer.UserLogin();
                BusinessLayer.Account acc = new BusinessLayer.Account();
                DataModels.UserLogin selectedclient = new DataModels.UserLogin();
                DataModels.AccountModel selectedclientaccount = new DataModels.AccountModel();
                selectedclient = ulogin.GetUserByAccountId(accountid);
                selectedclientaccount = acc.GetAccountDetails(selectedclient.AccountID);

                if (selectedclientaccount.IsActive)
                {
                    Session["loggeduser"] = selectedclient;
                    Session["adminuser"] = userlogin;
                    Response.Redirect("~/Dashboard.aspx");
                }
                else
                {
                    ToggleMessage("You cannot login as this account because account is inactive.", "display:block", "alert alert-danger");
                }
                

            }

            
        }

       

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            Pager.PageNum = 1;
            Refresh(Pager.PageNum.ToString());
        }

        private void ToggleMessage(string strText, string strDisplay, string strClass)
        {
            dvMessage.Attributes.Add("class", strClass);
            dvMessage.Attributes.Add("style", strDisplay);
            dvMessage.InnerText = strText;
        }

        protected void lnkBreadHome_Click(object sender, EventArgs e)
        {
            //Session["maintab"] = "HOME";
            Response.Redirect("~/Dashboard.aspx");
        }
    }
}