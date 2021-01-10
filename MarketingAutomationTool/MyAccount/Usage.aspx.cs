using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataModels;
using System.Data;


namespace MarketingAutomationTool.MyAccount
{
    public partial class Usage : System.Web.UI.Page
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

                ViewState["SelectedAccountid"] = value;

            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            userlogin = (DataModels.UserLogin)Session["loggeduser"];
            if (userlogin == null)
                Response.Redirect("~/Public/Login.aspx");

            if (!userlogin.IsSuperAdminUser)
                Response.Redirect("~/Public/Login.aspx");

            if (!Page.IsPostBack)
            {
                Guid gAccountID;
                if (Guid.TryParse(SelectedAccountid, out gAccountID))
                {
                    Account acc = new Account();
                    AccountModel accmodel = acc.GetAccountDetails(gAccountID);

                    lblAccountName.InnerText = accmodel.AccountName;
                    lblDateCreated.InnerText = string.Format("{0: dd MMM yyyy hh:mm:ss tt}", accmodel.CreatedDate);
                    lblStatus.InnerText = accmodel.IsActive ? "Active" : "Inactive";
                    PopulateYear(accmodel.CreatedDate.Year);
                    ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByValue(System.DateTime.UtcNow.AddHours(8).Year.ToString()));
                    lblYearSelected.InnerText = ddlYear.SelectedItem.Value.Trim();
                    PopulateTable();
                }
            }
        }

        private void PopulateYear(int pStartYear)
        {
            int iEndYear = System.DateTime.UtcNow.AddHours(8).Year;
            for (int iYear = iEndYear; iYear >= pStartYear; iYear--)
            {
                ddlYear.Items.Add( new ListItem(iYear.ToString(), iYear.ToString()));
            }
            
            lblYearSelected.InnerText = ddlYear.SelectedItem.Value.Trim();
        }

        private void PopulateTable()
        {

            BusinessLayer.Account acc = new BusinessLayer.Account();
            DataTable dt = new DataTable();
            dt=acc.GetAccountUsage(Guid.Parse(SelectedAccountid), int.Parse(ddlYear.SelectedItem.Value.ToString()), userlogin.UserID);
            grdUsage.DataSource = dt;
            grdUsage.DataBind();
            grdUsage.HeaderRow.TableSection = TableRowSection.TableHeader;

        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblYearSelected.InnerText = ddlYear.SelectedItem.Value.Trim();
            PopulateTable();
        }

        protected void lnkBreadHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard.aspx");
        }
    }
}