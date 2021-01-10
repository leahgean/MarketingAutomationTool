using System;
using System.Web.UI;
using BusinessLayer;

namespace MarketingAutomationTool.MyAccount
{
    public partial class UserDetails : System.Web.UI.Page
    {
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

        public string SelectedUserId
        {
            get
            {
                if (ViewState["SelectedUserId"] != null)
                {
                    return ViewState["SelectedUserId"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["SelectedUserId"] = value;
            }
        }

        private void PopulateUserFields()
        {
            Guid gUserId;
            if (Guid.TryParse(SelectedUserId, out gUserId))
            {
                User usr = new User();
                DataModels.UserModel selecteduser = usr.GetUserDetailByUserId(gUserId);

                if (selecteduser != null)
                {
                    txtFirstName.Text = selecteduser.FirstName;
                    txtLastName.Text = selecteduser.LastName;
                    txtPosition.Text = selecteduser.Position;
                    txtEmailAddress.Text = selecteduser.EmailAddress;
                    txtMobile.Text = selecteduser.MobileNumber;
                    lblUserName.InnerText = selecteduser.UserName;

                    string UserEnabled = selecteduser.IsActive ? "true" : "false";
                    rdActive.Checked = selecteduser.IsActive;
                    rdInactive.Checked = !selecteduser.IsActive;



                    if (selecteduser.UserID == userlogin.UserID && selecteduser.AccountID == userlogin.AccountID)
                    {
                        rdInactive.Attributes.Add("disabled", "true");
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            userlogin = (DataModels.UserLogin)Session["loggeduser"];
            if (userlogin == null)
                Response.Redirect("~/Public/Login.aspx");

            if (Request.QueryString["userid"] != null)
                SelectedUserId = Request.QueryString["userid"].ToString();

            if (!Page.IsPostBack)
            {
                PopulateUserFields();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            User usr = new User();
            DataModels.UserModel selecteduser = usr.GetUserDetailByUserId(Guid.Parse(SelectedUserId));

            if (usr.UniqueEmail_Update(selecteduser.AccountID, txtEmailAddress.Text.Trim(), Guid.Parse(SelectedUserId)))
            {
                bool result = usr.SaveUserDetail(selecteduser.AccountID, Guid.Parse(SelectedUserId), userlogin.UserID, txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtPosition.Text.Trim(), txtEmailAddress.Text.Trim(), txtMobile.Text.Trim(), rdActive.Checked);
                
                if (result)
                {
                    ToggleMessage("Success! User details updated.", "display:block", "alert alert-success");
                }
                else
                {
                    ToggleMessage("Error! User details not updated.", "display:block", "alert alert-danger");
                }
            }
            else
            {
                ToggleMessage("Error! A user is already using this email address.", "display:block", "alert alert-danger");
            }

            
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            PopulateUserFields();
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