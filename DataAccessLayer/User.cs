using System;
using CSDataSqlCommand;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Data;

namespace DataAccessLayer.Controller
{
   public class User
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AutoMarketerConnectionString"].ConnectionString.ToString();

        public DataModels.UserModel GetAccountOwnerByAccountId(Guid AccountId)
        {
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter();
                sqlParam[0].ParameterName = "AccountId";
                sqlParam[0].Value = AccountId;
                sqlParam[0].Direction = System.Data.ParameterDirection.Input;

                SqlDataReader sqlDR = SqlHelper.ExecuteReader(connectionString, "SELECT UserID,AccountID,UserName,[Password],FirstName,MiddleName,LastName,MobileNumber,PhoneNumber,EmailAddress,CountryId,PasswordQuestion,PasswordAnswer,LastLoginDate,LastLogoutDate,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsAdmin,IsActive,PasswordSalt,ID,Address,City,State,ZipCode,HasChangedSystemPassword, IsOwner, Position, IsSuperAdminUser FROM [User] WITH (NOLOCK) WHERE AccountID=@AccountId AND IsOwner=1", System.Data.CommandType.Text, sqlParam);
                DataModels.UserModel uModel = null;
                while (sqlDR.Read())
                {
                    uModel = new DataModels.UserModel();
                    uModel.UserID = Guid.Parse(sqlDR["UserID"].ToString());
                    uModel.AccountID = Guid.Parse(sqlDR["AccountID"].ToString());
                    uModel.UserName = sqlDR["UserName"].ToString();
                    uModel.SavedPassword = sqlDR["Password"].ToString();
                    uModel.FirstName = sqlDR["FirstName"].ToString();
                    uModel.MiddleName = sqlDR["MiddleName"].ToString();
                    uModel.LastName = sqlDR["LastName"].ToString();
                    uModel.MobileNumber = sqlDR["MobileNumber"].ToString();
                    uModel.PhoneNumber = sqlDR["PhoneNumber"].ToString();
                    uModel.EmailAddress = sqlDR["EmailAddress"].ToString();
                    uModel.CountryId = sqlDR["CountryId"].ToString();
                    uModel.PasswordQuestion = sqlDR["PasswordQuestion"].ToString();
                    uModel.PasswordAnswer = sqlDR["PasswordAnswer"].ToString();
                    if (sqlDR["LastLoginDate"] == System.DBNull.Value)
                    {
                        uModel.LastLoginDate = null;
                    }
                    else
                    {
                        uModel.LastLoginDate = DateTime.Parse(sqlDR["LastLoginDate"].ToString());
                    };

                    if (sqlDR["LastLogoutDate"] == System.DBNull.Value)
                    {
                        uModel.LastLogoutDate = null;
                    }
                    else
                    {
                        uModel.LastLogoutDate = DateTime.Parse(sqlDR["LastLogoutDate"].ToString());
                    };

                    uModel.CreatedDate = DateTime.Parse(sqlDR["CreatedDate"].ToString());
                    uModel.CreatedBy = Guid.Parse(sqlDR["CreatedBy"].ToString());

                    if (sqlDR["ModifiedDate"] == System.DBNull.Value)
                    {
                        uModel.ModifiedDate = null;
                    }
                    else
                    {
                        uModel.ModifiedDate = DateTime.Parse(sqlDR["ModifiedDate"].ToString());
                    };

                    if (sqlDR["ModifiedBy"] == System.DBNull.Value)
                    {
                        uModel.ModifiedBy = null;
                    }
                    else
                    {
                        uModel.ModifiedBy = Guid.Parse(sqlDR["ModifiedBy"].ToString());
                    };

                    uModel.IsAdmin = bool.Parse(sqlDR["IsAdmin"].ToString());
                    uModel.IsActive = bool.Parse(sqlDR["IsActive"].ToString());
                    uModel.PasswordSalt = sqlDR["PasswordSalt"].ToString();
                    uModel.ID = int.Parse(sqlDR["ID"].ToString());
                    uModel.Address = sqlDR["Address"].ToString();
                    uModel.City = sqlDR["City"].ToString();
                    uModel.State = sqlDR["State"].ToString();
                    uModel.ZipCode = sqlDR["ZipCode"].ToString();

                    if (sqlDR["HasChangedSystemPassword"] == System.DBNull.Value)
                    {
                        uModel.HasChangedSystemPassword = false;
                    }
                    else
                    {
                        uModel.HasChangedSystemPassword = bool.Parse(sqlDR["HasChangedSystemPassword"].ToString());
                    };

                    uModel.Position = sqlDR["Position"].ToString();
                    if (sqlDR["IsSuperAdminUser"] == System.DBNull.Value)
                    {
                        uModel.IsSuperAdminUser = false;
                    }
                    else
                    {
                        uModel.IsSuperAdminUser = bool.Parse(sqlDR["IsSuperAdminUser"].ToString());
                    };
                }
                return uModel;
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(AccountId.ToString(), "User", string.Empty, "Error", ex.Message);
                return null;
            }
        }

        public DataTable GetUsersByAccountId(int PageNum, int MaxRows, string SortBy, string SortDirection, Guid AccountId, string FirstName, string LastName, string UserName, out int MinItem, out int MaxItem, out int TotalRows)
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
                        command.CommandText = "S_USERS_GET";

                        command.Parameters.AddWithValue("@PageNum", PageNum);
                        command.Parameters.AddWithValue("@MaxRows", MaxRows);
                        command.Parameters.AddWithValue("@SortBy", SortBy);
                        command.Parameters.AddWithValue("@SortDirection", SortDirection);
                        command.Parameters.AddWithValue("@AccountID", AccountId);
                        command.Parameters.AddWithValue("@FirstName", FirstName);
                        command.Parameters.AddWithValue("@LastName", LastName);
                        command.Parameters.AddWithValue("@UserName", UserName);

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

                Logger.Logger.WriteLog(AccountId.ToString(), "User", string.Empty, "Error", ex.Message);
                return null;
            }
        }

        //public List<DataModels.UserModel> GetUsersByAccountId(int PageNum, int MaxRows, string SortBy, string SortDirection, Guid AccountId, out int MinItem, out int MaxItem, out int TotalRows)
        //{
        //    MinItem = 0;
        //    MaxItem = 0;
        //    TotalRows = 0;
        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();

        //            using (SqlCommand command = connection.CreateCommand())
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "S_USERS_GET";

        //                command.Parameters.AddWithValue("@PageNum", PageNum);
        //                command.Parameters.AddWithValue("@MaxRows", MaxRows);
        //                command.Parameters.AddWithValue("@SortBy", SortBy);
        //                command.Parameters.AddWithValue("@SortDirection", SortDirection);
        //                command.Parameters.AddWithValue("@AccountID", AccountId);

        //                SqlParameter out_MinItem = new SqlParameter("@MinItem", SqlDbType.Int);
        //                out_MinItem.Direction = ParameterDirection.InputOutput;
        //                out_MinItem.Value = 0;

        //                command.Parameters.Add(out_MinItem);

        //                SqlParameter out_MaxItem = new SqlParameter("@MaxItem", SqlDbType.Int);
        //                out_MaxItem.Direction = ParameterDirection.InputOutput;
        //                out_MaxItem.Value = 0;

        //                command.Parameters.Add(out_MaxItem);

        //                SqlParameter out_TotalRows = new SqlParameter("@TotalRows", SqlDbType.Int);
        //                out_TotalRows.Direction = ParameterDirection.InputOutput;
        //                out_TotalRows.Value = 0;

        //                command.Parameters.Add(out_TotalRows);

        //                SqlDataReader sqlDR = command.ExecuteReader();

        //                List<DataModels.UserModel> uModelList = null;
        //                DataModels.UserModel uModel = null;
        //                if (sqlDR.HasRows)
        //                {
        //                    uModelList = new List<DataModels.UserModel>();
        //                    while (sqlDR.Read())
        //                    {
        //                        uModel = new DataModels.UserModel();
        //                        uModel.UserID = Guid.Parse(sqlDR["UserID"].ToString());
        //                        uModel.AccountID = Guid.Parse(sqlDR["AccountID"].ToString());
        //                        uModel.UserName = sqlDR["UserName"].ToString();
        //                        uModel.SavedPassword = sqlDR["Password"].ToString();
        //                        uModel.FirstName = sqlDR["FirstName"].ToString();
        //                        uModel.MiddleName = sqlDR["MiddleName"].ToString();
        //                        uModel.LastName = sqlDR["LastName"].ToString();
        //                        uModel.MobileNumber = sqlDR["MobileNumber"].ToString();
        //                        uModel.PhoneNumber = sqlDR["PhoneNumber"].ToString();
        //                        uModel.EmailAddress = sqlDR["EmailAddress"].ToString();
        //                        uModel.CountryId = sqlDR["CountryId"].ToString();
        //                        uModel.PasswordQuestion = sqlDR["PasswordQuestion"].ToString();
        //                        uModel.PasswordAnswer = sqlDR["PasswordAnswer"].ToString();
        //                        if (sqlDR["LastLoginDate"] == System.DBNull.Value)
        //                        {
        //                            uModel.LastLoginDate = null;
        //                        }
        //                        else
        //                        {
        //                            uModel.LastLoginDate = DateTime.Parse(sqlDR["LastLoginDate"].ToString());
        //                        };

        //                        if (sqlDR["LastLogoutDate"] == System.DBNull.Value)
        //                        {
        //                            uModel.LastLogoutDate = null;
        //                        }
        //                        else
        //                        {
        //                            uModel.LastLogoutDate = DateTime.Parse(sqlDR["LastLogoutDate"].ToString());
        //                        };

        //                        uModel.CreatedDate = DateTime.Parse(sqlDR["CreatedDate"].ToString());
        //                        uModel.CreatedBy = Guid.Parse(sqlDR["CreatedBy"].ToString());

        //                        if (sqlDR["ModifiedDate"] == System.DBNull.Value)
        //                        {
        //                            uModel.ModifiedDate = null;
        //                        }
        //                        else
        //                        {
        //                            uModel.ModifiedDate = DateTime.Parse(sqlDR["ModifiedDate"].ToString());
        //                        };

        //                        if (sqlDR["ModifiedBy"] == System.DBNull.Value)
        //                        {
        //                            uModel.ModifiedBy = null;
        //                        }
        //                        else
        //                        {
        //                            uModel.ModifiedBy = Guid.Parse(sqlDR["ModifiedBy"].ToString());
        //                        };

        //                        uModel.IsAdmin = bool.Parse(sqlDR["IsAdmin"].ToString());
        //                        uModel.IsActive = bool.Parse(sqlDR["IsActive"].ToString());
        //                        uModel.PasswordSalt = sqlDR["PasswordSalt"].ToString();
        //                        uModel.ID = int.Parse(sqlDR["ID"].ToString());
        //                        uModel.Address = sqlDR["Address"].ToString();
        //                        uModel.City = sqlDR["City"].ToString();
        //                        uModel.State = sqlDR["State"].ToString();
        //                        uModel.ZipCode = sqlDR["ZipCode"].ToString();

        //                        if (sqlDR["HasChangedSystemPassword"] == System.DBNull.Value)
        //                        {
        //                            uModel.HasChangedSystemPassword = false;
        //                        }
        //                        else
        //                        {
        //                            uModel.HasChangedSystemPassword = bool.Parse(sqlDR["HasChangedSystemPassword"].ToString());
        //                        };

        //                        uModel.Position = sqlDR["Position"].ToString();

        //                        uModelList.Add(uModel);
        //                    }
        //                }

        //                if (!sqlDR.IsClosed)
        //                    sqlDR.Close();


        //                if (out_MinItem.Value != DBNull.Value)
        //                    MinItem = Convert.ToInt32(out_MinItem.Value);

        //                if (out_MaxItem.Value != DBNull.Value)
        //                    MaxItem = Convert.ToInt32(out_MaxItem.Value);

        //                if (out_TotalRows.Value != DBNull.Value)
        //                    TotalRows = Convert.ToInt32(out_TotalRows.Value);

        //                return uModelList;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Logger.Logger.WriteLog(AccountId.ToString(), "User", string.Empty, "Error", ex.Message);
        //        return null;
        //    }
        //}

        public DataModels.UserModel GetUserDetailByUserId(Guid UserId)
        {
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[1];
               
                sqlParam[0] = new SqlParameter();
                sqlParam[0].ParameterName = "UserId";
                sqlParam[0].Value = UserId;
                sqlParam[0].Direction = System.Data.ParameterDirection.Input;

                SqlDataReader sqlDR = SqlHelper.ExecuteReader(connectionString, "SELECT UserID,AccountID,UserName,[Password],FirstName,MiddleName,LastName,MobileNumber,PhoneNumber,EmailAddress,CountryId,PasswordQuestion,PasswordAnswer,LastLoginDate,LastLogoutDate,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsAdmin,IsActive,PasswordSalt,ID,Address,City,State,ZipCode,HasChangedSystemPassword, IsOwner, Position, IsSuperAdminUser FROM [User] WITH (NOLOCK) WHERE UserId=@UserId", System.Data.CommandType.Text, sqlParam);
                DataModels.UserModel uModel = null;
                while (sqlDR.Read())
                {
                    uModel = new DataModels.UserModel();
                    uModel.UserID = Guid.Parse(sqlDR["UserID"].ToString());
                    uModel.AccountID = Guid.Parse(sqlDR["AccountID"].ToString());
                    uModel.UserName = sqlDR["UserName"].ToString();
                    uModel.SavedPassword = sqlDR["Password"].ToString();
                    uModel.FirstName = sqlDR["FirstName"].ToString();
                    uModel.MiddleName = sqlDR["MiddleName"].ToString();
                    uModel.LastName = sqlDR["LastName"].ToString();
                    uModel.MobileNumber = sqlDR["MobileNumber"].ToString();
                    uModel.PhoneNumber = sqlDR["PhoneNumber"].ToString();
                    uModel.EmailAddress = sqlDR["EmailAddress"].ToString();
                    uModel.CountryId = sqlDR["CountryId"].ToString();
                    uModel.PasswordQuestion = sqlDR["PasswordQuestion"].ToString();
                    uModel.PasswordAnswer = sqlDR["PasswordAnswer"].ToString();
                    if (sqlDR["LastLoginDate"] == System.DBNull.Value)
                    {
                        uModel.LastLoginDate = null;
                    }
                    else
                    {
                        uModel.LastLoginDate = DateTime.Parse(sqlDR["LastLoginDate"].ToString());
                    };

                    if (sqlDR["LastLogoutDate"] == System.DBNull.Value)
                    {
                        uModel.LastLogoutDate = null;
                    }
                    else
                    {
                        uModel.LastLogoutDate = DateTime.Parse(sqlDR["LastLogoutDate"].ToString());
                    };

                    uModel.CreatedDate = DateTime.Parse(sqlDR["CreatedDate"].ToString());
                    uModel.CreatedBy = Guid.Parse(sqlDR["CreatedBy"].ToString());

                    if (sqlDR["ModifiedDate"] == System.DBNull.Value)
                    {
                        uModel.ModifiedDate = null;
                    }
                    else
                    {
                        uModel.ModifiedDate = DateTime.Parse(sqlDR["ModifiedDate"].ToString());
                    };

                    if (sqlDR["ModifiedBy"] == System.DBNull.Value)
                    {
                        uModel.ModifiedBy = null;
                    }
                    else
                    {
                        uModel.ModifiedBy = Guid.Parse(sqlDR["ModifiedBy"].ToString());
                    };

                    uModel.IsAdmin = bool.Parse(sqlDR["IsAdmin"].ToString());
                    uModel.IsActive = bool.Parse(sqlDR["IsActive"].ToString());
                    uModel.PasswordSalt = sqlDR["PasswordSalt"].ToString();
                    uModel.ID = int.Parse(sqlDR["ID"].ToString());
                    uModel.Address = sqlDR["Address"].ToString();
                    uModel.City = sqlDR["City"].ToString();
                    uModel.State = sqlDR["State"].ToString();
                    uModel.ZipCode = sqlDR["ZipCode"].ToString();

                    if (sqlDR["HasChangedSystemPassword"] == System.DBNull.Value)
                    {
                        uModel.HasChangedSystemPassword = false;
                    }
                    else
                    {
                        uModel.HasChangedSystemPassword = bool.Parse(sqlDR["HasChangedSystemPassword"].ToString());
                    };

                    if (sqlDR["IsOwner"] == System.DBNull.Value)
                    {
                        uModel.IsOwner = false;
                    }
                    else
                    {
                        uModel.IsOwner = bool.Parse(sqlDR["IsOwner"].ToString());
                    };

                    uModel.Position = sqlDR["Position"].ToString();
                    uModel.IsSuperAdminUser = bool.Parse(sqlDR["IsSuperAdminUser"].ToString());
                }
                return uModel;
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(UserId.ToString(), "User-GetUserDetailByUserId", UserId.ToString(), "Error", ex.Message);
                return null;
            }
        }

        public bool UniqueEmail_Update(Guid AccountID, string EmailAddress, Guid UserID)
        {
            try
            {
                SqlParameter[] sqlParam =
                {
                  new SqlParameter("@AccountID ", SqlDbType.UniqueIdentifier) { Value = AccountID },
                   new SqlParameter("@UserID ", SqlDbType.UniqueIdentifier) { Value = UserID },
                  new SqlParameter("@EmailAddress ", SqlDbType.NVarChar, 250) { Value = EmailAddress },
                };

                bool result = (bool)SqlHelper.ExecuteScalar(connectionString, @"SELECT dbo.fn_USERDETAILS_UNIQUEEMAILADDRESS(@AccountID, @UserID, @EmailAddress)", System.Data.CommandType.Text, sqlParam);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(EmailAddress, "User-UniqueEmail", string.Empty, "Error", ex.Message);
                return false;
            }
        }

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
                Logger.Logger.WriteLog(EmailAddress, "NewUser-UniqueEmail", string.Empty, "Error", ex.Message);
                return false;
            }
        }

        public bool SaveUserDetails(Guid AccountId,
                                Guid UserID,
                                string FirstName,
                                string LastName,
                                string Position,
                                string EmailAddress,
                                string Mobile,
                                bool Enabled,
                                Guid ModifiedBy)
        {
            try
            {
                SqlParameter[] sqlParam =
           {
              new SqlParameter("@AccountId ", SqlDbType.UniqueIdentifier) { Value = AccountId },
              new SqlParameter("@UserID", SqlDbType.UniqueIdentifier) { Value = UserID },
              new SqlParameter("@FirstName", SqlDbType.NVarChar, 100) { Value = FirstName },
              new SqlParameter("@LastName", SqlDbType.NVarChar, 100) { Value = LastName },
              new SqlParameter("@Position", SqlDbType.NVarChar, 128) { Value = Position },
              new SqlParameter("@EmailAddress", SqlDbType.NVarChar, 250) { Value = EmailAddress},
              new SqlParameter("@Mobile", SqlDbType.VarChar,100) { Value = Mobile },
              new SqlParameter("@Enabled", SqlDbType.Bit) { Value = Enabled },
              new SqlParameter("@ModifiedBy", SqlDbType.UniqueIdentifier) { Value = ModifiedBy }
            };

                int success = SqlHelper.ExecuteNonQuery(connectionString, true, "S_USERDETAILS_UPDATE", System.Data.CommandType.StoredProcedure, sqlParam);

                return success == 0 ? false : true;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(AccountId.ToString(), "User-SaveUserDetails", UserID.ToString(), "Error", ex.Message);
                return false;

            }
        }


        public bool CreateUser(Guid AccountId,
                                 string UserName,
                                string Password,
                                string FirstName,
                                string MiddleName,
                                string LastName,
                                string MobileNumber,
                                string PhoneNumber,
                                string EmailAddress,
                                string Address,
                                string City,
                                string State,
                                string ZipCode,
                                int CountryId,
                                bool IsAdminUser,
                                bool IsActiveUser,
                                bool IsOwnerUser,
                                bool IsSuperAdminUser,
                                string PasswordSalt,
                                System.Guid CreatedBy)
        {
            try
            {
                SqlParameter[] sqlParam =
                {
                  new SqlParameter("@AccountID ", SqlDbType.UniqueIdentifier) { Value = AccountId },
                  new SqlParameter("@UserName", SqlDbType.NVarChar,50) { Value = UserName },
                  new SqlParameter("@Password", SqlDbType.NVarChar, 128) { Value = Password },
                  new SqlParameter("@FirstName", SqlDbType.NVarChar, 100) { Value = FirstName },
                  new SqlParameter("@MiddleName", SqlDbType.NVarChar, 100) { Value = MiddleName },
                  new SqlParameter("@LastName", SqlDbType.NVarChar, 100) { Value = LastName },
                  new SqlParameter("@MobileNumber", SqlDbType.VarChar, 100) { Value = MobileNumber },
                  new SqlParameter("@PhoneNumber", SqlDbType.VarChar, 100) { Value = PhoneNumber },
                  new SqlParameter("@EmailAddress", SqlDbType.NVarChar, 250) { Value = EmailAddress },
                  new SqlParameter("@Address", SqlDbType.NVarChar, 250) { Value = Address },
                  new SqlParameter("@City", SqlDbType.NVarChar, 100) { Value = City },
                  new SqlParameter("@State", SqlDbType.NVarChar, 100) { Value = State },
                  new SqlParameter("@ZipCode", SqlDbType.NVarChar, 20) { Value = ZipCode},
                  new SqlParameter("@CountryId", SqlDbType.Int) { Value = CountryId },
                  new SqlParameter("@IsAdminUser", SqlDbType.Bit) { Value = IsAdminUser },
                  new SqlParameter("@IsActiveUser", SqlDbType.Bit) { Value = IsActiveUser },
                  new SqlParameter("@IsOwnerUser", SqlDbType.Bit) { Value = IsOwnerUser },
                  new SqlParameter("@IsSuperAdminUser", SqlDbType.Bit) { Value = IsOwnerUser },
                  new SqlParameter("@PasswordSalt", SqlDbType.NVarChar, 128) { Value = PasswordSalt },
                  new SqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier) { Value = CreatedBy }
              };

                int success = SqlHelper.ExecuteNonQuery(connectionString, true, "S_USER_CREATE", System.Data.CommandType.StoredProcedure, sqlParam);

                return success == 0 ? false : true;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(CreatedBy.ToString(), "User-CreateUser", UserName, "Error", ex.Message);
                return false;

            }
        }


        public int UniqueUserName(string UserName)
        {
            try
            {
                SqlParameter[] sqlParam =
                {
                      new SqlParameter("@UserName ", SqlDbType.NVarChar, 50) { Value = UserName }
                };

                int result = (int)SqlHelper.ExecuteScalar(connectionString, @"SELECT COUNT(*) FROM [User] WITH (NOLOCK) WHERE UserName=@UserName", System.Data.CommandType.Text, sqlParam);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(UserName, "Account-UniqueUserName", string.Empty, "Error", ex.Message);
                return 0;
            }
        }


        public string GetUserName(Guid UserId, Guid AccountId)
        {
            try
            {
                SqlParameter[] sqlParam =
                {
                  new SqlParameter("@UserId", SqlDbType.UniqueIdentifier) { Value = UserId },
                  new SqlParameter("@AccountId", SqlDbType.UniqueIdentifier) { Value = AccountId }
                };

                string result = SqlHelper.ExecuteScalar(connectionString, @"SELECT dbo.fn_GetUserName(@UserId, @AccountId)", System.Data.CommandType.Text, sqlParam).ToString();

                return result;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(string.Empty, "User-GetUserName", UserId.ToString(), "Error", ex.Message);
                return string.Empty;
            }
        }
    }
}
