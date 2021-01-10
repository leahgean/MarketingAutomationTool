using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CSDataSqlCommand;
using System.Configuration;
using System.Data;

namespace DataAccessLayer.Controller
{
    public class UserSettings
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AutoMarketerConnectionString"].ConnectionString.ToString();

        public List<DataModels.UserSettingsModel> GetUserAccountSettings(Guid AccountId, string SettingCategory)
        {
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter();
                sqlParam[0].ParameterName = "AccountId";
                sqlParam[0].Value = AccountId;
                sqlParam[0].Direction = System.Data.ParameterDirection.Input;

                sqlParam[1] = new SqlParameter();
                sqlParam[1].ParameterName = "SettingCategory";
                sqlParam[1].Value = SettingCategory;
                sqlParam[1].Direction = System.Data.ParameterDirection.Input;

                SqlDataReader sqlDR = SqlHelper.ExecuteReader(connectionString, "SELECT Setting_Id, Account_Id, [User_Id], Setting_Category, Setting_Key, Setting_Value, Sort_Order, Date_Modified, Modified_By, Date_Created, Created_By, Built_In FROM User_Account_Setting WITH (NOLOCK) WHERE Account_Id = @AccountId AND Setting_Category = @SettingCategory ORDER BY Sort_Order", System.Data.CommandType.Text, sqlParam);

                List<DataModels.UserSettingsModel> usrsetlist = null; 
                DataModels.UserSettingsModel usrset = null;

                if (sqlDR.HasRows)
                {
                    usrsetlist = new List<DataModels.UserSettingsModel>();
                    while (sqlDR.Read())
                    {
                        usrset = new DataModels.UserSettingsModel();
                        usrset.Setting_Id = Convert.ToInt32(sqlDR["Setting_Id"].ToString());
                        usrset.Account_Id = Guid.Parse(sqlDR["Account_Id"].ToString());
                        usrset.User_Id = Guid.Parse(sqlDR["User_Id"].ToString());
                        usrset.Setting_Category = sqlDR["Setting_Category"].ToString();
                        usrset.Setting_Key = sqlDR["Setting_Key"].ToString();
                        usrset.Setting_Value = sqlDR["Setting_Value"].ToString();
                        if (sqlDR["Sort_Order"] == System.DBNull.Value)
                        {
                            usrset.Sort_Order = 0;
                        }
                        else
                        {
                            usrset.Sort_Order = Convert.ToInt32(sqlDR["Sort_Order"].ToString());
                        }
                        usrset.Date_Modified = Convert.ToDateTime(sqlDR["Date_Modified"].ToString());
                        usrset.Modified_By = Guid.Parse(sqlDR["Modified_By"].ToString());
                        usrset.Date_Created = Convert.ToDateTime(sqlDR["Date_Created"].ToString());
                        usrset.Created_By = Guid.Parse(sqlDR["Created_By"].ToString());
                        usrset.Built_In = Convert.ToBoolean(sqlDR["Built_In"].ToString());

                        usrsetlist.Add(usrset);
                    }
                }

                sqlDR.Close();
                
                return usrsetlist;
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(AccountId.ToString(), "UserSettings-GetAccountDetails", AccountId.ToString(), "Error", ex.Message);
                return null;
            }
        }


        public bool AddUpdateUserSettings(Guid Account_Id, Guid User_Id, string Setting_Category, string Setting_Key, string Setting_Value,int Sort_Order, bool Built_In)
        {
            try
            {
                SqlParameter[] sqlParam =
                {
                  new SqlParameter("@Account_Id ", SqlDbType.UniqueIdentifier) { Value = Account_Id },
                  new SqlParameter("@User_Id", SqlDbType.UniqueIdentifier) { Value = User_Id },
                  new SqlParameter("@Setting_Category", SqlDbType.NVarChar, 64) { Value = Setting_Category },
                  new SqlParameter("@Setting_Key", SqlDbType.NVarChar,256) { Value = Setting_Key },
                  new SqlParameter("@Setting_Value ", SqlDbType.NVarChar,-1) { Value = Setting_Value },
                  new SqlParameter("@Sort_Order", SqlDbType.Int) { Value = Sort_Order },
                  new SqlParameter("@Built_In", SqlDbType.Bit) { Value = Built_In }
              };

                int success = SqlHelper.ExecuteNonQuery(connectionString, true, "S_ADD_UPDATE_USER_ACCOUNT_SETTING", System.Data.CommandType.StoredProcedure, sqlParam);

                return success == 0 ? false : true;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(User_Id.ToString(), "UserSettings-InsertUserSettings", Account_Id.ToString(), "Error", ex.Message);
                return false;

            }

        }


       

    }
}
