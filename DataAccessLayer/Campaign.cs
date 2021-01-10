using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Controller
{
   public class Campaign
   {
        private string connectionString = ConfigurationManager.ConnectionStrings["AutoMarketerConnectionString"].ConnectionString.ToString();

        public bool CreateCampaign(Guid AccountId,
                    Guid? in_param_CampaignUID,
                    int? in_param_MessageID,
                    string CampaignName,
                    int CampaignType,
                    int CampaignFormat,
                    string CampaignDescription,
                    bool UseBounceAddressInFromField,
                    bool HideInSearch,
                    int CampaignStatus,
                    int? TemplateId,
                    int SendingOption,
                    DateTime? SendingSchedule,
                    string Entity,
                    string Subject,
                    string SenderName,
                    string SenderEmail,
                    string MessageBody,
                    string SearchId,
                    string ExcludeList,
                    Guid CreatedBy,
                    string IPAddress,
                    int EmailJobStatus,
                    out int out_param_CampaignId,
                    out Guid? out_param_CampaignUID,
                    out int out_param_MessageId,
                    out Guid? out_param_MessageUID)
        {
            out_param_CampaignId = 0;
            out_param_CampaignUID = null;
            out_param_MessageId = 0;
            out_param_MessageUID = null;

            int JobId = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        SqlTransaction transaction;
                        transaction = connection.BeginTransaction("AddCampaign");

                        command.CommandType = CommandType.StoredProcedure;
                        command.Transaction = transaction;

                        int success = 0;
                        try
                        {
                            
                            /*1 Message*/
                            if (in_param_MessageID==null)
                            {
                                
                                    /************S_MESSAGE_INSERT*************/
                                    command.Parameters.AddWithValue("@AccountId", AccountId);
                                    command.Parameters.AddWithValue("@MessageFormat", CampaignFormat);
                                    command.Parameters.AddWithValue("@Entity", Entity);
                                    command.Parameters.AddWithValue("@Subject", Subject);
                                    command.Parameters.AddWithValue("@SenderName", SenderName);
                                    command.Parameters.AddWithValue("@SenderEmail", SenderEmail);

                                    if (!string.IsNullOrEmpty(MessageBody))
                                    {
                                        command.Parameters.AddWithValue("@MessageBody", MessageBody);
                                    }
                                    else
                                    {
                                        command.Parameters.AddWithValue("@MessageBody", DBNull.Value);
                                    }

                                    command.Parameters.AddWithValue("@CreatedBy", CreatedBy);

                                    SqlParameter out_MessageId = new SqlParameter("@MessageId", SqlDbType.Int);
                                    out_MessageId.Direction = ParameterDirection.Output;
                                    command.Parameters.Add(out_MessageId);

                                    SqlParameter out_MessageUId = new SqlParameter("@MessageUID", SqlDbType.UniqueIdentifier);
                                    out_MessageUId.Direction = ParameterDirection.Output;
                                    command.Parameters.Add(out_MessageUId);

                                    command.CommandText = "S_MESSAGE_INSERT";
                                    command.CommandType = CommandType.StoredProcedure;
                                    success = command.ExecuteNonQuery();

                                    if (out_MessageId.Value != DBNull.Value)
                                        out_param_MessageId = int.Parse(out_MessageId.Value.ToString());

                                    if (out_MessageUId.Value != DBNull.Value)
                                        out_param_MessageUID = Guid.Parse(out_MessageUId.Value.ToString());
                                
                            }
                            else
                            {
                                /************S_MESSAGE_UPDATE*************/
                                command.Parameters.AddWithValue("@AccountId", AccountId);
                                command.Parameters.AddWithValue("@MessageFormat", CampaignFormat);
                                command.Parameters.AddWithValue("@Entity", Entity);
                                command.Parameters.AddWithValue("@Subject", Subject);
                                command.Parameters.AddWithValue("@SenderName", SenderName);
                                command.Parameters.AddWithValue("@SenderEmail", SenderEmail);

                                if (!string.IsNullOrEmpty(MessageBody))
                                {
                                    command.Parameters.AddWithValue("@MessageBody", MessageBody);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@MessageBody", DBNull.Value);
                                }

                                command.Parameters.AddWithValue("@ModifiedBy", CreatedBy);
                                command.Parameters.AddWithValue("@MessageId", in_param_MessageID.Value);

                                
                                SqlParameter out_MessageUId = new SqlParameter("@MessageUID", SqlDbType.UniqueIdentifier);
                                out_MessageUId.Direction = ParameterDirection.Output;
                                command.Parameters.Add(out_MessageUId);

                                command.CommandText = "S_MESSAGE_UPDATE";
                                command.CommandType = CommandType.StoredProcedure;
                                success = command.ExecuteNonQuery();

                                out_param_MessageId = in_param_MessageID.Value;

                                if (out_MessageUId.Value != DBNull.Value)
                                    out_param_MessageUID = Guid.Parse(out_MessageUId.Value.ToString());

                            }

                            /*2 Campaign*/
                            if (in_param_CampaignUID == null)
                            {
                                /************S_CAMPAIGN_INSERT*************/
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("@AccountId", AccountId);
                                command.Parameters.AddWithValue("@CampaignName", CampaignName);
                                command.Parameters.AddWithValue("@CampaignType", CampaignType);
                                command.Parameters.AddWithValue("@CampaignFormat", CampaignFormat);
                                command.Parameters.AddWithValue("@CampaignDescription", CampaignDescription);

                                if (out_param_MessageId != 0)
                                {
                                    command.Parameters.AddWithValue("@MessageId", out_param_MessageId);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@MessageId", DBNull.Value);
                                }

                                command.Parameters.AddWithValue("@UseBounceAddressInFromField", UseBounceAddressInFromField);
                                command.Parameters.AddWithValue("@HideInSearch", HideInSearch);
                                command.Parameters.AddWithValue("@CampaignStatus", CampaignStatus);

                                if (TemplateId != null)
                                {
                                    command.Parameters.AddWithValue("@TemplateId", TemplateId);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@TemplateId", DBNull.Value);
                                }

                                command.Parameters.AddWithValue("@SendingOption", SendingOption);


                                if (SendingSchedule != null)
                                {
                                    command.Parameters.AddWithValue("@SendingSchedule", SendingSchedule);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@SendingSchedule", DBNull.Value);
                                }


                                if (!string.IsNullOrEmpty(SearchId))
                                {
                                    command.Parameters.AddWithValue("@SearchId", SearchId);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@SearchId", DBNull.Value);
                                }

                                    
                                command.Parameters.AddWithValue("@ExcludeList", ExcludeList);
                                command.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                                command.Parameters.AddWithValue("@IPAddress", IPAddress);

                                SqlParameter out_CampaignId = new SqlParameter("@CampaignId", SqlDbType.Int);
                                out_CampaignId.Direction = ParameterDirection.Output;
                                command.Parameters.Add(out_CampaignId);

                                SqlParameter out_CampaignUID = new SqlParameter("@CampaignUID", SqlDbType.UniqueIdentifier);
                                out_CampaignUID.Direction = ParameterDirection.Output;
                                command.Parameters.Add(out_CampaignUID);

                                command.CommandText = "S_CAMPAIGN_INSERT";
                                command.CommandType = CommandType.StoredProcedure;
                                success = command.ExecuteNonQuery();

                                if (out_CampaignId.Value != DBNull.Value)
                                    out_param_CampaignId = int.Parse(out_CampaignId.Value.ToString());

                                if (out_CampaignUID.Value != DBNull.Value)
                                    out_param_CampaignUID = Guid.Parse(out_CampaignUID.Value.ToString());
                            }
                            else
                            {
                                /************S_CAMPAIGN_UPDATE*************/
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("@AccountId", AccountId);
                                command.Parameters.AddWithValue("@CampaignName", CampaignName);
                                command.Parameters.AddWithValue("@CampaignType", CampaignType);
                                command.Parameters.AddWithValue("@CampaignFormat", CampaignFormat);
                                command.Parameters.AddWithValue("@CampaignDescription", CampaignDescription);

                                if (out_param_MessageId != 0)
                                {
                                    command.Parameters.AddWithValue("@MessageId", out_param_MessageId);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@MessageId", DBNull.Value);
                                }

                                command.Parameters.AddWithValue("@UseBounceAddressInFromField", UseBounceAddressInFromField);
                                command.Parameters.AddWithValue("@HideInSearch", HideInSearch);
                                command.Parameters.AddWithValue("@CampaignStatus", CampaignStatus);

                                if (TemplateId != null)
                                {
                                    command.Parameters.AddWithValue("@TemplateId", TemplateId);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@TemplateId", DBNull.Value);
                                }

                                command.Parameters.AddWithValue("@SendingOption", SendingOption);


                                if (SendingSchedule != null)
                                {
                                    command.Parameters.AddWithValue("@SendingSchedule", SendingSchedule);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@SendingSchedule", DBNull.Value);
                                }

                                if (!string.IsNullOrEmpty(SearchId))
                                {
                                    command.Parameters.AddWithValue("@SearchId", SearchId);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@SearchId", DBNull.Value);
                                }
                                command.Parameters.AddWithValue("@ExcludeList", ExcludeList);
                                command.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                                command.Parameters.AddWithValue("@IPAddress", IPAddress);
                                command.Parameters.AddWithValue("@CampaignUID", in_param_CampaignUID.Value);

                                SqlParameter out_CampaignId = new SqlParameter("@CampaignId", SqlDbType.Int);
                                out_CampaignId.Direction = ParameterDirection.Output;
                                command.Parameters.Add(out_CampaignId);

                                command.CommandText = "S_CAMPAIGN_UPDATE";
                                command.CommandType = CommandType.StoredProcedure;
                                success = command.ExecuteNonQuery();

                                out_param_CampaignUID = in_param_CampaignUID.Value;

                                if (out_CampaignId.Value != DBNull.Value)
                                    out_param_CampaignId = int.Parse(out_CampaignId.Value.ToString()); 
                            }
                            
                            if (!string.IsNullOrEmpty(SearchId))
                            {
                                /************S_Campaign_AddRecipient*************/
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("@SearchID", SearchId);
                                command.Parameters.AddWithValue("@AccountId", AccountId);
                                command.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                                command.Parameters.AddWithValue("@ExcludeList", ExcludeList);
                                command.Parameters.AddWithValue("@CampaignID", out_param_CampaignId);
                                command.CommandText = "S_Campaign_AddRecipient";
                                command.CommandType = CommandType.StoredProcedure;
                                success = command.ExecuteNonQuery();
                            }

                            if (CampaignStatus ==(int)ConstantValues.CampaignStatus.Submitted)
                            {
                                /************S_EMAIL_JOB_QUEUE_INSERT*************/
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("@MessageId", out_param_MessageId);
                                command.Parameters.AddWithValue("@CampaignId", out_param_CampaignId);
                                command.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                                command.Parameters.AddWithValue("@IPAddress", IPAddress);
                                if (SendingSchedule != null)
                                {
                                    command.Parameters.AddWithValue("@Schedule", SendingSchedule);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@Schedule", System.DBNull.Value);
                                }

                                command.Parameters.AddWithValue("@bUpdate", false);

                                SqlParameter out_JobId = new SqlParameter("@JobId", SqlDbType.Int);
                                out_JobId.Direction = ParameterDirection.Output;
                                command.Parameters.Add(out_JobId);

                                command.CommandText = "S_EMAIL_JOB_QUEUE_INSERT";
                                command.CommandType = CommandType.StoredProcedure;
                                success = command.ExecuteNonQuery();

                                if (out_JobId.Value != DBNull.Value)
                                    JobId = int.Parse(out_JobId.Value.ToString());

                                /************S_EMAIL_JOB_QUEUE_HISTORY_INSERT*************/
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("@JobId", JobId);
                                command.Parameters.AddWithValue("@STATUS", EmailJobStatus);

                                command.CommandText = "S_EMAIL_JOB_QUEUE_HISTORY_INSERT";
                                command.CommandType = CommandType.StoredProcedure;
                                success = command.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            return true;

                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            Logger.Logger.WriteLog(CreatedBy.ToString(), "Campaign-CreateCampaign", CampaignName, "Error", ex.Message);
                            return false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(CreatedBy.ToString(), "Campaign-CreateCampaign", CampaignName, "Error", ex.Message);
                return false;
            }
        }

        public DataTable GetCampaignsForSideBar(Guid AccountID, string SearchText, int? CampaignStatus, bool? isDeleted, bool? isHiddenFromSearch)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_CAMPAIGN_QUICKSEARCH";

                        command.Parameters.AddWithValue("@AccountId", AccountID);
                        command.Parameters.AddWithValue("@search", SearchText);

                        if (CampaignStatus == null)
                        {
                            command.Parameters.AddWithValue("@status_ID", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@status_ID", CampaignStatus);
                        }

                        if (isDeleted == null)
                        {
                            command.Parameters.AddWithValue("@deleted", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@deleted", isDeleted);
                        }

                        if (isHiddenFromSearch==null)
                        {
                            command.Parameters.AddWithValue("@includehidden", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@includehidden", isHiddenFromSearch);
                        }

                        DataTable dt = new DataTable();
                        SqlDataAdapter sqlA = new SqlDataAdapter(command);
                        sqlA.Fill(dt);

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(AccountID.ToString(), "Campaign-GetCampaignsForSideBar", string.Empty, "Error", ex.Message);
                return null;
            }
        }


        public bool DeleteCampaign(Guid AccountID,
        Guid CampaignUID,
        Guid DeletedBy)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        SqlTransaction transaction;
                        transaction = connection.BeginTransaction("DeleteCampaign");

                        command.CommandType = CommandType.StoredProcedure;
                        command.Transaction = transaction;

                        try
                        {
                            command.Parameters.AddWithValue("@AccountUID", AccountID);
                            command.Parameters.AddWithValue("@CampaignUID", CampaignUID);
                            command.Parameters.AddWithValue("@DeletedBy", DeletedBy);

                            command.CommandText = "S_CAMPAIGN_DELETE";
                            command.CommandType = CommandType.StoredProcedure;
                            int success = command.ExecuteNonQuery();

                            transaction.Commit();
                            return success == 0 ? false : true;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            Logger.Logger.WriteLog(DeletedBy.ToString(), "Campaign-DeleteCampaign", CampaignUID.ToString(), "Error", ex.Message);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(DeletedBy.ToString(), "Campaign-DeleteCampaign", CampaignUID.ToString(), "Error", ex.Message);
                return false;
            }
        }


        public DataTable GetAllCampaigns(Guid AccountID, bool? Deleted, bool? HideInSearch, Guid CreatedBy, int MaxRows, string SortBy, string SortDirection, int PageNum, out int NewPageNum, out int MinItem, out int MaxItem, out int TotalRows, out decimal MaxPages)
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
                        command.CommandText = "S_CAMPAIGN_GETALL";

                        command.Parameters.AddWithValue("@AccountID", AccountID);

                        if (Deleted==null)
                        {
                            command.Parameters.AddWithValue("@Deleted", System.DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Deleted", Deleted);
                        }

                        if (HideInSearch == null)
                        {
                            command.Parameters.AddWithValue("@HideInSearch", System.DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@HideInSearch", HideInSearch);
                        }


                        command.Parameters.AddWithValue("@MaxRows", MaxRows);
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

                Logger.Logger.WriteLog(CreatedBy.ToString(), "Campaign-GetAllCampaigns", string.Empty, "Error", ex.Message);
                return null;
            }
        }


        public DataTable GetCampaignStats(Guid AccountID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_Campaigns_GetStats";

                        command.Parameters.AddWithValue("@AccountID", AccountID);

                        DataTable dt = new DataTable();
                        SqlDataAdapter sqlA = new SqlDataAdapter(command);
                        sqlA.Fill(dt);

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(AccountID.ToString(), "Campaign-GetCampaignStats", string.Empty, "Error", ex.Message);
                return null;
            }
        }


        public DataTable GetCampaignUniqueStats(Guid AccountID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_Campaigns_GetUniqueStats";

                        command.Parameters.AddWithValue("@AccountID", AccountID);

                        DataTable dt = new DataTable();
                        SqlDataAdapter sqlA = new SqlDataAdapter(command);
                        sqlA.Fill(dt);

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(AccountID.ToString(), "Campaign-GetCampaignUniqueStats", string.Empty, "Error", ex.Message);
                return null;
            }
        }


        public DataTable GetCampaignReportEmailStats(Guid AccountID,DateTime? StartDate, DateTime? EndDate, int? CampaignId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_Campaigns_EmailStats";

                        command.Parameters.AddWithValue("@AccountId", AccountID);

                        if (StartDate==null)
                        {
                            command.Parameters.AddWithValue("@StartDate", System.DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@StartDate", StartDate);
                        }

                        if (EndDate == null)
                        {
                            command.Parameters.AddWithValue("@EndDate", System.DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@EndDate", EndDate);
                        }

                        if (CampaignId == null)
                        {
                            command.Parameters.AddWithValue("@CampaignId", System.DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@CampaignId", CampaignId);
                        }

                        DataTable dt = new DataTable();
                        SqlDataAdapter sqlA = new SqlDataAdapter(command);
                        sqlA.Fill(dt);

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(AccountID.ToString(), "Campaign-GetCampaignReportEmailStats", string.Empty, "Error", ex.Message);
                return null;
            }
        }

        public DataTable GetCampaignReportGetAllSubmittedCampaigns(Guid AccountID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_Campaign_GetAllSubmittedCampaigns";

                        command.Parameters.AddWithValue("@AccountId", AccountID);

                        DataTable dt = new DataTable();
                        SqlDataAdapter sqlA = new SqlDataAdapter(command);
                        sqlA.Fill(dt);

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(AccountID.ToString(), "Campaign-GetCampaignReportGetAllSubmittedCampaigns", string.Empty, "Error", ex.Message);
                return null;
            }
        }

        public DataTable GetCampaignReportEmailsSentListing(Guid AccountID, DateTime? StartDate, DateTime? EndDate, int? CampaignId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_Campaigns_EmailSentListing";

                        command.Parameters.AddWithValue("@AccountId", AccountID);

                        if (StartDate == null)
                        {
                            command.Parameters.AddWithValue("@StartDate", System.DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@StartDate", StartDate);
                        }

                        if (EndDate == null)
                        {
                            command.Parameters.AddWithValue("@EndDate", System.DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@EndDate", EndDate);
                        }

                        if (CampaignId == null)
                        {
                            command.Parameters.AddWithValue("@CampaignId", System.DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@CampaignId", CampaignId);
                        }

                        DataTable dt = new DataTable();
                        SqlDataAdapter sqlA = new SqlDataAdapter(command);
                        sqlA.Fill(dt);

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(AccountID.ToString(), "Campaign-GetCampaignReportEmailsSentListing", string.Empty, "Error", ex.Message);
                return null;
            }
        }


        public DataTable GetCampaignReportTotalOpensListing(Guid AccountID, DateTime? StartDate, DateTime? EndDate, int? CampaignId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_Campaigns_TotalOpensListing";

                        command.Parameters.AddWithValue("@AccountId", AccountID);

                        if (StartDate == null)
                        {
                            command.Parameters.AddWithValue("@StartDate", System.DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@StartDate", StartDate);
                        }

                        if (EndDate == null)
                        {
                            command.Parameters.AddWithValue("@EndDate", System.DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@EndDate", EndDate);
                        }

                        if (CampaignId == null)
                        {
                            command.Parameters.AddWithValue("@CampaignId", System.DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@CampaignId", CampaignId);
                        }

                        DataTable dt = new DataTable();
                        SqlDataAdapter sqlA = new SqlDataAdapter(command);
                        sqlA.Fill(dt);

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(AccountID.ToString(), "Campaign-GetCampaignReportTotalOpensListing", string.Empty, "Error", ex.Message);
                return null;
            }
        }


        public DataTable GetCampaignReportUniqueOpensListing(Guid AccountID, DateTime? StartDate, DateTime? EndDate, int? CampaignId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_Campaigns_UniqueOpensListing";

                        command.Parameters.AddWithValue("@AccountId", AccountID);

                        if (StartDate == null)
                        {
                            command.Parameters.AddWithValue("@StartDate", System.DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@StartDate", StartDate);
                        }

                        if (EndDate == null)
                        {
                            command.Parameters.AddWithValue("@EndDate", System.DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@EndDate", EndDate);
                        }

                        if (CampaignId == null)
                        {
                            command.Parameters.AddWithValue("@CampaignId", System.DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@CampaignId", CampaignId);
                        }

                        DataTable dt = new DataTable();
                        SqlDataAdapter sqlA = new SqlDataAdapter(command);
                        sqlA.Fill(dt);

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(AccountID.ToString(), "Campaign-GetCampaignReportUniqueOpensListing", string.Empty, "Error", ex.Message);
                return null;
            }
        }



        public DataTable GetCampaignReportTotalClicksListing(Guid AccountID, DateTime? StartDate, DateTime? EndDate, int? CampaignId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_Campaigns_TotalClicksListing";

                        command.Parameters.AddWithValue("@AccountId", AccountID);

                        if (StartDate == null)
                        {
                            command.Parameters.AddWithValue("@StartDate", System.DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@StartDate", StartDate);
                        }

                        if (EndDate == null)
                        {
                            command.Parameters.AddWithValue("@EndDate", System.DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@EndDate", EndDate);
                        }

                        if (CampaignId == null)
                        {
                            command.Parameters.AddWithValue("@CampaignId", System.DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@CampaignId", CampaignId);
                        }

                        DataTable dt = new DataTable();
                        SqlDataAdapter sqlA = new SqlDataAdapter(command);
                        sqlA.Fill(dt);

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(AccountID.ToString(), "Campaign-GetCampaignReportTotalClicksListing", string.Empty, "Error", ex.Message);
                return null;
            }
        }


        public DataTable GetCampaignReportUniqueClicksListing(Guid AccountID, DateTime? StartDate, DateTime? EndDate, int? CampaignId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_Campaigns_UniqueClicksListing";

                        command.Parameters.AddWithValue("@AccountId", AccountID);

                        if (StartDate == null)
                        {
                            command.Parameters.AddWithValue("@StartDate", System.DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@StartDate", StartDate);
                        }

                        if (EndDate == null)
                        {
                            command.Parameters.AddWithValue("@EndDate", System.DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@EndDate", EndDate);
                        }

                        if (CampaignId == null)
                        {
                            command.Parameters.AddWithValue("@CampaignId", System.DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@CampaignId", CampaignId);
                        }

                        DataTable dt = new DataTable();
                        SqlDataAdapter sqlA = new SqlDataAdapter(command);
                        sqlA.Fill(dt);

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(AccountID.ToString(), "Campaign-GetCampaignReportUniqueClicksListing", string.Empty, "Error", ex.Message);
                return null;
            }
        }
    }
}
