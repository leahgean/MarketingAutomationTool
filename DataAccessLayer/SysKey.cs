using System;
using CSDataSqlCommand;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataAccessLayer.Controller
{
    public class SysKey
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AutoMarketerConnectionString"].ConnectionString.ToString();

        public string GetKey(string KeyName)
        {
            try
            {
                SqlParameter[] sqlParam =
                {
                  new SqlParameter("@SysKey ", SqlDbType.NVarChar,50) { Value = KeyName }
                };

                string result = (string)SqlHelper.ExecuteScalar(connectionString, @"SELECT ISNULL(SYS_VALUE,'') SYS_VALUE FROM Sys_Key WITH (NOLOCK) WHERE Sys_Key=@SysKey", System.Data.CommandType.Text, sqlParam);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(KeyName.ToString(), "SysKey-GetKey", string.Empty, "Error", ex.Message);
                return string.Empty;
            }
        }
    }
}
