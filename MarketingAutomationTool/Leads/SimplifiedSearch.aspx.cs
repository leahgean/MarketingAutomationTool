using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DataModels;
using System.Web.Script.Serialization;
using BusinessLayer;
using System.Data;
using System.Configuration;
using MarketingAutomationTool.Utilities;
using System.IO;

namespace MarketingAutomationTool.Leads
{
    public partial class SimplifiedSearch : System.Web.UI.Page
    {
        delegate void UsersPagerMethod(string curPage);
        delegate void ViewRecentSearchesMethod();

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

        private string SearchID
        {
            get
            {
                if (ViewState["SearchID"] != null)
                {
                    return ViewState["SearchID"].ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ViewState["SearchID"] = value;
            }
        }

        private string SearchType
        {
            get
            {
                if (ViewState["SearchType"] != null)
                {
                    return ViewState["SearchType"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["SearchType"] = value;
            }
        }

        private List<string> ExcludeList
        {
            get
            {
                if (ViewState["ExcludeList"] != null)
                {
                    return (List<string>)ViewState["ExcludeList"];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ViewState["ExcludeList"] = value;
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


        protected void Page_Load(object sender, EventArgs e)
        {
            userlogin = (DataModels.UserLogin)Session["loggeduser"];

            if (userlogin == null)
                Response.Redirect("~/Public/Login.aspx");

            UsersPagerMethod userpager = new UsersPagerMethod(Refresh);
            Pager.CallingPageMethod = userpager;

            ViewRecentSearchesMethod viewrecentsearches = new ViewRecentSearchesMethod(RefreshRecentSearches);
            ViewRecentSearches.CallingPageMethod = viewrecentsearches;

            UsersPagerMethod excludepager = new UsersPagerMethod(RefreshExcludeList);
            PagerExcluded.CallingPageMethod = excludepager;

            SetPageBasedOnSearchType();

            if (!Page.IsPostBack)
            {
                PopulateCountryList();
                PopulateContactListLists();
                Pager.PageNum = 1;
                Pager.SortBy = "FirstName";
                Pager.SortDirection = "ASC";
                LoadExistingSearchCriteria();

                if (SearchType=="ADDMEMBER")
                {
                    PagerExcluded.PageNum = 1;
                    PagerExcluded.SortBy = "FirstName";
                    PagerExcluded.SortDirection = "ASC";
                    RefreshExcludeList(PagerExcluded.PageNum.ToString());
                    AssignContactListID();
                }
            }
        }

        private void SetPageBasedOnSearchType()
        {
            if (ViewState["SearchType"]==null)
            {
                if (Session["SearchType"] != null)
                {
                    SearchType = Session["SearchType"].ToString();
                }
            }

            if (string.IsNullOrEmpty(SearchType))
            {
                lblPageTitle.Text = "Search";
                dvExcluded.Visible = false;
                dvIncludedLabel.Visible = false;
                grdIncluded.Columns[0].Visible = false;//Exclude
                grdIncluded.Columns[7].Visible = true;//Delete

                dvAddMembers.Visible = false;
                dvExport.Visible = true;
            }
            else if (SearchType == "ADDMEMBER")
            {
                lblPageTitle.Text = "Add Member To List";
                dvExcluded.Visible = true;
                dvIncludedLabel.Visible = false; //set to true in Refresh if there are results 
                grdIncluded.Columns[0].Visible = true;//Include
                grdIncluded.Columns[7].Visible = false;//Delete

                dvAddMembers.Visible = true;
                dvExport.Visible = false;
            }
            else if (SearchType == "EXPORT")
            {
                lblPageTitle.Text = "Export";
                dvExcluded.Visible = false;
                dvIncludedLabel.Visible = false;
                grdIncluded.Columns[0].Visible = false;//Exclude
                grdIncluded.Columns[7].Visible = true;//Delete

                dvAddMembers.Visible = false;
                dvExport.Visible = true;
            }
            else if (SearchType == "VIEWALL")
            {
                lblPageTitle.Text = "View All";
                dvExcluded.Visible = false;
                dvIncludedLabel.Visible = false;
                grdIncluded.Columns[0].Visible = false;//Exclude
                grdIncluded.Columns[7].Visible = true;//Delete

                dvAddMembers.Visible = false;
                dvExport.Visible = true;
                hdnActiveTab.Value = "RESULTS";

                BusinessLayer.ContactSearch cs = new BusinessLayer.ContactSearch();
                SearchID = cs.GetSearchID_BySearchType(userlogin.UserID, userlogin.AccountID, SearchType);

                if (string.IsNullOrEmpty(SearchID))
                {
                    int iSearchId = 0;
                    Guid SearchUID = Guid.NewGuid();
                    List<SearchFieldsItems> lstsearchfieldsitems = new List<SearchFieldsItems>();
                    string searchfields = "{'searchfieldsitem':[]}";
                    lstsearchfieldsitems = GetSearchFieldsItems(searchfields);
                    if (cs.AddContactSearch(userlogin.AccountID, userlogin.UserID, searchfields, lstsearchfieldsitems, SearchType, out iSearchId, out SearchUID))
                    {
                        SearchID = iSearchId.ToString();
                    };
                }

            }
        }

        public void AssignContactListID()
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

        public void LoadExistingSearchCriteria()
        {
            if (Request["uid"] != null)
            {
                Guid gSearchGuid = new Guid();
                if (Guid.TryParse(Request["uid"].ToString(), out gSearchGuid))
                {
                    BusinessLayer.ContactSearch cs = new BusinessLayer.ContactSearch();
                    SearchID = cs.GetSearchID(gSearchGuid, userlogin.AccountID, userlogin.UserID);
                }
            }

            if ((Request["uid"] != null) && string.IsNullOrEmpty(SearchID))
            {
                ToggleMessage("Recent search has been deleted. Please create a new search.", "block", "alert alert-danger");
            }
            else if (((Request["uid"] != null) && (!string.IsNullOrEmpty(SearchID)))||
                    ((SearchType == "VIEWALL") && (!string.IsNullOrEmpty(SearchID))))
            {
                //Load Search
                int iSearchId = 0;
                if (int.TryParse(SearchID, out iSearchId))
                {
                    string sSearchCriteria = string.Empty;
                    string sJsonString = string.Empty;
                    BusinessLayer.ContactSearch oContactSearch = new BusinessLayer.ContactSearch();
                    sJsonString=oContactSearch.GetSearchJsonString(iSearchId,userlogin.AccountID, userlogin.UserID);
                    hdnSearchFields.Value = sJsonString.Trim();
                    hdnActiveTab.Value = "RESULTS";//show search results tab

                    sSearchCriteria = oContactSearch.GetSearchCriteriaDisplay(iSearchId, userlogin.AccountID, userlogin.UserID);
                    ToggleSearchCriteria(string.Format("Search Criteria: {0}", sSearchCriteria), "block", "alert alert-success searchcriteria");

                    if (SearchType=="VIEWALL")
                    {
                        Pager.SortBy = "CreatedDate";
                        Pager.SortDirection = "DESC";
                    }
                    Refresh(Pager.PageNum.ToString());
                }
            }
            else
            {
                Refresh(Pager.PageNum.ToString());
            }

           
        }

        public void RefreshRecentSearches()
        {
            ViewRecentSearches.LoadRecentSearches();
        }

        public void RefreshExcludeList(string curpage)
        {
            int iMinItem, iMaxItem, iTotalRows = 0;
            int iSearchId = 0;


            if (!string.IsNullOrEmpty(SearchID))
            {
                if (int.TryParse(SearchID.ToString(), out iSearchId))
                {
                    int iPageNum = Convert.ToInt32(curpage);
                    int iNewPageNum = iPageNum;
                    decimal dMaxPages = 0;
                    BindExcludeList(iSearchId, iPageNum, out iNewPageNum, out iMinItem, out iMaxItem, out iTotalRows, out dMaxPages);
                    SetExcludeListPager(iNewPageNum, Pager.SortBy, Pager.SortDirection, iTotalRows, iMinItem, iMaxItem,dMaxPages);
                }
            }
        }

        public void Refresh(string curpage)
        {
            int iMinItem, iMaxItem, iTotalRows = 0;
            int iSearchId = 0;


            if (!string.IsNullOrEmpty(SearchID))
            {
                if (int.TryParse(SearchID.ToString(), out iSearchId))
                {
                    int iPageNum = Convert.ToInt32(curpage);
                    int iNewPageNum = iPageNum;
                    decimal dMaxPages = 0;
                    BindData(iSearchId, iPageNum, out iNewPageNum, out iMinItem, out iMaxItem, out iTotalRows, out dMaxPages);
                    SetPager(iNewPageNum, Pager.SortBy, Pager.SortDirection, iTotalRows, iMinItem, iMaxItem, dMaxPages);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(SearchType))
                {
                    dvSearchCriteria.Visible = true;
                    dvSearchCriteria.InnerHtml = "Please enter search criteria and run a search to view results.";
                    Pager.Visible = false;
                }
                else if (SearchType== "ADDMEMBER")
                {
                    dvSearchCriteria.Visible = true;
                    dvSearchCriteria.InnerHtml = "Please run a search and select members to add to list.";
                    Pager.Visible = false;

                    dvIncludedLabel.Visible = false;
                    dvExcludedLabel.Visible = false;
                    PagerExcluded.Visible = false;
                }
                else if (SearchType == "EXPORT")
                    {
                    dvSearchCriteria.Visible = true;
                    dvSearchCriteria.InnerHtml = "Please run a search end export results.";
                    Pager.Visible = false;
                }

            }
        }

        private string GetExcludeList()
        {
            string sExcludeList = string.Empty;
            if (ExcludeList != null)
            {
                foreach (string sID in ExcludeList)
                {
                    sExcludeList = string.Concat(sExcludeList, sID, ",");
                }
            }

            if (!string.IsNullOrEmpty(sExcludeList))
                sExcludeList = sExcludeList.Remove(sExcludeList.Length - 1);
            return sExcludeList;
        }

        private void BindData(int iSearchId, int iPageNum, out int iNewPageNum, out int iMinItem, out int iMaxItem, out int iTotalRows, out decimal dMaxPages)
        {
            if (SearchType == "ADDMEMBER")
            {
                dvIncludedLabel.Visible = true;
            }

            BusinessLayer.ContactSearch oContactSearch = new BusinessLayer.ContactSearch();
            string sExcludeList = GetExcludeList();
            DataTable dt = oContactSearch.GetSearchResult(iSearchId, userlogin.AccountID, userlogin.UserID,  Pager.CurMaxRows, Pager.SortBy, Pager.SortDirection, sExcludeList, iPageNum, out iNewPageNum, out iMinItem, out iMaxItem, out iTotalRows, out dMaxPages);
            grdIncluded.DataSource = dt;
            grdIncluded.DataBind();

            
        }

        private void BindExcludeList(int SearchId, int iPageNum, out int iNewPageNum, out int iMinItem, out int iMaxItem, out int iTotalRows, out decimal dMaxPages)
        {
            
            dvExcludedLabel.Visible = true;
            
            BusinessLayer.ContactSearch oContactSearch = new BusinessLayer.ContactSearch();
            string sExcludeList = GetExcludeList();
            DataTable dt = new DataTable();
            
            if (!string.IsNullOrEmpty(sExcludeList))
            {
                dt = oContactSearch.GetExcludeList(SearchId, userlogin.AccountID, userlogin.UserID, PagerExcluded.CurMaxRows, PagerExcluded.SortBy, PagerExcluded.SortDirection, sExcludeList, iPageNum,out iNewPageNum, out iMinItem, out iMaxItem, out iTotalRows, out dMaxPages);
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
                dt.Columns.Add("MiddleName");
                dt.Columns.Add("LastName");
                dt.Columns.Add("MobileNumber");
                dt.Columns.Add("EmailAddress");
                dt.Columns.Add("FacebookID");
                dt.Columns.Add("IsDeleted");
                dt.Columns.Add("IsDeletedNum");
                dt.Columns.Add("RowNum");
            }
                
            grdExcluded.DataSource = dt;
            grdExcluded.DataBind();
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

        private void PopulateCountryList()
        {
            //GetCountries
            Country country = new Country();
            DataTable dtCountries = country.GetCountries();

            var json = new CountryList();
            List<CountryOptions> co_options = new List<CountryOptions>();
            foreach (DataRow dr in dtCountries.Rows)
            {
                CountryOptions co = new CountryOptions { Value = dr["CountryId"].ToString(), Text = dr["CountryName"].ToString() };
                co_options.Add(co);
            };
            json = new CountryList { countrylist = co_options };
            string jsonString = JsonConvert.SerializeObject(json, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented });
            hdnCountryList.Value = jsonString;
        }


        private void PopulateContactListLists()
        {
            //GetCountries
            ContactList cList = new ContactList();
            DataTable dtContactListNames = cList.GetListNames(userlogin.AccountID,false);

            var json = new DataModels.ContactListList();
            List<ContactListOptions> cl_options = new List<ContactListOptions>();
            foreach (DataRow dr in dtContactListNames.Rows)
            {
                ContactListOptions cl = new ContactListOptions { Value = dr["ID"].ToString(), Text = dr["ListName"].ToString() };
                cl_options.Add(cl);
            };


            ContactListOptions clo = new ContactListOptions();
            clo.Text = "Not In Lists";
            clo.Value = "-1";
            cl_options.Insert(0, clo);

            json = new ContactListList { contactlistlist = cl_options };
            string jsonString = JsonConvert.SerializeObject(json, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented });
            hdnContactListList.Value = jsonString;
        }

        protected void lnkLeads_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Leads/Lead.aspx");
        }

        protected void lnkBreadHome_Click(object sender, EventArgs e)
        {
            //Session["maintab"] = "HOME";
            Response.Redirect("~/Dashboard.aspx");
        }


        private List<SearchFieldsItems> GetSearchFieldsItems(string searchfields)
        {
            List<SearchFieldsItems> lstsearchfieldsitems = new List<SearchFieldsItems>();
            //Create json string
            JObject searchfieldsitems = JObject.Parse(searchfields);
            JArray searchfieldsitem = (JArray)searchfieldsitems["searchfieldsitem"];

            for (int i = 0; i < searchfieldsitem.Count(); i++)
            {
                string sitem = searchfieldsitem[i].ToString();
                SearchFieldsItems searchkeyvalues = new JavaScriptSerializer().Deserialize<SearchFieldsItems>(sitem);

                lstsearchfieldsitems.Add(searchkeyvalues);
            }

            return lstsearchfieldsitems;
        }

        private void CreateASearch()
        {
            Pager.PageNum = 1;
            ToggleMessage(string.Empty, "none", string.Empty);

            if (SearchType == "VIEWALL")
            {
                Session["SearchType"] = null;
                SearchType = null;//reset search type
                SearchID = string.Empty;//reset search id
                lblPageTitle.Text = "Search";
            }

            if (SearchType == "ADDMEMBER")
            {
                PagerExcluded.PageNum = 1;
                ExcludeList = new List<string>();
            }

            List<SearchFieldsItems> lstsearchfieldsitems = new List<SearchFieldsItems>();

            string searchfields = hdnSearchFields.Value.Trim();
            if (!(string.IsNullOrEmpty(searchfields)))
            {
                lstsearchfieldsitems=GetSearchFieldsItems(searchfields);

                int iSearchId = 0;
                Guid SearchUID = Guid.NewGuid();

                //Save Search Criteria
                //Do the search and populate search results
                BusinessLayer.ContactSearch oContactSearch = new BusinessLayer.ContactSearch();
                string sSearchCriteria = string.Empty;
                if (string.IsNullOrEmpty(SearchID))
                {
                    if (oContactSearch.AddContactSearch(userlogin.AccountID, userlogin.UserID, hdnSearchFields.Value.Trim(), lstsearchfieldsitems, SearchType, out iSearchId, out SearchUID))
                    {
                        SearchID = iSearchId.ToString();

                        sSearchCriteria = oContactSearch.GetSearchCriteriaDisplay(int.Parse(SearchID), userlogin.AccountID, userlogin.UserID);
                        ToggleSearchCriteria(string.Format("Search Criteria: {0}", sSearchCriteria), "block", "alert alert-success searchcriteria");
                        Refresh(Pager.PageNum.ToString());
                        RefreshExcludeList(PagerExcluded.PageNum.ToString());
                    }
                    else
                    {
                        ToggleMessage("An error in search occurred. Please try again or contact System Administrator.", "block", "alert alert-danger");
                    }
                }
                else
                {
                    if (oContactSearch.UpdateContactSearch(userlogin.AccountID, userlogin.UserID, hdnSearchFields.Value.Trim(), lstsearchfieldsitems, int.Parse(SearchID)))
                    {
                        sSearchCriteria = oContactSearch.GetSearchCriteriaDisplay(int.Parse(SearchID), userlogin.AccountID, userlogin.UserID);
                        ToggleSearchCriteria(string.Format("Search Criteria: {0}", sSearchCriteria), "block", "alert alert-success searchcriteria");
                        Refresh(Pager.PageNum.ToString());
                        RefreshExcludeList(PagerExcluded.PageNum.ToString());
                    }
                    else
                    {
                        ToggleMessage("An error in search occurred. Please try again or contact System Administrator.", "block", "alert alert-danger");
                    }
                }
                RefreshRecentSearches();
                //show active tab
                hdnActiveTab.Value = "RESULTS";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CreateASearch();
        }

        protected void grdIncluded_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void grdIncluded_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch(e.CommandName.ToUpper().Trim())
            {
                case "SELECT":
                    Response.Redirect("EditLead.aspx?Guid=" + e.CommandArgument);
                    break;
                case "DELETE":
                    BusinessLayer.Lead cLead = new BusinessLayer.Lead();
                    BusinessLayer.ContactSearch oContactSearch = new BusinessLayer.ContactSearch();
                    Guid gContactID;
                    if (Guid.TryParse(e.CommandArgument.ToString(), out gContactID))
                    {
                        string sSearchCriteria = string.Empty;

                        DataModels.Lead objLead = new DataModels.Lead();
                        objLead=cLead.SelectLead(gContactID, userlogin.AccountID);

                        if (cLead.DeleteLead(userlogin.AccountID, gContactID, userlogin.UserID,false,objLead.EmailAddress, objLead.CountryId, objLead.City, objLead.State, Request.UserHostAddress))
                        {
                            sSearchCriteria = oContactSearch.GetSearchCriteriaDisplay(int.Parse(SearchID), userlogin.AccountID, userlogin.UserID);
                            ToggleSearchCriteria(string.Format("Search Criteria: {0}", sSearchCriteria), "block", "alert alert-success searchcriteria");
                            Refresh(Pager.PageNum.ToString());
                            ToggleMessage("Lead was successfully deleted.", "block", "alert alert-success");
                        }
                        else
                        {
                            sSearchCriteria = oContactSearch.GetSearchCriteriaDisplay(int.Parse(SearchID), userlogin.AccountID, userlogin.UserID);
                            ToggleSearchCriteria(string.Format("Search Criteria: {0}", sSearchCriteria), "block", "alert alert-success searchcriteria");
                            Refresh(Pager.PageNum.ToString());
                            ToggleMessage("An error occurred deleting lead. Please try again or contact System Administrator.", "block", "alert alert-danger");
                        }
                    }
                    break;
                case "VIEWREPORT":
                    break;
                case "EXCLUDE":
                    string sID = e.CommandArgument.ToString();
                    int iID = 0;
                    if (int.TryParse(sID, out iID))
                    {
                        if (ExcludeList == null) ExcludeList = new List<string>();
                        ExcludeList.Add(iID.ToString());
                    }
                    Refresh(Pager.PageNum.ToString());
                    RefreshExcludeList(PagerExcluded.PageNum.ToString());
                    hdnActiveTab.Value = "RESULTS";//show search results tab
                    break;
            }
        }

        protected void grdIncluded_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.DataItem as DataRowView).Row["IsDeleted"].ToString().Trim().ToUpper() == "FALSE")
                {
                    e.Row.Cells[7].FindControl("lnkDelete").Visible = true;
                    e.Row.Cells[7].FindControl("lnkNoDelete").Visible = false;
                }
                else
                {
                    e.Row.Cells[7].FindControl("lnkDelete").Visible = false;
                    e.Row.Cells[7].FindControl("lnkNoDelete").Visible = true;
                }
            }
        }

