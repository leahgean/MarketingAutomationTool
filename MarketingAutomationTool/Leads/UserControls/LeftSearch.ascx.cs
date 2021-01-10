using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace MarketingAutomationTool.Leads.UserControls
{
    public partial class LeftSearch : System.Web.UI.UserControl
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

        public Guid? AccountID
        {
            get
            {
                if (ViewState["AccountID"] != null)
                    return Guid.Parse(ViewState["AccountID"].ToString());
                else
                    return null;
            }
            set
            {
                ViewState["AccountID"] = Guid.Parse(value.ToString());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            userlogin = (DataModels.UserLogin)Session["loggeduser"];

            if (userlogin == null)
                Response.Redirect("~/Public/Login.aspx");

            AccountID = userlogin.AccountID;
            BindData(userlogin.AccountID);

            if (!Page.IsPostBack)
            {
                
            }
        }

        private void BindData(Guid? AccountID)
        {
            BusinessLayer.Lead cLead = new BusinessLayer.Lead();

            int? iStatus = null;
            if (!string.IsNullOrEmpty(ddlStatus.SelectedItem.Value))
            {
                iStatus = int.Parse(ddlStatus.SelectedItem.Value);
            }
            DataTable dt = cLead.GetLeadsForSideBar(AccountID.Value, iStatus, chkDeletedOnly.Checked, txtSearchKey.Text);
            rvContact.DataSource = dt;
            rvContact.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData(AccountID);
        }

       

        protected void rvContact_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField hdnDeletedLC = (HiddenField)e.Item.FindControl("hdnDeletedLC");
                LinkButton lnkDelete = (LinkButton)e.Item.FindControl("lnkDelete");
                HtmlAnchor lnkNoDeleteLC = (HtmlAnchor)e.Item.FindControl("lnkNoDeleteLC");

                if (hdnDeletedLC.Value.Trim().ToUpper()=="TRUE")
                {
                    lnkDelete.Visible = false;
                    lnkNoDeleteLC.Visible = true;
                }
                else
                {
                    lnkDelete.Visible = true;
                    lnkNoDeleteLC.Visible = false;
                }

            }
        }

        protected void rvContact_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dpContact.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindData(AccountID);
        }

        protected void rvContact_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }

        protected void rvContact_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "DELETE":
                    BusinessLayer.Lead cLead = new BusinessLayer.Lead();
                    Guid gContactID;
                    if (Guid.TryParse(e.CommandArgument.ToString(), out gContactID))
                    {
                        DataModels.Lead objLead = new DataModels.Lead();
                        objLead = cLead.SelectLead(gContactID, userlogin.AccountID);

                        cLead.DeleteLead(AccountID.Value, gContactID, userlogin.UserID,false,objLead.EmailAddress, objLead.CountryId, objLead.City, objLead.State, Request.UserHostAddress);
                        BindData(AccountID);
                    }
                    break;
            }

        }
    }
}