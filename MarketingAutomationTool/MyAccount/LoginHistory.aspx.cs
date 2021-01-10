using System;
using BusinessLayer;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MarketingAutomationTool.MyAccount
{
    public partial class LoginHistory : System.Web.UI.Page
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


        

        protected void Page_Load(object sender, EventArgs e)
        {
            userlogin = (DataModels.UserLogin)Session["loggeduser"];
            if (userlogin == null)
                Response.Redirect("~/Public/Login.aspx");

            UsersPagerMethod userpager = new UsersPagerMethod(Refresh);
            Pager.CallingPageMethod = userpager;

            if (Request.QueryString["userid"] != null)
                SelectedUserId = Request.QueryString["userid"].ToString();
            else
                SelectedUserId = userlogin.UserID.ToString();

            if (!Page.IsPostBack)
            {
                Pager.PageNum = 1;
                Refresh(Pager.PageNum.ToString());
            }
        }

        public void Refresh(string curpage)
        {
            int iMinItem, iMaxItem, iTotalRows = 0;
            DateTime tmpLoginDateTimeFrom;
            DateTime tmpLoginDateTimeTo;
            DateTime tmpLogoutDateTimeFrom;
            DateTime tmpLogoutDateTimeTo;
            DateTime? LoginDateTimeFrom;
            DateTime? LoginDateTimeTo;
            DateTime? LogoutDateTimeFrom;
            DateTime? LogoutDateTimeTo;
            string IPAddress = txtIpAddress.Text.Trim();


            if (DateTime.TryParse(txtLoginStartDate.Text.Trim() + " " + txtLoginStartTime.Text.Trim(), out tmpLoginDateTimeFrom))
            {
                LoginDateTimeFrom = tmpLoginDateTimeFrom;
            }
            else
            {
                LoginDateTimeFrom = null;
            }

            if (DateTime.TryParse(txtLoginEndDate.Text.Trim() + " " + txtLoginEndTime.Text.Trim(), out tmpLoginDateTimeTo))
            {
                LoginDateTimeTo = tmpLoginDateTimeTo;
            }
            else
            {
                LoginDateTimeTo = null;
            }


            if (DateTime.TryParse(txtLogOutStartDate.Text.Trim() + " " + txtLogOutStartTime.Text.Trim(), out tmpLogoutDateTimeFrom))
            {
                LogoutDateTimeFrom = tmpLogoutDateTimeFrom;
            }
            else
            {
                LogoutDateTimeFrom = null;
            }

            if (DateTime.TryParse(txtLogOutEndDate.Text.Trim() + " " + txtLogOutEndTime.Text.Trim(), out tmpLogoutDateTimeTo))
            {
                LogoutDateTimeTo = tmpLogoutDateTimeTo;
            }
            else
            {
                LogoutDateTimeTo = null;
            }

            BindData(LoginDateTimeFrom, LoginDateTimeTo, LogoutDateTimeFrom, LogoutDateTimeTo, IPAddress, out iMinItem, out iMaxItem, out iTotalRows);
            SetPager(Convert.ToInt32(curpage), "Id", "ASC", iTotalRows, iMinItem, iMaxItem);
        }

        public void BindData(DateTime? LoginDateTimeFrom, DateTime? LoginDateTimeTo, DateTime? LogoutDateTimeFrom, DateTime? LogoutDateTimeTo, string IPAddress, out int iMinItem, out int iMaxItem, out int iTotalRows)
        {
            BusinessLayer.LoginHistory loghstry = new BusinessLayer.LoginHistory();
           
            DataTable dt = loghstry.GetLoginHistory(Guid.Parse(SelectedUserId), LoginDateTimeFrom, LoginDateTimeTo, LogoutDateTimeFrom, LogoutDateTimeTo, IPAddress, Pager.PageNum, Pager.CurMaxRows, out iMinItem, out iMaxItem, out iTotalRows);
            grdLoginHistory.DataSource = dt;
            grdLoginHistory.DataBind();
            grdLoginHistory.HeaderRow.TableSection = TableRowSection.TableHeader;

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

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            Pager.PageNum = 1;
            Refresh(Pager.PageNum.ToString());
        }

        protected void lnkBreadHome_Click(object sender, EventArgs e)
        {
            //Session["maintab"] = "HOME";
            Response.Redirect("~/Dashboard.aspx");
        }
    }
}