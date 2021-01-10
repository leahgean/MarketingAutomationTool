using System.Data.SqlClient;
using CSDataSqlCommand;
using System.Data;
using System.Configuration;
using System;
using System.Linq;

namespace DataAccessLayer.Controller
{
    public class Account
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AutoMarketerConnectionString"].ConnectionString.ToString();


        public bool UniqueEmail(string EmailAddress)
        {
            try
            {
                SqlParameter[] sqlParam =
                {
                  new SqlParameter("@EmailAddress ", SqlDbType.NVarChar, 250) { Value = EmailAddress },
                };

                bool result = (bool)SqlHelper.ExecuteScalar(connectionString, @"SELECT dbo.fn_NewAccountUser_UNIQUEEMAILADDRESS(@EmailAddress)", System.Data.CommandType.Text, sqlParam);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(EmailAddress, "NewAccount-UniqueEmail", string.Empty, "Error", ex.Message);
                return false;
            }
        }



        public bool CreateAccount(string AccountName,
                                string CompanyWebsite,
                                string PhoneNumber,
                                string FaxNo,
                                string CompanyEmail,
                                string Address,
                                string City,
                                string State,
                                string ZipCode,
                                int CountryId,
                                string UserName,
                                string Password,
                                string FirstName,
                                string MiddleName,
                                string LastName,
                                string MobileNumber,
                                string EmailAddress,
                                bool IsAdminUser,
                                bool IsActiveUser,
                                bool IsOwnerUser,
                                bool IsSuperAdminUser,
                                string PasswordSalt,
                                System.Guid CreatedBy,
                                bool IsActiveAccount,
                                bool IsSuperAdminAccount,
                                string CreatedFromIP)
        {
            try
            {
                SqlParameter[] sqlParam =
                {
                  new SqlParameter("@AccountName ", SqlDbType.NVarChar, 50) { Value = AccountName },
                  new SqlParameter("@CompanyWebsite", SqlDbType.VarChar, 400) { Value = CompanyWebsite },
                  new SqlParameter("@CompanyPhone", SqlDbType.NVarChar, 100) { Value = PhoneNumber },
                  new SqlParameter("@FaxNo", SqlDbType.VarChar,10) { Value = FaxNo },
                  new SqlParameter("@CompanyEmail ", SqlDbType.NVarChar, 250) { Value = CompanyEmail },
                  new SqlParameter("@Address", SqlDbType.NVarChar, 250) { Value = Address },
                  new SqlParameter("@City", SqlDbType.NVarChar, 100) { Value = City },
                  new SqlParameter("@State", SqlDbType.NVarChar, 100) { Value = State },
                  new SqlParameter("@ZipCode", SqlDbType.NVarChar, 20) { Value = ZipCode},
                  new SqlParameter("@CountryId", SqlDbType.Int) { Value = CountryId },
                  new SqlParameter("@UserName", SqlDbType.NVarChar,50) { Value = UserName },
                  new SqlParameter("@Password", SqlDbType.NVarChar, 128) { Value = Password },
                  new SqlParameter("@FirstName", SqlDbType.NVarChar, 100) { Value = FirstName },
                  new SqlParameter("@MiddleName", SqlDbType.NVarChar, 100) { Value = MiddleName },
                  new SqlParameter("@LastName", SqlDbType.NVarChar, 100) { Value = LastName },
                  new SqlParameter("@MobileNumber", SqlDbType.VarChar, 100) { Value = MobileNumber },
                  new SqlParameter("@PhoneNumber", SqlDbType.VarChar, 100) { Value = PhoneNumber },
                  new SqlParameter("@EmailAddress", SqlDbType.NVarChar, 250) { Value = EmailAddress },
                  new SqlParameter("@IsAdminUser", SqlDbType.Bit) { Value = IsAdminUser },
                  new SqlParameter("@IsActiveUser", SqlDbType.Bit) { Value = IsActiveUser },
                  new SqlParameter("@IsOwnerUser", SqlDbType.Bit) { Value = IsOwnerUser },
                  new SqlParameter("@IsSuperAdminUser", SqlDbType.Bit) { Value = IsSuperAdminUser },
                  new SqlParameter("@PasswordSalt", SqlDbType.NVarChar, 128) { Value = PasswordSalt },
                  new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier) { Value = CreatedBy },
                  new SqlParameter("@IsActiveAccount", SqlDbType.Bit) { Value = IsActiveAccount },
                  new SqlParameter("@IsSuperAdminAccount", SqlDbType.Bit) { Value = IsSuperAdminAccount },
                  new SqlParameter("@CreatedFromIP", SqlDbType.VarChar, 15) { Value = CreatedFromIP }
              };
                
                int success = SqlHelper.ExecuteNonQuery(connectionString, true, "S_CREATE_ACCOUNT", System.Data.CommandType.StoredProcedure, sqlParam);

                return success == 0 ? false : true;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(CreatedBy.ToString(), "Account-CreateAccount", AccountName, "Error", ex.Message);
                return false;

            }
        }


        public DataModels.AccountModel GetAccountDetails(Guid AccountId)
        {
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter();
                sqlParam[0].ParameterName = "AccountId";
                sqlParam[0].Value = AccountId;
                sqlParam[0].Direction = System.Data.ParameterDirection.Input;

                SqlDataReader sqlDR = SqlHelper.ExecuteReader(connectionString, "SELECT AccountID, AccountName, CompanyWebsite, CompanyPhone, FaxNo, CompanyEmail, CountryId, CompanyLogo, Personalize, PersonalizedURL, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy, ID, Address, City, State, ZipCode, RegistrationNumber, Email_Admin, Email_Finances, Email_Technical, Email_Marketing, Name_Admin, Name_Finances, Name_Technical, Name_Marketing, IsActive, IsSuperAdmin, CreatedFromIP FROM Account A WITH (NOLOCK) WHERE A.AccountId = @AccountId", System.Data.CommandType.Text, sqlParam);
                DataModels.AccountModel acc = null;

                if (sqlDR.HasRows)
                {
                    while (sqlDR.Read())
                    {
                        acc = new DataModels.AccountModel();
                        acc.AccountID = Guid.Parse(sqlDR["AccountID"].ToString());
                        acc.AccountName = sqlDR["AccountName"].ToString();
                        acc.CompanyWebsite = sqlDR["CompanyWebsite"].ToString();
                        acc.CompanyPhone = sqlDR["CompanyPhone"].ToString();
                        acc.FaxNo = sqlDR["FaxNo"].ToString();
                        acc.CompanyEmail = sqlDR["CompanyEmail"].ToString();
                        acc.CountryId = int.Parse(sqlDR["CountryId"].ToString());
                        acc.CompanyLogo = sqlDR["CompanyLogo"].ToString();

                        if (sqlDR["Personalize"] == System.DBNull.Value)
                        {
                            acc.Personalize = false;
                        }
                        else
                        {
                            acc.Personalize = bool.Parse(sqlDR["Personalize"].ToString());
                        }

                        if (sqlDR["PersonalizedURL"] == System.DBNull.Value)
                        {
                            acc.PersonalizedURL = string.Empty;
                        }
                        else
                        {
                            acc.PersonalizedURL = sqlDR["PersonalizedURL"].ToString();
                        }

                        acc.CreatedDate = DateTime.Parse(sqlDR["CreatedDate"].ToString());

                        if (sqlDR["CreatedBy"] == System.DBNull.Value)
                        {
                            acc.CreatedBy = null;
                        }
                        else
                        {
                            acc.CreatedBy = Guid.Parse(sqlDR["CreatedBy"].ToString());
                        }

                        if (sqlDR["ModifiedDate"] == System.DBNull.Value)
                        {
                            acc.ModifiedDate = null;
                        }
                        else
                        {
                            acc.ModifiedDate = DateTime.Parse(sqlDR["ModifiedDate"].ToString());
                        }

                        if (sqlDR["ModifiedBy"] == System.DBNull.Value)
                        {
                            acc.ModifiedBy = null;
                        }
                        else
                        {
                            acc.ModifiedBy = Guid.Parse(sqlDR["ModifiedBy"].ToString());
                        }

                        acc.Id = int.Parse(sqlDR["Id"].ToString());
                        acc.Address = sqlDR["Address"].ToString();
                        acc.City = sqlDR["City"].ToString();
                        acc.State = sqlDR["State"].ToString();
                        acc.ZipCode = sqlDR["ZipCode"].ToString();
                        if (sqlDR["RegistrationNumber"] == System.DBNull.Value)
                        {
                            acc.RegistrationNumber = string.Empty;
                        }
                        else
                        {
                            acc.RegistrationNumber = sqlDR["RegistrationNumber"].ToString();
                        }
                        acc.Email_Admin = sqlDR["Email_Admin"].ToString();
                        acc.Email_Finances = sqlDR["Email_Finances"].ToString();
                        acc.Email_Technical = sqlDR["Email_Technical"].ToString();
                        acc.Email_Marketing = sqlDR["Email_Marketing"].ToString();
                        acc.Name_Admin = sqlDR["Name_Admin"].ToString();
                        acc.Name_Finances = sqlDR["Name_Finances"].ToString();
                        acc.Name_Technical = sqlDR["Name_Technical"].ToString();
                        acc.Name_Marketing = sqlDR["Name_Marketing"].ToString();
                        acc.IsActive = bool.Parse(sqlDR["IsActive"].ToString());
                        acc.IsSuperAdmin = bool.Parse(sqlDR["IsSuperAdmin"].ToString());
                        acc.CreatedFromIP = sqlDR["CreatedFromIP"].ToString();
                    }
                }

                sqlDR.Close();
                
                return acc;
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(AccountId.ToString(), "Account-GetAccountDetails", AccountId.ToString(), "Error", ex.Message);
                return null;
            }
        }

        public bool UpdateAccountDetails(string RegistrationNumber,
                                string Address,
                                string City,
                                string State,
                                string ZipCode,
                                int CountryId,
                                string WebSite,
                                Guid ModifiedBy,
                                Guid AccountId,
                                string Email_Admin,
                                string Email_Finances,
                                string Email_Technical,
                                string Email_Marketing,
                                string Name_Admin,
                                string Name_Finances,
                                string Name_Technical,
                                string Name_Marketing)
        {
            try
            {
                SqlParameter[] sqlParam =
           {
              new SqlParameter("@RegistrationNumber ", SqlDbType.NVarChar, 50) { Value = RegistrationNumber },
              new SqlParameter("@CompanyWebsite", SqlDbType.VarChar, 400) { Value = WebSite },
              new SqlParameter("@Address", SqlDbType.NVarChar, 250) { Value = Address },
              new SqlParameter("@City", SqlDbType.NVarChar, 100) { Value = City },
              new SqlParameter("@State", SqlDbType.NVarChar, 100) { Value = State },
              new SqlParameter("@ZipCode", SqlDbType.NVarChar, 20) { Value = ZipCode},
              new SqlParameter("@CountryId", SqlDbType.Int) { Value = CountryId },
              new SqlParameter("@ModifiedBy", SqlDbType.UniqueIdentifier) { Value = ModifiedBy },
              new SqlParameter("@AccountId", SqlDbType.UniqueIdentifier) { Value = AccountId },
              new SqlParameter("@Email_Admin", SqlDbType.NVarChar,256) { Value = Email_Admin },
              new SqlParameter("@Email_Finances", SqlDbType.NVarChar,256) { Value = Email_Finances },
              new SqlParameter("@Email_Technical", SqlDbType.NVarChar,256) { Value = Email_Technical },
              new SqlParameter("@Email_Marketing", SqlDbType.NVarChar,256) { Value = Email_Marketing },
              new SqlParameter("@Name_Admin", SqlDbType.NVarChar,100) { Value = Name_Admin },
              new SqlParameter("@Name_Finances", SqlDbType.NVarChar,100) { Value = Name_Finances },
              new SqlParameter("@Name_Technical", SqlDbType.NVarChar,100) { Value = Name_Technical },
              new SqlParameter("@Name_Marketing", SqlDbType.NVarChar,100) { Value = Name_Marketing }
            };

                int success = SqlHelper.ExecuteNonQuery(connectionString, true, "UPDATE Account SET RegistrationNumber=@RegistrationNumber, CompanyWebsite = @CompanyWebsite, Address=@Address, City=@City, State=@State, ZipCode=@ZipCode, CountryId=@CountryId, ModifiedDate=GETDATE(), ModifiedBy=@ModifiedBy, Email_Admin=@Email_Admin, Email_Finances=@Email_Finances, Email_Technical=@Email_Technical, Email_Marketing=@Email_Marketing, Name_Admin=@Name_Admin,  Name_Finances=@Name_Finances, Name_Technical=@Name_Technical, Name_Marketing=@Name_Marketing  WHERE AccountId=@AccountId", System.Data.CommandType.Text, sqlParam);

                return success == 0 ? false : true;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(AccountId.ToString(), "Account-UpdateAccountDetails", AccountId.ToString(), "Error", ex.Message);
                return false;

            }
        }

        public DataTable GetAccounts(int PageNum, int MaxRows, string SortBy, string SortDirection, string AccountName, string IsActive, out int MinItem, out int MaxItem, out int TotalRows)
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
                        command.CommandText = "S_ACCOUNTS_GET";

                        command.Parameters.AddWithValue("@PageNum", PageNum);
                        command.Parameters.AddWithValue("@MaxRows", MaxRows);
                        command.Parameters.AddWithValue("@SortBy", SortBy);
                        command.Parameters.AddWithValue("@SortDirection", SortDirection);
                        command.Parameters.AddWithValue("@AccountName", AccountName);

                        if (!string.IsNullOrEmpty(IsActive))
                        {
                            command.Parameters.AddWithValue("@IsActive", IsActive);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@IsActive", System.DBNull.Value);
                        }
                        

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

                Logger.Logger.WriteLog("SuperAdmin", "Account-GetAccounts", string.Empty, "Error", ex.Message);
                return null;
            }
        }


        public string GetAccountName(Guid AccountId, Guid UserId)
        {
            try
            {
                DataAccessLayer.DataClasses1DataContext dc = new DataAccessLayer.DataClasses1DataContext(connectionString);

                DataAccessLayer.Account oAc = new DataAccessLayer.Account();
                oAc = dc.Accounts.FirstOrDefault(a => a.AccountID == AccountId);
                return oAc.AccountName;

            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(UserId.ToString(), "Accounts-GetAccountName", AccountId.ToString(), "Error", ex.Message);
                return null;
            }
        }


        public string GetAccountEmail(Guid AccountId, Guid UserId)
        {
            try
            {
                DataAccessLayer.DataClasses1DataContext dc = new DataAccessLayer.DataClasses1DataContext(connectionString);

                DataAccessLayer.Account oAc = new DataAccessLayer.Account();
                oAc = dc.Accounts.FirstOrDefault(a => a.AccountID == AccountId);
                return oAc.CompanyEmail;

            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(UserId.ToString(), "Accounts-GetAccountEmail", AccountId.ToString(), "Error", ex.Message);
                return null;
            }
        }


        public DataTable GetAccountUsage(Guid AccountID, int Year, Guid UserId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "S_Account_GetAccountUsage";

                        command.Parameters.AddWithValue("@AccountID", AccountID);
                        command.Parameters.AddWithValue("@Year", Year);

                        
                        DataTable dt = new DataTable();
                        SqlDataAdapter sqlA = new SqlDataAdapter(command);
                        sqlA.Fill(dt);

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(AccountID.ToString(), "Campaign-GetAccountUsage", UserId.ToString(), "Error", ex.Message);
                return null;
            }
        }
    }
}
