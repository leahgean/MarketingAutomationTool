using System;
using System.Data;


namespace BusinessLayer
{
    public class Account
    {
        public bool UniqueEmail(string strEmailAddress)
        {
            DataAccessLayer.Controller.Account acc = new DataAccessLayer.Controller.Account();
            return acc.UniqueEmail(strEmailAddress);
        }

       


        public bool CreateAccount(string strAccountName,
                                string strCompanyWebsite,
                                string strPhoneNumber,
                                string strFaxNo,
                                string strEmailAddress,
                                string strAddress,
                                string strCity,
                                string strState,
                                string strZipCode,
                                int iCountryCode,
                                string strUserName,
                                string strPassword,
                                string strFirstName,
                                string strMiddleName,
                                string strLastName,
                                string strMobileNumber,
                                bool blnIsAdminUser,
                                bool blnIsActiveUser,
                                bool blnIsOwnerUser,
                                bool blnIsSuperAdminUser,
                                string strPasswordSalt,
                                System.Guid gCreatedBy,
                                bool blnIsActiveAccount, 
                                bool blnIsSuperAdminAccount,
                                string CreatedFromIP)
        {
            DataAccessLayer.Controller.Account acc = new DataAccessLayer.Controller.Account();
            return acc.CreateAccount(strAccountName, strCompanyWebsite, strPhoneNumber, strFaxNo, strEmailAddress, strAddress, strCity, strState, strZipCode, iCountryCode, strUserName, strPassword, strFirstName, strMiddleName, strLastName, strMobileNumber,  strEmailAddress, blnIsAdminUser, blnIsActiveUser, blnIsOwnerUser, blnIsSuperAdminUser, strPasswordSalt, gCreatedBy, blnIsActiveAccount, blnIsSuperAdminAccount, CreatedFromIP);
        }


        public bool UpdateAccountDetail(
                               string strRegistrationNumber,
                               string strCompanyWebsite,
                               string strAddress,
                               string strCity,
                               string strState,
                               string strZipCode,
                               int iCountryCode,
                               System.Guid gModifiedBy,
                               System.Guid gAccountId,
                               string strEmail_Admin,
                               string strEmail_Finances,
                               string strEmail_Technical,
                               string strEmail_Marketing,
                               string strName_Admin,
                               string strName_Finances,
                               string strName_Technical,
                               string strName_Marketing)
        {
            DataAccessLayer.Controller.Account acc = new DataAccessLayer.Controller.Account();
            return acc.UpdateAccountDetails(strRegistrationNumber, strAddress, strCity, strState, strZipCode, iCountryCode, strCompanyWebsite,gModifiedBy, gAccountId, strEmail_Admin, strEmail_Finances, strEmail_Technical, strEmail_Marketing, strName_Admin, strName_Finances, strName_Technical, strName_Marketing);
        }


        public DataModels.AccountModel GetAccountDetails(Guid AccountId)
        {
            DataAccessLayer.Controller.Account acc = new DataAccessLayer.Controller.Account();
            DataModels.AccountModel accModel =  acc.GetAccountDetails(AccountId);
            return accModel;
        }

        public DataTable GetAccounts(int PageNum, int MaxRows, string SortBy, string SortDirection, string AccountName, string IsActive, out int MinItem, out int MaxItem, out int TotalRows)
        {
            DataAccessLayer.Controller.Account acc = new DataAccessLayer.Controller.Account();
            DataTable dtAccounts = acc.GetAccounts(PageNum, MaxRows, SortBy, SortDirection, AccountName, IsActive, out MinItem, out MaxItem, out TotalRows);
            return dtAccounts;
        }

        public string GetAccountName(Guid AccountId, Guid UserId)
        {
            DataAccessLayer.Controller.Account cAccount = new DataAccessLayer.Controller.Account();
            return cAccount.GetAccountName(AccountId, UserId);
        }


        public string GetAccountEmail(Guid AccountId, Guid UserId)
        {
            DataAccessLayer.Controller.Account cAccount = new DataAccessLayer.Controller.Account();
            return cAccount.GetAccountEmail(AccountId, UserId);
        }

        public DataTable GetAccountUsage(Guid AccountID, int Year, Guid UserId)
        {
            DataAccessLayer.Controller.Account cAccount = new DataAccessLayer.Controller.Account();
            return cAccount.GetAccountUsage(AccountID, Year, UserId);
        }

    }
}
