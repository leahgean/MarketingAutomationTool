using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

namespace MarketingAutomationTool.Leads
{
    public partial class BrowseList : System.Web.UI.Page
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
            Response.Redirect("~/Dashboard.aspx");
        }

        public void Refresh(string curpage)
        {
            int iMinItem, iMaxItem, iTotalRows = 0;
            int iNewPageNum = Convert.ToInt32(curpage);
            decimal dMaxPages = 0;
            BindData(false, out iNewPageNum, out iMinItem, out iMaxItem, out iTotalRows, out dMaxPages);
            SetPager(iNewPageNum, "Id", "ASC", iTotalRows, iMinItem, iMaxItem);
        }

        private void BindData(bool isDeleted, out int iNewPageNum, out int iMinItem, out int iMaxItem, out int iTotalRows, out decimal dMaxPages)
        {
            ContactList cList = new ContactList();
            DataTable dt = cList.GetLists(userlogin.AccountID, isDeleted, Pager.CurMaxRows, "ID", "ASC", Pager.PageNum, out iNewPageNum, out iMinItem, out iMaxItem, out iTotalRows, out dMaxPages);
            grdList.DataSource = dt;
            grdList.DataBind();
            grdList.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            switch (e.CommandName)
            {
                case "SELECT":
                    Session["ContactListId"] = e.CommandArgument;
                    Session["SuccessMessage"] = null;
                    Response.Redirect("~/Leads/EditList.aspx");
                    break;
                case "LISTMEMBERS":
                    Session["ContactListId"] = e.CommandArgument;
                    Session["ShowListMembers"] = "1";
                    Response.Redirect("~/Leads/EditList.aspx");
                    break;
                case "DELETE":
                    ContactList cList = new ContactList();
                    if (cList.DeleteContactList(userlogin.AccountID, Guid.Parse(userlogin.ModifiedBy.ToString()), int.Parse(e.CommandArgument.ToString())))
                    {
                        ToggleMessage("Success! List was deleted.", "display:block", "alert alert-success");
                        Refresh(Pager.PageNum.ToString());
                    }
                    else
                    {
                        ToggleMessage("Error! List was not deleted.", "display:block", "alert alert-danger");
                    };
                    break;
            }
        }

        protected void grdList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        private void ToggleMessage(string strText, string strDisplay, string strClass)
        {
            dvMessage.Attributes.Add("class", strClass);
            dvMessage.Attributes.Add("style", strDisplay);
            dvMessage.InnerText = strText;
        }

        protected void lnkRunSearch_Click(object sender, EventArgs e)
        {
            Session["SearchType"] = null;
            Response.Redirect("~/Leads/SimplifiedSearch.aspx");
        }
    }
}