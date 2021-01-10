using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using MarketingAutomationTool.Utilities;
using EnterpriseLibrary.LeadService.FileAsset;
using EnterpriseLibrary;

namespace MarketingAutomationTool.Leads
{
    public partial class ImportLeads : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            userlogin = (DataModels.UserLogin)Session["loggeduser"];

            if (userlogin == null)
                Response.Redirect("~/Public/Login.aspx");

            if (!Page.IsPostBack)
            {
                DataTable dt = new DataTable();
                ContactList cl = new ContactList();
                dt=cl.GetListNames(userlogin.AccountID, false);

                ddlList.DataSource = dt;
                ddlList.DataTextField = "ListName";
                ddlList.DataValueField = "ID";
                ddlList.DataBind();

                ddlList.Items.Insert(0, new ListItem("Don't add leads to list.", string.Empty));

            }
        }

        protected void lnkBreadHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard.aspx");
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string strFileExtension = string.Empty;
            string strOriginalFileName = fleUpload.FileName.Trim();
            string strUploadedFileName = string.Empty;
            int? iContactListId = null;

            if (!string.IsNullOrEmpty(ddlList.SelectedItem.Value))
            {
                iContactListId = int.Parse(ddlList.SelectedItem.Value);
            }


            strFileExtension= System.IO.Path.GetExtension(strOriginalFileName).Remove(0,1);
            
            if (string.Compare(strFileExtension, ConstantValues.ContactListJobFileValidExtension.ToString())!=0)
            {
                ToggleMessage("Error! Invalid file format. Please use import template.", "display:block", "alert alert-danger");
            }
            else
            {
                strUploadedFileName = string.Format(@"{0}_{1}_{2}_{3}", userlogin.ID, userlogin.UserName, DateTime.UtcNow.AddHours(8).ToString("ddMyyyyhhmmss"), strOriginalFileName);

                FileAssetManager fsManager = new FileAssetManager(userlogin.UserID,userlogin.AccountID);

                Result result = fsManager.CreateUploadFile(strUploadedFileName, fleUpload.FileBytes);

                if (result.Success)
                {
                    ContactList cl = new ContactList();

                    string strIPAddress = Request.UserHostAddress;

                    if (cl.CreateContactListJob(txtJobName.Text.Trim(), (int)ConstantValues.ContactJobStatus.PENDING, userlogin.AccountID, userlogin.UserID, strFileExtension, strOriginalFileName, strUploadedFileName, iContactListId, strIPAddress))
                    {
                        ToggleMessage(string.Format("Your file has been successfully uploaded and will be processed shortly. An email will be sent to {0} after the contacts have been added to the system.", userlogin.EmailAddress), "display:block", "alert alert-success");
                    }
                    else
                    {
                        ToggleMessage("Error! An occurred creating import job. Please contact system administrator.", "display:block", "alert alert-danger");
                    }
                }
                else
                {
                    ToggleMessage(string.Format("{0} {1} {2}","Error!",result.ErrorMessage,"Please contact system administrator."), "display:block", "alert alert-danger");
                }

            }
        }

        protected void btnViewExistingJobs_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewExistingImportJobs.aspx");
        }

        private void ToggleMessage(string strText, string strDisplay, string strClass)
        {
            dvMessage.Attributes.Add("class", strClass);
            dvMessage.Attributes.Add("style", strDisplay);
            dvMessage.InnerText = strText;
        }
    }
}