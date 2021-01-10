using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Security;
using BusinessLayer;
using System.Data;
using DataModels;
using Newtonsoft.Json;
using MarketingAutomationTool.Utilities;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;


namespace MarketingAutomationTool.Campaigns
{
    public partial class CampaignBuilder2 : System.Web.UI.Page
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

        private string SearchID
        {
            get
            {
                if (ViewState["SearchID"] != null)
                {
                    return ViewState["SearchID"].ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ViewState["SearchID"] = value;
            }
        }


        private string CampaignUID
        {
            get
            {
                if (ViewState["CampaignUID"] != null)
                {
                    return ViewState["CampaignUID"].ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ViewState["CampaignUID"] = value;
            }
        }

        private string SourceCampaignUID
        {
            get
            {
                if (ViewState["SourceCampaignUID"] != null)
                {
                    return ViewState["SourceCampaignUID"].ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ViewState["SourceCampaignUID"] = value;
            }
        }

        private string MessageID
        {
            get
            {
                if (ViewState["MessageID"] != null)
                {
                    return ViewState["MessageID"].ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ViewState["MessageID"] = value;
            }
        }

        private List<string> ExcludeList
        {
            get
            {
                if (ViewState["ExcludeList"] != null)
                {
                    return (List<string>)ViewState["ExcludeList"];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ViewState["ExcludeList"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            userlogin = (DataModels.UserLogin)Session["loggeduser"];
            if (userlogin == null)
                Response.Redirect("~/Public/Login.aspx");

            UsersPagerMethod userpager = new UsersPagerMethod(Refresh);
            Pager.CallingPageMethod = userpager;

            UsersPagerMethod excludepager = new UsersPagerMethod(RefreshExcludeList);
            PagerExcluded.CallingPageMethod = excludepager;

            SelectModuleTab();
            AssignLoggedUserNames();

            SetPageBasedOnSearchType();
            

            if (!Page.IsPostBack)
            {
                PopulateDatabaseFieldDropDown();

                /*Copy Campaign*/
                if (Request["sc"] == null)
                {
                    LoadNormalCampaign();  
                }
                else
                {
                    LoadCopiedCampaign();
                }
            }
        }

        private void LoadCopiedCampaign()
        {
            SourceCampaignUID = Request["sc"].ToString();
            Guid gSourceCampaignUID;

            if (Guid.TryParse(SourceCampaignUID, out gSourceCampaignUID))
            {
                Campaign2 oCampaign2 = new Campaign2();
                CampaignModel mCam = new CampaignModel();
                mCam = oCampaign2.GetCampaign(userlogin.AccountID, gSourceCampaignUID, userlogin.UserID);
                LoadCopiedCampaignDetails(mCam);
                SetRightMenu(null);
                SetRecipientDefaults();
                SetCampaignStatusDetails(null);
                SetActiveStepToCampaignDetails();
            }
            else
            {
                ToggleCampaignDetailsMessage("Incorrect Source Campaign ID. Please select campaign to copy and copy campaign again.", "block", "alert alert-danger");
            }
           
        }


        private void LoadNormalCampaign()
        {
            if (Request["c"] != null)
            {
                CampaignUID = Request["c"].ToString();
                hdnCampaignUID.Value = CampaignUID;
            }

            if (CampaignUID == null)
            {
                PopulateCampaignDetailsDefaultValues();
                SetDefaultBounceAddress();
                SetRightMenu(null);
                SetRecipientDefaults();
                SetCampaignStatusDetails(null);
            }
            else
            {
                Campaign2 oCampaign2 = new Campaign2();
                CampaignModel mCam = new CampaignModel();
                Guid gCampaignUID;
                if (Guid.TryParse(CampaignUID, out gCampaignUID))
                {
                    mCam = oCampaign2.GetCampaign(userlogin.AccountID, Guid.Parse(CampaignUID), userlogin.UserID);
                    LoadExistingCampaign(mCam);
                    SetRightMenu(mCam);
                    SetCampaignStatusDetails(mCam);
                    SetRecipientDefaults();
                    SetActiveStepToCampaignDetails();
                }
                else
                {
                    ToggleCampaignDetailsMessage("Incorrect Campaign ID. Please select campaign again.","block","alert alert-danger");
                }
                    
            }
        }
        private void SetRightMenu(CampaignModel mCam)
        {
            if (CampaignUID == null)
            {
                lnkSaveAsDraft.Visible = true;
                liCopy.Visible = false;
                liDelete.Visible = false;
            }
            else
            {
                if (mCam == null)
                {
                    liSaveDraft.Visible = true;
                    liCopy.Visible = false;
                    liDelete.Visible = false;
                }
                else
                {
                    if (mCam.CampaignStatus == (int)ConstantValues.CampaignStatus.Draft)
                    {
                        if (mCam.Deleted.HasValue)
                        {
                            if (mCam.Deleted.Value == true)
                            {
                                liSaveDraft.Visible = false;
                            }
                            else
                            {
                                liSaveDraft.Visible = true;
                            }
                        }
                        else
                        {
                            liSaveDraft.Visible = true;
                        }

                    }
                    else
                    {
                        liSaveDraft.Visible = false;
                    }

                    liCopy.Visible = true;

                    if (mCam.Deleted.HasValue)
                    {
                        liDelete.Visible = !mCam.Deleted.Value;
                    }
                    else
                    {
                        liDelete.Visible = true;
                    }

                }
            }
        }

        private void SetCampaignStatusDetails(CampaignModel mCam)
        {
            if (mCam == null)
            {
                divCampaignStatus.Attributes.Add("style", "display:block;margin-bottom:0px;");
                divCampaignStatus.Attributes.Add("class", "alert alert-warning");
                divCampaignStatus.InnerHtml = "<strong>Status:</strong>&nbsp;New";
            }
            else if (mCam.CampaignStatus == ((int)ConstantValues.CampaignStatus.Draft))
            {
                divCampaignStatus.Attributes.Add("style", "display:block;margin-bottom:0px;");
                BusinessLayer.User bUser = new BusinessLayer.User();
                string sText = string.Empty;
                string sTabSpace = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                string UserName = string.Empty;
                if (mCam.Deleted == false)
                {
                    divCampaignStatus.Attributes.Add("class", "alert alert-danger");
                }
                else if (mCam.Deleted == true)
                {
                    divCampaignStatus.Attributes.Add("class", "alert alert-secondary");
                }
                sText = "<strong>Status:</strong>&nbsp;Draft";
                sText += "<br/><strong>Created Date:</strong>&nbsp;" + string.Format("{0:dd MMM yyyy}", mCam.CreatedDate.AddHours(8));
                UserName = bUser.GetUserName(mCam.CreatedBy, mCam.AccountId);
                sText += sTabSpace + "<strong>Created By:</strong>&nbsp;" + UserName;

                if (mCam.Deleted == true)
                {
                    sText += "<br/><strong>Deleted:</strong>&nbsp;Yes";
                    sText += "<br/><strong>Deleted Date:</strong>&nbsp;" + string.Format("{0:dd MMM yyyy}", mCam.DeletedDate.Value.AddHours(8));
                    UserName = bUser.GetUserName(mCam.DeletedBy.Value, mCam.AccountId);
                    sText += sTabSpace + "<strong>Deleted By:</strong>&nbsp;" + UserName;
                }
                divCampaignStatus.InnerHtml = sText;
            }
            else if (mCam.CampaignStatus == ((int)ConstantValues.CampaignStatus.Submitted))
            {
                divCampaignStatus.Attributes.Add("style", "display:block;margin-bottom:0px;");
                BusinessLayer.User bUser = new BusinessLayer.User();
                string sText = string.Empty;
                string sTabSpace = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                string UserName = string.Empty;
                if (mCam.Deleted == false)
                {
                    divCampaignStatus.Attributes.Add("class", "alert alert-info");
                }
                else if (mCam.Deleted == true)
                {
                    divCampaignStatus.Attributes.Add("class", "alert alert-secondary");
                }
                sText = "<strong>Status:</strong>&nbsp;Submitted";
                sText += "<br/><strong>Created Date:</strong>&nbsp;" + string.Format("{0:dd MMM yyyy}", mCam.CreatedDate.AddHours(8));
                UserName = bUser.GetUserName(mCam.CreatedBy, mCam.AccountId);
                sText += sTabSpace + "<strong>Created By:</strong>&nbsp;" + UserName;
                sText += "<br/><strong>Submitted Date:</strong>&nbsp;" + string.Format("{0:dd MMM yyyy}", mCam.SubmittedDate.Value.AddHours(8));
                UserName = bUser.GetUserName(mCam.SubmittedBy.Value, mCam.AccountId);
                sText += sTabSpace + "<strong>Submitted By:</strong>&nbsp;" + UserName;
                
                if (mCam.Deleted == true)
                {
                    sText += "<br/><strong>Deleted:</strong>&nbsp;Yes";
                    sText += "<br/><strong>Deleted Date:</strong>&nbsp;" + string.Format("{0:dd MMM yyyy}", mCam.DeletedDate.Value.AddHours(8));
                    UserName = bUser.GetUserName(mCam.DeletedBy.Value, mCam.AccountId);
                    sText += sTabSpace + "<strong>Deleted By:</strong>&nbsp;" + UserName;
                }
                divCampaignStatus.InnerHtml = sText;
            }
            else
            {
                divCampaignStatus.Attributes.Add("class", string.Empty);
                divCampaignStatus.Attributes.Add("style", "display:none;margin-bottom:0px;");
            }
        }

        private string IsDeleted(bool? bVal)
        {
            if (bVal == true)
            {
                return "Yes";
            }
            else
            {
                return "No";
            }
        }
        

        private void SetActiveStepToCampaignDetails()
        {
            hdnActiveStep.Value = ((int)ConstantValues.CampaignActiveStep.CampaignDetails).ToString();
        }

       private void LoadExistingCampaign(CampaignModel mCam)
       {
            txtCampaignName.Text = mCam.CampaignName;
            ddlCampaignFormat.SelectedIndex = ddlCampaignFormat.Items.IndexOf(ddlCampaignFormat.Items.FindByValue(mCam.CampaignFormat.ToString()));
            txtCampaignDescription.Text = mCam.CampaignDescription;
            txtCampaignSubject.Text = mCam.Message.Subject;
            txtSenderName.Text = mCam.Message.SenderName;
            txtSenderEmail.Text = mCam.Message.SenderEmail;
            hdnCampaignStatus.Value = mCam.CampaignStatus.ToString();
            if (mCam.Deleted!=null)
            {
                hdnDeleted.Value = (mCam.Deleted.Value ? "1" : "0");
            }
            else
            {
                hdnDeleted.Value =  "0";
            }
             

            if (mCam.MessageId!=null)
            {
                MessageID = mCam.MessageId.ToString();
            }
            

            if (!string.IsNullOrEmpty(mCam.ExcludeList))
            {
                string[] aExcludeList = mCam.ExcludeList.Split(',');

                if (ExcludeList == null) ExcludeList = new List<string>();
                foreach (string s in aExcludeList)
                {
                    ExcludeList.Add(s);
                }
            }
            
            if (mCam.UseBounceAddressInFromField!=null)
            {
                chkUseBounceAddress.Checked = mCam.UseBounceAddressInFromField.Value;
            }
            else
            {
                chkUseBounceAddress.Checked = false;
            }
            

            if (mCam.HideInSearch!=null)
            {
                if (mCam.HideInSearch.Value == true)
                {
                    rdbYes.Checked = true;
                    rdbNo.Checked = false;
                }
                else
                {
                    rdbYes.Checked = false;
                    rdbNo.Checked = true;
                }
            }
            else
            {
                rdbYes.Checked = false;
                rdbNo.Checked = true;
            }
            

            if (mCam.CampaignFormat==(int)Utilities.ConstantValues.MessageBodyFormat.HTML)
            {
                txtBody.Text = mCam.Message.MessageBody;
            }
            else
            {
                txtTextEditor.Text = mCam.Message.MessageBody;
            }

            if (mCam.SendingOption!=null)
            {
                ddlSendingOptions.SelectedIndex = ddlSendingOptions.Items.IndexOf(ddlSendingOptions.Items.FindByValue(mCam.SendingOption.Value.ToString()));

                if (mCam.SendingOption.Value==(int)ConstantValues.SendingOptions.Scheduled)
                {
                    DateTime dteSendSchedule;
                    if ((mCam.SendingSchedule.Value!=null)&&DateTime.TryParse(mCam.SendingSchedule.Value.ToString(), out dteSendSchedule))
                    {
                        txtScheduleDate.Text = string.Format("{0:yyyy-MM-dd}",dteSendSchedule);
                        txtScheduleTime.Text = string.Format("{0:HH:mm}", dteSendSchedule);
                    } 
                }
            }
            else
            {
                ddlSendingOptions.SelectedIndex = ddlSendingOptions.Items.IndexOf(ddlSendingOptions.Items.FindByValue(((int)ConstantValues.SendingOptions.SendNow).ToString()));
            }


            if (mCam.Deleted == true)
            {
                btnSend.Attributes.Add("class", "btn btn-primary disabled");
                ToggleSendorScheduleMessage("This campaign has been deleted and can no longer be sent or edited. To reuse the campaign, use Copy Campaign function.", "block", "alert alert-info");
            }
            else
            {
                if (mCam.CampaignStatus == (int)ConstantValues.CampaignStatus.Submitted)
                {
                    btnSend.Attributes.Add("class", "btn btn-primary disabled");
                    ToggleSendorScheduleMessage("This campaign has been submitted and can no longer be sent again or edited. To reuse the campaign, use Copy Campaign function.", "block", "alert alert-info");
                }
            }
            
        }


        private void LoadCopiedCampaignDetails(CampaignModel mCam)
        {
            txtCampaignName.Text = string.Format("Copy of {0}", mCam.CampaignName);
            ddlCampaignFormat.SelectedIndex = ddlCampaignFormat.Items.IndexOf(ddlCampaignFormat.Items.FindByValue(mCam.CampaignFormat.ToString()));
            txtCampaignDescription.Text = mCam.CampaignDescription;
            txtCampaignSubject.Text = mCam.Message.Subject;
            txtSenderName.Text = mCam.Message.SenderName;
            txtSenderEmail.Text = mCam.Message.SenderEmail;
           

            if (mCam.UseBounceAddressInFromField != null)
            {
                chkUseBounceAddress.Checked = mCam.UseBounceAddressInFromField.Value;
            }
            else
            {
                chkUseBounceAddress.Checked = false;
            }


            if (mCam.HideInSearch != null)
            {
                if (mCam.HideInSearch.Value == true)
                {
                    rdbYes.Checked = true;
                    rdbNo.Checked = false;
                }
                else
                {
                    rdbYes.Checked = false;
                    rdbNo.Checked = true;
                }
            }
            else
            {
                rdbYes.Checked = false;
                rdbNo.Checked = true;
            }


            if (mCam.CampaignFormat == (int)Utilities.ConstantValues.MessageBodyFormat.HTML)
            {
                txtBody.Text = mCam.Message.MessageBody;
            }
            else
            {
                txtTextEditor.Text = mCam.Message.MessageBody;
            }
        }

        private void SetPageBasedOnSearchType()
        {
            dvExcluded.Visible = true;
            dvIncludedLabel.Visible = false; //set to true in Refresh if there are results 
            grdIncluded.Columns[0].Visible = true;//Include
        }

        private void SelectModuleTab()
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;
            string[] pathArr = path.Split('/');
            string tab = pathArr[1].Trim().ToUpper();

            switch (tab)
            {
                case "LEADS":
                    lnkLeads.CssClass = "nav-link active";
                    break;
                case "CAMPAIGNS":
                    lnkCampaigns.CssClass = "nav-link active";
                    break;
                default:
                    lnkHome.CssClass = "nav-link active";
                    break;
            }
        }

        private void AssignLoggedUserNames()
        {
            lblLoggedUser.Text = userlogin.FirstName;
            lnkSwitchBack.Visible = Session["adminuser"] != null;
            spSwitchBackSeparator.Visible = Session["adminuser"] != null;
        }


        private void PopulateCampaignDetailsDefaultValues()
        {
            txtCampaignName.Text = string.Format("Campaign created by {0} on {1}", userlogin.FirstName, string.Format("{0:dd MMM yyyy hh:mm:ss tt}", System.DateTime.UtcNow.AddHours(8)));
            txtSenderName.Text = string.Format("{0} {1}", userlogin.FirstName, userlogin.LastName);
            txtSenderEmail.Text = userlogin.EmailAddress;
        }

        private void SetDefaultBounceAddress()
        {
            hdnbouncename.Value = ConfigurationManager.AppSettings["bouncename"].ToString().Trim();
            hdnemailaddress.Value = ConfigurationManager.AppSettings["bounceaddress"].ToString().Trim();
        }

        private void SetRecipientDefaults()
        {
            PopulateCountryList();
            PopulateContactListLists();
            Pager.PageNum = 1;
            Pager.SortBy = "FirstName";
            Pager.SortDirection = "ASC";
            LoadExistingSearchCriteria();
            PagerExcluded.PageNum = 1;
            PagerExcluded.SortBy = "FirstName";
            PagerExcluded.SortDirection = "ASC";
            RefreshExcludeList(PagerExcluded.PageNum.ToString());
        }

        public void LoadExistingSearchCriteria()
        {
            if (Request["uid"] != null)
            {
                Guid gSearchGuid = new Guid();
                if (Guid.TryParse(Request["uid"].ToString(), out gSearchGuid))
                {
                    BusinessLayer.ContactSearch cs = new BusinessLayer.ContactSearch();
                    SearchID = cs.GetSearchID(gSearchGuid, userlogin.AccountID, userlogin.UserID);
                }
            }

            if ((Request["uid"] != null) && string.IsNullOrEmpty(SearchID))
            {
                ToggleRecipientMessage("Recent search has been deleted. Please create a new search.", "block", "alert alert-danger");
            }
            else if (((Request["uid"] != null) && (!string.IsNullOrEmpty(SearchID))))
            {
                //Load Search
                int iSearchId = 0;
                if (int.TryParse(SearchID, out iSearchId))
                {
                    string sSearchCriteria = string.Empty;
                    string sJsonString = string.Empty;
                    BusinessLayer.ContactSearch oContactSearch = new BusinessLayer.ContactSearch();
                    sJsonString = oContactSearch.GetSearchJsonString(iSearchId, userlogin.AccountID, userlogin.UserID);
                    hdnSearchFields.Value = sJsonString.Trim();
                    hdnActiveTab.Value = "RESULTS";//show search results tab
                    hdnActiveStep.Value = ((int)ConstantValues.CampaignActiveStep.Recipients).ToString();

                    sSearchCriteria = oContactSearch.GetSearchCriteriaDisplay(iSearchId, userlogin.AccountID, userlogin.UserID);
                    ToggleSearchCriteria(string.Format("Search Criteria: {0}", sSearchCriteria), "block", "alert alert-success searchcriteria");

                  
                    Refresh(Pager.PageNum.ToString());
                }
            }
            else
            {
                Refresh(Pager.PageNum.ToString());
            }


        }

        private void PopulateCountryList()
        {
            //GetCountries
            Country country = new Country();
            DataTable dtCountries = country.GetCountries();

            var json = new CountryList();
            List<CountryOptions> co_options = new List<CountryOptions>();
            foreach (DataRow dr in dtCountries.Rows)
            {
                CountryOptions co = new CountryOptions { Value = dr["CountryId"].ToString(), Text = dr["CountryName"].ToString() };
                co_options.Add(co);
            };
            json = new CountryList { countrylist = co_options };
            string jsonString = JsonConvert.SerializeObject(json, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented });
            hdnCountryList.Value = jsonString;
        }


        private void PopulateContactListLists()
        {
            //GetCountries
            ContactList cList = new ContactList();
            DataTable dtContactListNames = cList.GetListNames(userlogin.AccountID, false);

            var json = new DataModels.ContactListList();
            List<ContactListOptions> cl_options = new List<ContactListOptions>();
            foreach (DataRow dr in dtContactListNames.Rows)
            {
                ContactListOptions cl = new ContactListOptions { Value = dr["ID"].ToString(), Text = dr["ListName"].ToString() };
                cl_options.Add(cl);
            };

            ContactListOptions clo = new ContactListOptions();
            clo.Text = "Not In Lists";
            clo.Value = "-1";
            cl_options.Insert(0, clo);

            json = new ContactListList { contactlistlist = cl_options };
            string jsonString = JsonConvert.SerializeObject(json, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented });
            hdnContactListList.Value = jsonString;
        }

        private void PopulateDatabaseFieldDropDown()
        {
            ddlDatabaseField.Items.Add(new ListItem("Please select database field to add to Subject line", string.Empty));
            ddlDatabaseField.Items.Add(new ListItem("First Name", "%%FirstName%%"));
            ddlDatabaseField.Items.Add(new ListItem("Middle Name", "%%MiddleName%%"));
            ddlDatabaseField.Items.Add(new ListItem("Last Name", "%%LastName%%"));
            ddlDatabaseField.Items.Add(new ListItem("Mobile Number", "%%MobileNumber%%"));
            ddlDatabaseField.Items.Add(new ListItem("Phone Number", "%%PhoneNumber%%"));
            ddlDatabaseField.Items.Add(new ListItem("Email Address", "%%EmailAddress%%"));
            ddlDatabaseField.Items.Add(new ListItem("CompanyName", "%%CompanyName%%"));
            ddlDatabaseField.Items.Add(new ListItem("WebSite", "%%WebSite%%"));
            ddlDatabaseField.Items.Add(new ListItem("Position", "%%Position%%"));
            ddlDatabaseField.Items.Add(new ListItem("Title", "%%Title%%"));
            ddlDatabaseField.Items.Add(new ListItem("Gender", "%%Gender%%"));
            ddlDatabaseField.Items.Add(new ListItem("Address", "%%Address%%"));
            ddlDatabaseField.Items.Add(new ListItem("City", "%%City%%"));
            ddlDatabaseField.Items.Add(new ListItem("State", "%%State%%"));
            ddlDatabaseField.Items.Add(new ListItem("Country", "%%Country%%"));
            ddlDatabaseField.Items.Add(new ListItem("ZipCode", "%%ZipCode%%"));
        }

        protected void lnkBreadHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard.aspx");
        }

        protected void lnkSaveAsDraft_Click(object sender, EventArgs e)
        {
            ClearAlerts();
            DateTime? dteSendingSchedule = GetSendingSchedule();

            if (ValidateSendSchedule(dteSendingSchedule))
            {
                Campaign oCam = new Campaign();
                string sBody = GetMessageBody();
                bool bHideInSearch = GetHideInSearch();
                int out_iCampaignID;
                Guid? out_gCampaignGUID;
                int out_iMessageId;
                Guid? out_gMessageUID;
                string sExcludeList = GetExcludeList();
                Guid? in_gCampaignUID = null;
                int? in_iMessageId = null;

                if (CampaignUID != null)
                    in_gCampaignUID = Guid.Parse(CampaignUID);

                if (MessageID != null)
                    in_iMessageId = int.Parse(MessageID);

                if (oCam.CreateCampaign(userlogin.AccountID,
                    in_gCampaignUID,
                    in_iMessageId,
                    txtCampaignName.Text.Trim(),
                    (int)Utilities.ConstantValues.CampaignType.Email,
                    int.Parse(ddlCampaignFormat.SelectedItem.Value),
                    txtCampaignDescription.Text.Trim(),
                    chkUseBounceAddress.Checked,
                    bHideInSearch,
                    (int)Utilities.ConstantValues.CampaignStatus.Draft,
                    null,
                    int.Parse(ddlSendingOptions.SelectedItem.Value),
                    dteSendingSchedule,
                    Utilities.ConstantValues.CampaignType.Email.ToString().ToUpper().Trim(),
                    txtCampaignSubject.Text.Trim(),
                    txtSenderName.Text.Trim(),
                    txtSenderEmail.Text.Trim(),
                    sBody,
                    SearchID,
                    sExcludeList,
                    userlogin.UserID,
                    Request.UserHostAddress,
                    (int)ConstantValues.EmailJobStatus.Pending,
                    out out_iCampaignID,
                    out out_gCampaignGUID,
                    out out_iMessageId,
                    out out_gMessageUID))
                {

                    ShowMessageBasedOnActiveTab("Email campaign has been saved as draft.", "block", "alert alert-success");

                    CampaignModel mCam = new CampaignModel();
                    Campaign2 oCampaign2 = new Campaign2();
                    mCam = oCampaign2.GetCampaign(userlogin.AccountID, out_gCampaignGUID.Value, userlogin.UserID);

                    if (out_iMessageId == 0)
                    {
                        MessageID = null;
                    }
                    else
                    {
                        MessageID = out_iMessageId.ToString();
                    }

                    CampaignUID = out_gCampaignGUID.Value.ToString();
                    hdnCampaignUID.Value = CampaignUID;
                    hdnCampaignStatus.Value = mCam.CampaignStatus.ToString();
                    SetCampaignStatusDetails(mCam);
                    SetRightMenu(mCam);
                    CampaignLeftSearch.RefreshLeftMenu();
                }
                else
                {
                    ShowMessageBasedOnActiveTab("Error saving campaign as draft. Please contact System Administrator.", "block", "alert alert-danger");
                }
            }
               
        }

        private void ShowMessageBasedOnActiveTab(string sMessage, string sDisplay, string sClass, ConstantValues.MessageBodyFormat eMsgFormat=ConstantValues.MessageBodyFormat.Text)
        {
            if (hdnActiveStep.Value == ((int)ConstantValues.CampaignActiveStep.CampaignDetails).ToString())
            {
                ToggleCampaignDetailsMessage(sMessage, sDisplay, sClass,eMsgFormat);
            }
            else if (hdnActiveStep.Value == ((int)ConstantValues.CampaignActiveStep.Message).ToString())
            {
                ToggleMessageBodyMessage(sMessage, sDisplay, sClass, eMsgFormat);
            }
            else if (hdnActiveStep.Value == ((int)ConstantValues.CampaignActiveStep.Recipients).ToString())
            {
                ToggleRecipientMessage(sMessage, sDisplay, sClass, eMsgFormat);
            }
            else if (hdnActiveStep.Value == ((int)ConstantValues.CampaignActiveStep.SendingOption).ToString())
            {
                ToggleSendorScheduleMessage(sMessage, sDisplay, sClass, eMsgFormat);
            }
        }

        protected void lnkMyAccount_Click(object sender, EventArgs e)
        {
            //Session["maintab"] = "HOME";
            Response.Redirect("~/MyAccount/MyAccount.aspx");
        }

        protected void lnkSignOut_Click(object sender, EventArgs e)
        {
            if (Session["adminuser"] != null)
            {
                Session["loggeduser"] = Session["adminuser"];
                Session["adminuser"] = null;
            }

            DataModels.UserLogin userlogin = (DataModels.UserLogin)Session["loggeduser"];


            BusinessLayer.UserLogin uLogout = new BusinessLayer.UserLogin();
            if (Session["uniquesessionid"] != null)
                uLogout.UpdateUserAccess(Session["uniquesessionid"].ToString(), userlogin.UserID);
            HttpContext.Current.Session.Clear();
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage("&Logout=true");
        }

        protected void lnkSwitchBack_Click(object sender, EventArgs e)
        {
            Session["loggeduser"] = Session["adminuser"];
            Session["adminuser"] = null;
            Response.Redirect("~/MyAccount/Accounts.aspx");
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard.aspx");
        }

        protected void lnkLeads_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Leads/Lead.aspx");
        }

        protected void lnkCampaigns_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Campaigns/Dashboard.aspx");
        }

        protected void txtBody_HtmlEditorExtender_ImageUploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
        {
            string fullpath = Server.MapPath("~/Public/Campaigns/Images/") + e.FileName;
            txtBody_HtmlEditorExtender.AjaxFileUpload.SaveAs(fullpath);
            e.PostedUrl = Page.ResolveUrl("~/Public/Campaigns/Images/" + e.FileName);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CreateASearch();
        }

        private List<SearchFieldsItems> GetSearchFieldsItems(string searchfields)
        {
            List<SearchFieldsItems> lstsearchfieldsitems = new List<SearchFieldsItems>();
            //Create json string
            JObject searchfieldsitems = JObject.Parse(searchfields);
            JArray searchfieldsitem = (JArray)searchfieldsitems["searchfieldsitem"];

            for (int i = 0; i < searchfieldsitem.Count(); i++)
            {
                string sitem = searchfieldsitem[i].ToString();
                SearchFieldsItems searchkeyvalues = new JavaScriptSerializer().Deserialize<SearchFieldsItems>(sitem);

                lstsearchfieldsitems.Add(searchkeyvalues);
            }

            return lstsearchfieldsitems;
        }

        private void CreateASearch()
        {
            Pager.PageNum = 1;
            ToggleRecipientMessage(string.Empty, "none", string.Empty);

            PagerExcluded.PageNum = 1;
            ExcludeList = new List<string>();

            List<SearchFieldsItems> lstsearchfieldsitems = new List<SearchFieldsItems>();

            string searchfields = hdnSearchFields.Value.Trim();
            if (!(string.IsNullOrEmpty(searchfields)))
            {
                lstsearchfieldsitems = GetSearchFieldsItems(searchfields);

                int iSearchId = 0;
                Guid SearchUID = Guid.NewGuid();

                //Save Search Criteria
                //Do the search and populate search results
                BusinessLayer.ContactSearch oContactSearch = new BusinessLayer.ContactSearch();
                string sSearchCriteria = string.Empty;
                if (string.IsNullOrEmpty(SearchID))
                {
                    if (oContactSearch.AddContactSearch(userlogin.AccountID, userlogin.UserID, hdnSearchFields.Value.Trim(), lstsearchfieldsitems, "EMAILCAMPAIGN", out iSearchId, out SearchUID))
                    {
                        SearchID = iSearchId.ToString();

                        sSearchCriteria = oContactSearch.GetSearchCriteriaDisplay(int.Parse(SearchID), userlogin.AccountID, userlogin.UserID);
                        ToggleSearchCriteria(string.Format("Search Criteria: {0}", sSearchCriteria), "block", "alert alert-success searchcriteria");
                        Refresh(Pager.PageNum.ToString());
                        RefreshExcludeList(PagerExcluded.PageNum.ToString());
                    }
                    else
                    {
                        ToggleRecipientMessage("An error in search occurred. Please try again or contact System Administrator.", "block", "alert alert-danger");
                    }
                }
                else
                {
                    if (oContactSearch.UpdateContactSearch(userlogin.AccountID, userlogin.UserID, hdnSearchFields.Value.Trim(), lstsearchfieldsitems, int.Parse(SearchID)))
                    {
                        sSearchCriteria = oContactSearch.GetSearchCriteriaDisplay(int.Parse(SearchID), userlogin.AccountID, userlogin.UserID);
                        ToggleSearchCriteria(string.Format("Search Criteria: {0}", sSearchCriteria), "block", "alert alert-success searchcriteria");
                        Refresh(Pager.PageNum.ToString());
                        RefreshExcludeList(PagerExcluded.PageNum.ToString());
                    }
                    else
                    {
                        ToggleRecipientMessage("An error in search occurred. Please try again or contact System Administrator.", "block", "alert alert-danger");
                    }
                }
                //show active tab
                hdnActiveTab.Value = "RESULTS";
                hdnActiveStep.Value = ((int)ConstantValues.CampaignActiveStep.Recipients).ToString();
            }
        }

        public void RefreshExcludeList(string curpage)
        {
            int iMinItem, iMaxItem, iTotalRows = 0;
            int iSearchId = 0;


            if (!string.IsNullOrEmpty(SearchID))
            {
                if (int.TryParse(SearchID.ToString(), out iSearchId))
                {
                    int iPageNum = Convert.ToInt32(curpage);
                    int iNewPageNum = iPageNum;
                    decimal dMaxPages = 0;
                    BindExcludeList(iSearchId, iPageNum, out iNewPageNum, out iMinItem, out iMaxItem, out iTotalRows, out dMaxPages);
                    SetExcludeListPager(iNewPageNum, Pager.SortBy, Pager.SortDirection, iTotalRows, iMinItem, iMaxItem, dMaxPages);
                }
            }
        }

        private void SetExcludeListPager(int curpage, string sortby, string sortdir, int totalrows, int minitem, int maxitem, decimal maxpages)
        {
            if (totalrows > 0)
            {
                PagerExcluded.Visible = true;
                PagerExcluded.PageNum = curpage;
                PagerExcluded.SortBy = sortby;
                PagerExcluded.SortDirection = sortdir;
                PagerExcluded.TotalRows = totalrows;
                PagerExcluded.MinItem = minitem;
                PagerExcluded.MaxItem = maxitem;
                PagerExcluded.TotalRows = totalrows;
                PagerExcluded.MaxPages = Convert.ToInt32(maxpages); //Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalrows) / Convert.ToDecimal(PagerExcluded.CurMaxRows)));
            }
            else
            {
                PagerExcluded.Visible = false;
            }
        }

        private void BindExcludeList(int SearchId, int iPageNum, out int iNewPageNum, out int iMinItem, out int iMaxItem, out int iTotalRows, out decimal dMaxPages)
        {

            dvExcludedLabel.Visible = true;

            BusinessLayer.ContactSearch oContactSearch = new BusinessLayer.ContactSearch();
            string sExcludeList = GetExcludeList();
            DataTable dt = new DataTable();

            if (!string.IsNullOrEmpty(sExcludeList))
            {
                dt = oContactSearch.GetExcludeList(SearchId, userlogin.AccountID, userlogin.UserID, PagerExcluded.CurMaxRows, PagerExcluded.SortBy, PagerExcluded.SortDirection, sExcludeList, iPageNum, out iNewPageNum, out iMinItem, out iMaxItem, out iTotalRows, out dMaxPages);
            }
            else
            {
                iNewPageNum = 0;
                iMinItem = 0;
                iMaxItem = 0;
                iTotalRows = 0;
                dMaxPages = 0;

                dt = new DataTable();
                dt.Columns.Add("");
                dt.Columns.Add("ID");
                dt.Columns.Add("ContactID");
                dt.Columns.Add("AccountID");
                dt.Columns.Add("FirstName");
                dt.Columns.Add("MiddleName");
                dt.Columns.Add("LastName");
                dt.Columns.Add("MobileNumber");
                dt.Columns.Add("EmailAddress");
                dt.Columns.Add("FacebookID");
                dt.Columns.Add("IsDeleted");
                dt.Columns.Add("IsDeletedNum");
                dt.Columns.Add("RowNum");
            }

            grdExcluded.DataSource = dt;
            grdExcluded.DataBind();
        }

        public void Refresh(string curpage)
        {
            int iMinItem, iMaxItem, iTotalRows = 0;
            int iSearchId = 0;


            if (!string.IsNullOrEmpty(SearchID))
            {
                if (int.TryParse(SearchID.ToString(), out iSearchId))
                {
                    int iPageNum = Convert.ToInt32(curpage);
                    int iNewPageNum = iPageNum;
                    decimal dMaxPages = 0;
                    BindData(iSearchId, iPageNum, out iNewPageNum, out iMinItem, out iMaxItem, out iTotalRows, out dMaxPages);
                    SetPager(iNewPageNum, Pager.SortBy, Pager.SortDirection, iTotalRows, iMinItem, iMaxItem, dMaxPages);
                }
            }
            else
            {   dvSearchCriteria.Visible = true;
                    dvSearchCriteria.InnerHtml = "Please run a search and select campaign recipients.";
                    Pager.Visible = false;

                    dvIncludedLabel.Visible = false;
                    dvExcludedLabel.Visible = false;
                    PagerExcluded.Visible = false;
            }
        }

        private void SetPager(int curpage, string sortby, string sortdir, int totalrows, int minitem, int maxitem, decimal maxpages)
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
                Pager.MaxPages = Convert.ToInt32(maxpages);//Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalrows) / Convert.ToDecimal(Pager.CurMaxRows)));
            }
            else
            {
                Pager.Visible = false;
            }
        }

        private void BindData(int iSearchId, int iPageNum, out int iNewPageNum, out int iMinItem, out int iMaxItem, out int iTotalRows, out decimal dMaxPages)
        {
            
            dvIncludedLabel.Visible = true;

            BusinessLayer.ContactSearch oContactSearch = new BusinessLayer.ContactSearch();
            string sExcludeList = GetExcludeList();
            DataTable dt = oContactSearch.GetSearchResult(iSearchId, userlogin.AccountID, userlogin.UserID, Pager.CurMaxRows, Pager.SortBy, Pager.SortDirection, sExcludeList, iPageNum, out iNewPageNum, out iMinItem, out iMaxItem, out iTotalRows, out dMaxPages);
            grdIncluded.DataSource = dt;
            grdIncluded.DataBind();


        }

        private string GetExcludeList()
        {
            string sExcludeList = string.Empty;
            if (ExcludeList != null)
            {
                foreach (string sID in ExcludeList)
                {
                    sExcludeList = string.Concat(sExcludeList, sID, ",");
                }
            }

            if (!string.IsNullOrEmpty(sExcludeList))
                sExcludeList = sExcludeList.Remove(sExcludeList.Length - 1);
            return sExcludeList;
        }

        private void ToggleSearchCriteria(string strText, string strDisplay, string strClass)
        {
            dvSearchCriteria.Attributes.Add("class", strClass);
            dvSearchCriteria.Attributes.Add("style", strDisplay);
            dvSearchCriteria.InnerText = strText;
        }

        private void ToggleCampaignDetailsMessage(string strText, string strDisplay, string strClass, ConstantValues.MessageBodyFormat eFormat = ConstantValues.MessageBodyFormat.Text)
        {
            dvCampaignDetailsMessage.Attributes.Add("class", strClass);
            dvCampaignDetailsMessage.Attributes.Add("style", strDisplay);
            if (eFormat == ConstantValues.MessageBodyFormat.HTML)
            {
                dvCampaignDetailsMessage.InnerHtml = strText;
            }
            else
            {
                dvCampaignDetailsMessage.InnerText = strText;
            }
        }

        private void ToggleRecipientMessage(string strText, string strDisplay, string strClass, ConstantValues.MessageBodyFormat eFormat = ConstantValues.MessageBodyFormat.Text)
        {
            dvRecipientMessage.Attributes.Add("class", strClass);
            dvRecipientMessage.Attributes.Add("style", strDisplay);
            if (eFormat == ConstantValues.MessageBodyFormat.HTML)
            {
                dvRecipientMessage.InnerHtml = strText;
            }
            else
            {
                dvRecipientMessage.InnerText = strText;
            }
        }


        private void ToggleMessageBodyMessage(string strText, string strDisplay, string strClass, ConstantValues.MessageBodyFormat eFormat = ConstantValues.MessageBodyFormat.Text)
        {
            dvMessageBodyMessage.Attributes.Add("class", strClass);
            dvMessageBodyMessage.Attributes.Add("style", strDisplay);
            if (eFormat == ConstantValues.MessageBodyFormat.HTML)
            {
                dvMessageBodyMessage.InnerHtml = strText;
            }
            else
            {
                dvMessageBodyMessage.InnerText = strText;
            }
        }

        private void ToggleSendorScheduleMessage(string strText, string strDisplay, string strClass, ConstantValues.MessageBodyFormat eFormat = ConstantValues.MessageBodyFormat.Text)
        {
            dvSendorScheduleMessage.Attributes.Add("class", strClass);
            dvSendorScheduleMessage.Attributes.Add("style", strDisplay);
            if (eFormat == ConstantValues.MessageBodyFormat.HTML)
            {
                dvSendorScheduleMessage.InnerHtml = strText;
            }
            else
            {
                dvSendorScheduleMessage.InnerText = strText;
            }
        }
        protected void grdIncluded_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void grdIncluded_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper().Trim())
            {
                case "SELECT":
                    Response.Redirect("EditLead.aspx?Guid=" + e.CommandArgument);
                    break;
                case "EXCLUDE":
                    string sID = e.CommandArgument.ToString();
                    int iID = 0;
                    if (int.TryParse(sID, out iID))
                    {
                        if (ExcludeList == null) ExcludeList = new List<string>();
                        ExcludeList.Add(iID.ToString());
                    }
                    Refresh(Pager.PageNum.ToString());
                    RefreshExcludeList(PagerExcluded.PageNum.ToString());
                    hdnActiveTab.Value = "RESULTS";//show search results tab
                    hdnActiveStep.Value = ((int)ConstantValues.CampaignActiveStep.Recipients).ToString();
                    break;
            }
        }

        protected void grdIncluded_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

        protected void grdExcluded_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper().Trim())
            {
                case "SELECT":
                    Response.Redirect("EditLead.aspx?Guid=" + e.CommandArgument);
                    break;
                case "INCLUDE":

                    string sID = e.CommandArgument.ToString();
                    int iID = 0;
                    if (int.TryParse(sID, out iID))
                    {
                        if (ExcludeList != null)
                        {
                            ExcludeList.Remove(iID.ToString());
                        }
                    }
                    Refresh(Pager.PageNum.ToString());
                    RefreshExcludeList(PagerExcluded.PageNum.ToString());
                    hdnActiveTab.Value = "RESULTS";//show search results tab
                    hdnActiveStep.Value = ((int)ConstantValues.CampaignActiveStep.Recipients).ToString();
                    break;
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            ClearAlerts();
            DateTime? dteSendingSchedule = GetSendingSchedule();

            if ((ValidateMessageBody()&&ValidateRecipients()&& ValidateSendSchedule(dteSendingSchedule)))
            {
                Campaign oCam = new Campaign();
                string sBody = GetMessageBody();
                bool bHideInSearch = GetHideInSearch();
                int out_iCampaignID;
                Guid? out_gCampaignGUID;
                int out_iMessageId;
                Guid? out_gMessageUID;
                string sExcludeList = GetExcludeList();
                Guid? in_gCampaignUID = null;
                int? in_iMessageId = null;

                if (CampaignUID != null)
                    in_gCampaignUID = Guid.Parse(CampaignUID);

                if (MessageID != null)
                    in_iMessageId = int.Parse(MessageID);

                if (oCam.CreateCampaign(userlogin.AccountID,
                    in_gCampaignUID,
                    in_iMessageId,
                    txtCampaignName.Text.Trim(),
                    (int)Utilities.ConstantValues.CampaignType.Email,
                    int.Parse(ddlCampaignFormat.SelectedItem.Value),
                    txtCampaignDescription.Text.Trim(),
                    chkUseBounceAddress.Checked,
                    bHideInSearch,
                    (int)Utilities.ConstantValues.CampaignStatus.Submitted,
                    null,
                    int.Parse(ddlSendingOptions.SelectedItem.Value),
                    dteSendingSchedule,
                    Utilities.ConstantValues.CampaignType.Email.ToString().ToUpper().Trim(),
                    txtCampaignSubject.Text.Trim(),
                    txtSenderName.Text.Trim(),
                    txtSenderEmail.Text.Trim(),
                    sBody,
                    SearchID,
                    sExcludeList,
                    userlogin.UserID,
                    Request.UserHostAddress,
                    (int)ConstantValues.EmailJobStatus.Pending,
                    out out_iCampaignID, 
                    out out_gCampaignGUID, 
                    out out_iMessageId, 
                    out out_gMessageUID))
                {
                    hdnActiveStep.Value = ((int)ConstantValues.CampaignActiveStep.SendingOption).ToString();

                    if (int.Parse(ddlSendingOptions.SelectedItem.Value)==(int)ConstantValues.SendingOptions.SendNow)
                    {
                        ShowMessageBasedOnActiveTab("Email Campaign has been sent to queue and will be sent out shortly.", "block", "alert alert-success");
                    }
                    else
                    {
                        ShowMessageBasedOnActiveTab(string.Format("Email Campaign has been sent to queue and will be sent on {0}.", string.Format("{0: dd MMMM yyyy hh:mm tt}", dteSendingSchedule)), "block", "alert alert-success");
                    }

                    btnSend.Attributes.Add("class", "btn btn-primary disabled");

                    CampaignModel mCam = new CampaignModel();
                    Campaign2 oCampaign2 = new Campaign2();
                    mCam = oCampaign2.GetCampaign(userlogin.AccountID, out_gCampaignGUID.Value, userlogin.UserID);

                    MessageID = out_iMessageId.ToString();
                    CampaignUID = out_gCampaignGUID.Value.ToString();
                    hdnCampaignUID.Value = CampaignUID;
                    hdnCampaignStatus.Value = mCam.CampaignStatus.ToString();
                    SetCampaignStatusDetails(mCam);
                    SetRightMenu(mCam);
                    CampaignLeftSearch.RefreshLeftMenu();
                }
                else
                {
                    ShowMessageBasedOnActiveTab("Error creating campaign. Please contact System Administrator.", "block", "alert alert-danger");
                }
            }
        }

        private bool ValidateMessageBody()
        {
            if (ddlCampaignFormat.SelectedItem.Value == ((int)Utilities.ConstantValues.MessageBodyFormat.HTML).ToString())
            {
                if (string.IsNullOrEmpty(txtBody.Text.Trim()))
                {
                    hdnActiveStep.Value = ((int)ConstantValues.CampaignActiveStep.Message).ToString();
                    ToggleMessageBodyMessage("Message Body is a required field.", "block", "alert alert-danger");
                    return false;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtTextEditor.Text.Trim()))
                {
                    hdnActiveStep.Value = ((int)ConstantValues.CampaignActiveStep.Message).ToString();
                    ToggleMessageBodyMessage("Message Body is a required field.", "block", "alert alert-danger");
                    return false;
                }
            }

            return true;
        }

        private bool ValidateRecipients()
        {
            if ((SearchID !=null)&& (Pager.TotalRows>0))
            {
                return true;
            }
            else
            {
                hdnActiveStep.Value = ((int)ConstantValues.CampaignActiveStep.Recipients).ToString();
                ToggleRecipientMessage("Recipients is a required field. Please select campaign recipients.", "block", "alert alert-danger");
                return false;
            }
           
        }
        
        private bool ValidateSendSchedule(DateTime? pSendingSchedule)
        {
            if (ddlSendingOptions.SelectedItem.Value==((int)ConstantValues.SendingOptions.Scheduled).ToString())
            {
                if (pSendingSchedule == null)
                {
                    ToggleSendorScheduleMessage("Please enter Send Schedule Date and Time.","block","alert alert-danger");
                    return false;
                }
                else
                {
                    DateTime dtSendSchedule;
                    if (DateTime.TryParse(pSendingSchedule.ToString(), out dtSendSchedule))
                    {

                        if (dtSendSchedule<=System.DateTime.UtcNow.AddHours(8))
                        {
                            ToggleSendorScheduleMessage("Send Schedule Date and Time must be a future schedule not less than or equal to current date and time.", "block", "alert alert-danger");
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                        
                    }
                    else
                    {
                        ToggleSendorScheduleMessage("Please enter a valid Send Schedule Date and Time.", "block", "alert alert-danger");
                        return false;
                    }
                }   
            }
            else
            {
                return true;
            }
        }

        private DateTime? GetSendingSchedule()
        {
            DateTime dteTempSendingSchedule;
            DateTime? dteSendingSchedule=null;

            if (int.Parse(ddlSendingOptions.SelectedItem.Value)==(int)Utilities.ConstantValues.SendingOptions.Scheduled)
            {
                if (DateTime.TryParse(txtScheduleDate.Text.Trim() + " " + txtScheduleTime.Text.Trim(), out dteTempSendingSchedule))
                {
                    dteSendingSchedule = dteTempSendingSchedule;
                }
                else
                {
                    dteSendingSchedule = null;
                }
            }
            return dteSendingSchedule;
        }


        private string GetMessageBody()
        {
            string sBody = string.Empty;
            if (ddlCampaignFormat.SelectedItem.Value == ((int)Utilities.ConstantValues.MessageBodyFormat.HTML).ToString())
            {
                sBody = txtBody.Text;
                sBody = sBody.Replace("&amp;amp;", "&");
                sBody = sBody.Replace("&amp;", "&");
            }
            else
            {
                sBody = txtTextEditor.Text;
            }
            return sBody;
        }


        private bool GetHideInSearch()
        {
            return rdbYes.Checked;

        }


        private void ClearAlerts()
        {
            ToggleCampaignDetailsMessage(string.Empty, "none", string.Empty);
            ToggleMessageBodyMessage(string.Empty, "none", string.Empty);
            ToggleRecipientMessage(string.Empty, "none", string.Empty);
            ToggleSendorScheduleMessage(string.Empty, "none", string.Empty);
        }

        protected void lnkCopy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CampaignUID))
            {
                ToggleCampaignDetailsMessage("Please select a campaign to copy.", "block", "alert alert-info");
            }
            else
            {
                Response.Redirect(string.Format("~/Campaigns/CampaignBuilder2.aspx?sc={0}", CampaignUID));
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CampaignUID))
            {
                ToggleCampaignDetailsMessage("Please select a campaign to delete.", "block", "alert alert-info");
            }
            else
            {
                BusinessLayer.Campaign cCampaign = new BusinessLayer.Campaign();
                Guid gCampaignUID;
                if (Guid.TryParse(CampaignUID, out gCampaignUID))
                {
                    if (cCampaign.DeleteCampaign(userlogin.AccountID, gCampaignUID, userlogin.UserID))
                    {
                        CampaignLeftSearch.RefreshLeftMenu();
                        Response.Redirect("~/Campaigns/CampaignBuilder2.aspx");
                    }
                    
                }
            }
        }

        protected void lnkCampaignsCrumb_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Campaigns/Dashboard.aspx");
        }
    }
}