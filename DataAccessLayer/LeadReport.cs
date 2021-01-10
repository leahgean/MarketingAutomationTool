using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class LeadReport
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AutoMarketerConnectionString"].ConnectionString.ToString();

        public DataTable NewLeadsStatistics(DateTime? FromDate, DateTime? ToDate, string Source, int? TypeID, int? StatusID, Guid AccountID, out int iTotalLeadCnt)
        {
            iTotalLeadCnt = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_LeadReports_NewLeadsStatistics";


                        if (!FromDate.HasValue)
                        {
                            command.Parameters.AddWithValue("@FROM", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@FROM", FromDate);
                        }

                        if (!ToDate.HasValue)
                        {
                            command.Parameters.AddWithValue("@TO", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@TO", ToDate);
                        }

                        if (string.IsNullOrEmpty(Source))
                        {
                            command.Parameters.AddWithValue("@SOURCE", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@SOURCE", Source);
                        }

                        if (!(TypeID.HasValue))
                        {
                            command.Parameters.AddWithValue("@TYPE_ID", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@TYPE_ID", TypeID);
                        }

                        if (!(StatusID.HasValue))
                        {
                            command.Parameters.AddWithValue("@STATUS_ID", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@STATUS_ID", StatusID);
                        }
                      
                        command.Parameters.AddWithValue("@ACCOUNT_ID", AccountID);

                        SqlParameter out_TotalLeadCnt = new SqlParameter("@TOTALLEADCNT", SqlDbType.Int);
                        out_TotalLeadCnt.Direction = ParameterDirection.Output;
                        out_TotalLeadCnt.Value = 0;

                        command.Parameters.Add(out_TotalLeadCnt);

                        DataTable dt = new DataTable();
                        SqlDataAdapter sqlA = new SqlDataAdapter(command);
                        sqlA.Fill(dt);

                        if (out_TotalLeadCnt.Value != DBNull.Value)
                            iTotalLeadCnt = Convert.ToInt32(out_TotalLeadCnt.Value);

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(AccountID.ToString(), "LeadReport-NewLeadsStatistics", string.Empty, "Error", ex.Message);
                return null;
            }
        }


        public DataTable LeadsListing(DateTime? FromDate, DateTime? ToDate, string Source, int? TypeID, int? StatusID, Guid AccountID, out int iTotalLeadCnt)
        {
            iTotalLeadCnt = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_LeadReports_LeadsListing";


                        if (!FromDate.HasValue)
                        {
                            command.Parameters.AddWithValue("@FROM", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@FROM", FromDate);
                        }

                        if (!ToDate.HasValue)
                        {
                            command.Parameters.AddWithValue("@TO", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@TO", ToDate);
                        }

                        if (string.IsNullOrEmpty(Source))
                        {
                            command.Parameters.AddWithValue("@SOURCE", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@SOURCE", Source);
                        }

                        if (!(TypeID.HasValue))
                        {
                            command.Parameters.AddWithValue("@TYPE_ID", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@TYPE_ID", TypeID);
                        }

                        if (!(StatusID.HasValue))
                        {
                            command.Parameters.AddWithValue("@STATUS_ID", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@STATUS_ID", StatusID);
                        }

                        command.Parameters.AddWithValue("@ACCOUNT_ID", AccountID);

                        SqlParameter out_TotalLeadCnt = new SqlParameter("@TOTALLEADCNT", SqlDbType.Int);
                        out_TotalLeadCnt.Direction = ParameterDirection.Output;
                        out_TotalLeadCnt.Value = 0;

                        command.Parameters.Add(out_TotalLeadCnt);

                        DataTable dt = new DataTable();
                        SqlDataAdapter sqlA = new SqlDataAdapter(command);
                        sqlA.Fill(dt);

                        if (out_TotalLeadCnt.Value != DBNull.Value)
                            iTotalLeadCnt = Convert.ToInt32(out_TotalLeadCnt.Value);

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(AccountID.ToString(), "LeadReport-LeadsListing", string.Empty, "Error", ex.Message);
                return null;
            }
        }

        public DataTable DeletedLeadsListing(DateTime? FromDate, DateTime? ToDate, int? TypeID, int? StatusID, Guid AccountID, out int iTotalLeadCnt)
        {
            iTotalLeadCnt = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_LeadReports_DeletedLeadsListing";


                        if (!FromDate.HasValue)
                        {
                            command.Parameters.AddWithValue("@FROM", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@FROM", FromDate);
                        }

                        if (!ToDate.HasValue)
                        {
                            command.Parameters.AddWithValue("@TO", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@TO", ToDate);
                        }

                        if (!(TypeID.HasValue))
                        {
                            command.Parameters.AddWithValue("@TYPE_ID", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@TYPE_ID", TypeID);
                        }

                        if (!(StatusID.HasValue))
                        {
                            command.Parameters.AddWithValue("@STATUS_ID", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@STATUS_ID", StatusID);
                        }

                        command.Parameters.AddWithValue("@ACCOUNT_ID", AccountID);

                        SqlParameter out_TotalLeadCnt = new SqlParameter("@TOTALLEADCNT", SqlDbType.Int);
                        out_TotalLeadCnt.Direction = ParameterDirection.Output;
                        out_TotalLeadCnt.Value = 0;

                        command.Parameters.Add(out_TotalLeadCnt);

                        DataTable dt = new DataTable();
                        SqlDataAdapter sqlA = new SqlDataAdapter(command);
                        sqlA.Fill(dt);

                        if (out_TotalLeadCnt.Value != DBNull.Value)
                            iTotalLeadCnt = Convert.ToInt32(out_TotalLeadCnt.Value);

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(AccountID.ToString(), "LeadReport-DeletedLeadsListing", string.Empty, "Error", ex.Message);
                return null;
            }
        }

        public DataTable UnsubscribedLeadsListing(DateTime? FromDate, DateTime? ToDate, string Source, int? TypeID, int? StatusID, Guid AccountID, out int iTotalLeadCnt)
        {
            iTotalLeadCnt = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_LeadReports_UnsubscribedLeadsListing";


                        if (!FromDate.HasValue)
                        {
                            command.Parameters.AddWithValue("@FROM", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@FROM", FromDate);
                        }

                        if (!ToDate.HasValue)
                        {
                            command.Parameters.AddWithValue("@TO", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@TO", ToDate);
                        }

                        if (string.IsNullOrEmpty(Source))
                        {
                            command.Parameters.AddWithValue("@SOURCE", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@SOURCE", Source);
                        }

                        if (!(TypeID.HasValue))
                        {
                            command.Parameters.AddWithValue("@TYPE_ID", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@TYPE_ID", TypeID);
                        }

                        if (!(StatusID.HasValue))
                        {
                            command.Parameters.AddWithValue("@STATUS_ID", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@STATUS_ID", StatusID);
                        }

                        command.Parameters.AddWithValue("@ACCOUNT_ID", AccountID);

                        SqlParameter out_TotalLeadCnt = new SqlParameter("@TOTALLEADCNT", SqlDbType.Int);
                        out_TotalLeadCnt.Direction = ParameterDirection.Output;
                        out_TotalLeadCnt.Value = 0;

                        command.Parameters.Add(out_TotalLeadCnt);

                        DataTable dt = new DataTable();
                        SqlDataAdapter sqlA = new SqlDataAdapter(command);
                        sqlA.Fill(dt);

                        if (out_TotalLeadCnt.Value != DBNull.Value)
                            iTotalLeadCnt = Convert.ToInt32(out_TotalLeadCnt.Value);

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(AccountID.ToString(), "LeadReport-UnsubscribedLeadsListing", string.Empty, "Error", ex.Message);
                return null;
            }
        }


        public DataTable DuplicateLeadsListing(Guid AccountID, out int iTotalLeadCnt)
        {
            iTotalLeadCnt = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_LeadReports_DuplicateLeadsListing";
                        
                        command.Parameters.AddWithValue("@ACCOUNT_ID", AccountID);

                        SqlParameter out_TotalLeadCnt = new SqlParameter("@TOTALLEADCNT", SqlDbType.Int);
                        out_TotalLeadCnt.Direction = ParameterDirection.Output;
                        out_TotalLeadCnt.Value = 0;

                        command.Parameters.Add(out_TotalLeadCnt);

                        DataTable dt = new DataTable();
                        SqlDataAdapter sqlA = new SqlDataAdapter(command);
                        sqlA.Fill(dt);

                        if (out_TotalLeadCnt.Value != DBNull.Value)
                            iTotalLeadCnt = Convert.ToInt32(out_TotalLeadCnt.Value);

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(AccountID.ToString(), "LeadReport-DuplicateLeadsListing", string.Empty, "Error", ex.Message);
                return null;
            }
        }
    }
}