        private void ToggleSearchCriteria(string strText, string strDisplay, string strClass)
        {
            dvSearchCriteria.Attributes.Add("class", strClass);
            dvSearchCriteria.Attributes.Add("style", strDisplay);
            dvSearchCriteria.InnerText = strText;
        }

        private void ToggleMessage(string strText, string strDisplay, string strClass, ConstantValues.MessageBodyFormat eFormat=ConstantValues.MessageBodyFormat.Text)
        {
            dvMessage.Attributes.Add("class", strClass);
            dvMessage.Attributes.Add("style", strDisplay);
            if (eFormat== ConstantValues.MessageBodyFormat.HTML)
            {
                dvMessage.InnerHtml = strText;
            }
            else
            {
                dvMessage.InnerText = strText;
            }
        }

        protected void btnAddMembers_Click(object sender, EventArgs e)
        {
                if (!string.IsNullOrEmpty(SearchID))
                {
                    if (Pager.TotalRows == 0)
                    {
                        ToggleMessage("No members to add. Search has no results.", "block", "alert alert-info");
                    }
                    else
                    {
                        ContactList cl = new ContactList();
                        string sExcludeList = GetExcludeList();
                        if (cl.AddMemberToList(int.Parse(SearchID), userlogin.AccountID, userlogin.UserID, sExcludeList, ContactListId))
                        {
                            Session["ContactListId"] = ContactListId;
                            Session["ShowListMembers"] = 1;
                            Session["SuccessMessage"] = "Success! List Members added.";
                            Response.Redirect("EditList.aspx");
                        }
                        else
                        {
                            ToggleMessage("Error adding members to list.", "block", "alert alert-danger");
                        }                    }
                }
                else
                {
                    ToggleMessage("No members to add. Please run a search to add members to list.", "block", "alert alert-info");
                }
        }

