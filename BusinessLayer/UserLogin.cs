using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class UserLogin
    {
        public DataModels.UserLogin GetUserByUserName(string username)
        {
            DataAccessLayer.Controller.UserLogin ulogin = new DataAccessLayer.Controller.UserLogin();
            DataModels.UserLogin user = ulogin.GetUserByUserName(username);
            return user;
        }

        public DataModels.UserLogin GetUserByAccountId(Guid accountid)
        {
            DataAccessLayer.Controller.UserLogin ulogin = new DataAccessLayer.Controller.UserLogin();
            DataModels.UserLogin user = ulogin.GetUserByAccountId(accountid);
            return user;
        }

        public bool LogUserAccess(Guid userid, string ipAddress, string sessionId)
        {
            DataAccessLayer.Controller.UserLogin ulogin = new DataAccessLayer.Controller.UserLogin();
            bool result = ulogin.LogUserAccess(userid,ipAddress,sessionId);
            return result;
        }

        public bool UpdateUserAccess(string sessionId, Guid userid)
        {
            DataAccessLayer.Controller.UserLogin ulogin = new DataAccessLayer.Controller.UserLogin();
            bool result = ulogin.LogUserLogout(sessionId, userid);
            return result;
        }


        public bool UpdatePassword(string newpassword, string passwordsalt, Guid userid)
        {
            DataAccessLayer.Controller.UserLogin ulogin = new DataAccessLayer.Controller.UserLogin();
            bool result = ulogin.UpdatePassword(newpassword, passwordsalt, userid);
            return result;
        }

        public bool SetSystemPasswordChanged(Guid userid)
        {
            DataAccessLayer.Controller.UserLogin ulogin = new DataAccessLayer.Controller.UserLogin();
            bool result = ulogin.SetSystemPasswordChanged(userid);
            return result;
        }


        public bool GetHasChangedSystemPassword(Guid userid)
        {
            DataAccessLayer.Controller.UserLogin ulogin = new DataAccessLayer.Controller.UserLogin();
            bool result = ulogin.GetHasChangedSystemPassword(userid);
            return result;
        }

        public List<DataModels.UserLogin> GetUserNameByEmailAddress(string EmailAddress)
        {
            DataAccessLayer.Controller.UserLogin ulogin = new DataAccessLayer.Controller.UserLogin();
            List<DataModels.UserLogin> usernames = new List<DataModels.UserLogin>();
            usernames = ulogin.GetUserNameByEmailAddress(EmailAddress);
            return usernames;
        }


        public bool ResetHasChangedSystemPassword(Guid userid)
        {
            DataAccessLayer.Controller.UserLogin ulogin = new DataAccessLayer.Controller.UserLogin();
            bool result = ulogin.ResetHasChangedSystemPassword(userid);
            return result;
        }
    }
}
