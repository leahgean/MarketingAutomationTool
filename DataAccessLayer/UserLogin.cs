using System;
using CSDataSqlCommand;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections.Generic;

namespace DataAccessLayer.Controller
{
    public class UserLogin
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AutoMarketerConnectionString"].ConnectionString.ToString();

        public DataModels.UserLogin GetUserByUserName(string username)
        {
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter();
                sqlParam[0].ParameterName = "UserName";
                sqlParam[0].Value = username;
                sqlParam[0].Direction = System.Data.ParameterDirection.Input;

                SqlDataReader sqlDR = SqlHelper.ExecuteReader(connectionString, "SELECT UserID,AccountID,UserName,[Password],FirstName,MiddleName,LastName,MobileNumber,PhoneNumber,EmailAddress,CountryId,PasswordQuestion,PasswordAnswer,LastLoginDate,LastLogoutDate,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsAdmin,IsActive,PasswordSalt,ID,Address,City,State,ZipCode,HasChangedSystemPassword, IsOwner, Position, IsSuperAdminUser FROM [User] WITH (NOLOCK) WHERE UserName=@UserName", System.Data.CommandType.Text, sqlParam);
                DataModels.UserLogin ulogin = null;

                if (sqlDR.HasRows)
                {
                    while (sqlDR.Read())
                    {
                        ulogin = new DataModels.UserLogin();
                        ulogin.UserID = Guid.Parse(sqlDR["UserID"].ToString());
                        ulogin.AccountID = Guid.Parse(sqlDR["AccountID"].ToString());
                        ulogin.UserName = sqlDR["UserName"].ToString();
                        ulogin.SavedPassword = sqlDR["Password"].ToString();
                        ulogin.FirstName = sqlDR["FirstName"].ToString();
                        ulogin.MiddleName = sqlDR["MiddleName"].ToString();
                        ulogin.LastName = sqlDR["LastName"].ToString();
                        ulogin.MobileNumber = sqlDR["MobileNumber"].ToString();
                        ulogin.PhoneNumber = sqlDR["PhoneNumber"].ToString();
                        ulogin.EmailAddress = sqlDR["EmailAddress"].ToString();
                        ulogin.CountryId = sqlDR["CountryId"].ToString();
                        ulogin.PasswordQuestion = sqlDR["PasswordQuestion"].ToString();
                        ulogin.PasswordAnswer = sqlDR["PasswordAnswer"].ToString();
                        if (sqlDR["LastLoginDate"] == System.DBNull.Value)
                        {
                            ulogin.LastLoginDate = null;
                        }
                        else
                        {
                            ulogin.LastLoginDate = DateTime.Parse(sqlDR["LastLoginDate"].ToString());
                        };

                        if (sqlDR["LastLogoutDate"] == System.DBNull.Value)
                        {
                            ulogin.LastLogoutDate = null;
                        }
                        else
                        {
                            ulogin.LastLogoutDate = DateTime.Parse(sqlDR["LastLogoutDate"].ToString());
                        };

                        ulogin.CreatedDate = DateTime.Parse(sqlDR["CreatedDate"].ToString());
                        ulogin.CreatedBy = Guid.Parse(sqlDR["CreatedBy"].ToString());

                        if (sqlDR["ModifiedDate"] == System.DBNull.Value)
                        {
                            ulogin.ModifiedDate = null;
                        }
                        else
                        {
                            ulogin.ModifiedDate = DateTime.Parse(sqlDR["ModifiedDate"].ToString());
                        };

                        if (sqlDR["ModifiedBy"] == System.DBNull.Value)
                        {
                            ulogin.ModifiedBy = null;
                        }
                        else
                        {
                            ulogin.ModifiedBy = Guid.Parse(sqlDR["ModifiedBy"].ToString());
                        };

                        ulogin.IsAdmin = bool.Parse(sqlDR["IsAdmin"].ToString());
                        ulogin.IsActive = bool.Parse(sqlDR["IsActive"].ToString());
                        ulogin.PasswordSalt = sqlDR["PasswordSalt"].ToString();
                        ulogin.ID = int.Parse(sqlDR["ID"].ToString());
                        ulogin.Address = sqlDR["Address"].ToString();
                        ulogin.City = sqlDR["City"].ToString();
                        ulogin.State = sqlDR["State"].ToString();
                        ulogin.ZipCode = sqlDR["ZipCode"].ToString();

                        if (sqlDR["HasChangedSystemPassword"] == System.DBNull.Value)
                        {
                            ulogin.HasChangedSystemPassword = false;
                        }
                        else
                        {
                            ulogin.HasChangedSystemPassword = bool.Parse(sqlDR["HasChangedSystemPassword"].ToString());
                        };

                        if (sqlDR["IsOwner"] == System.DBNull.Value)
                        {
                            ulogin.IsOwner = false;
                        }
                        else
                        {
                            ulogin.IsOwner = bool.Parse(sqlDR["IsOwner"].ToString());
                        };

                        ulogin.Position = sqlDR["Position"].ToString();

                        
                        ulogin.IsSuperAdminUser = bool.Parse(sqlDR["IsSuperAdminUser"].ToString());
                    }

                }

                sqlDR.Close();

                return ulogin;
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(username, "UserLogin", string.Empty, "Error", ex.Message);
                return null;
            }
        }


        public DataModels.UserLogin GetUserByAccountId(Guid accountid)
        {
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter();
                sqlParam[0].ParameterName = "AccountID";
                sqlParam[0].Value = accountid;
                sqlParam[0].Direction = System.Data.ParameterDirection.Input;

                SqlDataReader sqlDR = SqlHelper.ExecuteReader(connectionString, "SELECT UserID,AccountID,UserName,[Password],FirstName,MiddleName,LastName,MobileNumber,PhoneNumber,EmailAddress,CountryId,PasswordQuestion,PasswordAnswer,LastLoginDate,LastLogoutDate,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsAdmin,IsActive,PasswordSalt,ID,Address,City,State,ZipCode,HasChangedSystemPassword, IsOwner, Position, IsSuperAdminUser FROM [User] WITH (NOLOCK) WHERE AccountID=@AccountID AND IsOwner=1", System.Data.CommandType.Text, sqlParam);
                DataModels.UserLogin ulogin = null;

                if (sqlDR.HasRows)
                {
                    while (sqlDR.Read())
                    {
                        ulogin = new DataModels.UserLogin();
                        ulogin.UserID = Guid.Parse(sqlDR["UserID"].ToString());
                        ulogin.AccountID = Guid.Parse(sqlDR["AccountID"].ToString());
                        ulogin.UserName = sqlDR["UserName"].ToString();
                        ulogin.SavedPassword = sqlDR["Password"].ToString();
                        ulogin.FirstName = sqlDR["FirstName"].ToString();
                        ulogin.MiddleName = sqlDR["MiddleName"].ToString();
                        ulogin.LastName = sqlDR["LastName"].ToString();
                        ulogin.MobileNumber = sqlDR["MobileNumber"].ToString();
                        ulogin.PhoneNumber = sqlDR["PhoneNumber"].ToString();
                        ulogin.EmailAddress = sqlDR["EmailAddress"].ToString();
                        ulogin.CountryId = sqlDR["CountryId"].ToString();
                        ulogin.PasswordQuestion = sqlDR["PasswordQuestion"].ToString();
                        ulogin.PasswordAnswer = sqlDR["PasswordAnswer"].ToString();
                        if (sqlDR["LastLoginDate"] == System.DBNull.Value)
                        {
                            ulogin.LastLoginDate = null;
                        }
                        else
                        {
                            ulogin.LastLoginDate = DateTime.Parse(sqlDR["LastLoginDate"].ToString());
                        };

                        if (sqlDR["LastLogoutDate"] == System.DBNull.Value)
                        {
                            ulogin.LastLogoutDate = null;
                        }
                        else
                        {
                            ulogin.LastLogoutDate = DateTime.Parse(sqlDR["LastLogoutDate"].ToString());
                        };

                        ulogin.CreatedDate = DateTime.Parse(sqlDR["CreatedDate"].ToString());
                        ulogin.CreatedBy = Guid.Parse(sqlDR["CreatedBy"].ToString());

                        if (sqlDR["ModifiedDate"] == System.DBNull.Value)
                        {
                            ulogin.ModifiedDate = null;
                        }
                        else
                        {
                            ulogin.ModifiedDate = DateTime.Parse(sqlDR["ModifiedDate"].ToString());
                        };

                        if (sqlDR["ModifiedBy"] == System.DBNull.Value)
                        {
                            ulogin.ModifiedBy = null;
                        }
                        else
                        {
                            ulogin.ModifiedBy = Guid.Parse(sqlDR["ModifiedBy"].ToString());
                        };

                        ulogin.IsAdmin = bool.Parse(sqlDR["IsAdmin"].ToString());
                        ulogin.IsActive = bool.Parse(sqlDR["IsActive"].ToString());
                        ulogin.PasswordSalt = sqlDR["PasswordSalt"].ToString();
                        ulogin.ID = int.Parse(sqlDR["ID"].ToString());
                        ulogin.Address = sqlDR["Address"].ToString();
                        ulogin.City = sqlDR["City"].ToString();
                        ulogin.State = sqlDR["State"].ToString();
                        ulogin.ZipCode = sqlDR["ZipCode"].ToString();

                        if (sqlDR["HasChangedSystemPassword"] == System.DBNull.Value)
                        {
                            ulogin.HasChangedSystemPassword = false;
                        }
                        else
                        {
                            ulogin.HasChangedSystemPassword = bool.Parse(sqlDR["HasChangedSystemPassword"].ToString());
                        };

                        if (sqlDR["IsOwner"] == System.DBNull.Value)
                        {
                            ulogin.IsOwner = false;
                        }
                        else
                        {
                            ulogin.IsOwner = bool.Parse(sqlDR["IsOwner"].ToString());
                        };

                        ulogin.Position = sqlDR["Position"].ToString();


                        ulogin.IsSuperAdminUser = bool.Parse(sqlDR["IsSuperAdminUser"].ToString());
                    }

                }

                sqlDR.Close();

                return ulogin;
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(accountid.ToString(), "UserLogin-GetUserByAccountId", string.Empty, "Error", ex.Message);
                return null;
            }
        }


        public bool LogUserAccess(Guid userid, string ipAddress, string sessionId)
        {
            try
            {
                SqlParameter[] sqlParam =
                {
                  new SqlParameter("@UserId", SqlDbType.UniqueIdentifier) { Value = userid },
                  new SqlParameter("@IP_Address", SqlDbType.NVarChar, 15) { Value = ipAddress },
                  new SqlParameter("@SessionId", SqlDbType.VarChar, 50) { Value = sessionId }
                };

                int success = SqlHelper.ExecuteNonQuery(connectionString, true, @"S_LOG_USER_LOGIN", System.Data.CommandType.StoredProcedure, sqlParam);

                return success == 0 ? false : true;
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(userid.ToString(), "UserLogin-LogUserAccess", userid.ToString(), "Error", ex.Message);
                return false;
            }

        }


        public bool LogUserLogout(string sessionId, Guid userid)
        {
            try
            {
                SqlParameter[] sqlParam =
                {
                    new SqlParameter("@Session_Id", SqlDbType.VarChar, 50) { Value = sessionId },
                    new SqlParameter("@User_Id", SqlDbType.UniqueIdentifier) { Value = userid }
                };

                int success = SqlHelper.ExecuteNonQuery(connectionString, true, @"S_LOG_USER_LOGOUT", System.Data.CommandType.StoredProcedure, sqlParam);

                return success == 0 ? false : true;
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(sessionId.ToString(), "UserLogin-LogUserLogout", sessionId.ToString(), "Error", ex.Message);
                return false;
            }

        }


        public bool UpdatePassword(string newpassword, string passwordsalt, Guid userid)
        {
            try
            {
                SqlParameter[] sqlParam =
                {
                    new SqlParameter("@Password", SqlDbType.VarChar, 128) { Value = newpassword },
                    new SqlParameter("@PasswordSalt", SqlDbType.VarChar, 128) { Value = passwordsalt },
                    new SqlParameter("@UserId", SqlDbType.UniqueIdentifier) { Value = userid }
                };

                int success = SqlHelper.ExecuteNonQuery(connectionString, true, @"UPDATE [User] SET Password=@Password, PasswordSalt=@PasswordSalt WHERE  UserId=@UserId", System.Data.CommandType.Text, sqlParam);

                return success == 0 ? false : true;
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(userid.ToString(), "UserLogin-UpdatePassword", userid.ToString(), "Error", ex.Message);
                return false;
            }
        }


        public bool ResetHasChangedSystemPassword(Guid userid)
        {
            try
            {
                Guid systemGuid = GetSystemAccount();

                SqlParameter[] sqlParam =
                {
                    new SqlParameter("@UserId", SqlDbType.UniqueIdentifier) { Value = userid },
                    new SqlParameter("@systemGuid", SqlDbType.UniqueIdentifier) { Value = systemGuid }
                };

                int success = SqlHelper.ExecuteNonQuery(connectionString, true, @"UPDATE [User] SET HasChangedSystemPassword=0, ModifiedDate=GETDATE(), ModifiedBy=@systemGuid  WHERE UserId =@UserID", System.Data.CommandType.Text, sqlParam);

                return success == 0 ? false : true;
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(userid.ToString(), "UserLogin-SetChangePasswordGuid", userid.ToString(), "Error", ex.Message);
                return false;
            }
        }


        public bool SetSystemPasswordChanged(Guid userid)
        {
            try
            {
                SqlParameter[] sqlParam =
                {
                    new SqlParameter("@UserId", SqlDbType.UniqueIdentifier) { Value = userid }
                };

                int success = SqlHelper.ExecuteNonQuery(connectionString, true, @"UPDATE [User] SET HasChangedSystemPassword=1, ModifiedDate=GETDATE(), ModifiedBy=@UserId WHERE UserId=@UserId", System.Data.CommandType.Text, sqlParam);

                return success == 0 ? false : true;
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(userid.ToString(), "UserLogin-SetSystemPasswordChanged", userid.ToString(), "Error", ex.Message);
                return false;
            }
        }

        public bool GetHasChangedSystemPassword(Guid userid)
        {
            try
            {
                SqlParameter[] sqlParam =
                {
                    new SqlParameter("@UserId", SqlDbType.UniqueIdentifier) { Value = userid }
                };

                bool result = bool.Parse(SqlHelper.ExecuteScalar(connectionString, @"SELECT ISNULL(HasChangedSystemPassword,0) HasChangedSystemPassword FROM [User] WITH (NOLOCK) WHERE  UserId=@UserId", System.Data.CommandType.Text, sqlParam).ToString());

                return result;
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(userid.ToString(), "UserLogin-GetHasChangedSystemPassword", userid.ToString(), "Error", ex.Message);
                return false;
            }
        }


        public Guid GetSystemAccount()
        {
            try
            {
               
                Guid result = Guid.Parse(SqlHelper.ExecuteScalar(connectionString, @"SELECT U.UserID FROM Account A WITH (NOLOCK) INNER JOIN [User] U WITH (NOLOCK) ON A.AccountId = U.AccountID WHERE A.IsSuperAdmin=1 AND U.IsSuperAdminUser=1", System.Data.CommandType.Text).ToString());

                return result;
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(string.Empty, "UserLogin-GetSystemAccount", string.Empty, "Error", ex.Message);
                return new Guid();
            }
        }

        public List<DataModels.UserLogin> GetUserNameByEmailAddress(string EmailAddress)
        {
            List<DataModels.UserLogin> uModels = new List<DataModels.UserLogin>();
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter();
                sqlParam[0].ParameterName = "EmailAddress";
                sqlParam[0].Value = EmailAddress;
                sqlParam[0].Direction = System.Data.ParameterDirection.Input;

                SqlDataReader sqlDR = SqlHelper.ExecuteReader(connectionString, "S_USER_USEREXISTS", System.Data.CommandType.StoredProcedure, sqlParam);
                DataModels.UserLogin uModel = null;

                while (sqlDR.Read())
                {
                    uModel = new DataModels.UserLogin();
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

                    uModels.Add(uModel);
                }

                return uModels;
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(EmailAddress.ToString(), "UserLogin-GetUserNameByEmailAddress", string.Empty, "Error", ex.Message);
                return null;
            }
        }
    }
}
