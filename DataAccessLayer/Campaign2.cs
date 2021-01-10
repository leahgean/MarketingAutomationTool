using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;

namespace DataAccessLayer.Controller
{
   public class Campaign2
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AutoMarketerConnectionString"].ConnectionString.ToString();

        public CampaignModel GetCampaign(Guid AccountId, Guid CampaignUID, Guid UserId)
        {
            try
            {
                DataAccessLayer.DataClasses1DataContext dc = new DataAccessLayer.DataClasses1DataContext(connectionString);
                CampaignModel cam = new CampaignModel();
                DataAccessLayer.Campaign cObj = dc.Campaigns.FirstOrDefault(c => c.CampaignUID == CampaignUID && c.AccountId == AccountId);

                cam.Id = cObj.Id;
                cam.CampaignUID = cObj.CampaignUID;
                cam.AccountId = cObj.AccountId;
                cam.CampaignName = cObj.CampaignName;
                cam.CampaignType = cObj.CampaignType;
                cam.CampaignFormat = cObj.CampaignFormat;
                cam.CampaignDescription = cObj.CampaignDescription;
                cam.MessageId = cObj.MessageId;
                cam.UseBounceAddressInFromField = cObj.UseBounceAddressInFromField;
                cam.HideInSearch = cObj.HideInSearch;
                cam.CampaignStatus = cObj.CampaignStatus;
                cam.TemplateId = cObj.TemplateId;
                cam.CreatedDate = cObj.CreatedDate;
                cam.CreatedBy = cObj.CreatedBy;
                cam.ModifiedDate = cObj.ModifiedDate;
                cam.ModifiedBy = cObj.ModifiedBy;
                cam.Deleted = cObj.Deleted;
                cam.DeletedDate = cObj.DeletedDate;
                cam.DeletedBy = cObj.DeletedBy;
                cam.SendingOption = cObj.SendingOption;
                cam.SendingSchedule = cObj.SendingSchedule;
                cam.SearchID = cObj.SearchID;
                cam.ExcludeList = cObj.ExcludeList;
                cam.EnableScoring = cObj.EnableScoring;
                cam.IPAddress = cObj.IPAddress;
                cam.SubmittedDate = cObj.SubmittedDate;
                cam.SubmittedBy = cObj.SubmittedBy;

                if (cObj.Message!=null)
                {
                    MessageModel msg = new MessageModel();
                    msg.Id = cObj.Message.Id;
                    msg.MessageUID = cObj.Message.MessageUID;
                    msg.AccountId = cObj.Message.AccountId;
                    msg.MessageFormat = cObj.Message.MessageFormat;
                    msg.Entity = cObj.Message.Entity;
                    msg.Subject = cObj.Message.Subject;
                    msg.SenderName = cObj.Message.SenderName;
                    msg.SenderEmail = cObj.Message.SenderEmail;
                    msg.MessageBody = cObj.Message.MessageBody;
                    msg.CreatedDate = cObj.Message.CreatedDate;
                    msg.CreatedBy = cObj.Message.CreatedBy;
                    msg.ModifiedDate = cObj.Message.ModifiedDate;
                    msg.ModifiedBy = cObj.Message.ModifiedBy;
                    msg.Deleted = cObj.Message.Deleted;
                    msg.DeletedDate = cObj.Message.DeletedDate;
                    msg.DeletedBy = cObj.Message.DeletedBy;
                    cam.Message = msg;
                }
                else
                {
                    cam.Message = null;
                }

                cam.CampaignContacts = new List<CampaignContacts>();

                foreach (DataAccessLayer.CampaignContact cco in cObj.CampaignContacts.ToList())
                {
                    CampaignContacts camco = new CampaignContacts();

                    camco.Id = cco.Id;
                    camco.AccountId = cco.AccountId;
                    camco.CampaignID = cco.CampaignID;
                    camco.ContactID = cco.ContactID;
                    camco.CampaignScore = cco.CampaignScore;
                    camco.CreatedBy = cco.CreatedBy;
                    camco.CreatedDate = cco.CreatedDate;
                    camco.ModifiedBy = cco.ModifiedBy;
                    camco.ModifiedDate = cco.ModifiedDate;

                    camco.Contact = new DataModels.Lead();
                    camco.Contact.ContactID = cco.Contact.ContactID;
                    camco.Contact.AccountID = cco.Contact.AccountID.Value;
                    camco.Contact.FirstName = cco.Contact.FirstName;
                    camco.Contact.MiddleName = cco.Contact.MiddleName;
                    camco.Contact.LastName = cco.Contact.LastName;
                    camco.Contact.MobileNumber = cco.Contact.MobileNumber;
                    camco.Contact.PhoneNumber = cco.Contact.PhoneNumber;
                    camco.Contact.EmailAddress = cco.Contact.EmailAddress;
                    camco.Contact.Address1 = cco.Contact.Address1;
                    camco.Contact.Address2 = cco.Contact.Address2;
                    camco.Contact.CountryId = cco.Contact.CountryId;
                    camco.Contact.ZipCode = cco.Contact.ZipCode;
                    camco.Contact.CompanyName = cco.Contact.CompanyName;
                    camco.Contact.CreatedDate = cco.Contact.CreatedDate.Value;
                    camco.Contact.CreatedBy = cco.Contact.CreatedBy.Value;
                    camco.Contact.ModifiedDate = cco.Contact.ModifiedDate;
                    camco.Contact.ModifiedBy = cco.Contact.ModifiedBy;
                    camco.Contact.WebSite = cco.Contact.WebSite;
                    camco.Contact.Position = cco.Contact.Position;
                    camco.Contact.FacebookID = cco.Contact.FacebookID;
                    camco.Contact.ID = cco.Contact.ID;
                    camco.Contact.LeadStatus = cco.Contact.LeadStatus;
                    camco.Contact.ContactStatus = cco.Contact.ContactStatus;
                    camco.Contact.ContactType = cco.Contact.ContactType;
                    camco.Contact.Title = cco.Contact.Title;
                    if (cco.Contact.Gender.HasValue)
                    {
                        camco.Contact.Gender = cco.Contact.Gender.Value.ToString();
                    }
                    else
                    {
                        camco.Contact.Gender = string.Empty;
                    };

                    camco.Contact.City = cco.Contact.City;
                    camco.Contact.State = cco.Contact.State;

                    if (cco.Contact.SubscribedToEmail.HasValue)
                    {
                        camco.Contact.SubscribedToEmail = cco.Contact.SubscribedToEmail.Value;
                    }
                    else
                    {
                        camco.Contact.SubscribedToEmail = true;
                    }

                    if (cco.Contact.UseforTesting.HasValue)
                    {
                        camco.Contact.UseforTesting = cco.Contact.UseforTesting.Value;
                    }
                    else
                    {
                        camco.Contact.UseforTesting = false;
                    }

                    camco.Contact.IsDeleted = cco.Contact.IsDeleted;
                    camco.Contact.DeletedDate = cco.Contact.DeletedDate;

                    cam.CampaignContacts.Add(camco);
                }
                return cam;
            }
            catch(Exception ex)
            {
                Logger.Logger.WriteLog(UserId.ToString(), "Campaign2-GetCampaign", CampaignUID.ToString(), "Error", ex.Message);
                return null;
            }
        }

