using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using CSDataSqlCommand;
using System.Configuration;
using DataModels;

namespace DataAccessLayer.Controller
{
   public class ContactSearchExport
   {
        private string connectionString = ConfigurationManager.ConnectionStrings["AutoMarketerConnectionString"].ConnectionString.ToString();

        public bool AddContactSearchExportFiles(int SearchID, Guid AccountID, Guid CreatedBy, string FileTimeStamp, string FileName)
        { 
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        try
                        {
                            /*1.Add Contact Search*/
                            command.CommandText = "S_ContactSearchExportFiles_Insert";
                            command.Parameters.AddWithValue("@SearchID", SearchID);
                            command.Parameters.AddWithValue("@AccountID", AccountID);
                            command.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                            command.Parameters.AddWithValue("@FileTimeStamp", FileTimeStamp);
                            command.Parameters.AddWithValue("@FileName", FileName);

                            command.CommandType = CommandType.StoredProcedure;
                            command.ExecuteNonQuery();
                            return true;

                        }
                        catch (Exception ex)
                        {
                            
                            Logger.Logger.WriteLog(CreatedBy.ToString(), "ContactSearchExport-AddContactSearchExportFiles", SearchID.ToString(), "Error", ex.Message);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(CreatedBy.ToString(), "ContactSearchExport-AddContactSearchExportFiles", SearchID.ToString(), "Error", ex.Message);
                return false;
            }
        }

        public string GetExportFile(int SearchID, Guid AccountID, string FileTimeStamp)
        {
            try
            {
                SqlParameter[] sqlParam =
                {
                  new SqlParameter("@AccountID", SqlDbType.UniqueIdentifier) { Value = AccountID },
                  new SqlParameter("@SearchID", SqlDbType.Int) { Value = SearchID },
                  new SqlParameter("@FileTimeStamp", SqlDbType.NVarChar,250) { Value = FileTimeStamp }
                };

                string result = SqlHelper.ExecuteScalar(connectionString, @"SELECT dbo.fn_ContactSearchExportFiles_GetFile(@AccountID,@SearchID,@FileTimeStamp)", System.Data.CommandType.Text, sqlParam).ToString();

                return result;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(AccountID.ToString(), "ContactSearchExport-GetExportFile", SearchID.ToString(), "Error", ex.Message);
                return string.Empty;
            }
        }


        

    }
}
