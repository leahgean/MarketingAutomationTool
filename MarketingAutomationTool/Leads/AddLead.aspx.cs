using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;

namespace MarketingAutomationTool.Leads
{
    public partial class AddLead : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataModels.UserLogin userlogin = (DataModels.UserLogin)Session["loggeduser"];
            if (userlogin == null)
                Response.Redirect("~/Public/Login.aspx");

            if (!Page.IsPostBack)
            {
                Country country = new Country();
                DataTable dtCountries = country.GetCountries();
                ddlCountry.DataSource = dtCountries;
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataBind();

                ddlCountry.Items.Insert(0, new ListItem("Please select country", string.Empty));
            }
        }

        protected void lnkBreadHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Dashboard.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataModels.UserLogin userlogin = (DataModels.UserLogin)Session["loggeduser"];

            BusinessLayer.Lead objlead = new BusinessLayer.Lead();
            if (objlead.UniqueEmail(txtEmail.Text.Trim(), userlogin.AccountID, null))
            {
                int? iContactType = null;
                int? iLeadStatus = null;
                int? iContactStatus = null;
                int? iCountry = null;
                Guid? LeadGUID = null;

                if (!string.IsNullOrEmpty(ddlType.SelectedItem.Value.Trim()))
                {
                    iContactType = int.Parse(ddlType.SelectedItem.Value.Trim());
                    if (ddlType.SelectedItem.Text.Trim().ToUpper() == "LEAD")
                    {
                        if (!string.IsNullOrEmpty(hdnStatus.Value.Trim()))
                        {
                            iLeadStatus = int.Parse(hdnStatus.Value.Trim());
                        }
                        else
                        {
                            iLeadStatus = null;
                        };
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(hdnStatus.Value.Trim()))
                        {
                            iContactStatus = int.Parse(hdnStatus.Value.Trim());
                        }
                        else
                        {
                            iContactStatus = null;
                        };
                    }
                }

                if (!string.IsNullOrEmpty(ddlCountry.SelectedItem.Value.Trim()))
                {
                    iCountry = int.Parse(ddlCountry.SelectedItem.Value.Trim());
                }
               
                if (objlead.AddNewLead(
                    userlogin.AccountID,
                    ddlTitle.SelectedItem.Value.Trim(),
                    txtFirstName.Text.Trim(),
                    txtMiddleName.Text.Trim(),
                    txtLastName.Text.Trim(),
                    txtCompany.Text.Trim(),
                    txtWebsite.Text.Trim(),
                    txtPosition.Text.Trim(),
                    iContactType,
                    iLeadStatus,
                    iContactStatus,
                    ddlGender.SelectedItem.Value,
                    txtEmail.Text.Trim(),
                    txtPhoneNo.Text.Trim(),
                    txtMobile.Text.Trim(),
                    txtFacebookID.Text.Trim(),
                    txtAddress1.Text.Trim(),
                    txtAddress2.Text.Trim(),
                    txtCity.Text.Trim(),
                    txtState.Text.Trim(),
                    iCountry,
                    txtPostcode.Text.Trim(),
                    cbSubscribedToEmail.Checked,
                    chkUseForTesting.Checked,
                    userlogin.UserID,
                    Request.UserHostAddress,
                    out LeadGUID))
                {
                        ToggleMessage("Success! New lead created.", "display:block", "alert alert-success");
                        ClearFields();
                        Response.Redirect(string.Format("EditLead.aspx?Guid={0}&msg={1}", LeadGUID.ToString(), "SUCCESS"));
                }
                else
                {
                    ToggleMessage("Error! New lead was not created.", "display:block", "alert alert-danger");
                }
            }
            else
            {
                ToggleMessage("Error! There is an existing lead with this Email Address.", "display:block", "alert alert-danger");
            }
        }

        private void ToggleMessage(string strText, string strDisplay, string strClass)
        {
            dvMessage.Attributes.Add("class", strClass);
            dvMessage.Attributes.Add("style", strDisplay);
            dvMessage.InnerText = strText;
        }

        private void ClearFields()
        {
            ddlTitle.SelectedIndex = 0;
            txtFirstName.Text = string.Empty;
            txtMiddleName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtCompany.Text = string.Empty;
            txtWebsite.Text = string.Empty;
            txtPosition.Text = string.Empty;
            ddlType.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            ddlGender.SelectedIndex = 0;
            txtEmail.Text = string.Empty;
            txtPhoneNo.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtFacebookID.Text = string.Empty;
            txtAddress1.Text = string.Empty;
            txtAddress2.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtState.Text = string.Empty;
            ddlCountry.SelectedIndex = 0;
            txtPostcode.Text = string.Empty;
            cbSubscribedToEmail.Checked = false;
            chkUseForTesting.Checked = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}