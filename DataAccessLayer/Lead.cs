using System;
using System.Data.SqlClient;
using System.Data;
using CSDataSqlCommand;
using System.Configuration;



namespace DataAccessLayer.Controller
{
   public class Lead
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AutoMarketerConnectionString"].ConnectionString.ToString();

        public bool UniqueEmail(string EmailAddress, Guid AccountId, Guid? ContactID)
        {
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[3];

                sqlParam[0] = new SqlParameter("@EmailAddress", SqlDbType.NVarChar, 250);
                sqlParam[0].Value = EmailAddress;

                sqlParam[1] = new SqlParameter("@AccountId", SqlDbType.UniqueIdentifier);
                sqlParam[1].Value = AccountId;

                sqlParam[2] = new SqlParameter("@ContactID", SqlDbType.UniqueIdentifier);

                if (ContactID == null)
                {
                    sqlParam[2].Value = System.DBNull.Value;
                }
                else
                {
                    sqlParam[2].Value = ContactID;
                }

                bool result = (bool)SqlHelper.ExecuteScalar(connectionString, @"SELECT dbo.fn_NewLead_UNIQUEEMAILADDRESS(@EmailAddress,@AccountId,@ContactID)", System.Data.CommandType.Text, sqlParam);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(EmailAddress, "Lead-UniqueEmail", string.Empty, "Error", ex.Message);
                return false;
            }
        }

        public bool CreateLead(Guid AccountID,
        string Title,
        string FirstName,
        string MiddleName,
        string LastName,
        string CompanyName,
        string WebSite,
        string Position,
        int? ContactType,
        int? LeadStatus,
        int? ContactStatus,
        string Gender,
        string EmailAddress,
        string PhoneNumber,
        string MobileNumber,
        string FacebookID,
        string Address1,
        string Address2,
        string City,
        string State,
        int? CountryId,
        string ZipCode,
        bool IsSubscribedToEmail,
        bool UseForTesting,
        Guid CreatedBy,
        string IPAddress,
        out Guid? LeadGuid)
        {
            LeadGuid = null;
            
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        SqlTransaction transaction;
                        transaction = connection.BeginTransaction("AddLead");

                        command.CommandType = CommandType.StoredProcedure;
                        command.Transaction = transaction;

                        try
                        {

                            command.Parameters.AddWithValue("@AccountID", AccountID);

                            if (string.IsNullOrEmpty(Title))
                            {
                                command.Parameters.AddWithValue("@Title", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Title", Title);
                            }

                            command.Parameters.AddWithValue("@FirstName", FirstName);

                            if (string.IsNullOrEmpty(MiddleName))
                            {
                                command.Parameters.AddWithValue("@MiddleName", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@MiddleName", MiddleName);
                            }

                            command.Parameters.AddWithValue("@LastName", LastName);

                            if (string.IsNullOrEmpty(CompanyName))
                            {
                                command.Parameters.AddWithValue("@CompanyName", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@CompanyName", CompanyName);
                            }

                            if (string.IsNullOrEmpty(WebSite))
                            {
                                command.Parameters.AddWithValue("@WebSite", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@WebSite", WebSite);
                            }

                            if (string.IsNullOrEmpty(Position))
                            {
                                command.Parameters.AddWithValue("@Position", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Position", Position);
                            }

                            if (!ContactType.HasValue)
                            {
                                command.Parameters.AddWithValue("@ContactType", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@ContactType", ContactType);
                            }

                            if (!LeadStatus.HasValue)
                            {
                                command.Parameters.AddWithValue("@LeadStatus", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@LeadStatus", LeadStatus);
                            }

                            if (!ContactStatus.HasValue)
                            {
                                command.Parameters.AddWithValue("@ContactStatus", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@ContactStatus", ContactStatus);
                            }

                            if (string.IsNullOrEmpty(Gender))
                            {
                                command.Parameters.AddWithValue("@Gender", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Gender", Gender);
                            }

                            command.Parameters.AddWithValue("@EmailAddress", EmailAddress);

                            if (string.IsNullOrEmpty(PhoneNumber))
                            {
                                command.Parameters.AddWithValue("@PhoneNumber", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                            }

                            if (string.IsNullOrEmpty(MobileNumber))
                            {
                                command.Parameters.AddWithValue("@MobileNumber", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@MobileNumber", MobileNumber);
                            }

                            if (string.IsNullOrEmpty(FacebookID))
                            {
                                command.Parameters.AddWithValue("@FacebookID", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@FacebookID", FacebookID);
                            }

                            if (string.IsNullOrEmpty(Address1))
                            {
                                command.Parameters.AddWithValue("@Address1", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Address1", Address1);
                            }

                            if (string.IsNullOrEmpty(Address2))
                            {
                                command.Parameters.AddWithValue("@Address2", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Address2", Address2);
                            }

                            if (string.IsNullOrEmpty(City))
                            {
                                command.Parameters.AddWithValue("@City", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@City", City);
                            }


                            if (string.IsNullOrEmpty(State))
                            {
                                command.Parameters.AddWithValue("@State", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@State", State);
                            }

                            if (!CountryId.HasValue)
                            {
                                command.Parameters.AddWithValue("@CountryId", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@CountryId", CountryId);
                            }

                            if (string.IsNullOrEmpty(ZipCode))
                            {
                                command.Parameters.AddWithValue("@ZipCode", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@ZipCode", ZipCode);
                            }

                            command.Parameters.AddWithValue("@SubscribedToEmail", IsSubscribedToEmail);
                            command.Parameters.AddWithValue("@UseForTesting", UseForTesting);
                            command.Parameters.AddWithValue("@CreatedBy", CreatedBy);

                            SqlParameter out_LeadGUID = new SqlParameter("@LeadGUID", SqlDbType.UniqueIdentifier);
                            out_LeadGUID.Direction = ParameterDirection.Output;
                            command.Parameters.Add(out_LeadGUID);


                            command.CommandText = "S_LEAD_ADDLEAD";
                            command.CommandType = CommandType.StoredProcedure;
                            int success = command.ExecuteNonQuery();

                            if (out_LeadGUID.Value != DBNull.Value)
                                LeadGuid = Guid.Parse(out_LeadGUID.Value.ToString());

                            /************S_LEAD_ADDContactSubscription*************/

                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@CONTACT_ID", LeadGuid);

                            if (IsSubscribedToEmail)
                            {
                                command.Parameters.AddWithValue("@ACTION", (int)ConstantValues.ContactAction.SUBSCRIBE);
                                command.Parameters.AddWithValue("@SUBSCRIBED_VIA", "PC");
                                command.Parameters.AddWithValue("@SUBSCRIBED_BY", CreatedBy);

                                if (!string.IsNullOrEmpty(IPAddress))
                                {
                                    command.Parameters.AddWithValue("@SUBSCRIBE_IP_ADDRESS", IPAddress);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@SUBSCRIBE_IP_ADDRESS", System.DBNull.Value);
                                }
                                command.Parameters.AddWithValue("@UNSUBSCRIBED_VIA", System.DBNull.Value);
                                command.Parameters.AddWithValue("@UNSUBSCRIBED_BY", System.DBNull.Value);
                                command.Parameters.AddWithValue("@UNSUBSCRIBE_IP_ADDRESS", System.DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@ACTION", (int)ConstantValues.ContactAction.UNSUBSCRIBE);
                                command.Parameters.AddWithValue("@SUBSCRIBED_VIA", System.DBNull.Value);
                                command.Parameters.AddWithValue("@SUBSCRIBED_BY", System.DBNull.Value);
                                command.Parameters.AddWithValue("@SUBSCRIBE_IP_ADDRESS", System.DBNull.Value);

                                command.Parameters.AddWithValue("@UNSUBSCRIBED_VIA", "PC");
                                command.Parameters.AddWithValue("@UNSUBSCRIBED_BY", CreatedBy);

                                if (!string.IsNullOrEmpty(IPAddress))
                                {
                                    command.Parameters.AddWithValue("@UNSUBSCRIBE_IP_ADDRESS", IPAddress);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@UNSUBSCRIBE_IP_ADDRESS", System.DBNull.Value);
                                }
                            }
                            

                            command.CommandText = "S_LEAD_ADDContactSubscription";
                            command.CommandType = CommandType.StoredProcedure;
                            success = command.ExecuteNonQuery();

                            /************S_LEAD_ADDContactSubscriptionHistory*************/
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@CONTACT_ID", LeadGuid);
                            command.Parameters.AddWithValue("@EMAIL_ADDRESS", EmailAddress);
                            if (IsSubscribedToEmail)
                            {
                                command.Parameters.AddWithValue("@ACTION_ID", (int)ConstantValues.ContactAction.SUBSCRIBE);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@ACTION_ID", (int)ConstantValues.ContactAction.UNSUBSCRIBE);
                            }
                                
                            command.Parameters.AddWithValue("@ACTION_BY", CreatedBy.ToString());
                            command.Parameters.AddWithValue("@ACTION_VIA", "PC");

                            if (!string.IsNullOrEmpty(IPAddress))
                            {
                                command.Parameters.AddWithValue("@IP_ADDRESS", IPAddress);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@IP_ADDRESS", System.DBNull.Value);
                            }

                            command.Parameters.AddWithValue("@MESSAGE_ID", System.DBNull.Value);
                            command.Parameters.AddWithValue("@COUNTRY_CODE", CountryId.ToString());
                            command.Parameters.AddWithValue("@CITY_NAME", City);
                            command.Parameters.AddWithValue("@REGION_NAME", State);
                            command.Parameters.AddWithValue("@CampaignID", System.DBNull.Value);

                            command.CommandText = "S_LEAD_ADDContactSubscriptionHistory";
                            command.CommandType = CommandType.StoredProcedure;
                            success = command.ExecuteNonQuery();

                            transaction.Commit();
                            return success == 0 ? false : true;
                        }
                        catch(Exception ex)
                        {
                            transaction.Rollback();
                            Logger.Logger.WriteLog(CreatedBy.ToString(), "Lead-CreateLead", EmailAddress, "Error", ex.Message);
                            return false;
                        }
                         
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(CreatedBy.ToString(), "Lead-CreateLead", EmailAddress, "Error", ex.Message);
                return false;
            }
        }

        public DataModels.Lead SelectLead(Guid LeadGuid, Guid AccountID)
        {
           try
            {
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter();
                sqlParam[0].ParameterName = "@ContactID";
                sqlParam[0].Value = LeadGuid;
                sqlParam[0].Direction = System.Data.ParameterDirection.Input;

                sqlParam[1] = new SqlParameter();
                sqlParam[1].ParameterName = "@AccountID";
                sqlParam[1].Value = AccountID;
                sqlParam[1].Direction = System.Data.ParameterDirection.Input;


                SqlDataReader sqlDR = SqlHelper.ExecuteReader(connectionString, "S_LEAD_GET", System.Data.CommandType.StoredProcedure, sqlParam);
                DataModels.Lead objLead = null;

                if (sqlDR.HasRows)
                {
                    while (sqlDR.Read())
                    {
                        objLead = new DataModels.Lead();
                        objLead.ContactID = Guid.Parse(sqlDR["ContactID"].ToString().Trim());
                        objLead.AccountID = Guid.Parse(sqlDR["AccountID"].ToString().Trim());
                        objLead.FirstName = sqlDR["FirstName"].ToString().Trim();

                        if (sqlDR["MiddleName"] == System.DBNull.Value)
                        {
                            objLead.MiddleName = string.Empty;
                        }
                        else
                        {
                            objLead.MiddleName = sqlDR["MiddleName"].ToString().Trim();
                        }
                       
                        objLead.LastName = sqlDR["LastName"].ToString().Trim();

                        if (sqlDR["MobileNumber"] == System.DBNull.Value)
                        {
                            objLead.MobileNumber = string.Empty;
                        }
                        else
                        {
                            objLead.MobileNumber = sqlDR["MobileNumber"].ToString().Trim();
                        }

                        if (sqlDR["PhoneNumber"] == System.DBNull.Value)
                        {
                            objLead.PhoneNumber = string.Empty;
                        }
                        else
                        {
                            objLead.PhoneNumber = sqlDR["PhoneNumber"].ToString().Trim();
                        }

                        if (sqlDR["EmailAddress"] == System.DBNull.Value)
                        {
                            objLead.EmailAddress = string.Empty;
                        }
                        else
                        {
                            objLead.EmailAddress = sqlDR["EmailAddress"].ToString().Trim();
                        }

                        if (sqlDR["Address1"] == System.DBNull.Value)
                        {
                            objLead.Address1 = string.Empty;
                        }
                        else
                        {
                            objLead.Address1 = sqlDR["Address1"].ToString().Trim();
                        }

                        if (sqlDR["Address2"] == System.DBNull.Value)
                        {
                            objLead.Address2 = string.Empty;
                        }
                        else
                        {
                            objLead.Address2 = sqlDR["Address2"].ToString().Trim();
                        }

                        if (sqlDR["CountryId"] == System.DBNull.Value)
                        {
                            objLead.CountryId = null;
                        }
                        else
                        {
                            objLead.CountryId = int.Parse(sqlDR["CountryId"].ToString().Trim());
                        }

                        if (sqlDR["ZipCode"] == System.DBNull.Value)
                        {
                            objLead.ZipCode = string.Empty;
                        }
                        else
                        {
                            objLead.ZipCode = sqlDR["ZipCode"].ToString().Trim();
                        }

                        if (sqlDR["CompanyName"] == System.DBNull.Value)
                        {
                            objLead.CompanyName = string.Empty;
                        }
                        else
                        {
                            objLead.CompanyName = sqlDR["CompanyName"].ToString().Trim();
                        }

                        
                        objLead.CreatedDate = DateTime.Parse(sqlDR["CreatedDate"].ToString().Trim());
                        objLead.CreatedBy = Guid.Parse(sqlDR["CreatedBy"].ToString().Trim());

                        if (sqlDR["ModifiedDate"] == System.DBNull.Value)
                        {
                            objLead.ModifiedDate = null;
                        }
                        else
                        {
                            objLead.ModifiedDate = DateTime.Parse(sqlDR["ModifiedDate"].ToString().Trim());
                        }

                        if (sqlDR["ModifiedBy"] == System.DBNull.Value)
                        {
                            objLead.ModifiedBy = null;
                        }
                        else
                        {
                            objLead.ModifiedBy = Guid.Parse(sqlDR["ModifiedBy"].ToString().Trim());
                        }

                        if (sqlDR["WebSite"] == System.DBNull.Value)
                        {
                            objLead.WebSite = string.Empty;
                        }
                        else
                        {
                            objLead.WebSite = sqlDR["WebSite"].ToString().Trim();
                        }

                        if (sqlDR["Position"] == System.DBNull.Value)
                        {
                            objLead.Position = string.Empty;
                        }
                        else
                        {
                            objLead.Position = sqlDR["Position"].ToString().Trim();
                        }


                        if (sqlDR["FacebookID"] == System.DBNull.Value)
                        {
                            objLead.FacebookID = string.Empty;
                        }
                        else
                        {
                            objLead.FacebookID = sqlDR["FacebookID"].ToString().Trim();
                        }

                        objLead.ID = long.Parse(sqlDR["ID"].ToString());

                        if (sqlDR["LeadStatus"] == System.DBNull.Value)
                        {
                            objLead.LeadStatus = null;
                        }
                        else
                        {
                            objLead.LeadStatus = int.Parse(sqlDR["LeadStatus"].ToString().Trim());
                        }

                        if (sqlDR["ContactStatus"] == System.DBNull.Value)
                        {
                            objLead.ContactStatus = null;
                        }
                        else
                        {
                            objLead.ContactStatus = int.Parse(sqlDR["ContactStatus"].ToString().Trim());
                        }

                        if (sqlDR["ContactType"] == System.DBNull.Value)
                        {
                            objLead.ContactType = null;
                        }
                        else
                        {
                            objLead.ContactType = int.Parse(sqlDR["ContactType"].ToString().Trim());
                        }

                        if (sqlDR["Title"] == System.DBNull.Value)
                        {
                            objLead.Title = string.Empty;
                        }
                        else
                        {
                            objLead.Title = sqlDR["Title"].ToString().Trim();
                        }

                        if (sqlDR["Gender"] == System.DBNull.Value)
                        {
                            objLead.Gender = null;
                        }
                        else
                        {
                            objLead.Gender = sqlDR["Gender"].ToString().Trim();
                        }

                        if (sqlDR["City"] == System.DBNull.Value)
                        {
                            objLead.City = string.Empty;
                        }
                        else
                        {
                            objLead.City = sqlDR["City"].ToString().Trim();
                        }

                        if (sqlDR["State"] == System.DBNull.Value)
                        {
                            objLead.State = string.Empty;
                        }
                        else
                        {
                            objLead.State = sqlDR["State"].ToString().Trim();
                        }

                        if (sqlDR["SubscribedToEmail"] == System.DBNull.Value)
                        {
                            objLead.SubscribedToEmail =false;
                        }
                        else
                        {
                            objLead.SubscribedToEmail = bool.Parse(sqlDR["SubscribedToEmail"].ToString().Trim());
                        }

                        if (sqlDR["UseforTesting"] == System.DBNull.Value)
                        {
                            objLead.UseforTesting = false;
                        }
                        else
                        {
                            objLead.UseforTesting = bool.Parse(sqlDR["UseforTesting"].ToString().Trim());
                        }

                        if (sqlDR["IsDeleted"] == System.DBNull.Value)
                        {
                            objLead.IsDeleted = false;
                        }
                        else
                        {
                            objLead.IsDeleted = bool.Parse(sqlDR["IsDeleted"].ToString().Trim());
                        }

                        if (sqlDR["DeletedDate"] == System.DBNull.Value)
                        {
                            objLead.DeletedDate = null;
                        }
                        else
                        {
                            objLead.DeletedDate = DateTime.Parse(sqlDR["DeletedDate"].ToString().Trim());
                        }
                    }
                }

                sqlDR.Close();

                return objLead;
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(LeadGuid.ToString(), "Lead-SelectLead", LeadGuid.ToString(), "Error", ex.Message);
                return null;
            }
        }



        public bool UpdateLead(Guid AccountID,
        Guid ContactID,
        string Title,
        string FirstName,
        string MiddleName,
        string LastName,
        string CompanyName,
        string WebSite,
        string Position,
        int? ContactType,
        int? LeadStatus,
        int? ContactStatus,
        string Gender,
        string EmailAddress,
        string PhoneNumber,
        string MobileNumber,
        string FacebookID,
        string Address1,
        string Address2,
        string City,
        string State,
        int? CountryId,
        string ZipCode,
        bool IsSubscribedToEmail,
        bool UseForTesting,
        Guid ModifiedBy,
        string IPAddress,
        bool SubscriptionChanged)
        {
            
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        SqlTransaction transaction;
                        transaction = connection.BeginTransaction("UpdateLead");

                        command.CommandType = CommandType.StoredProcedure;
                        command.Transaction = transaction;

                        try
                        {

                            command.Parameters.AddWithValue("@AccountID", AccountID);
                            command.Parameters.AddWithValue("@ContactID", ContactID);

                            if (string.IsNullOrEmpty(Title))
                            {
                                command.Parameters.AddWithValue("@Title", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Title", Title);
                            }

                            command.Parameters.AddWithValue("@FirstName", FirstName);

                            if (string.IsNullOrEmpty(MiddleName))
                            {
                                command.Parameters.AddWithValue("@MiddleName", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@MiddleName", MiddleName);
                            }

                            command.Parameters.AddWithValue("@LastName", LastName);

                            if (string.IsNullOrEmpty(CompanyName))
                            {
                                command.Parameters.AddWithValue("@CompanyName", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@CompanyName", CompanyName);
                            }

                            if (string.IsNullOrEmpty(WebSite))
                            {
                                command.Parameters.AddWithValue("@WebSite", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@WebSite", WebSite);
                            }

                            if (string.IsNullOrEmpty(Position))
                            {
                                command.Parameters.AddWithValue("@Position", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Position", Position);
                            }

                            if (!ContactType.HasValue)
                            {
                                command.Parameters.AddWithValue("@ContactType", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@ContactType", ContactType);
                            }

                            if (!LeadStatus.HasValue)
                            {
                                command.Parameters.AddWithValue("@LeadStatus", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@LeadStatus", LeadStatus);
                            }

                            if (!ContactStatus.HasValue)
                            {
                                command.Parameters.AddWithValue("@ContactStatus", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@ContactStatus", ContactStatus);
                            }

                            if (string.IsNullOrEmpty(Gender))
                            {
                                command.Parameters.AddWithValue("@Gender", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Gender", Gender);
                            }

                            command.Parameters.AddWithValue("@EmailAddress", EmailAddress);

                            if (string.IsNullOrEmpty(PhoneNumber))
                            {
                                command.Parameters.AddWithValue("@PhoneNumber", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                            }

                            if (string.IsNullOrEmpty(MobileNumber))
                            {
                                command.Parameters.AddWithValue("@MobileNumber", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@MobileNumber", MobileNumber);
                            }

                            if (string.IsNullOrEmpty(FacebookID))
                            {
                                command.Parameters.AddWithValue("@FacebookID", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@FacebookID", FacebookID);
                            }

                            if (string.IsNullOrEmpty(Address1))
                            {
                                command.Parameters.AddWithValue("@Address1", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Address1", Address1);
                            }

                            if (string.IsNullOrEmpty(Address2))
                            {
                                command.Parameters.AddWithValue("@Address2", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Address2", Address2);
                            }

                            if (string.IsNullOrEmpty(City))
                            {
                                command.Parameters.AddWithValue("@City", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@City", City);
                            }


                            if (string.IsNullOrEmpty(State))
                            {
                                command.Parameters.AddWithValue("@State", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@State", State);
                            }

                            if (!CountryId.HasValue)
                            {
                                command.Parameters.AddWithValue("@CountryId", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@CountryId", CountryId);
                            }

                            if (string.IsNullOrEmpty(ZipCode))
                            {
                                command.Parameters.AddWithValue("@ZipCode", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@ZipCode", ZipCode);
                            }

                            command.Parameters.AddWithValue("@SubscribedToEmail", IsSubscribedToEmail);
                            command.Parameters.AddWithValue("@UseForTesting", UseForTesting);
                            command.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);

                            
                            command.CommandText = "S_LEAD_UPDATELEAD";
                            command.CommandType = CommandType.StoredProcedure;
                            int success = command.ExecuteNonQuery();


                            /************S_LEAD_UpdateContactSubscription*************/

                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@CONTACT_ID", ContactID);

                            if (SubscriptionChanged)
                            {
                                if (IsSubscribedToEmail)
                                {
                                    command.Parameters.AddWithValue("@ACTION", (int)ConstantValues.ContactAction.SUBSCRIBE);
                                    command.Parameters.AddWithValue("@SUBSCRIBED_VIA", "PC");
                                    command.Parameters.AddWithValue("@SUBSCRIBED_BY", ModifiedBy);

                                    if (!string.IsNullOrEmpty(IPAddress))
                                    {
                                        command.Parameters.AddWithValue("@SUBSCRIBE_IP_ADDRESS", IPAddress);
                                    }
                                    else
                                    {
                                        command.Parameters.AddWithValue("@SUBSCRIBE_IP_ADDRESS", System.DBNull.Value);
                                    }
                                    command.Parameters.AddWithValue("@UNSUBSCRIBED_VIA", System.DBNull.Value);
                                    command.Parameters.AddWithValue("@UNSUBSCRIBED_BY", System.DBNull.Value);
                                    command.Parameters.AddWithValue("@UNSUBSCRIBE_IP_ADDRESS", System.DBNull.Value);
                                    command.Parameters.AddWithValue("@SUBCRIPTIONCHANGED", SubscriptionChanged);
                                    command.Parameters.AddWithValue("@CampaignID", System.DBNull.Value);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@ACTION", (int)ConstantValues.ContactAction.UNSUBSCRIBE);
                                    command.Parameters.AddWithValue("@SUBSCRIBED_VIA", System.DBNull.Value);
                                    command.Parameters.AddWithValue("@SUBSCRIBED_BY", System.DBNull.Value);
                                    command.Parameters.AddWithValue("@SUBSCRIBE_IP_ADDRESS", System.DBNull.Value);

                                    command.Parameters.AddWithValue("@UNSUBSCRIBED_VIA", "PC");
                                    command.Parameters.AddWithValue("@UNSUBSCRIBED_BY", ModifiedBy);

                                    if (!string.IsNullOrEmpty(IPAddress))
                                    {
                                        command.Parameters.AddWithValue("@UNSUBSCRIBE_IP_ADDRESS", IPAddress);
                                    }
                                    else
                                    {
                                        command.Parameters.AddWithValue("@UNSUBSCRIBE_IP_ADDRESS", System.DBNull.Value);
                                    }
                                    command.Parameters.AddWithValue("@SUBCRIPTIONCHANGED", SubscriptionChanged);
                                    command.Parameters.AddWithValue("@CampaignID", System.DBNull.Value);
                                }
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@ACTION", (int)ConstantValues.ContactAction.UPDATECONTACTDETAILS);
                                command.Parameters.AddWithValue("@SUBSCRIBED_VIA", System.DBNull.Value);
                                command.Parameters.AddWithValue("@SUBSCRIBED_BY", System.DBNull.Value);
                                command.Parameters.AddWithValue("@SUBSCRIBE_IP_ADDRESS", System.DBNull.Value);
                                command.Parameters.AddWithValue("@UNSUBSCRIBED_VIA", System.DBNull.Value);
                                command.Parameters.AddWithValue("@UNSUBSCRIBED_BY", System.DBNull.Value);
                                command.Parameters.AddWithValue("@UNSUBSCRIBE_IP_ADDRESS", System.DBNull.Value);
                                command.Parameters.AddWithValue("@SUBCRIPTIONCHANGED", SubscriptionChanged);
                                command.Parameters.AddWithValue("@CampaignID", System.DBNull.Value);
                            }

                            command.CommandText = "S_LEAD_UpdateContactSubscription";
                            command.CommandType = CommandType.StoredProcedure;
                            success = command.ExecuteNonQuery();

                            /************S_LEAD_ADDContactSubscriptionHistory*************/
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@CONTACT_ID", ContactID);
                            command.Parameters.AddWithValue("@EMAIL_ADDRESS", EmailAddress);
                            if (SubscriptionChanged)
                            {
                                if (IsSubscribedToEmail)
                                {
                                    command.Parameters.AddWithValue("@ACTION_ID", (int)ConstantValues.ContactAction.SUBSCRIBE);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@ACTION_ID", (int)ConstantValues.ContactAction.UNSUBSCRIBE);
                                }
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@ACTION_ID", (int)ConstantValues.ContactAction.UPDATECONTACTDETAILS);
                            }

                            command.Parameters.AddWithValue("@ACTION_BY", ModifiedBy.ToString());
                            command.Parameters.AddWithValue("@ACTION_VIA", "PC");

                            if (!string.IsNullOrEmpty(IPAddress))
                            {
                                command.Parameters.AddWithValue("@IP_ADDRESS", IPAddress);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@IP_ADDRESS", System.DBNull.Value);
                            }

                            command.Parameters.AddWithValue("@MESSAGE_ID", System.DBNull.Value);

                            if (!CountryId.HasValue)
                            {
                                command.Parameters.AddWithValue("@COUNTRY_CODE", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@COUNTRY_CODE", CountryId.ToString());
                            }

                            if (string.IsNullOrEmpty(City))
                            {
                                command.Parameters.AddWithValue("@CITY_NAME", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@CITY_NAME", City);
                            }


                            if (string.IsNullOrEmpty(State))
                            {
                                command.Parameters.AddWithValue("@REGION_NAME", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@REGION_NAME", State);
                            }

                            command.Parameters.AddWithValue("@CampaignID", System.DBNull.Value);

                            command.CommandText = "S_LEAD_ADDContactSubscriptionHistory";
                            command.CommandType = CommandType.StoredProcedure;
                            success = command.ExecuteNonQuery();

                            transaction.Commit();
                            return success == 0 ? false : true;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            Logger.Logger.WriteLog(ModifiedBy.ToString(), "Lead-UpdateLead", EmailAddress.ToString(), "Error", ex.Message);
                            return false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(ModifiedBy.ToString(), "Lead-UpdateLead", ContactID.ToString(), "Error", ex.Message);
                return false;
            }
        }


        public bool Unsubscribe(Guid AccountID,
       Guid ContactID,
        int CampaignId,
         int MessageId,
         int ContactAction,
       Guid ModifiedBy,
       string IPAddress,
       string UnsubscribedVia,
       bool SubscriptionChanged,
       string EmailAddress,
       int? CountryId,
       string City,
       string State)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        SqlTransaction transaction;
                        transaction = connection.BeginTransaction("UnsubscribeLead");

                        command.CommandType = CommandType.StoredProcedure;
                        command.Transaction = transaction;

                        try
                        {
                            /************S_LEAD_UNSUBSCRIBE*************/

                            command.Parameters.AddWithValue("@AccountID", AccountID);
                            command.Parameters.AddWithValue("@ContactID", ContactID);
                            command.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);


                            command.CommandText = "S_LEAD_UNSUBSCRIBE";
                            command.CommandType = CommandType.StoredProcedure;
                            int success = command.ExecuteNonQuery();


                            /************S_LEAD_UpdateContactSubscription*************/

                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@CONTACT_ID", ContactID);
                            command.Parameters.AddWithValue("@ACTION", ContactAction);
                            command.Parameters.AddWithValue("@SUBSCRIBED_VIA", System.DBNull.Value);
                            command.Parameters.AddWithValue("@SUBSCRIBED_BY", System.DBNull.Value);
                            command.Parameters.AddWithValue("@SUBSCRIBE_IP_ADDRESS", System.DBNull.Value);

                            command.Parameters.AddWithValue("@UNSUBSCRIBED_VIA", UnsubscribedVia);
                            command.Parameters.AddWithValue("@UNSUBSCRIBED_BY", ModifiedBy);

                            if (!string.IsNullOrEmpty(IPAddress))
                            {
                                command.Parameters.AddWithValue("@UNSUBSCRIBE_IP_ADDRESS", IPAddress);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@UNSUBSCRIBE_IP_ADDRESS", System.DBNull.Value);
                            }
                            command.Parameters.AddWithValue("@SUBCRIPTIONCHANGED", SubscriptionChanged);
                            command.Parameters.AddWithValue("@CampaignID", CampaignId);

                            command.CommandText = "S_LEAD_UpdateContactSubscription";
                            command.CommandType = CommandType.StoredProcedure;
                            success = command.ExecuteNonQuery();

                            /************S_LEAD_ADDContactSubscriptionHistory*************/
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@CONTACT_ID", ContactID);
                            command.Parameters.AddWithValue("@EMAIL_ADDRESS", EmailAddress);
                            command.Parameters.AddWithValue("@ACTION_ID", ContactAction);
                            
                            

                            command.Parameters.AddWithValue("@ACTION_BY", ModifiedBy.ToString());
                            command.Parameters.AddWithValue("@ACTION_VIA", UnsubscribedVia);

                            if (!string.IsNullOrEmpty(IPAddress))
                            {
                                command.Parameters.AddWithValue("@IP_ADDRESS", IPAddress);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@IP_ADDRESS", System.DBNull.Value);
                            }

                            command.Parameters.AddWithValue("@MESSAGE_ID", MessageId);

                            if (!CountryId.HasValue)
                            {
                                command.Parameters.AddWithValue("@COUNTRY_CODE", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@COUNTRY_CODE", CountryId.ToString());
                            }

                            if (string.IsNullOrEmpty(City))
                            {
                                command.Parameters.AddWithValue("@CITY_NAME", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@CITY_NAME", City);
                            }


                            if (string.IsNullOrEmpty(State))
                            {
                                command.Parameters.AddWithValue("@REGION_NAME", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@REGION_NAME", State);
                            }

                            command.Parameters.AddWithValue("@CampaignID", CampaignId);

                            command.CommandText = "S_LEAD_ADDContactSubscriptionHistory";
                            command.CommandType = CommandType.StoredProcedure;
                            success = command.ExecuteNonQuery();

                            transaction.Commit();
                            return success == 0 ? false : true;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            Logger.Logger.WriteLog(ModifiedBy.ToString(), "Lead-Unsubscribe", EmailAddress.ToString(), "Error", ex.Message);
                            return false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(ModifiedBy.ToString(), "Lead-Unsubscribe", ContactID.ToString(), "Error", ex.Message);
                return false;
            }
        }

        public bool DeleteLead(Guid AccountID,
        Guid ContactID,
        Guid ModifiedBy,
        bool SubscriptionChanged,
        string EmailAddress,
        int? CountryId,
        string City,
        string State,
        string IPAddress)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        SqlTransaction transaction;
                        transaction = connection.BeginTransaction("DeleteLead");

                        command.CommandType = CommandType.StoredProcedure;
                        command.Transaction = transaction;

                        try
                        {
                            command.Parameters.AddWithValue("@ACCOUNT_ID", AccountID);
                            command.Parameters.AddWithValue("@CONTACT_ID", ContactID);
                            command.Parameters.AddWithValue("@MODIFIED_BY", ModifiedBy);

                            command.CommandText = "S_LEAD_DELETELEAD";
                            command.CommandType = CommandType.StoredProcedure;
                            int success = command.ExecuteNonQuery();

                            /************S_LEAD_UpdateContactSubscription*************/

                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@CONTACT_ID", ContactID);
                            command.Parameters.AddWithValue("@ACTION", (int)ConstantValues.ContactAction.DELETE);
                            command.Parameters.AddWithValue("@SUBSCRIBED_VIA", System.DBNull.Value);
                            command.Parameters.AddWithValue("@SUBSCRIBED_BY", System.DBNull.Value);
                            command.Parameters.AddWithValue("@SUBSCRIBE_IP_ADDRESS", System.DBNull.Value);
                            command.Parameters.AddWithValue("@UNSUBSCRIBED_VIA", System.DBNull.Value);
                            command.Parameters.AddWithValue("@UNSUBSCRIBED_BY", System.DBNull.Value);
                            command.Parameters.AddWithValue("@UNSUBSCRIBE_IP_ADDRESS", System.DBNull.Value);
                            command.Parameters.AddWithValue("@SUBCRIPTIONCHANGED", SubscriptionChanged);
                            command.Parameters.AddWithValue("@CampaignID", System.DBNull.Value);

                            command.CommandText = "S_LEAD_UpdateContactSubscription";
                            command.CommandType = CommandType.StoredProcedure;
                            success = command.ExecuteNonQuery();

                            /************S_LEAD_ADDContactSubscriptionHistory*************/
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@CONTACT_ID", ContactID);
                            command.Parameters.AddWithValue("@EMAIL_ADDRESS", EmailAddress);
                            command.Parameters.AddWithValue("@ACTION_ID", (int)ConstantValues.ContactAction.DELETE);
                            command.Parameters.AddWithValue("@ACTION_BY", ModifiedBy.ToString());
                            command.Parameters.AddWithValue("@ACTION_VIA", "PC");
                           
                            if (!string.IsNullOrEmpty(IPAddress))
                            {
                                command.Parameters.AddWithValue("@IP_ADDRESS", IPAddress);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@IP_ADDRESS", System.DBNull.Value);
                            }

                            command.Parameters.AddWithValue("@MESSAGE_ID", System.DBNull.Value);

                            if (!CountryId.HasValue)
                            {
                                command.Parameters.AddWithValue("@COUNTRY_CODE", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@COUNTRY_CODE", CountryId.ToString());
                            }

                            if (string.IsNullOrEmpty(City))
                            {
                                command.Parameters.AddWithValue("@CITY_NAME", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@CITY_NAME", City);
                            }


                            if (string.IsNullOrEmpty(State))
                            {
                                command.Parameters.AddWithValue("@REGION_NAME", DBNull.Value);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@REGION_NAME", State);
                            }
                            command.Parameters.AddWithValue("@CampaignID", System.DBNull.Value);

                            command.CommandText = "S_LEAD_ADDContactSubscriptionHistory";
                            command.CommandType = CommandType.StoredProcedure;
                            success = command.ExecuteNonQuery();

                            transaction.Commit();
                            return success == 0 ? false : true;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            Logger.Logger.WriteLog(ModifiedBy.ToString(), "Lead-DeleteLead", ContactID.ToString(), "Error", ex.Message);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(ModifiedBy.ToString(), "Lead-DeleteLead", ContactID.ToString(), "Error", ex.Message);
                return false;
            }
        }

        public DataTable GetRecentLeads(Guid AccountID, Guid CreatedBy, int MaxRows, string SortBy, string SortDirection, int PageNum, out int NewPageNum, out int MinItem, out int MaxItem, out int TotalRows, out decimal MaxPages)
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
                        command.CommandText = "S_LEAD_GETRECENT";

                        command.Parameters.AddWithValue("@AccountID", AccountID);
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

                Logger.Logger.WriteLog(CreatedBy.ToString(), "Lead-GetRecent", string.Empty, "Error", ex.Message);
                return null;
            }
        }

        public DataTable GetContactPerCountry(Guid AccountID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_LEAD_CONTACT_PerCountry";

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

                Logger.Logger.WriteLog(AccountID.ToString(), "Lead-GetContactPerCountry", string.Empty, "Error", ex.Message);
                return null;
            }
        }

        public DataTable GetLeadTotalByMonth(Guid AccountID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_LEAD_TotalByMonthProgress";

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

                Logger.Logger.WriteLog(AccountID.ToString(), "Lead-GetLeadTotalByMonth", string.Empty, "Error", ex.Message);
                return null;
            }
        }

        public DataTable GetLeadsForSideBar(Guid AccountID, int? Status, bool? Deleted, string SearchText)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_Lead_QuickSearch";

                        command.Parameters.AddWithValue("@Account_ID", AccountID);

                        if (Status == null)
                        {
                            command.Parameters.AddWithValue("@Status_ID", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Status_ID", Status);
                        }

                        if (Deleted == null)
                        {
                            command.Parameters.AddWithValue("@Deleted", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Deleted", Deleted);
                        }

                        if (string.IsNullOrEmpty(SearchText))
                        {
                            command.Parameters.AddWithValue("@Search", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Search", SearchText);
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

                Logger.Logger.WriteLog(AccountID.ToString(), "Lead-S_Lead_QuickSearch", string.Empty, "Error", ex.Message);
                return null;
            }
        }

        public DataTable GetSource(Guid AccountID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_LeadReports_GetSource";

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

                Logger.Logger.WriteLog(AccountID.ToString(), "Lead-GetSource", string.Empty, "Error", ex.Message);
                return null;
            }
        }

        public DataTable GetUnsubscribeSource(Guid AccountID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_LeadReports_GetUnsubcribeSource";

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

                Logger.Logger.WriteLog(AccountID.ToString(), "Lead-GetUnsubscribeSource", string.Empty, "Error", ex.Message);
                return null;
            }
        }
    }
}
