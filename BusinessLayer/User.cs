using System;
using System.Collections.Generic;
using System.Data;

namespace BusinessLayer
{
   public class User
    {
        public DataModels.UserModel GetAccountOwnerByAccountId(Guid AccountId)
        {
            DataAccessLayer.Controller.User usr = new DataAccessLayer.Controller.User();
            DataModels.UserModel user = usr.GetAccountOwnerByAccountId(AccountId);
            return user;
        }

        //public List<DataModels.UserModel> GetUsersByAccountId(int PageNum, int MaxRows, string SortBy, string SortDirection, Guid AccountId, out int  MinItem, out int MaxItem, out int TotalRows)
        //{
        //    DataAccessLayer.Controller.User usr = new DataAccessLayer.Controller.User();
        //    List<DataModels.UserModel> user = usr.GetUsersByAccountId(PageNum, MaxRows, SortBy,  SortDirection, AccountId, out MinItem, out MaxItem, out TotalRows);
        //    return user;
        //}

        public DataTable GetUsersByAccountId(int PageNum, int MaxRows, string SortBy, string SortDirection, Guid AccountId, string FirstName, string LastName, string UserName, out int MinItem, out int MaxItem, out int TotalRows)
        {
            DataAccessLayer.Controller.User usr = new DataAccessLayer.Controller.User();
            DataTable dtUser = usr.GetUsersByAccountId(PageNum, MaxRows, SortBy, SortDirection, AccountId, FirstName, LastName, UserName, out MinItem, out MaxItem, out TotalRows);
            return dtUser;
        }

        public DataModels.UserModel GetUserDetailByUserId(Guid UserId)
        {
            DataAccessLayer.Controller.User usr = new DataAccessLayer.Controller.User();
            DataModels.UserModel user = usr.GetUserDetailByUserId(UserId);
            return user;
        }

        public bool UniqueEmail_Update(Guid AccountID, string strEmailAddress, Guid UserID)
        {
            DataAccessLayer.Controller.User usr = new DataAccessLayer.Controller.User();
            return usr.UniqueEmail_Update(AccountID, strEmailAddress, UserID);
        }

        public bool UniqueEmail(string strEmailAddress)
        {
            DataAccessLayer.Controller.User usr = new DataAccessLayer.Controller.User();
            return usr.UniqueEmail(strEmailAddress);
        }

        public bool SaveUserDetail(Guid AccountId, Guid UserId, Guid LoggedUserId,  string FirstName, string LastName, string Position, string EmailAddress, string Mobile, bool IsEnabled)
        {
            DataAccessLayer.Controller.User usr = new DataAccessLayer.Controller.User();
            bool result = usr.SaveUserDetails(AccountId, UserId, FirstName, LastName, Position, EmailAddress, Mobile, IsEnabled, LoggedUserId);
            return result;
        }


        public bool CreateUser( Guid AccountId,string UserName,string Password,string FirstName,string MiddleName,string LastName,string MobileNumber,string PhoneNumber,string EmailAddress,string Address,string City,string State,string ZipCode,int CountryId,bool IsAdminUser,bool IsActiveUser,bool IsOwnerUser,bool IsSuperAdminUser,string PasswordSalt,System.Guid CreatedBy)
        {
            DataAccessLayer.Controller.User usr = new DataAccessLayer.Controller.User();
            bool result = usr.CreateUser(AccountId, UserName, Password, FirstName, MiddleName, LastName, MobileNumber, PhoneNumber, EmailAddress,City, Address, State, ZipCode, CountryId, IsAdminUser, IsActiveUser, IsOwnerUser, IsSuperAdminUser, PasswordSalt, CreatedBy);
            return result;
        }

        public int UniqueUserName(string strUserName)
        {
            DataAccessLayer.Controller.User usr = new DataAccessLayer.Controller.User();
            return usr.UniqueUserName(strUserName);
        }

        public string GetUserName(Guid UserId, Guid AccountId)
        {
            DataAccessLayer.Controller.User usr = new DataAccessLayer.Controller.User();
            return usr.GetUserName(UserId,AccountId);
        }
    }
}
