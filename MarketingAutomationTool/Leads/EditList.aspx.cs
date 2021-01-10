using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MarketingAutomationTool.Leads
{
    public partial class EditList : System.Web.UI.Page
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

        public int ContactListId
        {
            get
            {
                if (ViewState["ContactListId"] != null)
                {
                    return int.Parse(ViewState["ContactListId"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["ContactListId"] = value;
            }
        }

        public string ListName
        {
            get
            {
                if (ViewState["ListName"] != null)
                {
                    return ViewState["ListName"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["ListName"] = value;
            }
        }

        private List<string> RemoveList
        {
            get
            {
                if (ViewState["RemoveList"] != null)
                {
                    return (List<string>)ViewState["RemoveList"];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ViewState["RemoveList"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            userlogin = (DataModels.UserLogin)Session["loggeduser"];

            if (userlogin == null)
                Response.Redirect("~/Public/Login.aspx");

            UsersPagerMethod userpager = new UsersPagerMethod(Refresh);
            Pager.CallingPageMethod = userpager;

            UsersPagerMethod excludepager = new UsersPagerMethod(RefreshExcludeList);
            PagerExcluded.CallingPageMethod = excludepager;

            if (!Page.IsPostBack)
            {
                Pager.PageNum = 1;
                Pager.SortBy = "FirstName";

                PagerExcluded.PageNum = 1;
                PagerExcluded.SortBy = "FirstName";

                dvExcluded.Visible = false;
                SetContactListID();
                SetMessage();
                SetVisibleTab();
                PopulateFields();
                Refresh(Pager.PageNum.ToString());

            }
            else
            {
                lblEditList.InnerText = ListName;
                SetVisibleTab();
            }
        }

        public void RefreshExcludeList(string curpage)
        {
            int iMinItem, iMaxItem, iTotalRows = 0;

            int iPageNum = Convert.ToInt32(curpage);
            int iNewPageNum = iPageNum;
            decimal dMaxPages = 0;
            BindExcludeList(iPageNum, out iNewPageNum, out iMinItem, out iMaxItem, out iTotalRows, out dMaxPages);
            SetExcludeListPager(iNewPageNum, "FirstName", "ASC", iTotalRows, iMinItem, iMaxItem, dMaxPages);
        }

        private void BindExcludeList(int iPageNum, out int iNewPageNum, out int iMinItem, out int iMaxItem, out int iTotalRows, out decimal dMaxPages)
        {
            BusinessLayer.ContactList oContactList = new BusinessLayer.ContactList();
            string sRemoveList = GetRemoveList();
            DataTable dt = new DataTable();

            if (!string.IsNullOrEmpty(sRemoveList))
            {
                dt = oContactList.GetRemovedListMembers(ContactListId, userlogin.AccountID, userlogin.UserID, PagerExcluded.CurMaxRows, PagerExcluded.SortBy, PagerExcluded.SortDirection, sRemoveList, iPageNum, out iNewPageNum, out iMinItem, out iMaxItem, out iTotalRows, out dMaxPages);
            }
            else
            {
                iNewPageNum = 0;
                iMinItem = 0;
                iMaxItem = 0;
                iTotalRows = 0;
                dMaxPages = 0;

                dt = new DataTable();
                dt.Columns.Add("");
                dt.Columns.Add("ID");
                dt.Columns.Add("ContactID");
                dt.Columns.Add("AccountID");
                dt.Columns.Add("FirstName");
                dt.Columns.Add("LastName");
                dt.Columns.Add("MobileNumber");
                dt.Columns.Add("EmailAddress");
                dt.Columns.Add("FacebookID");
                dt.Columns.Add("IsDeleted");
                dt.Columns.Add("SubscribedToEmail");
                dt.Columns.Add("RowNum");
            }

            grdExcluded.DataSource = dt;
            grdExcluded.DataBind();
        }

        private void SetExcludeListPager(int curpage, string sortby, string sortdir, int totalrows, int minitem, int maxitem, decimal maxpages)
        {
            if (totalrows > 0)
            {
                PagerExcluded.Visible = true;
                PagerExcluded.PageNum = curpage;
                PagerExcluded.SortBy = sortby;
                PagerExcluded.SortDirection = sortdir;
                PagerExcluded.TotalRows = totalrows;
                PagerExcluded.MinItem = minitem;
                PagerExcluded.MaxItem = maxitem;
                PagerExcluded.TotalRows = totalrows;
                PagerExcluded.MaxPages = Convert.ToInt32(maxpages); //Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalrows) / Convert.ToDecimal(PagerExcluded.CurMaxRows)));
            }
            else
            {
                PagerExcluded.Visible = false;
            }
        }

        public void SetContactListID()
        {
            if (Session["ContactListId"] != null)
            {
                ContactListId = int.Parse(Session["ContactListId"].ToString());
            }
            else
            {
                ContactListId = 0;
            }
        }

        public void SetVisibleTab()
        {
            if (Session["ShowListMembers"] != null)
            {
                hdnShowListMembers.Value = Session["ShowListMembers"].ToString();
                Session["ShowListMembers"] = null;
            }
        }

        public void SetMessage()
        {
            if (Session["SuccessMessage"] != null)
            {
                ToggleMessage(Session["SuccessMessage"].ToString(), "display:block", "alert alert-success");
                Session["SuccessMessage"] = null;
            }
        }

        public void Refresh(string curpage)
        {
            int iMinItem, iMaxItem, iTotalRows = 0;
            int iPageNum = Convert.ToInt32(curpage);
            int iNewPageNum = iPageNum;
            decimal dMaxPages = 0;
            BindData(iPageNum, out iNewPageNum, out iMinItem, out iMaxItem, out iTotalRows, out dMaxPages);
            SetPager(iNewPageNum, "FirstName", "ASC", iTotalRows, iMinItem, iMaxItem, dMaxPages);
        }

        private void BindData(int iPageNum, out int iNewPageNum, out int iMinItem, out int iMaxItem, out int iTotalRows, out decimal dMaxPages)
        {
            iNewPageNum = 0;
            iMinItem = 0;
            iMaxItem = 0;
            iTotalRows = 0;
            dMaxPages = 0;
            BusinessLayer.ContactList oContactList = new BusinessLayer.ContactList();
            string sRemoveList = GetRemoveList();
            DataTable dt = new DataTable();
            if (ContactListId >0)
            {
                dt = oContactList.GetListMembers(ContactListId, userlogin.AccountID, userlogin.UserID, Pager.CurMaxRows, Pager.SortBy, Pager.SortDirection, sRemoveList, iPageNum, out iNewPageNum, out iMinItem, out iMaxItem, out iTotalRows, out dMaxPages);
            }
            else
            {
                iNewPageNum = 0;
                iMinItem = 0;
                iMaxItem = 0;
                iTotalRows = 0;
                dMaxPages = 0;

                dt = new DataTable();
                dt.Columns.Add("");
                dt.Columns.Add("ID");
                dt.Columns.Add("ContactID");
                dt.Columns.Add("AccountID");
                dt.Columns.Add("FirstName");
                dt.Columns.Add("LastName");
                dt.Columns.Add("MobileNumber");
                dt.Columns.Add("EmailAddress");
                dt.Columns.Add("FacebookID");
                dt.Columns.Add("IsDeleted");
                dt.Columns.Add("SubscribedToEmail");
                dt.Columns.Add("RowNum");
            }
                
            grdIncluded.DataSource = dt;
            grdIncluded.DataBind();
        }

        private string GetRemoveList()
        {
            string sRemoveList = string.Empty;
            if (RemoveList != null)
            {
                foreach (string sID in RemoveList)
                {
                    sRemoveList = string.Concat(sRemoveList, sID, ",");
                }
            }

            if (!string.IsNullOrEmpty(sRemoveList))
                sRemoveList = sRemoveList.Remove(sRemoveList.Length - 1);
            return sRemoveList;
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

        private void PopulateFields()
        {
            ContactList cList = new ContactList();
            DataModels.ContactListModel clModel = cList.SelectContactList(userlogin.AccountID, ContactListId);

            txtName.Text = clModel.ListName;
            txtDescription.Text = clModel.ListDescription;
            ListName = clModel.ListName;
            lblEditList.InnerText = ListName;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ContactList cList = new ContactList();

            if (cList.UniqueListName(txtName.Text.Trim(), userlogin.AccountID, ContactListId))
            {
                if (txtDescription.Text.Trim().Length<=500)
                {
                    if (cList.UpdateContactList(userlogin.AccountID, txtName.Text.Trim(), txtDescription.Text.Trim(), userlogin.UserID, ContactListId))
                    {
                        ToggleMessage("Success! List updated.", "display:block", "alert alert-success");
                    }
                    else
                    {
                        ToggleMessage("Error! List was not updated.", "display:block", "alert alert-danger");
                    }
                }
                else
                {
                    ToggleMessage("List Description is more than 500 characters.", "display:block", "alert alert-danger");
                }

            }
            else
            {
                ToggleMessage("Error! Name already exists.", "display:block", "alert alert-danger");
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            PopulateFields();
            ToggleMessage(string.Empty, "display:none", string.Empty);
        }

       

        private void ClearFields()
        {
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }

        private void ToggleMessage(string strText, string strDisplay, string strClass)
        {
            dvMessage.Attributes.Add("class", strClass);
            dvMessage.Attributes.Add("style", strDisplay);
            dvMessage.InnerText = strText;
        }

        protected void lnkBreadHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard.aspx");
        }

        protected void grdIncluded_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper().Trim())
            {
                case "SELECT":
                    Response.Redirect("EditLead.aspx?Guid=" + e.CommandArgument);
                    break;
                case "VIEWREPORT":
                    break;
                case "REMOVE":

                    string sID = e.CommandArgument.ToString();
                    int iID = 0;
                    if (int.TryParse(sID, out iID))
                    {
                        if (RemoveList == null) RemoveList = new List<string>();
                        RemoveList.Add(iID.ToString());
                    }

                    dvExcluded.Visible = true;
                    Refresh(Pager.PageNum.ToString());
                    RefreshExcludeList(PagerExcluded.PageNum.ToString());
                    hdnShowListMembers.Value = "1";//show members tab
                    break;
            }

        }

        protected void grdIncluded_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.DataItem as DataRowView).Row["IsDeleted"].ToString().ToUpper() == "FALSE")
                {
                    e.Row.Cells[7].FindControl("lnkActiveContact").Visible = true;
                    e.Row.Cells[7].FindControl("lnkDeletedContact").Visible = false;
                }
                else
                {
                    e.Row.Cells[7].FindControl("lnkActiveContact").Visible = false;
                    e.Row.Cells[7].FindControl("lnkDeletedContact").Visible = true;
                }
            }
        }

        protected void grdIncluded_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            

        }

        protected void btnSaveMembersChanges_Click(object sender, EventArgs e)
        {
            ContactList cl = new ContactList();
            string sRemoveList = GetRemoveList();
            if (!string.IsNullOrEmpty(sRemoveList))
            {
                if (cl.ModifyMemberList(ContactListId, userlogin.AccountID, userlogin.UserID, sRemoveList))
                {
                    ToggleMessage("Success! Leads removed from list.", "block", "alert alert-success");
                    RemoveList.Clear();
                    PagerExcluded.PageNum = 1;
                    Refresh(Pager.PageNum.ToString());
                    RefreshExcludeList(PagerExcluded.PageNum.ToString());
                    hdnShowListMembers.Value = "1";//show members tab
                }
                else
                {
                    ToggleMessage("Error! Lead not removed from list.", "block", "alert alert-danger");
                }
            }
            else
            {
                ToggleMessage("No changes to List Members to save.", "block", "alert alert-info");
            }
        }

        protected void grdExcluded_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper().Trim())
            {
                case "SELECT":
                    Response.Redirect("EditLead.aspx?Guid=" + e.CommandArgument);
                    break;
                case "ADD":

                    string sID = e.CommandArgument.ToString();
                    int iID = 0;
                    if (int.TryParse(sID, out iID))
                    {
                        if (RemoveList != null)
                        {
                            RemoveList.Remove(iID.ToString());
                        }

                    }
                    Refresh(Pager.PageNum.ToString());
                    RefreshExcludeList(PagerExcluded.PageNum.ToString());
                    hdnShowListMembers.Value = "1";//show members tab
                    break;
            }
        }
    }
}