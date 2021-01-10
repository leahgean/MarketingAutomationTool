using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
   public class Lead
   {
        public bool UniqueEmail(string strEmailAddress, Guid accountID, Guid? contactID)
        {
            DataAccessLayer.Controller.Lead objLead = new DataAccessLayer.Controller.Lead();
            return objLead.UniqueEmail(strEmailAddress, accountID, contactID);
        }

        public bool AddNewLead(Guid AccountID,
        string Title,
        string FirstName,
        string MiddleName,
        string LastName,
        string CompanyName,
        string WebSite,
        string Position,
        int? ContactType,
        int? LeadStatus,
        int? ContactStatus,
        string Gender,
        string EmailAddress,
        string PhoneNumber,
        string MobileNumber,
        string FacebookID,
        string Address1,
        string Address2,
        string City,
        string State,
        int? CountryId,
        string ZipCode,
        bool IsSubscribedToEmail,
        bool UseForTesting,
        Guid CreatedBy,
        string IPAddress, 
        out Guid? LeadGUID )
        {
            LeadGUID = null;
            DataAccessLayer.Controller.Lead cLead = new DataAccessLayer.Controller.Lead();
            return cLead.CreateLead(AccountID, Title, FirstName, MiddleName, LastName, CompanyName, WebSite, Position, ContactType, LeadStatus, ContactStatus, Gender, EmailAddress, PhoneNumber, MobileNumber, FacebookID, Address1, Address2, City, State, CountryId, ZipCode, IsSubscribedToEmail, UseForTesting, CreatedBy,IPAddress, out LeadGUID);
        }

        public DataModels.Lead SelectLead(Guid LeadGUID, Guid AccountID)
        {
            DataAccessLayer.Controller.Lead cLead = new DataAccessLayer.Controller.Lead();
            return cLead.SelectLead(LeadGUID, AccountID);
        }

        public bool UpdateLead(Guid AccountID,
        Guid ContactID,
        string Title,
        string FirstName,
        string MiddleName,
        string LastName,
        string CompanyName,
        string WebSite,
        string Position,
        int? ContactType,
        int? LeadStatus,
        int? ContactStatus,
        string Gender,
        string EmailAddress,
        string PhoneNumber,
        string MobileNumber,
        string FacebookID,
        string Address1,
        string Address2,
        string City,
        string State,
        int? CountryId,
        string ZipCode,
        bool IsSubscribedToEmail,
        bool UseForTesting,
        Guid ModifiedBy,
        string IPAddress,
        bool SubscriptionChanged)
        {
            DataAccessLayer.Controller.Lead cLead = new DataAccessLayer.Controller.Lead();
            return cLead.UpdateLead(AccountID, ContactID, Title, FirstName, MiddleName, LastName, CompanyName, WebSite, Position, ContactType, LeadStatus, ContactStatus, Gender, EmailAddress, PhoneNumber, MobileNumber, FacebookID, Address1, Address2, City, State, CountryId, ZipCode, IsSubscribedToEmail, UseForTesting, ModifiedBy, IPAddress, SubscriptionChanged);
        }


        public bool DeleteLead(Guid AccountID,
        Guid ContactID,
        Guid ModifiedBy,
        bool SubcriptionChanged,
        string EmailAddress,
        int? CountryId,
        string City,
        string State,
        string IPAddress)
        {
            DataAccessLayer.Controller.Lead cLead = new DataAccessLayer.Controller.Lead();
            return cLead.DeleteLead(AccountID, ContactID, ModifiedBy, SubcriptionChanged,EmailAddress,CountryId,City,State,IPAddress);
        }

        public DataTable GetRecentLeads(Guid AccountID, Guid CreatedBy, int MaxRows, string SortBy, string SortDirection, int PageNum, out int NewPageNum, out int MinItem, out int MaxItem, out int TotalRows, out decimal MaxPages)
        {
            DataAccessLayer.Controller.Lead cLead = new DataAccessLayer.Controller.Lead();
            return cLead.GetRecentLeads(AccountID, CreatedBy, MaxRows, SortBy,SortDirection, PageNum, out NewPageNum, out MinItem, out MaxItem, out TotalRows, out MaxPages );
        }

        public DataTable GetContactPerCountry(Guid AccountID)
        {
            DataAccessLayer.Controller.Lead cLead = new DataAccessLayer.Controller.Lead();
            return cLead.GetContactPerCountry(AccountID);
        }

        public DataTable GetLeadTotalByMonth(Guid AccountID)
        {
            DataAccessLayer.Controller.Lead cLead = new DataAccessLayer.Controller.Lead();
            return cLead.GetLeadTotalByMonth(AccountID);
        }

        public DataTable GetLeadsForSideBar(Guid AccountID, int? Status, bool? Deleted, string SearchText)
        {
            DataAccessLayer.Controller.Lead cLead = new DataAccessLayer.Controller.Lead();
            return cLead.GetLeadsForSideBar(AccountID, Status, Deleted, SearchText);
        }

        public DataTable GetSource(Guid AccountID)
        {
            DataAccessLayer.Controller.Lead cLead = new DataAccessLayer.Controller.Lead();
            return cLead.GetSource(AccountID);
        }


        public DataTable GetUnsubscribeSource(Guid AccountID)
        {
            DataAccessLayer.Controller.Lead cLead = new DataAccessLayer.Controller.Lead();
            return cLead.GetUnsubscribeSource(AccountID);
        }

        public bool Unsubscribe(Guid AccountID,Guid ContactID,int CampaignId, int MessageId,int ContactAction, Guid ModifiedBy, string IPAddress,string UnsubscribedVia,bool SubscriptionChanged,string EmailAddress,int? CountryId,string City,string State)
        {
            DataAccessLayer.Controller.Lead cLead = new DataAccessLayer.Controller.Lead();
            return cLead.Unsubscribe( AccountID,ContactID,CampaignId,MessageId,ContactAction,ModifiedBy,IPAddress,UnsubscribedVia,SubscriptionChanged,EmailAddress,CountryId,City,State);
        }
  }

   
}
