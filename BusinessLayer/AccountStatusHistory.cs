using System.Data;
using System;
using DataAccessLayer;

namespace BusinessLayer
{
    public class AccountStatusHistory
    {
        public DataTable GetAccountStatusHistory(Guid AccountID)
        {
            DataAccessLayer.Controller.AccountStatusHistory acc = new DataAccessLayer.Controller.AccountStatusHistory();
            DataTable dtAccountStatusHistory = acc.GetAccountStatusHistory(AccountID);
            return dtAccountStatusHistory;
        }

        public bool UpdateAccountStatus(string Status, string IP, Guid AccountId, Guid ModifiedBy)
        {
            DataAccessLayer.Controller.AccountStatusHistory acc = new DataAccessLayer.Controller.AccountStatusHistory();
            return acc.UpdateAccountStatus(Status, IP, AccountId, ModifiedBy);
        }
    }
}
