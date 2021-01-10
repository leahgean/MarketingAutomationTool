using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;

namespace MarketingAutomationTool.Leads
{
    public partial class EditLead : System.Web.UI.Page
    {
        public string LeadGUID
        {
            get
            {
                if (ViewState["LeadGUID"] != null)
                    return ViewState["LeadGUID"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                ViewState["LeadGUID"] = value;
            }
        }


        public bool IsDeleted
        {
            get
            {
                if (ViewState["IsDeleted"] != null)
                    return bool.Parse(ViewState["IsDeleted"].ToString());
                else
                    return false;
            }
            set
            {
                ViewState["IsDeleted"] = value;
            }
        }


        public bool CurrentIsSubscribedToEmail
        {
            get
            {
                if (ViewState["CurrentIsSubscribedToEmail"] != null)
                    return bool.Parse(ViewState["CurrentIsSubscribedToEmail"].ToString());
                else
                    return false;
            }
            set
            {
                ViewState["CurrentIsSubscribedToEmail"] = value;
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
            DataModels.UserLogin userlogin = (DataModels.UserLogin)Session["loggeduser"];
            if (userlogin == null)
                Response.Redirect("~/Public/Login.aspx");

            if (!Page.IsPostBack)
            {
                AccountID = userlogin.AccountID;

                Country country = new Country();
                DataTable dtCountries = country.GetCountries();
                ddlCountry.DataSource = dtCountries;
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataBind();

                ddlCountry.Items.Insert(0, new ListItem("Please select country", string.Empty));

                //display success message
                if (Request.QueryString["msg"]!=null)
                {
                    if (Request.QueryString["msg"].ToString() == "SUCCESS")
                    {
                        ToggleMessage("Success! New lead created.", "display:block", "alert alert-success");
                    }
                }

                //load lead detail
                LoadLead();  
            }
        }


        private void LoadLead()
        {
            if (Request.QueryString["Guid"] != null)
            {
                Guid gResult = new Guid();
                if (Guid.TryParse(Request.QueryString["Guid"].ToString().Trim(), out gResult))
                {
                    LeadGUID = gResult.ToString();
                    BusinessLayer.Lead cLead = new BusinessLayer.Lead();
                    DataModels.Lead oLead = cLead.SelectLead(gResult, AccountID.Value);

                    lblLeadID.InnerText = Utilities.Tools.PadLeadID(oLead.ID.ToString());
                    ddlTitle.SelectedIndex = ddlTitle.Items.IndexOf(ddlTitle.Items.FindByValue(oLead.Title));
                    txtFirstName.Text = oLead.FirstName;
                    txtMiddleName.Text = oLead.MiddleName;
                    txtLastName.Text = oLead.LastName;
                    lblLeadName.InnerText = string.Format("{0}, {1} {2}", oLead.LastName, oLead.FirstName, oLead.MiddleName); 
                    txtCompany.Text = oLead.CompanyName;
                    txtWebsite.Text = oLead.WebSite;
                    txtPosition.Text = oLead.Position;
                    if (oLead.ContactType!=null)
                    {
                        ddlType.SelectedIndex = ddlType.Items.IndexOf(ddlType.Items.FindByValue(oLead.ContactType.ToString()));

                        if (oLead.ContactType.ToString()=="1") //Lead
                        {
                            if (oLead.LeadStatus != null)
                            {
                                ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByValue(oLead.LeadStatus.ToString()));
                            }
                            else
                            {
                                ddlStatus.SelectedIndex = 0;
                            }
                        }
                        else
                        {
                            if (oLead.ContactStatus != null)
                            {
                                ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByValue(oLead.ContactStatus.ToString()));
                            }
                            else
                            {
                                ddlStatus.SelectedIndex = 0;
                            }
                        }
                    }
                    else
                    {
                        ddlType.SelectedIndex = 0;
                        ddlStatus.SelectedIndex = 0;
                    }

                    if (oLead.Gender != null)
                    {
                        ddlGender.SelectedIndex = ddlGender.Items.IndexOf(ddlGender.Items.FindByValue(oLead.Gender.ToString()));
                    }
                    else
                    {
                        ddlGender.SelectedIndex = 0;
                    }
                    
                    txtEmail.Text = oLead.EmailAddress;
                    txtPhoneNo.Text = oLead.PhoneNumber;
                    txtMobile.Text = oLead.MobileNumber;
                    txtFacebookID.Text = oLead.FacebookID;
                    txtAddress1.Text = oLead.Address1;
                    txtAddress2.Text = oLead.Address2;
                    txtCity.Text = oLead.City;
                    txtState.Text = oLead.State;

                    if (oLead.CountryId != null)
                    {
                        ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(oLead.CountryId.ToString()));
                    }
                    else
                    {
                        ddlCountry.SelectedIndex = 0;
                    }
                    txtPostcode.Text = oLead.ZipCode;
                    cbSubscribedToEmail.Checked = oLead.SubscribedToEmail;
                    CurrentIsSubscribedToEmail = oLead.SubscribedToEmail;
                    chkUseForTesting.Checked = oLead.UseforTesting;
                    IsDeleted = oLead.IsDeleted;

                    if (oLead.IsDeleted)
                    {

                        BusinessLayer.User oUser = new BusinessLayer.User();
                        Guid gModifiedBy;
                        DateTime dDeletedDate;
                        btnSave.Attributes.Add("class", "btn btn-primary disabled");
                        btnCancel.Attributes.Add("class", "btn btn-light disabled");
                        dvDeleteInformation.Visible = true;
                        lblDateDeleted.InnerText = string.Empty;
                        if (oLead.DeletedDate != null)
                        {
                            if (DateTime.TryParse(oLead.DeletedDate.ToString(), out dDeletedDate))
                            {
                                lblDateDeleted.InnerText = string.Format("{0:dd MMM yyyy}", dDeletedDate);
                            }
                        }
                        
                        lblDeletedBy.InnerText = string.Empty;
                        if (oLead.ModifiedBy!=null)
                        {
                            if (Guid.TryParse(oLead.ModifiedBy.ToString(), out gModifiedBy))
                            {
                                lblDeletedBy.InnerText = oUser.GetUserName(gModifiedBy, oLead.AccountID);
                            }
                        }
                        ToggleMessage("This lead has been deleted and cannot be edited.", "display:block", "alert alert-danger");
                    }
                    else
                    {
                        dvDeleteInformation.Visible = false;
                        ToggleMessage(string.Empty, "display:none", string.Empty);
                    }
                    
                    
                }
                else
                {
                    ToggleMessage("Error! Failed to load lead. Incorrect Guid.", "display:block", "alert alert-danger");
                }
            }
        }

