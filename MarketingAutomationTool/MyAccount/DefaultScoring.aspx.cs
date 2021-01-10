using System;
using System.Web.UI;
using BusinessLayer;
using DataModels;

namespace MarketingAutomationTool.MyAccount
{
    public partial class DefaultScoring : System.Web.UI.Page
    {
        public DataModels.UserLogin userlogin
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

        public int DefaultScoreId
        {
            get
            {

                if (ViewState["DefaultScoreId"] != null)
                {
                    return int.Parse(ViewState["DefaultScoreId"].ToString());
                }
                else
                {
                    return -1;
                }
            }
            set
            {
                ViewState["DefaultScoreId"] = value;
            }
        }

        private void AssignDefaultScore()
        {
            userlogin = (DataModels.UserLogin)Session["loggeduser"];

            if (userlogin == null)
                Response.Redirect("~/Public/Login.aspx");

            DefaultScore ds = new DefaultScore();
            DefaultScoreModel dsModel = ds.GetDefaultScore(userlogin.AccountID);

            if (dsModel != null)
            {
                DefaultScoreId = dsModel.Id;
                txtFormSubmittingForm.Text = dsModel.Form_Submit.ToString();
                txtFormConfReqEmBo.Text = dsModel.Form_Confirmation_Email_Bounce.ToString();
                txtFormUnsubsFromConfEm.Text = dsModel.Form_Unsubscribe_From_Confirmation_Email.ToString();
                txtFormAckEmBounces.Text = dsModel.Form_Acknowledgment_Email_Bounce.ToString();
                txtFormFirstOpenAckEm.Text = dsModel.Form_Acknowledgment_Email_First_Open.ToString();
                txtFormSubsequentOpen.Text = dsModel.Form_Subsequent_Open.ToString();
                txtFormUnsubscribeFromAckEm.Text = dsModel.Form_Unsubscribe_From_Acknowledgment_Email.ToString();
                txtFormFirstClickAck.Text = dsModel.Form_Click_Link_On_Acknowledgment_Email.ToString();
                txtFormSubsequenctClicks.Text = dsModel.Form_Subsequent_click.ToString();

                txtEmailMessageBounces.Text = dsModel.Email_Bounce.ToString();
                txtEmailUnsubscribeEmail.Text = dsModel.Email_Unsubscribe.ToString();
                txtEmailFirstClick.Text = dsModel.Email_Click_Link_First.ToString();
                txtEmailSubsequentClicks.Text = dsModel.Email_Subsequent_Click.ToString();

                txtSMSMessageBounces.Text = dsModel.SMS_Bounce.ToString();
                txtSMSUnsubscribeAfterSMS.Text = dsModel.SMS_Unsubscribe.ToString();
            }
            else
            {
                txtFormSubmittingForm.Text = string.Empty;
                txtFormConfReqEmBo.Text = string.Empty;
                txtFormUnsubsFromConfEm.Text = string.Empty;
                txtFormAckEmBounces.Text = string.Empty;
                txtFormFirstOpenAckEm.Text = string.Empty;
                txtFormSubsequentOpen.Text = string.Empty;
                txtFormUnsubscribeFromAckEm.Text = string.Empty;
                txtFormFirstClickAck.Text = string.Empty;
                txtFormSubsequenctClicks.Text = string.Empty;

                txtEmailMessageBounces.Text = string.Empty;
                txtEmailUnsubscribeEmail.Text = string.Empty;
                txtEmailFirstClick.Text = string.Empty;
                txtEmailSubsequentClicks.Text = string.Empty;

                txtSMSMessageBounces.Text = string.Empty;
                txtSMSUnsubscribeAfterSMS.Text = string.Empty;
            }
            
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                AssignDefaultScore();
            }

        }