        public bool InsertEmailCampaignsSent(DataModels.EmailCampaignsSent pECS)
        {
            try
            {
                DataAccessLayer.DataClasses1DataContext dc = new DataAccessLayer.DataClasses1DataContext(connectionString);

                DataAccessLayer.EmailCampaignsSent oECS = new DataAccessLayer.EmailCampaignsSent();
               
                oECS.AccountUID = pECS.AccountUID;

                oECS.CampaignId = pECS.CampaignId;
                oECS.MessageId = pECS.MessageId;
                oECS.ContactID = pECS.ContactID;
                oECS.EmailAddress = pECS.EmailAddress;
                oECS.EmailSent = pECS.EmailSent;
                oECS.ErrorMessage = pECS.ErrorMessage;
                oECS.CreatedDate = pECS.CreatedDate;
                oECS.CreatedBy = pECS.CreatedBy;
                oECS.ModifiedDate = pECS.ModifiedDate;
                oECS.ModifiedBy = pECS.ModifiedBy;
                oECS.IPAddress = pECS.IPAddress;

                dc.EmailCampaignsSents.InsertOnSubmit(oECS);
                dc.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(pECS.CreatedBy.ToString(), "Campaign2-InsertEmailCampaignsSent", pECS.CampaignId.ToString(), "Error", ex.Message);
                return false;
            }
        }


