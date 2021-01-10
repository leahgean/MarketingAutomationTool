using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using CSDataSqlCommand;

namespace DataAccessLayer.Controller
{
    public class AccountStatusHistory
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AutoMarketerConnectionString"].ConnectionString.ToString();

    
        public DataTable GetAccountStatusHistory(Guid AccountId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = "	SELECT A.Id, A.AccountId, CASE A.[Status] WHEN 'ACT' THEN 'Active' ELSE 'Inactive' END [Status], A.DateChanged, A.IP, A.ChangedBy, U.FirstName + ' ' + U.LastName ChangedByName FROM Account_Status_History A WITH (NOLOCK) INNER JOIN [User] U WITH (NOLOCK) ON A.ChangedBy = U.UserID WHERE A.AccountID=@AccountID";

                        command.Parameters.AddWithValue("@AccountID", AccountId);

                        DataTable dt = new DataTable();
                        SqlDataAdapter sqlA = new SqlDataAdapter(command);
                        sqlA.Fill(dt);

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(AccountId.ToString(), "AccountStatusHistory-GetAccountStatusHistory", string.Empty, "Error", ex.Message);
                return null;
            }
        }



        public bool UpdateAccountStatus(string Status, string IP, Guid AccountId, Guid ModifiedBy)
        {
            try
            {
                SqlParameter[] sqlParam =
           {
              new SqlParameter("@Status ", SqlDbType.Char, 3) { Value = Status },
              new SqlParameter("@IP", SqlDbType.VarChar,15) { Value = IP },
              new SqlParameter("@AccountID ", SqlDbType.UniqueIdentifier) { Value = AccountId },
              new SqlParameter("@ModifiedBy", SqlDbType.UniqueIdentifier) { Value = ModifiedBy }
            };

                int success = SqlHelper.ExecuteNonQuery(connectionString, true, "S_ACCOUNT_UPDATESTATUS", System.Data.CommandType.StoredProcedure, sqlParam);

                return success == 0 ? false : true;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(AccountId.ToString(), "AccountStatusHistory-UpdateAccountStatus", AccountId.ToString(), "Error", ex.Message);
                return false;

            }
        }
    }
}