        private void SaveScore()
        {
            DefaultScore ds = new DefaultScore();
            bool result = false;
         
            short sFormSubmittingForm = 0;
            short sFormConfReqEmBo = 0;
            short sFormUnsubsFromConfEm = 0;
            short sFormAckEmBounces = 0;
            short sFormFirstOpenAckEm = 0;
            short sFormSubsequentOpen = 0;
            short sFormUnsubscribeFromAckEm = 0;
            short sFormFirstClickAck = 0;
            short sFormSubsequenctClicks = 0;
            short sEmailMessageBounces = 0;
            short sEmailUnsubscribeEmail = 0;
            short sEmailFirstClick = 0;
            short sEmailSubsequentClicks = 0;
            short sSMSMessageBounces = 0;
            short sSMSUnsubscribeAfterSMS = 0;

            if (!short.TryParse(txtFormSubmittingForm.Text.Trim(), out sFormSubmittingForm))
            {
                sFormSubmittingForm = 0;
            }

            if (!short.TryParse(txtFormConfReqEmBo.Text.Trim(), out sFormConfReqEmBo))
            {
                sFormConfReqEmBo = 0;
            }

            if (!short.TryParse(txtFormUnsubsFromConfEm.Text.Trim(), out sFormUnsubsFromConfEm))
            {
                sFormUnsubsFromConfEm = 0;
            }

            if (!short.TryParse(txtFormAckEmBounces.Text.Trim(), out sFormAckEmBounces))
            {
                sFormAckEmBounces = 0;
            }

            if (!short.TryParse(txtFormFirstOpenAckEm.Text.Trim(), out sFormFirstOpenAckEm))
            {
                sFormFirstOpenAckEm = 0;
            }

            if (!short.TryParse(txtFormSubsequentOpen.Text.Trim(), out sFormSubsequentOpen))
            {
                sFormSubsequentOpen = 0;
            }

            if (!short.TryParse(txtFormUnsubscribeFromAckEm.Text.Trim(), out sFormUnsubscribeFromAckEm))
            {
                sFormUnsubscribeFromAckEm = 0;
            }

            if (!short.TryParse(txtFormFirstClickAck.Text.Trim(), out sFormFirstClickAck))
            {
                sFormFirstClickAck = 0;
            }

            if (!short.TryParse(txtFormSubsequenctClicks.Text.Trim(), out sFormSubsequenctClicks))
            {
                sFormSubsequenctClicks = 0;
            }

            if (!short.TryParse(txtEmailMessageBounces.Text.Trim(), out sEmailMessageBounces))
            {
                sEmailMessageBounces = 0;
            }

            if (!short.TryParse(txtEmailUnsubscribeEmail.Text.Trim(), out sEmailUnsubscribeEmail))
            {
                sEmailUnsubscribeEmail = 0;
            }

            if (!short.TryParse(txtEmailFirstClick.Text.Trim(), out sEmailFirstClick))
            {
                sEmailFirstClick = 0;
            }

            if (!short.TryParse(txtEmailSubsequentClicks.Text.Trim(), out sEmailSubsequentClicks))
            {
                sEmailSubsequentClicks = 0;
            }

            if (!short.TryParse(txtSMSMessageBounces.Text.Trim(), out sSMSMessageBounces))
            {
                sSMSMessageBounces = 0;
            }

            if (!short.TryParse(txtSMSUnsubscribeAfterSMS.Text.Trim(), out sSMSUnsubscribeAfterSMS))
            {
                sSMSUnsubscribeAfterSMS = 0;
            }

            if (DefaultScoreId == -1)
            {
                result = ds.InsertDefaultScore(userlogin.AccountID,
                    sFormSubmittingForm,
                    sFormConfReqEmBo,
                    sFormUnsubsFromConfEm,
                    sFormAckEmBounces,
                    sFormFirstOpenAckEm,
                    sFormSubsequentOpen,
                    sFormUnsubscribeFromAckEm,
                    sFormFirstClickAck,
                    sFormSubsequenctClicks,
                    sEmailMessageBounces,
                    sEmailUnsubscribeEmail,
                    sEmailFirstClick,
                    sEmailSubsequentClicks,
                    sSMSMessageBounces,
                    sSMSUnsubscribeAfterSMS);

                if (result)
                {
                    ToggleMessage("Success! Default Scoring was added.", "display:block", "alert alert-success");
                }
                else
                {
                    ToggleMessage("Error adding Default Scoring.", "display:block", "alert alert-danger");
                }
            }
            else
            {
                result = ds.UpdateDefaultScore(DefaultScoreId,
                    userlogin.AccountID,
                     sFormSubmittingForm,
                    sFormConfReqEmBo,
                    sFormUnsubsFromConfEm,
                    sFormAckEmBounces,
                    sFormFirstOpenAckEm,
                    sFormSubsequentOpen,
                    sFormUnsubscribeFromAckEm,
                    sFormFirstClickAck,
                    sFormSubsequenctClicks,
                    sEmailMessageBounces,
                    sEmailUnsubscribeEmail,
                    sEmailFirstClick,
                    sEmailSubsequentClicks,
                    sSMSMessageBounces,
                    sSMSUnsubscribeAfterSMS);

                if (result)
                {
                    ToggleMessage("Success! Default Scoring was updated.", "display:block", "alert alert-success");
                }
                else
                {
                    ToggleMessage("Error updating Default Scoring.", "display:block", "alert alert-danger");
                }
            }
        }

       

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveScore();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            AssignDefaultScore();
        }


        private void ToggleMessage(string strText, string strDisplay, string strClass)
        {
            dvMessage.Attributes.Add("class", strClass);
            dvMessage.Attributes.Add("style", strDisplay);
            dvMessage.InnerText = strText;
        }

        protected void lnkSaveScore_Click(object sender, EventArgs e)
        {
            SaveScore();
        }

        protected void lnkBreadHome_Click(object sender, EventArgs e)
        {
            //Session["maintab"] = "HOME";
            Response.Redirect("~/Dashboard.aspx");
        }
    }
}