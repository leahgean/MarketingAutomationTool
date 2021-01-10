using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MarketingAutomationTool.Campaigns.UserControls
{
    public partial class LeftMenu : System.Web.UI.UserControl
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
            BusinessLayer.Campaign cCampaign = new BusinessLayer.Campaign();

            int? iStatus = null;
            if (!string.IsNullOrEmpty(ddlStatus.SelectedItem.Value))
            {
                iStatus = int.Parse(ddlStatus.SelectedItem.Value);
            }
            DataTable dt = cCampaign.GetCampaignsForSideBar(AccountID.Value, txtSearchKey.Text.Trim(), iStatus, chkDeletedCampaigns.Checked,chkIncludeHidden.Checked);
            rvCampaign.DataSource = dt;
            rvCampaign.DataBind();
        }

        public void RefreshLeftMenu()
        {
            BindData(AccountID);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData(AccountID);
        }

        protected void rvCampaign_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField hdnDeletedLC = (HiddenField)e.Item.FindControl("hdnDeletedLC");
                LinkButton lnkDelete = (LinkButton)e.Item.FindControl("lnkDelete");
                HtmlAnchor lnkNoDeleteLC = (HtmlAnchor)e.Item.FindControl("lnkNoDeleteLC");

                if (hdnDeletedLC.Value.Trim().ToUpper() == "TRUE")
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

        protected void rvCampaign_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            Guid gCampaignUID;
            switch (e.CommandName)
            {
                case "DELETE":
                    BusinessLayer.Campaign cCampaign = new BusinessLayer.Campaign();
                  
                    if (Guid.TryParse(e.CommandArgument.ToString(), out gCampaignUID))
                    {
                        cCampaign.DeleteCampaign(AccountID.Value, gCampaignUID, userlogin.UserID);
                        BindData(AccountID);
                    }
                    break;
                case "EDIT":
                    BusinessLayer.ContactSearch cCS = new BusinessLayer.ContactSearch();
                    string sSearchUID;
                    if (Guid.TryParse(e.CommandArgument.ToString(), out gCampaignUID))
                    {
                        sSearchUID = cCS.GetSearchUIDByCampaignUID(gCampaignUID, userlogin.AccountID, userlogin.AccountID);

                        if (!string.IsNullOrEmpty(sSearchUID))
                        {
                            Response.Redirect(string.Format("~/Campaigns/CampaignBuilder2.aspx?c={0}&uid={1}", gCampaignUID.ToString(), sSearchUID));
                        }
                        else
                        {
                            Response.Redirect(string.Format("~/Campaigns/CampaignBuilder2.aspx?c={0}", gCampaignUID.ToString()));
                        } 
                    } 
                    break;
                case "COPY":
                    if (Guid.TryParse(e.CommandArgument.ToString(), out gCampaignUID))
                    {
                        Response.Redirect(string.Format("~/Campaigns/CampaignBuilder2.aspx?sc={0}", gCampaignUID));
                    } 
                break;
            }
        }

        protected void rvCampaign_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }

        protected void rvCampaign_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dpCampaign.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindData(AccountID);
        }
    }
}