using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MarketingAutomationTool.Leads.UserControls
{
    public partial class LeadLists : System.Web.UI.UserControl
    {
        public string liLeadListsClass = string.Empty;
        public string aLeadListsClass = string.Empty;

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
            if (LeadActiveList == ((int)Utilities.ConstantValues.LeadActiveList.Lists).ToString())
            {
                liLeadListsClass = "dcjq-current-parent";
                aLeadListsClass = "dcjq-parent active";
            }
            else
            {
                liLeadListsClass = string.Empty;
                aLeadListsClass = string.Empty;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AssignActiveList();
            SetActiveList();
            ToggleAddMember();
        }

        private void ToggleAddMember()
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;
            string[] pathArr = path.Split('/');
            string page = pathArr[pathArr.Length - 1].Trim().ToUpper();

            if (page == "EDITLIST")
            {
                liAddMember.Visible = true;
            }
            else
            {
                liAddMember.Visible = false;
            }
        }

        protected void lnkAddMember_Click(object sender, EventArgs e)
        {
            Session["LeadActiveList"] = ((int)Utilities.ConstantValues.LeadActiveList.Lists).ToString();
            Session["SearchType"] = "ADDMEMBER";
            Response.Redirect("~/Leads/SimplifiedSearch.aspx");
        }

        protected void lnkCreateNewList_Click(object sender, EventArgs e)
        {
            Session["LeadActiveList"] = ((int)Utilities.ConstantValues.LeadActiveList.Lists).ToString();
            Response.Redirect("~/Leads/AddList.aspx");
        }

        protected void lnkBrowseList_Click(object sender, EventArgs e)
        {
            Session["LeadActiveList"] = ((int)Utilities.ConstantValues.LeadActiveList.Lists).ToString();
            Response.Redirect("~/Leads/BrowseList.aspx");
        }
    }
}