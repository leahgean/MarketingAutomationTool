using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer.Controller
{
    public class LoginHistory
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AutoMarketerConnectionString"].ConnectionString.ToString();

        //public DataTable GetLoginHistory(Guid UserId)
        //{
        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();

        //            using (SqlCommand command = connection.CreateCommand())
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "S_GET_USER_ACCESS_LOG";

        //                command.Parameters.AddWithValue("@UserId", UserId);

        //                DataTable dt = new DataTable();
        //                SqlDataAdapter sqlA = new SqlDataAdapter(command);
        //                sqlA.Fill(dt);
                        

        //                return dt;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Logger.Logger.WriteLog(UserId.ToString(), "LoginHistory", UserId.ToString(), "Error", ex.Message);
        //        return null;
        //    }
        //}


        public DataTable GetLoginHistory(Guid UserId, DateTime? LoginDateTimeFrom, DateTime? LoginDateTimeTo, DateTime? LogoutDateTimeFrom, DateTime? LogoutDateTimeTo, string IPAddress, int PageNum, int MaxRows, out int MinItem, out int MaxItem, out int TotalRows)
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
                        command.CommandText = "S_GET_USER_ACCESS_LOG";


                        command.Parameters.AddWithValue("@UserId", UserId);
                        if (LoginDateTimeFrom != null)
                        {
                            command.Parameters.AddWithValue("@LoginDateTimeFrom", LoginDateTimeFrom);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@LoginDateTimeFrom", System.DBNull.Value);
                        }


                        if (LoginDateTimeTo != null)
                        {
                            command.Parameters.AddWithValue("@LoginDateTimeTo", LoginDateTimeTo);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@LoginDateTimeTo", System.DBNull.Value);
                        }


                        if (LogoutDateTimeFrom != null)
                        {
                            command.Parameters.AddWithValue("@LogoutDateTimeFrom", LogoutDateTimeFrom);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@LogoutDateTimeFrom", System.DBNull.Value);
                        }


                        if (LogoutDateTimeTo != null)
                        {
                            command.Parameters.AddWithValue("@LogoutDateTimeTo", LogoutDateTimeTo);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@LogoutDateTimeTo", System.DBNull.Value);
                        }

                       
                        command.Parameters.AddWithValue("@IPAddress", IPAddress);

                        command.Parameters.AddWithValue("@PageNum", PageNum);
                        command.Parameters.AddWithValue("@MaxRows", MaxRows);

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

                Logger.Logger.WriteLog(UserId.ToString(), "LoginHistory-GetLoginHistory", UserId.ToString(), "Error", ex.Message);
                return null;
            }
        }
    }
}
