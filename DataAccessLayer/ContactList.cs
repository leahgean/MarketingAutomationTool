using CSDataSqlCommand;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer.Controller
{
    public class ContactList
    {
       private string connectionString = ConfigurationManager.ConnectionStrings["AutoMarketerConnectionString"].ConnectionString.ToString();

        public bool UniqueListName(string ListName, Guid AccountId, int? ContactListId)
        {
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[3];

                sqlParam[0] = new SqlParameter("@ListName", SqlDbType.NVarChar, 100);
                sqlParam[0].Value = ListName;

                sqlParam[1] = new SqlParameter("@AccountId", SqlDbType.UniqueIdentifier);
                sqlParam[1].Value = AccountId;

                sqlParam[2] = new SqlParameter("@ContactListId", SqlDbType.Int);
                if (ContactListId == null)
                {
                    sqlParam[2].Value = System.DBNull.Value;
                }
                else
                {
                    sqlParam[2].Value = ContactListId;
                }

                bool result = (bool)SqlHelper.ExecuteScalar(connectionString, @"SELECT dbo.fn_NewList_UniqueListName(@ListName,@AccountId, @ContactListId)", System.Data.CommandType.Text, sqlParam);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(ListName, "ContactList-UniqueListName", string.Empty, "Error", ex.Message);
                return false;
            }
        }

        public bool CreateContactList(Guid AccountID,
                                   string ListName,
                                   string ListDescription,
                                   Guid CreatedBy,
                                   out int ContactListId)
        {
            ContactListId = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_ContactList_Insert";

                        command.Parameters.AddWithValue("@AccountID", AccountID);

                        command.Parameters.AddWithValue("@ListName", ListName);

                        if (string.IsNullOrEmpty(ListDescription))
                        {
                            command.Parameters.AddWithValue("@ListDescription", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@ListDescription", ListDescription);
                        }

                        command.Parameters.AddWithValue("@CreatedBy", CreatedBy);

                        SqlParameter out_ContactListId = new SqlParameter("@Id", SqlDbType.Int);
                        out_ContactListId.Direction = ParameterDirection.Output;
                        command.Parameters.Add(out_ContactListId);

                        int success = command.ExecuteNonQuery();

                        if (out_ContactListId.Value != DBNull.Value)
                            ContactListId = int.Parse(out_ContactListId.Value.ToString());

                        return success == 0 ? false : true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(CreatedBy.ToString(), "ContactList-CreateContactList", ListName.ToString(), "Error", ex.Message);
                return false;
            }
        }


      public bool UpdateContactList(Guid AccountID,
                                  string ListName,
                                  string ListDescription,
                                  Guid ModifiedBy,
                                  int ContactListId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_ContactList_Update";

                        command.Parameters.AddWithValue("@AccountID", AccountID);

                        command.Parameters.AddWithValue("@ListName", ListName);

                        if (string.IsNullOrEmpty(ListDescription))
                        {
                            command.Parameters.AddWithValue("@ListDescription", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@ListDescription", ListDescription);
                        }

                        command.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);
                        command.Parameters.AddWithValue("@Id", ContactListId);

                        int success = command.ExecuteNonQuery();

                        return success == 0 ? false : true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(ModifiedBy.ToString(), "ContactList-UpdateContactList", ListName.ToString(), "Error", ex.Message);
                return false;
            }
        }


        public DataModels.ContactListModel SelectContactList( Guid AccountID, int Id)
        {
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter();
                sqlParam[0].ParameterName = "@AccountID";
                sqlParam[0].Value = AccountID;
                sqlParam[0].Direction = System.Data.ParameterDirection.Input;

                sqlParam[1] = new SqlParameter();
                sqlParam[1].ParameterName = "@Id";
                sqlParam[1].Value = Id;
                sqlParam[1].Direction = System.Data.ParameterDirection.Input;

                SqlDataReader sqlDR = SqlHelper.ExecuteReader(connectionString, "S_ContactList_Get", System.Data.CommandType.StoredProcedure, sqlParam);
                DataModels.ContactListModel objContactList = null;

                if (sqlDR.HasRows)
                {
                    while (sqlDR.Read())
                    {
                        objContactList = new DataModels.ContactListModel();
                        objContactList.AccountID = Guid.Parse(sqlDR["AccountID"].ToString().Trim());
                        objContactList.ID = int.Parse(sqlDR["ID"].ToString().Trim());
                        objContactList.ListName = sqlDR["ListName"].ToString().Trim();

                        if (sqlDR["ListDescription"] == System.DBNull.Value)
                        {
                            objContactList.ListDescription = string.Empty;
                        }
                        else
                        {
                            objContactList.ListDescription = sqlDR["ListDescription"].ToString().Trim();
                        }

                        objContactList.DateCreated = DateTime.Parse(sqlDR["DateCreated"].ToString().Trim());
                        objContactList.CreatedBy = Guid.Parse(sqlDR["CreatedBy"].ToString().Trim());
                        

                        if (sqlDR["ModifiedDate"] == System.DBNull.Value)
                        {
                            objContactList.ModifiedDate = null;
                        }
                        else
                        {
                            objContactList.ModifiedDate = DateTime.Parse(sqlDR["ModifiedDate"].ToString().Trim());
                        }

                        if (sqlDR["ModifiedBy"] == System.DBNull.Value)
                        {
                            objContactList.ModifiedBy = null;
                        }
                        else
                        {
                            objContactList.ModifiedBy = Guid.Parse(sqlDR["ModifiedBy"].ToString().Trim());
                        }
                    }
                }

                sqlDR.Close();

                return objContactList;
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(Id.ToString(), "Lead-SelectContactList", AccountID.ToString(), "Error", ex.Message);
                return null;
            }
        }
        
        public DataTable GetLists(Guid AccountID, bool isDeleted, int PageSize, string SortBy, string SortDirection, int PageNum, out int NewPageNum, out int MinItem, out int MaxItem, out int TotalRows, out decimal MaxPages)
        {
            MinItem = 0;
            MaxItem = 0;
            TotalRows = 0;
            NewPageNum = 0;
            MaxPages = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_LIST_GET";

                        command.Parameters.AddWithValue("@AccountID", AccountID);
                        command.Parameters.AddWithValue("@IsDeleted", isDeleted);
                        command.Parameters.AddWithValue("@MaxRows", PageSize);
                        command.Parameters.AddWithValue("@SortBy", SortBy);
                        command.Parameters.AddWithValue("@SortDirection", SortDirection);
                        command.Parameters.AddWithValue("@PageNum", PageNum);

                        SqlParameter out_NewPageNum = new SqlParameter("@NewPageNum", SqlDbType.Int);
                        out_NewPageNum.Direction = ParameterDirection.Output;
                        out_NewPageNum.Value = 0;

                        command.Parameters.Add(out_NewPageNum);

                        SqlParameter out_MinItem = new SqlParameter("@MinItem", SqlDbType.Int);
                        out_MinItem.Direction = ParameterDirection.Output;
                        out_MinItem.Value = 0;

                        command.Parameters.Add(out_MinItem);

                        SqlParameter out_MaxItem = new SqlParameter("@MaxItem", SqlDbType.Int);
                        out_MaxItem.Direction = ParameterDirection.Output;
                        out_MaxItem.Value = 0;

                        command.Parameters.Add(out_MaxItem);

                        SqlParameter out_TotalRows = new SqlParameter("@TotalRows", SqlDbType.Int);
                        out_TotalRows.Direction = ParameterDirection.Output;
                        out_TotalRows.Value = 0;

                        command.Parameters.Add(out_TotalRows);

                        SqlParameter out_MaxPages = new SqlParameter("@MaxPages", SqlDbType.Decimal);
                        out_MaxPages.Direction = ParameterDirection.Output;
                        out_MaxPages.Value = 0;

                        command.Parameters.Add(out_MaxPages);

                        DataTable dt = new DataTable();
                        SqlDataAdapter sqlA = new SqlDataAdapter(command);
                        sqlA.Fill(dt);

                        if (out_NewPageNum.Value != DBNull.Value)
                            NewPageNum = Convert.ToInt32(out_NewPageNum.Value);

                        if (out_MinItem.Value != DBNull.Value)
                            MinItem = Convert.ToInt32(out_MinItem.Value);

                        if (out_MaxItem.Value != DBNull.Value)
                            MaxItem = Convert.ToInt32(out_MaxItem.Value);

                        if (out_TotalRows.Value != DBNull.Value)
                            TotalRows = Convert.ToInt32(out_TotalRows.Value);

                        if (out_TotalRows.Value != DBNull.Value)
                            MaxPages = Convert.ToDecimal(out_MaxPages.Value);

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(AccountID.ToString(), "ContactList-GetLists", string.Empty, "Error", ex.Message);
                return null;
            }
        }

        public DataTable GetListNames(Guid AccountID, bool isDeleted )
        {
           

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                       command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_GET_LISTNAMES";

                        command.Parameters.AddWithValue("@AccountID", AccountID);
                        command.Parameters.AddWithValue("@IsDeleted", isDeleted);

                        DataTable dt = new DataTable();
                        SqlDataAdapter sqlA = new SqlDataAdapter(command);
                        sqlA.Fill(dt);

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(AccountID.ToString(), "ContactList-GetListNames", string.Empty, "Error", ex.Message);
                return null;
            }
        }

        public bool DeleteContactList(Guid AccountID,
                                  Guid ModifiedBy,
                                  int ContactListId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_ContactList_Delete";

                        command.Parameters.AddWithValue("@AccountID", AccountID);

                        command.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);

                        command.Parameters.AddWithValue("@Id", ContactListId);

                        int success = command.ExecuteNonQuery();

                        return success == 0 ? false : true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(ModifiedBy.ToString(), "ContactList-S_ContactList_Delete", ContactListId.ToString(), "Error", ex.Message);
                return false;
            }
        }


        public bool CreateContactListJob(string JobName,
            int JobStatusId,
            Guid AccountID,
            Guid CreatedBy,
            string FileFormat,
            string OriginalFileName,
            string FileName,
            int? ContactListId,
            string IPAddress)
        {
            

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_ContactJob_Insert";

                        command.Parameters.AddWithValue("@JobName", JobName);
                        command.Parameters.AddWithValue("@JobStatusId", JobStatusId);
                        command.Parameters.AddWithValue("@AccountID", AccountID);
                        command.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                        command.Parameters.AddWithValue("@FileFormat", FileFormat);
                        command.Parameters.AddWithValue("@OriginalFileName", OriginalFileName);
                        command.Parameters.AddWithValue("@FileName", FileName);
                        
                        if (ContactListId!=null)
                        {
                            command.Parameters.AddWithValue("@ContactListId", ContactListId);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@ContactListId", DBNull.Value);
                        }

                        command.Parameters.AddWithValue("@IPAddress", IPAddress);


                        int success = command.ExecuteNonQuery();

                        return success == 0 ? false : true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(CreatedBy.ToString(), "ContactList-CreateContactListJob", JobName.ToString(), "Error", ex.Message);
                return false;
            }
        }

        public bool AddMemberToList(int SearchID,
                                   Guid AccountID,
                                   Guid CreatedBy,
                                   string ExcludeList,
                                   int ContactListId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_List_AddMember";

                        command.Parameters.AddWithValue("@SearchID", SearchID);
                        command.Parameters.AddWithValue("@AccountId", AccountID);
                        command.Parameters.AddWithValue("@CreatedBy", CreatedBy);

                        if (string.IsNullOrEmpty(ExcludeList))
                        {
                            command.Parameters.AddWithValue("@ExcludeList", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@ExcludeList", ExcludeList);
                        }

                        command.Parameters.AddWithValue("@ContactListId", ContactListId);

                        int success = command.ExecuteNonQuery();

                        return success == 0 ? false : true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(CreatedBy.ToString(), "ContactList-AddMemberToList", ContactListId.ToString(), "Error", ex.Message);
                return false;
            }
        }

        public DataTable GetListMembers(int ContactListID, Guid AccountID, Guid CreatedBy, int MaxRows, string SortBy, string SortDirection, string ExcludeList, int PageNum, out int NewPageNum, out int MinItem, out int MaxItem, out int TotalRows, out decimal MaxPages)
        {
            MinItem = 0;
            MaxItem = 0;
            TotalRows = 0;
            NewPageNum = 0;
            MaxPages = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_ContactList_GetListMembers";

                        command.Parameters.AddWithValue("@ContactListID", ContactListID);
                        command.Parameters.AddWithValue("@AccountId", AccountID);

                        command.Parameters.AddWithValue("@MaxRows", MaxRows);
                        command.Parameters.AddWithValue("@SortBy", SortBy);
                        command.Parameters.AddWithValue("@SortDirection", SortDirection);

                        if (string.IsNullOrEmpty(ExcludeList))
                        {
                            command.Parameters.AddWithValue("@ExcludeList", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@ExcludeList", ExcludeList);
                        }

                        command.Parameters.AddWithValue("@PageNum", PageNum);

                        SqlParameter out_NewPageNum = new SqlParameter("@NewPageNum", SqlDbType.Int);
                        out_NewPageNum.Direction = ParameterDirection.Output;
                        out_NewPageNum.Value = 0;

                        command.Parameters.Add(out_NewPageNum);

                        SqlParameter out_MinItem = new SqlParameter("@MinItem", SqlDbType.Int);
                        out_MinItem.Direction = ParameterDirection.Output;
                        out_MinItem.Value = 0;

                        command.Parameters.Add(out_MinItem);

                        SqlParameter out_MaxItem = new SqlParameter("@MaxItem", SqlDbType.Int);
                        out_MaxItem.Direction = ParameterDirection.Output;
                        out_MaxItem.Value = 0;

                        command.Parameters.Add(out_MaxItem);

                        SqlParameter out_TotalRows = new SqlParameter("@TotalRows", SqlDbType.Int);
                        out_TotalRows.Direction = ParameterDirection.Output;
                        out_TotalRows.Value = 0;

                        command.Parameters.Add(out_TotalRows);

                        SqlParameter out_MaxPages = new SqlParameter("@MaxPages", SqlDbType.Decimal);
                        out_MaxPages.Direction = ParameterDirection.Output;
                        out_MaxPages.Value = 0;

                        command.Parameters.Add(out_MaxPages);

                        DataTable dt = new DataTable();
                        SqlDataAdapter sqlA = new SqlDataAdapter(command);
                        sqlA.Fill(dt);

                        if (out_NewPageNum.Value != DBNull.Value)
                            NewPageNum = Convert.ToInt32(out_NewPageNum.Value);

                        if (out_MinItem.Value != DBNull.Value)
                            MinItem = Convert.ToInt32(out_MinItem.Value);

                        if (out_MaxItem.Value != DBNull.Value)
                            MaxItem = Convert.ToInt32(out_MaxItem.Value);

                        if (out_TotalRows.Value != DBNull.Value)
                            TotalRows = Convert.ToInt32(out_TotalRows.Value);

                        if (out_TotalRows.Value != DBNull.Value)
                            MaxPages = Convert.ToDecimal(out_MaxPages.Value);

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(CreatedBy.ToString(), "ContactList-GetListMembers", ContactListID.ToString(), "Error", ex.Message);
                return null;
            }
        }


        public DataTable GetRemovedListMembers(int ContactListID, Guid AccountID, Guid CreatedBy, int MaxRows, string SortBy, string SortDirection, string RemoveList, int PageNum, out int NewPageNum, out int MinItem, out int MaxItem, out int TotalRows, out decimal MaxPages)
        {
            MinItem = 0;
            MaxItem = 0;
            TotalRows = 0;
            NewPageNum = 0;
            MaxPages = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_ContactList_GetRemovedListMembers";

                        command.Parameters.AddWithValue("@ContactListID", ContactListID);
                        command.Parameters.AddWithValue("@AccountId", AccountID);

                        command.Parameters.AddWithValue("@MaxRows", MaxRows);
                        command.Parameters.AddWithValue("@SortBy", SortBy);
                        command.Parameters.AddWithValue("@SortDirection", SortDirection);

                        if (string.IsNullOrEmpty(RemoveList))
                        {
                            command.Parameters.AddWithValue("@RemoveList", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@RemoveList", RemoveList);
                        }

                        command.Parameters.AddWithValue("@PageNum", PageNum);

                        SqlParameter out_NewPageNum = new SqlParameter("@NewPageNum", SqlDbType.Int);
                        out_NewPageNum.Direction = ParameterDirection.Output;
                        out_NewPageNum.Value = 0;

                        command.Parameters.Add(out_NewPageNum);

                        SqlParameter out_MinItem = new SqlParameter("@MinItem", SqlDbType.Int);
                        out_MinItem.Direction = ParameterDirection.Output;
                        out_MinItem.Value = 0;

                        command.Parameters.Add(out_MinItem);

                        SqlParameter out_MaxItem = new SqlParameter("@MaxItem", SqlDbType.Int);
                        out_MaxItem.Direction = ParameterDirection.Output;
                        out_MaxItem.Value = 0;

                        command.Parameters.Add(out_MaxItem);

                        SqlParameter out_TotalRows = new SqlParameter("@TotalRows", SqlDbType.Int);
                        out_TotalRows.Direction = ParameterDirection.Output;
                        out_TotalRows.Value = 0;

                        command.Parameters.Add(out_TotalRows);

                        SqlParameter out_MaxPages = new SqlParameter("@MaxPages", SqlDbType.Decimal);
                        out_MaxPages.Direction = ParameterDirection.Output;
                        out_MaxPages.Value = 0;

                        command.Parameters.Add(out_MaxPages);

                        DataTable dt = new DataTable();
                        SqlDataAdapter sqlA = new SqlDataAdapter(command);
                        sqlA.Fill(dt);

                        if (out_NewPageNum.Value != DBNull.Value)
                            NewPageNum = Convert.ToInt32(out_NewPageNum.Value);

                        if (out_MinItem.Value != DBNull.Value)
                            MinItem = Convert.ToInt32(out_MinItem.Value);

                        if (out_MaxItem.Value != DBNull.Value)
                            MaxItem = Convert.ToInt32(out_MaxItem.Value);

                        if (out_TotalRows.Value != DBNull.Value)
                            TotalRows = Convert.ToInt32(out_TotalRows.Value);

                        if (out_TotalRows.Value != DBNull.Value)
                            MaxPages = Convert.ToDecimal(out_MaxPages.Value);

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(CreatedBy.ToString(), "ContactList-GetRemovedListMembers", ContactListID.ToString(), "Error", ex.Message);
                return null;
            }
        }

        public bool ModifyMemberList(int ContactListId,
                                   Guid AccountID,
                                   Guid UserId,
                                   string RemoveList)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_List_ModifyMembers";

                        command.Parameters.AddWithValue("@ContactListID", ContactListId);
                        command.Parameters.AddWithValue("@AccountID", AccountID);
                        command.Parameters.AddWithValue("@ModifiedBy", UserId);

                        if (string.IsNullOrEmpty(RemoveList))
                        {
                            command.Parameters.AddWithValue("@RemoveList", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@RemoveList", RemoveList);
                        }

                        int success = command.ExecuteNonQuery();

                        return success == 0 ? false : true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(UserId.ToString(), "ContactList-ModifyMemberList", ContactListId.ToString(), "Error", ex.Message);
                return false;
            }
        }
    }
}
