using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class AccountModel
    {
        public System.Guid AccountID;
        public string AccountName;
        public string CompanyWebsite;
        public string CompanyPhone;
        public string FaxNo;
        public string CompanyEmail;
        public string Address;
        public string City;
        public string State;
        public string ZipCode;
        public int CountryId;
        public string CompanyLogo;
        public bool Personalize;
        public string PersonalizedURL;
        public DateTime CreatedDate;
        public System.Guid? CreatedBy;
        public DateTime? ModifiedDate;
        public System.Guid? ModifiedBy;
        public int Id;
        public string RegistrationNumber;
        public string Email_Admin;
        public string Email_Finances;
        public string Email_Technical;
        public string Email_Marketing;
        public string Name_Admin;
        public string Name_Finances;
        public string Name_Technical;
        public string Name_Marketing;
        public bool IsActive;
        public bool IsSuperAdmin;
        public string CreatedFromIP;

    }
}
