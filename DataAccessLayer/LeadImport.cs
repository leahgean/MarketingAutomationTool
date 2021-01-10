using System;
using System.Data.SqlClient;
using System.Data;
using CSDataSqlCommand;
using System.Configuration;
using DataModels;


namespace DataAccessLayer.Controller
{
    public class LeadImport
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AutoMarketerConnectionString"].ConnectionString.ToString();

    
        public DataTable GetExistingImports(Guid AccountID, int PageNum, int PageSize, string SortColumn, string SortOrder, out int MinItem, out int MaxItem, out int TotalRows)
        {
            MinItem = 0;
            MaxItem = 0;
            TotalRows = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_ContactListJob_GetExistingImports";

                        command.Parameters.AddWithValue("@AccountID", AccountID);
                        command.Parameters.AddWithValue("@PageNum", PageNum);
                        command.Parameters.AddWithValue("@MaxRows", PageSize);
                        command.Parameters.AddWithValue("@SortBy", SortColumn);
                        command.Parameters.AddWithValue("@SortDirection", SortOrder);

                        SqlParameter out_MinItem = new SqlParameter("@MinItem", SqlDbType.Int);
                        out_MinItem.Direction = ParameterDirection.InputOutput;
                        out_MinItem.Value = 0;

                        command.Parameters.Add(out_MinItem);

                        SqlParameter out_MaxItem = new SqlParameter("@MaxItem", SqlDbType.Int);
                        out_MaxItem.Direction = ParameterDirection.InputOutput;
                        out_MaxItem.Value = 0;

                        command.Parameters.Add(out_MaxItem);

                        SqlParameter out_TotalRows = new SqlParameter("@TotalRows", SqlDbType.Int);
                        out_TotalRows.Direction = ParameterDirection.InputOutput;
                        out_TotalRows.Value = 0;

                        command.Parameters.Add(out_TotalRows);

                        DataTable dt = new DataTable();
                        SqlDataAdapter sqlA = new SqlDataAdapter(command);
                        sqlA.Fill(dt);

                        if (out_MinItem.Value != DBNull.Value)
                            MinItem = Convert.ToInt32(out_MinItem.Value);

                        if (out_MaxItem.Value != DBNull.Value)
                            MaxItem = Convert.ToInt32(out_MaxItem.Value);

                        if (out_TotalRows.Value != DBNull.Value)
                            TotalRows = Convert.ToInt32(out_TotalRows.Value);

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(AccountID.ToString(), "LeadImport-S_ContactListJob_GetExistingImports", string.Empty, "Error", ex.Message);
                return null;
            }
        }

        public ContactJob GetContactJob(int JobId, Guid AccountId)
        {
           

           

            try
            {
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter();
                sqlParam[0].ParameterName = "@JobId";
                sqlParam[0].Value = JobId;
                sqlParam[0].Direction = System.Data.ParameterDirection.Input;

                sqlParam[1] = new SqlParameter();
                sqlParam[1].ParameterName = "@AccountID";
                sqlParam[1].Value = AccountId;
                sqlParam[1].Direction = System.Data.ParameterDirection.Input;


                SqlDataReader sqlDR = SqlHelper.ExecuteReader(connectionString, "S_GET_ContactJob", System.Data.CommandType.StoredProcedure, sqlParam);
                ContactJob cj = null;

                if (sqlDR.HasRows)
                {
                    while (sqlDR.Read())
                    {
                        cj = new ContactJob();
                        cj.JobId = int.Parse(sqlDR["JobId"].ToString().Trim());
                        cj.JobName = sqlDR["JobName"].ToString().Trim();
                        cj.JobStatusId = int.Parse(sqlDR["JobStatusId"].ToString().Trim());
                        cj.AccountID = Guid.Parse(sqlDR["AccountID"].ToString().Trim());
                        cj.CreatedBy = Guid.Parse(sqlDR["CreatedBy"].ToString().Trim());
                        cj.FileFormat = sqlDR["FileFormat"].ToString().Trim();
                        cj.OriginalFileName = sqlDR["OriginalFileName"].ToString().Trim();
                        cj.FileName = sqlDR["FileName"].ToString().Trim();

                        if (sqlDR["ContactListId"] != System.DBNull.Value)
                        {
                            cj.ContactListId = int.Parse(sqlDR["ContactListId"].ToString().Trim());
                        }
                        else
                        {
                            cj.ContactListId = null;
                        }

                        cj.DateCreated = DateTime.Parse(sqlDR["DateCreated"].ToString().Trim());

                        if (sqlDR["JobStarted"] != System.DBNull.Value)
                        {
                            cj.JobStarted = DateTime.Parse(sqlDR["JobStarted"].ToString().Trim());
                        }
                        else
                        {
                            cj.JobStarted = null;
                        }

                        if (sqlDR["JobFinished"] != System.DBNull.Value)
                        {
                            cj.JobFinished = DateTime.Parse(sqlDR["JobFinished"].ToString().Trim());
                        }
                        else
                        {
                            cj.JobFinished = null;
                        }

                        cj.Error = sqlDR["Error"].ToString().Trim();

                        if (sqlDR["TotalContacts"] != System.DBNull.Value)
                        {
                            cj.TotalContacts = int.Parse(sqlDR["TotalContacts"].ToString().Trim());
                        }
                        else
                        {
                            cj.TotalContacts = null;
                        }

                        if (sqlDR["UploadedContacts"] != System.DBNull.Value)
                        {
                            cj.UploadedContacts = int.Parse(sqlDR["UploadedContacts"].ToString().Trim());
                        }
                        else
                        {
                            cj.UploadedContacts = null;
                        }

                        if (sqlDR["CurrentRowParsedInExcel"] != System.DBNull.Value)
                        {
                            cj.CurrentRowParsedInExcel = int.Parse(sqlDR["CurrentRowParsedInExcel"].ToString().Trim());
                        }
                        else
                        {
                            cj.CurrentRowParsedInExcel = null;
                        }

                    }
                }

                sqlDR.Close();

                return cj;
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(JobId.ToString(), "LeadImport-GetContactJob", AccountId.ToString(), "Error", ex.Message);
                return null;
            }
        }
    }
}
