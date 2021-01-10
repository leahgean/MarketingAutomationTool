using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MarketingAutomationTool.Leads.UserControls
{
    public partial class Shortcuts : System.Web.UI.UserControl
    {
        public string liShortcutsClass = string.Empty;
        public string aShortcutsClass = string.Empty;

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

       

        protected void Page_Load(object sender, EventArgs e)
        {
            AssignActiveList();
            SetActiveList();
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
            if (LeadActiveList == ((int)Utilities.ConstantValues.LeadActiveList.Shortcuts).ToString())
            {
                liShortcutsClass = "dcjq-current-parent";
                aShortcutsClass = "dcjq-parent active";
            }
            else
            {
                liShortcutsClass = string.Empty;
                aShortcutsClass = string.Empty;
            }
        }

        protected void lnkRunSearch_Click(object sender, EventArgs e)
        {
            Session["SearchType"] = null;
            Session["LeadActiveList"] = ((int)Utilities.ConstantValues.LeadActiveList.Shortcuts).ToString();
            Response.Redirect("~/Leads/SimplifiedSearch.aspx");
        }

        protected void lnkAddNewLead_Click(object sender, EventArgs e)
        {
            Session["LeadActiveList"] = ((int)Utilities.ConstantValues.LeadActiveList.Shortcuts).ToString();
            Response.Redirect("~/Leads/AddLead.aspx"); 
        }
    }
}