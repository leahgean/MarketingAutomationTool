using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class UserModel
    {
        public Guid UserID { get; set; }
        public Guid AccountID { get; set; }
        public string UserName { get; set; }
        public string SavedPassword { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string CountryId { get; set; }
        public string PasswordQuestion { get; set; }
        public string PasswordAnswer { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastLogoutDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public string PasswordSalt { get; set; }
        public int ID { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public bool HasChangedSystemPassword { get; set; }
        public bool IsOwner { get; set; }
        public string Position { get; set; }
        public bool IsSuperAdminUser { get; set; }

    }
}
