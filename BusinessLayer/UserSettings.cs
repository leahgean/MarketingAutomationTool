using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class UserSettings
    {
        public List<DataModels.UserSettingsModel> GetUserAccountSettings(Guid AccountId, string SettingCategory)
        {
            DataAccessLayer.Controller.UserSettings acc = new DataAccessLayer.Controller.UserSettings();
            List<DataModels.UserSettingsModel> usraccsetList = acc.GetUserAccountSettings(AccountId, SettingCategory);
            return usraccsetList;
        }


        public bool AddUpdateUserSettings(Guid Account_Id, Guid User_Id, string Setting_Category, string Setting_Key, string Setting_Value, int Sort_Order, bool Built_In)
        {
            DataAccessLayer.Controller.UserSettings usr = new DataAccessLayer.Controller.UserSettings();
            return usr.AddUpdateUserSettings(Account_Id, User_Id, Setting_Category, Setting_Key, Setting_Value, Sort_Order, Built_In);
        }

    }
}
