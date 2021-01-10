using System;
using BusinessLayer;

namespace MarketingAutomationTool.Leads
{
    public partial class AddList : System.Web.UI.Page
    {
        public DataModels.UserLogin userlogin
        {
            get {
                if (ViewState["userlogin"] != null)
                {
                    return (DataModels.UserLogin)ViewState["userlogin"];
                }
                else
                {
                    return null;
                }
            }
            set {
                ViewState["userlogin"] = (DataModels.UserLogin)value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            userlogin = (DataModels.UserLogin)Session["loggeduser"];
            if (userlogin == null)
                Response.Redirect("~/Public/Login.aspx");

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ContactList cList = new ContactList();
            int iContactListId = 0;

            if (cList.UniqueListName(txtName.Text.Trim(), userlogin.AccountID, null))
            {
                if (txtDescription.Text.Trim().Length<=500)
                {
                    if (cList.CreateContactList(userlogin.AccountID, txtName.Text.Trim(), txtDescription.Text.Trim(), userlogin.UserID, out iContactListId))
                    {
                        ClearFields();
                        Session["ContactListId"] = iContactListId;
                        Session["SuccessMessage"] = "Success! New list created.";
                        Response.Redirect("EditList.aspx");
                    }
                    else
                    {
                        ToggleMessage("Error! New list was not created.", "display:block", "alert alert-danger");
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

        private void ClearFields()
        {
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        protected void lnkBreadHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Dashboard.aspx");
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
            Response.Redirect("/Leads/SimplifiedSearch.aspx");
        }
    }
}