        protected void grdExcluded_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper().Trim())
            {
                case "SELECT":
                    Response.Redirect("EditLead.aspx?Guid=" + e.CommandArgument);
                    break;
                case "INCLUDE":

                    string sID = e.CommandArgument.ToString();
                    int iID = 0;
                    if (int.TryParse(sID, out iID))
                    {
                        if (ExcludeList != null)
                        {
                            ExcludeList.Remove(iID.ToString());
                        }
                    }
                    Refresh(Pager.PageNum.ToString());
                    RefreshExcludeList(PagerExcluded.PageNum.ToString());
                    hdnActiveTab.Value = "RESULTS";//show search results tab
                    break;
            }
        }

        private DataTable GetDataToExport()
        {
            DataTable dt = new DataTable();
            try
            {
                BusinessLayer.ContactSearch oContactSearch = new BusinessLayer.ContactSearch();
                int iPageNum = 1;
                int iNewPageNum = 1;
                int iMinItem = 1;
                int iMaxItem = Pager.TotalRows;
                int iTotalRows = Pager.TotalRows;
                decimal dMaxPages = Pager.MaxPages;
                string sExcludeList = GetExcludeList();
                dt = oContactSearch.GetSearchResult(int.Parse(SearchID), userlogin.AccountID, userlogin.UserID, Pager.TotalRows, Pager.SortBy, Pager.SortDirection, sExcludeList, iPageNum, out iNewPageNum, out iMinItem, out iMaxItem, out iTotalRows, out dMaxPages);
                return dt;
            }
            catch(Exception ex)
            {
                dt = null;
                return dt;
            }
        }

       

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string sLeadsExportFolder = ConfigurationManager.AppSettings["LeadsExportFolder"].ToString().Trim();
            string sLeadsExportFileFormat = ConfigurationManager.AppSettings["LeadsExportFileName"].ToString().Trim();
            string sLeadsExportFileTimeStamp = string.Format("{0}{1}{2}{3}{4}{5}", System.DateTime.UtcNow.AddHours(8).Year.ToString(), System.DateTime.UtcNow.AddHours(8).Month.ToString(), System.DateTime.UtcNow.AddHours(8).Day.ToString(), System.DateTime.UtcNow.AddHours(8).Hour.ToString(), System.DateTime.UtcNow.AddHours(8).Minute.ToString(), System.DateTime.UtcNow.AddHours(8).Second.ToString());
            string sLeadsExportFileName = string.Format(sLeadsExportFileFormat, SearchID, sLeadsExportFileTimeStamp);
            string sLeadsExportFolderPath = HttpContext.Current.Server.MapPath(string.Format("~/{0}", sLeadsExportFolder));
            string sLeadsExportFilePath = HttpContext.Current.Server.MapPath(string.Format("~/{0}/{1}", sLeadsExportFolder, sLeadsExportFileName));

            if (!System.IO.Directory.Exists(sLeadsExportFolderPath))
            {
                System.IO.Directory.CreateDirectory(sLeadsExportFolderPath);
            }


            if (!string.IsNullOrEmpty(SearchID))
            {
                DataTable dt = GetDataToExport();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        XMLExcelUtility eu = new XMLExcelUtility();
                        if (eu.WriteDataTableToExcel(userlogin.UserID, dt, sLeadsExportFolderPath, sLeadsExportFilePath))
                        {
                            BusinessLayer.ContactSearchExport cSE = new BusinessLayer.ContactSearchExport();
                            if (cSE.AddContactSearchExportFiles(int.Parse(SearchID.ToString()), userlogin.AccountID, userlogin.UserID, sLeadsExportFileTimeStamp, sLeadsExportFileName))
                            {
                                ToggleMessage(string.Format("Success! Please click <a href='{0}'>here</a> to download export file.", string.Format("../HttpHandler/Download.ashx?file=exportfile&id={0}&acct={1}&ts={2}", SearchID, userlogin.AccountID.ToString(), sLeadsExportFileTimeStamp)), "block", "alert alert-success", ConstantValues.MessageBodyFormat.HTML);
                            }
                            else
                            {
                                ToggleMessage(string.Format("Error! FileName not saved. Please click <a href='{0}'>here</a> to download export file.", string.Format("../HttpHandler/Download.ashx?file=exportfile&id={0}&acct={1}&ts={2}", SearchID, userlogin.AccountID.ToString(), sLeadsExportFileTimeStamp)), "block", "alert alert-danger", ConstantValues.MessageBodyFormat.HTML);
                            }
                        }
                        else
                        {
                            ToggleMessage("Failed! Export file not created. Please contact system administrator.", "block", "alert alert-danger");
                        }
                        
                    }
                    else
                    {
                        ToggleMessage("No data to export. Search has no results.", "block", "alert alert-info");
                    }

                }
                else
                {
                    ToggleMessage("Failed! Export file not created. Please contact system administrator.", "block", "alert alert-danger");
                }
            }
            else
            {
                ToggleMessage("No data to export. Please run a search and then export the results.", "block", "alert alert-info");
            }
        }
    }
}