        public bool InsertUnsubscribe(DataModels.CampaignUnsubscribes pCU)
        {
            try
            {
                DataAccessLayer.DataClasses1DataContext dc = new DataAccessLayer.DataClasses1DataContext(connectionString);

                DataAccessLayer.CampaignUnsubscribe oCU = new DataAccessLayer.CampaignUnsubscribe();

                oCU.AccountID = pCU.AccountID;
                oCU.CampaignID = pCU.CampaignID;
                oCU.ContactId = pCU.ContactId;
                oCU.CreatedBy = pCU.CreatedBy;
                oCU.CreatedDate = pCU.CreatedDate;
                oCU.IPAddress = pCU.IPAddress;

                dc.CampaignUnsubscribes.InsertOnSubmit(oCU);
                dc.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(pCU.ContactId.ToString(), "Campaign2-InsertUnsubscribe", pCU.CampaignID.ToString(), "Error", ex.Message);
                return false;
            }
        }

        public bool InsertClickthroughs(DataModels.CampaignClickthroughs pCC)
        {
            try
            {
                DataAccessLayer.DataClasses1DataContext dc = new DataAccessLayer.DataClasses1DataContext(connectionString);

                DataAccessLayer.CampaignClickThrough oCC = new DataAccessLayer.CampaignClickThrough();

                oCC.AccountID = pCC.AccountID;
                oCC.CampaignID = pCC.CampaignID;
                oCC.ContactId = pCC.ContactId;
                oCC.Link = pCC.Link;
                oCC.CreatedBy = pCC.CreatedBy;
                oCC.CreatedDate = pCC.CreatedDate;
                oCC.IPAddress = pCC.IPAddress;

                dc.CampaignClickThroughs.InsertOnSubmit(oCC);
                dc.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(pCC.ContactId.ToString(), "Campaign2-InsertClickthroughs", pCC.CampaignID.ToString(), "Error", ex.Message);
                return false;
            }
        }


        public bool InsertEmailsOpened(DataModels.CampaignOpened pCO)
        {
            try
            {
                DataAccessLayer.DataClasses1DataContext dc = new DataAccessLayer.DataClasses1DataContext(connectionString);

                DataAccessLayer.CampaignsOpened oCO = new DataAccessLayer.CampaignsOpened();

                oCO.AccountID = pCO.AccountID;
                oCO.CampaignID = pCO.CampaignID;
                oCO.ContactID = pCO.ContactId;
                oCO.CreatedBy = pCO.CreatedBy;
                oCO.CreatedDate = pCO.CreatedDate;
                oCO.IPAddress = pCO.IPAddress;

                dc.CampaignsOpeneds.InsertOnSubmit(oCO);
                dc.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(pCO.ContactId.ToString(), "Campaign2-InsertEmailsOpened", pCO.CampaignID.ToString(), "Error", ex.Message);
                return false;
            }
        }


        public int? SelectCampaignId(Guid AccountId, Guid CampaignId, Guid UserId)
        {
            try
            {
                DataAccessLayer.DataClasses1DataContext dc = new DataAccessLayer.DataClasses1DataContext(connectionString);

                DataAccessLayer.Campaign oC = new DataAccessLayer.Campaign();
                oC =dc.Campaigns.FirstOrDefault(e=> e.AccountId==AccountId && e.CampaignUID == CampaignId);
                return oC.Id;

            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(UserId.ToString(), "Campaign2-SelectCampaign", CampaignId.ToString(), "Error", ex.Message);
                return null;
            }
        }


        public int? SelectMessageId(Guid AccountId, Guid CampaignId, Guid UserId)
        {
            try
            {
                DataAccessLayer.DataClasses1DataContext dc = new DataAccessLayer.DataClasses1DataContext(connectionString);

                DataAccessLayer.Campaign oC = new DataAccessLayer.Campaign();
                oC = dc.Campaigns.FirstOrDefault(e => e.AccountId == AccountId && e.CampaignUID == CampaignId);
                return oC.MessageId;

            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(UserId.ToString(), "Campaign2-SelectCampaign", CampaignId.ToString(), "Error", ex.Message);
                return null;
            }
        }

        public string GetCampaignName(int CampaignId, Guid AccountId, Guid UserId)
        {
            try
            {
                DataAccessLayer.DataClasses1DataContext dc = new DataAccessLayer.DataClasses1DataContext(connectionString);

                DataAccessLayer.Campaign oC = new DataAccessLayer.Campaign();
                oC = dc.Campaigns.FirstOrDefault(e => e.AccountId == AccountId && e.Id == CampaignId);
                return oC.CampaignName;

            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(UserId.ToString(), "Campaign2-GetCampaignName", CampaignId.ToString(), "Error", ex.Message);
                return null;
            }
        }
    }
}
