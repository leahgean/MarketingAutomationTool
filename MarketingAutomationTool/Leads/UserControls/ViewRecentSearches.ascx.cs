using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MarketingAutomationTool.Leads.UserControls
{
    public partial class ViewRecentSearches : System.Web.UI.UserControl
    {
        public string liViewRecentSearchesClass = string.Empty;
        public string aViewRecentSearchesClass = string.Empty;

        private System.Delegate _delPageMethod;
        public Delegate CallingPageMethod
        {
            set { _delPageMethod = value; }
        }

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

        private string LeadActiveList
        {
            get
            {
                if (ViewState["LeadActiveList"] != null)
                {
                    return ViewState["LeadActiveList"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["LeadActiveList"] = value;
            }
        }


        private void AssignActiveList()
        {
            if (Session["LeadActiveList"] != null)
            {
                LeadActiveList = Session["LeadActiveList"].ToString();
            }
            else
            {
                LeadActiveList = string.Empty;
            }
        }

        private void SetActiveList()
        {
            if (LeadActiveList == ((int)Utilities.ConstantValues.LeadActiveList.ViewRecentSearches).ToString())
            {
                liViewRecentSearchesClass = "dcjq-current-parent";
                aViewRecentSearchesClass = "dcjq-parent active";
            }
            else
            {
                liViewRecentSearchesClass = string.Empty;
                aViewRecentSearchesClass = string.Empty;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            userlogin = (DataModels.UserLogin)Session["loggeduser"];

            if (userlogin == null)
                Response.Redirect("~/Public/Login.aspx");


            AssignActiveList();
            SetActiveList();
            LoadRecentSearches();
        }

        public void LoadRecentSearches()
        {
            if  (ulRecentSearches.Controls.Count>0)
            {
                ulRecentSearches.Controls.Clear();
            }

            BusinessLayer.ContactSearch oContactSearch = new BusinessLayer.ContactSearch();
            DataTable dt = oContactSearch.ViewRecentSearches(userlogin.AccountID, userlogin.UserID);

            int iRowNumber = dt.Rows.Count;
            foreach (DataRow dr in dt.Rows)
            {
                LinkButton lnkRecentSearch = new LinkButton();
                lnkRecentSearch.ID = string.Format("lnkRecentSearch{0}", iRowNumber.ToString());
                lnkRecentSearch.Text = dr["Name"].ToString();
                lnkRecentSearch.CommandName = "SELECT";
                lnkRecentSearch.CommandArgument = string.Format("{0}|{1}", dr["SEARCHUID"].ToString(),dr["SearchType"].ToString());
                lnkRecentSearch.Click += LnkRecentSearch_Click;

                HtmlGenericControl li = new HtmlGenericControl("li");
                li.Controls.Add(lnkRecentSearch);

                ulRecentSearches.Controls.Add(li);
                iRowNumber--;
            }
        }

        private void LnkRecentSearch_Click(object sender, EventArgs e)
        {
            LinkButton lnkRecentSearch = (LinkButton)sender;
            string sCommandArgument = lnkRecentSearch.CommandArgument.Trim();
            string[] sCommandArgumentList = sCommandArgument.Split('|');

            string sSearchUID = sCommandArgumentList[0].ToString().Trim();
            string sSearchType = sCommandArgumentList[1].ToString().Trim();

            if (string.IsNullOrEmpty(sSearchType))
            {
                Session["SearchType"] = null;
            }
            else
            {
                Session["SearchType"] = sSearchType;
            }
            Session["LeadActiveList"] = ((int)Utilities.ConstantValues.LeadActiveList.ViewRecentSearches).ToString();
            Response.Redirect(string.Format("~/Leads/SimplifiedSearch.aspx?uid={0}", sSearchUID));
        }

        
    }
}