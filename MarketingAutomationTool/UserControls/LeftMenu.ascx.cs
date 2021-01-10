using System;
using System.Web.UI;

namespace MarketingAutomationTool.UserControls
{
    public partial class LeftMenu : System.Web.UI.UserControl
    {
        public string liAccountClass = string.Empty;
        public string lnkAccountClass = string.Empty;
        public string liManageAccountClass = string.Empty;
        public string lnkManageAccountClass = string.Empty;
        public string liManageUsersClass = string.Empty;
        public string lnkManageUsersClass = string.Empty;
        public string liCurrentUserClass = string.Empty;
        public string lnkCurrentUserClass = string.Empty;

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

      

        public string LoggedUserID
        {
            get
            {
                if (userlogin != null)
                {
                    return userlogin.UserID.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }



        public void  ToggleManageAccount()
        {
            liManageAccounts.Visible = userlogin.IsSuperAdminUser;
        }


        public void ToggleManageUsers()
        {
            liManageUsers.Visible = (userlogin.IsOwner && userlogin.IsAdmin) || userlogin.IsSuperAdminUser;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            userlogin = (DataModels.UserLogin)Session["loggeduser"];
            if (userlogin == null)
                Response.Redirect("~/Public/Login.aspx");

            ToggleManageAccount();
            ToggleManageUsers();
            SetActiveMenu();

            if (!Page.IsPostBack)
            {
               
            }


        }

        private void SetActiveMenu()
        {
            string opencategory = string.Empty;
            if (Request.QueryString["c"] != null)
                opencategory = Request.QueryString["c"].ToString();

            switch (opencategory)
            {
                case "2":
                    if (userlogin.IsSuperAdminUser)
                    {
                        SetManageAccountActive();
                    }
                    break;
                case "3":
                    SetManagerUsersActive();
                    break;
                case "4":
                    SetCurrentUserActive();
                    break;
                default:
                    SetMyAccountActive();
                    break;

            }
        }

        private void SetMyAccountActive()
        {
            liAccountClass = "dcjq-current-parent";
            lnkAccountClass = "dcjq-parent active";
            liManageAccountClass = string.Empty;
            lnkManageAccountClass = string.Empty;
            liManageUsersClass = string.Empty;
            lnkManageUsersClass = string.Empty;
            liCurrentUserClass = string.Empty;
            lnkCurrentUserClass = string.Empty;

        }

        private void SetManageAccountActive()
        {
            liAccountClass = string.Empty;
            lnkAccountClass = string.Empty;
            liManageAccountClass = "dcjq-current-parent";
            lnkManageAccountClass = "dcjq-parent active";
            liManageUsersClass = string.Empty;
            lnkManageUsersClass = string.Empty;
            liCurrentUserClass = string.Empty;
            lnkCurrentUserClass = string.Empty;
        }

        private void SetCurrentUserActive()
        {
            liAccountClass = string.Empty;
            lnkAccountClass = string.Empty;
            liManageAccountClass = string.Empty;
            lnkManageAccountClass = string.Empty;
            liManageUsersClass = string.Empty;
            lnkManageUsersClass = string.Empty;
            liCurrentUserClass = "dcjq-current-parent";
            lnkCurrentUserClass = "dcjq-parent active";
        }


        private void SetManagerUsersActive()
        {
            liAccountClass = string.Empty;
            lnkAccountClass = string.Empty;
            liManageAccountClass = string.Empty;
            lnkManageAccountClass = string.Empty;
            liManageUsersClass = "dcjq-current-parent";
            lnkManageUsersClass = "dcjq-parent active";
            liCurrentUserClass = string.Empty;
            lnkCurrentUserClass = string.Empty;
        }


    }
}