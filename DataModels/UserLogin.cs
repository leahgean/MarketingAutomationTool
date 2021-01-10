using System;
using System.Runtime.Serialization;

namespace DataModels 
{
    [Serializable()]
    public class UserLogin
    {
        public Guid UserID;
        public Guid AccountID;
        public string UserName;
        public string SavedPassword;
        public string FirstName;
        public string MiddleName;
        public string LastName;
        public string MobileNumber;
        public string PhoneNumber;
        public string EmailAddress;
        public string CountryId;
        public string PasswordQuestion;
        public string PasswordAnswer;
        public DateTime? LastLoginDate;
        public DateTime? LastLogoutDate;
        public DateTime? CreatedDate;
        public Guid CreatedBy;
        public DateTime? ModifiedDate;
        public Guid? ModifiedBy;
        public bool IsAdmin;
        public bool IsActive;
        public string PasswordSalt;
        public int ID;
        public string Address;
        public string City;
        public string State;
        public string ZipCode;
        public bool HasChangedSystemPassword;
        public bool IsOwner;
        public string Position;
        public bool IsSuperAdminUser;
    }
}
