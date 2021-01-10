using System;
using CSDataSqlCommand;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace DataAccessLayer.Controller
{
    public class Country
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AutoMarketerConnectionString"].ConnectionString.ToString();

        public DataTable GetCountries()
        {
            try
            {
                DataTable dtResult = SqlHelper.SqlDataFill(@"SELECT CountryId, CountryName FROM Country WITH (NOLOCK)", CommandType.Text, connectionString);
                return dtResult;
            }
            catch(Exception ex)
            {
                Logger.Logger.WriteLog(string.Empty, "Country-GetCountries", string.Empty, "Error", ex.Message);
                return null;
            }
        }

        public string GetCountry(int CountryId)
        {
            try
            {
                SqlParameter[] sqlParam =
                {
                  new SqlParameter("@CountryId ", SqlDbType.Int) { Value = CountryId }
                };

                string result = (string)SqlHelper.ExecuteScalar(connectionString, @"SELECT CountryName FROM Country WITH (NOLOCK) WHERE CountryId=@CountryId", System.Data.CommandType.Text, sqlParam);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(CountryId.ToString(), "Country-GetCountry", string.Empty, "Error", ex.Message);
                return string.Empty;
            }
        }

    }
}
