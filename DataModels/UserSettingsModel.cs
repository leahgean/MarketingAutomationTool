using System;


namespace DataModels
{
    public class UserSettingsModel
    {
        public int Setting_Id;
        public Guid Account_Id;
        public Guid User_Id;
        public string Setting_Category;
        public string Setting_Key;
        public string Setting_Value;
        public int Sort_Order;
        public DateTime Date_Modified;
        public Guid Modified_By;
        public DateTime Date_Created;
        public Guid Created_By;
        public bool Built_In;
    }
}
