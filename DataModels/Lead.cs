using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class Lead
    {
        public Guid ContactID;
        public Guid AccountID;
        public string FirstName;
        public string MiddleName;
        public string LastName;
        public string MobileNumber;
        public string PhoneNumber;
        public string EmailAddress;
        public string Address1;
        public string Address2;
        public int? CountryId;
        public string ZipCode;
        public string CompanyName;
        public DateTime CreatedDate;
        public Guid CreatedBy;
        public DateTime? ModifiedDate;
        public Guid? ModifiedBy;
        public string WebSite;
        public string Position;
        public string FacebookID;
        public long ID;
        public int? LeadStatus;
        public int? ContactStatus;
        public int? ContactType;
        public string Title;
        public string Gender;
        public string City;
        public string State;
        public bool SubscribedToEmail;
        public bool UseforTesting;
        public bool IsDeleted;
        public DateTime? DeletedDate;
    }
}
