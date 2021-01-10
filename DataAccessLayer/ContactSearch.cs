using System;
using System.Data.SqlClient;
using System.Data;
using CSDataSqlCommand;
using System.Configuration;
using System.Collections.Generic;
using DataModels;

namespace DataAccessLayer.Controller
{
    public class ContactSearch
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AutoMarketerConnectionString"].ConnectionString.ToString();

        public bool AddContactSearch(Guid AccountId, Guid CreatedBy, string SearchJsonString, List<SearchFieldsItems> lstSearchFieldsItems, string SearchType, out int SearchId, out Guid SearchUID)
        {
            SearchId = 0;
            SearchUID = Guid.NewGuid();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                      
                        SqlTransaction transaction;
                        transaction = connection.BeginTransaction("AddContactSearch");

                        command.CommandType = CommandType.StoredProcedure;
                        command.Transaction = transaction;

                        try
                        {
                            /*1.Add Contact Search*/
                            command.CommandText = "S_ContactSearch_Insert";
                            command.Parameters.AddWithValue("@AccountId", AccountId);
                            command.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                            command.Parameters.AddWithValue("@SearchJsonString", SearchJsonString);

                            if (string.IsNullOrEmpty(SearchType))
                            {
                                command.Parameters.AddWithValue("@SearchType", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@SearchType", SearchType);
                            }
                            

                            SqlParameter out_SearchId = new SqlParameter("@Id", SqlDbType.Int);
                            out_SearchId.Direction = ParameterDirection.Output;
                            command.Parameters.Add(out_SearchId);

                            SqlParameter out_SearchUID = new SqlParameter("@SearchUID", SqlDbType.UniqueIdentifier);
                            out_SearchUID.Direction = ParameterDirection.Output;
                            command.Parameters.Add(out_SearchUID);

                            command.CommandType = CommandType.StoredProcedure;
                            command.ExecuteNonQuery();
                        

                            if (out_SearchId.Value != DBNull.Value)
                                SearchId = int.Parse(out_SearchId.Value.ToString());

                            if (out_SearchUID.Value != DBNull.Value)
                                SearchUID = Guid.Parse(out_SearchUID.Value.ToString());

                            /*2.Add Contact Search Fields*/
                            if (lstSearchFieldsItems.Count>0)
                            {
                                foreach (SearchFieldsItems searchfielditem in lstSearchFieldsItems)
                                {
                                    command.Parameters.Clear();
                                    command.Parameters.AddWithValue("@SearchID", SearchId);
                                    command.Parameters.AddWithValue("@AccountId", AccountId);
                                    command.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                                    command.Parameters.AddWithValue("@SearchKey", searchfielditem.Field);
                                    command.Parameters.AddWithValue("@SearchOperator", searchfielditem.Operator);
                                    command.Parameters.AddWithValue("@SearchValue", searchfielditem.SearchValue);
                                    command.Parameters.AddWithValue("@SearchLogicalOperator", searchfielditem.LogicalOperator);
                                    command.CommandText = "S_ContactSearchField_Insert";
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("@SearchID", SearchId);
                                command.Parameters.AddWithValue("@AccountId", AccountId);
                                command.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                                command.Parameters.AddWithValue("@SearchKey", "ALL");
                                command.Parameters.AddWithValue("@SearchOperator", string.Empty);
                                command.Parameters.AddWithValue("@SearchValue", string.Empty);
                                command.Parameters.AddWithValue("@SearchLogicalOperator", string.Empty);
                                command.CommandText = "S_ContactSearchField_Insert";
                                command.CommandType = CommandType.StoredProcedure;
                                command.ExecuteNonQuery();
                            }
                            

                            transaction.Commit();
                            return true;

                        }
                        catch(Exception ex)
                        {
                            transaction.Rollback();
                            Logger.Logger.WriteLog(CreatedBy.ToString(), "ContactSearch-AddContactSearch", AccountId.ToString(), "Error", ex.Message);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(CreatedBy.ToString(), "ContactSearch-AddContactSearch", AccountId.ToString(), "Error", ex.Message);
                return false;
            }
        }


        public bool UpdateContactSearch(Guid AccountId, Guid ModifiedBy, string SearchJsonString, List<SearchFieldsItems> lstSearchFieldsItems, int SearchId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {

                        SqlTransaction transaction;
                        transaction = connection.BeginTransaction("UpdateContactSearch");

                        command.CommandType = CommandType.StoredProcedure;
                        command.Transaction = transaction;

                        try
                        {
                            /*1.Add Contact Search*/
                            command.CommandText = "S_ContactSearch_Update";
                            command.Parameters.AddWithValue("@AccountId", AccountId);
                            command.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);
                            command.Parameters.AddWithValue("@SearchJsonString", SearchJsonString);
                            command.Parameters.AddWithValue("@Id", SearchId);

                            command.CommandType = CommandType.StoredProcedure;
                            command.ExecuteNonQuery();

                            /*2.Delete Contact Search Fields*/
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@SearchID", SearchId);
                            command.Parameters.AddWithValue("@AccountId", AccountId);

                            command.CommandText = "S_ContactSearchField_Delete";
                            command.CommandType = CommandType.StoredProcedure;
                            command.ExecuteNonQuery();

                            /*3.Add new Contact Search Fields*/
                            if (lstSearchFieldsItems.Count > 0)
                            {
                                foreach (SearchFieldsItems searchfielditem in lstSearchFieldsItems)
                                {
                                    command.Parameters.Clear();
                                    command.Parameters.AddWithValue("@SearchID", SearchId);
                                    command.Parameters.AddWithValue("@AccountId", AccountId);
                                    command.Parameters.AddWithValue("@CreatedBy", ModifiedBy);
                                    command.Parameters.AddWithValue("@SearchKey", searchfielditem.Field);
                                    command.Parameters.AddWithValue("@SearchOperator", searchfielditem.Operator);
                                    command.Parameters.AddWithValue("@SearchValue", searchfielditem.SearchValue);
                                    command.Parameters.AddWithValue("@SearchLogicalOperator", searchfielditem.LogicalOperator);
                                    command.CommandText = "S_ContactSearchField_Insert";
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("@SearchID", SearchId);
                                command.Parameters.AddWithValue("@AccountId", AccountId);
                                command.Parameters.AddWithValue("@CreatedBy", ModifiedBy);
                                command.Parameters.AddWithValue("@SearchKey", "ALL");
                                command.Parameters.AddWithValue("@SearchOperator", string.Empty);
                                command.Parameters.AddWithValue("@SearchValue", string.Empty);
                                command.Parameters.AddWithValue("@SearchLogicalOperator", string.Empty);
                                command.CommandText = "S_ContactSearchField_Insert";
                                command.CommandType = CommandType.StoredProcedure;
                                command.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            return true;

                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            Logger.Logger.WriteLog(ModifiedBy.ToString(), "ContactSearch-UpdateContactSearch", AccountId.ToString(), "Error", ex.Message);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(ModifiedBy.ToString(), "ContactSearch-UpdateContactSearch", AccountId.ToString(), "Error", ex.Message);
                return false;
            }
        }

        public DataTable GetSearchResult(int SearchID, Guid AccountId, Guid CreatedBy, int MaxRows, string SortBy, string SortDirection, string ExcludeList, int PageNum, out int NewPageNum, out int MinItem, out int MaxItem, out int TotalRows, out decimal MaxPages)
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
                        command.CommandText = "S_ContactSearch_Search";

                        command.Parameters.AddWithValue("@SearchID", SearchID);
                        command.Parameters.AddWithValue("@AccountId", AccountId);
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

                Logger.Logger.WriteLog(CreatedBy.ToString(), "ContactSearch-GetSearchResult", SearchID.ToString(), "Error", ex.Message);
                return null;
            }
        }

        public DataTable GetExcludeList(int SearchID, Guid AccountId, Guid CreatedBy, int MaxRows, string SortBy, string SortDirection, string ExcludeList, int PageNum, out int NewPageNum, out int MinItem, out int MaxItem, out int TotalRows, out decimal MaxPages)
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
                        command.CommandText = "S_ContactSearch_GetExcludeList";

                        command.Parameters.AddWithValue("@SearchID", SearchID);
                        command.Parameters.AddWithValue("@AccountId", AccountId);
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
                Logger.Logger.WriteLog(CreatedBy.ToString(), "ContactSearch-S_CONTACTSEARCH_GETEXCLUDELIST", string.Empty, "Error", ex.Message);
                return null;
            }
        }

