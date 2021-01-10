using System;
using System.Web.UI;
using BusinessLayer;
using DataModels;
using System.Data;
using System.Web.UI.WebControls;

namespace MarketingAutomationTool.MyAccount
{
    public partial class AccountDetails : System.Web.UI.Page
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


        

        public string SelectedAccountid
        {       
            get
            {
                if (Request.QueryString["AccountId"] != null)
                {
                    ViewState["SelectedAccountid"] = Request.QueryString["AccountId"].ToString();
                }
                else
                {
                    ViewState["SelectedAccountid"] = string.Empty;
                }

                return ViewState["SelectedAccountid"].ToString();
            }
            set
            {
               
                    ViewState["SelectedAccountid"]= value;
                
            }

        }


       


        protected void Page_Load(object sender, EventArgs e)
        {
            userlogin = (DataModels.UserLogin)Session["loggeduser"];

            if (userlogin == null)
                Response.Redirect("~/Public/Login.aspx");

            UsersPagerMethod userpager = new UsersPagerMethod(RefreshUsers);
            Pager.CallingPageMethod = userpager;

            if (!Page.IsPostBack)
            {
                PopulateAccountInformation();
            }
        }


        private void PopulateAccountInformation()
        {
            Guid gAccountID;
            if (Guid.TryParse(SelectedAccountid, out gAccountID))
            {
                PopulateAccountDetailsFields(gAccountID);
                PopulateAccountStatusHistory(gAccountID);

                Pager.PageNum = 1;
                RefreshUsers(Pager.PageNum.ToString());

                PopulateMore(gAccountID);

            }
        }

        private void PopulateAccountStatusHistory(Guid accountid)
        {
            BusinessLayer.AccountStatusHistory acc = new BusinessLayer.AccountStatusHistory();
            DataTable dt = acc.GetAccountStatusHistory(accountid);
            grdAccountStatusHistory.DataSource = dt;
            grdAccountStatusHistory.DataBind();
            grdAccountStatusHistory.HeaderRow.TableSection = TableRowSection.TableHeader;


        }

        private void PopulateAccountDetailsFields(Guid accountid)
        {
            Account acc = new Account();
            AccountModel accmodel = acc.GetAccountDetails(accountid);

            lblAccountName.InnerText = accmodel.AccountName;
            lblDateCreated.InnerText = string.Format("{0: dd MMM yyyy hh:mm:ss tt}", accmodel.CreatedDate);
            lblSignupIP.InnerText = accmodel.CreatedFromIP;

            rdActive.Checked = accmodel.IsActive;
            rdInactive.Checked = !accmodel.IsActive;

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.AccountStatusHistory acc = new BusinessLayer.AccountStatusHistory();

            string sStatus = string.Empty;
            Guid gAccountID;

            if (rdActive.Checked)
            {
                sStatus = "ACT";
            }

            if (rdInactive.Checked)
            {
                sStatus = "INA";
            }

           
            if (Guid.TryParse(SelectedAccountid, out gAccountID))
            {
                if (acc.UpdateAccountStatus(sStatus, Request.UserHostAddress, gAccountID, userlogin.UserID))
                {
                    ToggleMessage("Success! Account Status updated.", "display:block", "alert alert-success");
                }
                else
                {
                    ToggleMessage("Account Status failed to update.", "display:block", "alert alert-danger");
                }
            }
            else
            {
                ToggleMessage("Account Status failed to update. Invalid Account ID", "display:block", "alert alert-danger");
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            PopulateAccountInformation();
        }


        public void RefreshUsers(string curpage)
        {
            int iMinItem, iMaxItem, iTotalRows = 0;
            Guid gAccountID;
            if (Guid.TryParse(SelectedAccountid, out gAccountID))
            {
                BindData(gAccountID, out iMinItem, out iMaxItem, out iTotalRows);
                SetPager(Convert.ToInt32(curpage), "Id", "ASC", iTotalRows, iMinItem, iMaxItem);
            }  
        }

        private void BindData(Guid accountid, out int iMinItem, out int iMaxItem, out int iTotalRows)
        {
            User usr = new User();
            DataTable dt = usr.GetUsersByAccountId(Pager.PageNum, Pager.CurMaxRows, Pager.SortBy, Pager.SortDirection, accountid, string.Empty, string.Empty, string.Empty, out iMinItem, out iMaxItem, out iTotalRows);
            grdUsers.DataSource = dt;
            grdUsers.DataBind();
            grdUsers.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        protected void grdUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SELECT")
            {
                Response.Redirect(string.Format("~/MyAccount/UserDetails.aspx?c=3&userid={0}", e.CommandArgument.ToString()));
            }
        }

        private void PopulateMore(Guid accountid)
        {
            User usr = new User();
            DataModels.UserModel usrModel = usr.GetAccountOwnerByAccountId(accountid);

            Account acc = new Account();
            AccountModel accmodel = acc.GetAccountDetails(accountid);

            Country cnt = new Country();

            lblUserName.InnerText = usrModel.UserName;
            lblFirstName.InnerText = usrModel.FirstName;
            lblLastName.InnerText = usrModel.LastName;
            lblPhoneNo.InnerText = usrModel.PhoneNumber;
            lblMobileNo.InnerText = usrModel.MobileNumber;
            lblEmailAddress.InnerText = usrModel.EmailAddress;

            lblCompanyName.InnerText = accmodel.AccountName;
            lblAddress.InnerText = accmodel.Address;
            lblCityTownSuburb.InnerText = accmodel.City;
            lblState.InnerText = accmodel.State;
            lblPostalZipCode.InnerText = accmodel.ZipCode;
            lblCountry.InnerText = cnt.GetCountry(accmodel.CountryId);
            lblWebSite.InnerText = accmodel.CompanyWebsite;
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