        private void ToggleMessage(string strText, string strDisplay, string strClass)
        {
            dvMessage.Attributes.Add("class", strClass);
            dvMessage.Attributes.Add("style", strDisplay);
            dvMessage.InnerText = strText;
        }

        public void SaveLead()
        {
            DataModels.UserLogin userlogin = (DataModels.UserLogin)Session["loggeduser"];
            BusinessLayer.Lead objlead = new BusinessLayer.Lead();
            Guid gResult = new Guid();

            if (Guid.TryParse(LeadGUID, out gResult))
            {
                if (objlead.UniqueEmail(txtEmail.Text.Trim(), userlogin.AccountID, gResult))
                {
                    int? iContactType = null;
                    int? iLeadStatus = null;
                    int? iContactStatus = null;
                    int? iCountry = null;

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

                    if (objlead.UpdateLead(
                        userlogin.AccountID,
                        gResult,
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
                        (!(cbSubscribedToEmail.Checked== CurrentIsSubscribedToEmail))))
                    {
                        CurrentIsSubscribedToEmail = cbSubscribedToEmail.Checked;
                        ToggleMessage("Success! Lead updated.", "display:block", "alert alert-success");
                    }
                    else
                    {
                        ToggleMessage("Error! Lead was not updated.", "display:block", "alert alert-danger");
                    }
                }
                else
                {
                    ToggleMessage("Error! There is an existing lead with this Email Address.", "display:block", "alert alert-danger");
                }
            }
            else
            {
                ToggleMessage("Error! Failed to update lead. Invalid GUID", "display:block", "alert alert-danger");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsDeleted)
            {
                SaveLead();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (!IsDeleted)
            {
                LoadLead();
                ToggleMessage(string.Empty, "display:none", string.Empty);
            }
        }

        protected void lnkBreadHome_Click(object sender, EventArgs e)
        {
            //Session["maintab"] = "HOME";
            Response.Redirect("~/Dashboard.aspx");
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
            ToggleMessage(string.Empty, "display:none", string.Empty);
        }

        protected void lnkRunSearch_Click(object sender, EventArgs e)
        {
            Session["SearchType"] = null;
            Response.Redirect("~/Leads/SimplifiedSearch.aspx");
        }
    }
}