        public DataTable ViewRecentSearches(Guid AccountId, Guid CreatedBy)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_ContactSearch_GetRecentSearches";

                        command.Parameters.AddWithValue("@ACCOUNTID", AccountId);
                        command.Parameters.AddWithValue("@CREATEDBY", CreatedBy);

                        DataTable dt = new DataTable();
                        SqlDataAdapter sqlA = new SqlDataAdapter(command);
                        sqlA.Fill(dt);

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(CreatedBy.ToString(), "ContactSearch-ViewRecentSearches", AccountId.ToString(), "Error", ex.Message);
                return null;
            }
        }

        public string GetSearchCriteriaDisplay(int SearchID, Guid AccountId, Guid UserId )
        {
            try
            {
                SqlParameter[] sqlParam =
                {
                  new SqlParameter("@SearchID", SqlDbType.Int) { Value = SearchID },
                   new SqlParameter("@AccountID", SqlDbType.UniqueIdentifier) { Value = AccountId }
                };

                string result = SqlHelper.ExecuteScalar(connectionString, @"SELECT dbo.fn_ContactSearch_VisibleCriteria(@SearchID, @AccountID)", System.Data.CommandType.Text, sqlParam).ToString();

                return result;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(UserId.ToString(), "ContactSearch-GetSearchCriteriaDisplay", SearchID.ToString(), "Error", ex.Message);
                return string.Empty;
            }
        }

        public string GetSearchJsonString(int SearchID, Guid AccountId, Guid UserId)
        {
            try
            {
                SqlParameter[] sqlParam =
                {
                  new SqlParameter("@SearchID", SqlDbType.Int) { Value = SearchID },
                  new SqlParameter("@AccountID", SqlDbType.UniqueIdentifier) { Value = AccountId }
                };

                string result = SqlHelper.ExecuteScalar(connectionString, @"SELECT dbo.fn_ContactSearch_GetSearchJsonString(@SearchID,@AccountID)", System.Data.CommandType.Text, sqlParam).ToString();

                return result;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(UserId.ToString(), "ContactSearch-GetSearchJsonString", SearchID.ToString(), "Error", ex.Message);
                return string.Empty;
            }
        }

        public bool DeleteContactSearchFields(int SearchId, Guid AccountId)
        {
           
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_ContactSearchField_Delete";

                        command.Parameters.AddWithValue("@SearchID", SearchId);
                        command.Parameters.AddWithValue("@AccountId", AccountId);

                        command.CommandText = "S_ContactSearchField_Delete";
                        command.CommandType = CommandType.StoredProcedure;
                        int success = command.ExecuteNonQuery();

                        return success == 0 ? false : true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(SearchId.ToString(), "ContactSearch-DeleteContactSearchFields", AccountId.ToString(), "Error", ex.Message);
                return false;
            }
        }

        public bool AddContactSearchFields(int SearchId, Guid AccountId, Guid CreatedBy,string SearchKey, string SearchOperator, string SearchValue, string SearchLogicalOperator)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_ContactSearchField_Insert";

                        command.Parameters.AddWithValue("@SearchID", SearchId);
                        command.Parameters.AddWithValue("@AccountId", AccountId);
                        command.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                        command.Parameters.AddWithValue("@SearchKey", SearchKey);
                        command.Parameters.AddWithValue("@SearchOperator", SearchOperator);
                        command.Parameters.AddWithValue("@SearchValue", SearchValue);
                        command.Parameters.AddWithValue("@SearchLogicalOperator", SearchLogicalOperator);

                        command.CommandText = "S_ContactSearchField_Insert";
                        command.CommandType = CommandType.StoredProcedure;
                        int success = command.ExecuteNonQuery();

                        return success == 0 ? false : true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(SearchId.ToString(), "ContactSearch-AddContactSearchFields", AccountId.ToString(), "Error", ex.Message);
                return false;
            }
        }

        public string GetSearchID(Guid SearchUID, Guid AccountID, Guid CreatedBy)
        {
            try
            {
                SqlParameter[] sqlParam =
                {
                  new SqlParameter("@SearchGUID", SqlDbType.UniqueIdentifier) { Value = SearchUID },
                  new SqlParameter("@AccountID", SqlDbType.UniqueIdentifier) { Value = AccountID }
                };

                string result = SqlHelper.ExecuteScalar(connectionString, @"SELECT dbo.fn_GetSearchId(@SearchGUID,@AccountID)", System.Data.CommandType.Text, sqlParam).ToString();

                return result;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(CreatedBy.ToString(), "ContactSearch-GetSearchID", SearchUID.ToString(), "Error", ex.Message);
                return string.Empty;
            }
        }


        public string GetSearchUID(int SearchID, Guid AccountID, Guid UserId)
        {
            try
            {
                SqlParameter[] sqlParam =
                {
                  new SqlParameter("@SearchID", SqlDbType.Int) { Value = SearchID },
                  new SqlParameter("@AccountID", SqlDbType.UniqueIdentifier) { Value = AccountID }
                };

                string result = SqlHelper.ExecuteScalar(connectionString, @"SELECT dbo.fn_GetSearchGUID(@SearchID,@AccountID)", System.Data.CommandType.Text, sqlParam).ToString();

                return result;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(UserId.ToString(), "ContactSearch-GetSearchUID", SearchID.ToString(), "Error", ex.Message);
                return string.Empty;
            }
        }


        public string GetSearchUIDByCampaignUID(Guid CampaignUID, Guid AccountID, Guid UserId)
        {
            try
            {
                SqlParameter[] sqlParam =
                {
                  new SqlParameter("@CampaignUID", SqlDbType.UniqueIdentifier) { Value = CampaignUID },
                  new SqlParameter("@AccountID", SqlDbType.UniqueIdentifier) { Value = AccountID }
                };

                string result = SqlHelper.ExecuteScalar(connectionString, @"SELECT dbo.fn_GetSearchGUIDByCampaignUID(@CampaignUID,@AccountID)", System.Data.CommandType.Text, sqlParam).ToString();

                return result;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(UserId.ToString(), "ContactSearch-GetSearchUIDByCampaignUID", CampaignUID.ToString(), "Error", ex.Message);
                return string.Empty;
            }
        }


        public string GetSearchID_BySearchType(Guid CreatedBy, Guid AccountID, string SearchType)
        {
            try
            {
                SqlParameter[] sqlParam =
                {
                  new SqlParameter("@AccountID", SqlDbType.UniqueIdentifier) { Value = AccountID },
                  new SqlParameter("@SearchType", SqlDbType.NVarChar,20) { Value = SearchType }
                };

                string result = SqlHelper.ExecuteScalar(connectionString, @"SELECT dbo.fn_GetSearchId_BySearchType(@AccountID,@SearchType)", System.Data.CommandType.Text, sqlParam).ToString();

                return result;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(CreatedBy.ToString(), "ContactSearch-GetSearchID_BySearchType", AccountID.ToString(), "Error", ex.Message);
                return string.Empty;
            }
        }
    }
}
