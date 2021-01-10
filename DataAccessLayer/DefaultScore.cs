using System.Data.SqlClient;
using CSDataSqlCommand;
using System.Data;
using System.Configuration;
using System;


namespace DataAccessLayer.Controller
{
    public class DefaultScore
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AutoMarketerConnectionString"].ConnectionString.ToString();

        public DataModels.DefaultScoreModel GetDefaultScore(Guid AccountId)
        {
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter();
                sqlParam[0].ParameterName = "Account_Id";
                sqlParam[0].Value = AccountId;
                sqlParam[0].Direction = System.Data.ParameterDirection.Input;

                SqlDataReader sqlDR = SqlHelper.ExecuteReader(connectionString, "SELECT Id,Account_Id,Form_Submit,Form_Confirmation_Email_Bounce,Form_Unsubscribe_From_Confirmation_Email,Form_Acknowledgment_Email_Bounce,Form_Acknowledgment_Email_First_Open,Form_Subsequent_Open,Form_Unsubscribe_From_Acknowledgment_Email,Form_Click_Link_On_Acknowledgment_Email,Form_Subsequent_click,Email_Bounce,Email_Unsubscribe,Email_Click_Link_First,Email_Subsequent_Click,SMS_Bounce,SMS_Unsubscribe FROM Default_Scoring WITH (NOLOCK) WHERE Account_Id=@Account_Id", System.Data.CommandType.Text, sqlParam);
                DataModels.DefaultScoreModel def = null;

                if (sqlDR.HasRows)
                {
                    while (sqlDR.Read())
                    {
                        def = new DataModels.DefaultScoreModel();
                        def.Id = int.Parse(sqlDR["Id"].ToString());
                        def.Account_Id = Guid.Parse(sqlDR["Account_Id"].ToString());
                        def.Form_Submit = short.Parse(sqlDR["Form_Submit"].ToString());
                        def.Form_Confirmation_Email_Bounce = short.Parse(sqlDR["Form_Confirmation_Email_Bounce"].ToString());
                        def.Form_Unsubscribe_From_Confirmation_Email = short.Parse(sqlDR["Form_Unsubscribe_From_Confirmation_Email"].ToString());
                        def.Form_Acknowledgment_Email_Bounce = short.Parse(sqlDR["Form_Acknowledgment_Email_Bounce"].ToString());
                        def.Form_Acknowledgment_Email_First_Open = short.Parse(sqlDR["Form_Acknowledgment_Email_First_Open"].ToString());
                        def.Form_Subsequent_Open = short.Parse(sqlDR["Form_Subsequent_Open"].ToString());
                        def.Form_Unsubscribe_From_Acknowledgment_Email = short.Parse(sqlDR["Form_Unsubscribe_From_Acknowledgment_Email"].ToString());
                        def.Form_Click_Link_On_Acknowledgment_Email = short.Parse(sqlDR["Form_Click_Link_On_Acknowledgment_Email"].ToString());
                        def.Form_Subsequent_click = short.Parse(sqlDR["Form_Subsequent_click"].ToString());
                        def.Email_Bounce = short.Parse(sqlDR["Email_Bounce"].ToString());
                        def.Email_Unsubscribe = short.Parse(sqlDR["Email_Unsubscribe"].ToString());
                        def.Email_Click_Link_First = short.Parse(sqlDR["Email_Click_Link_First"].ToString());
                        def.Email_Subsequent_Click = short.Parse(sqlDR["Email_Subsequent_Click"].ToString());
                        def.SMS_Bounce = short.Parse(sqlDR["SMS_Bounce"].ToString());
                        def.SMS_Unsubscribe = short.Parse(sqlDR["SMS_Unsubscribe"].ToString());
                    }
                }

                sqlDR.Close();

                return def;
            }
            catch (Exception ex)
            {

                Logger.Logger.WriteLog(AccountId.ToString(), "DefaultSCore-GetDefaultScore", AccountId.ToString(), "Error", ex.Message);
                return null;
            }
        }


        public bool AddDefaultScore(Guid Account_Id, short Form_Submit, short Form_Confirmation_Email_Bounce, short Form_Unsubscribe_From_Confirmation_Email,  short Form_Acknowledgment_Email_Bounce, short Form_Acknowledgment_Email_First_Open, short Form_Subsequent_Open, short Form_Unsubscribe_From_Acknowledgment_Email, short Form_Click_Link_On_Acknowledgment_Email, short Form_Subsequent_click, short Email_Bounce, short Email_Unsubscribe, short Email_Click_Link_First, short Email_Subsequent_Click, short SMS_Bounce, short SMS_Unsubscribe)
        {
            try
            {
                SqlParameter[] sqlParam =
                {
                  new SqlParameter("@Account_Id", SqlDbType.UniqueIdentifier) { Value = Account_Id },
                  new SqlParameter("@Form_Submit", SqlDbType.SmallInt) { Value = Form_Submit },
                  new SqlParameter("@Form_Confirmation_Email_Bounce", SqlDbType.SmallInt) { Value = Form_Confirmation_Email_Bounce },
                  new SqlParameter("@Form_Unsubscribe_From_Confirmation_Email ", SqlDbType.SmallInt) { Value = Form_Unsubscribe_From_Confirmation_Email },
                  new SqlParameter("@Form_Acknowledgment_Email_Bounce", SqlDbType.SmallInt) { Value = Form_Acknowledgment_Email_Bounce },
                  new SqlParameter("@Form_Acknowledgment_Email_First_Open", SqlDbType.SmallInt) { Value = Form_Acknowledgment_Email_First_Open },
                  new SqlParameter("@Form_Subsequent_Open", SqlDbType.SmallInt) { Value = Form_Subsequent_Open },
                  new SqlParameter("@Form_Unsubscribe_From_Acknowledgment_Email ", SqlDbType.SmallInt) { Value = Form_Unsubscribe_From_Acknowledgment_Email },
                  new SqlParameter("@Form_Click_Link_On_Acknowledgment_Email", SqlDbType.SmallInt) { Value = Form_Click_Link_On_Acknowledgment_Email },
                  new SqlParameter("@Form_Subsequent_click", SqlDbType.SmallInt) { Value = Form_Subsequent_click },
                  new SqlParameter("@Email_Bounce", SqlDbType.SmallInt) { Value = Email_Bounce },
                  new SqlParameter("@Email_Unsubscribe", SqlDbType.SmallInt) { Value = Email_Unsubscribe },
                  new SqlParameter("@Email_Click_Link_First", SqlDbType.SmallInt) { Value = Email_Click_Link_First },
                  new SqlParameter("@Email_Subsequent_Click", SqlDbType.SmallInt) { Value = Email_Subsequent_Click },
                  new SqlParameter("@SMS_Bounce", SqlDbType.SmallInt) { Value = SMS_Bounce },
                  new SqlParameter("@SMS_Unsubscribe", SqlDbType.SmallInt) { Value = SMS_Unsubscribe },
              };

                string sCommandText = @"INSERT INTO Default_Scoring (Account_Id,Form_Submit,Form_Confirmation_Email_Bounce,Form_Unsubscribe_From_Confirmation_Email,Form_Acknowledgment_Email_Bounce,Form_Acknowledgment_Email_First_Open,Form_Subsequent_Open,Form_Unsubscribe_From_Acknowledgment_Email,Form_Click_Link_On_Acknowledgment_Email,Form_Subsequent_click,Email_Bounce,Email_Unsubscribe,Email_Click_Link_First,Email_Subsequent_Click,SMS_Bounce,SMS_Unsubscribe) " +
                                        "VALUES(@Account_Id, @Form_Submit, @Form_Confirmation_Email_Bounce, @Form_Unsubscribe_From_Confirmation_Email, @Form_Acknowledgment_Email_Bounce, @Form_Acknowledgment_Email_First_Open, @Form_Subsequent_Open, @Form_Unsubscribe_From_Acknowledgment_Email, @Form_Click_Link_On_Acknowledgment_Email, @Form_Subsequent_click, @Email_Bounce, @Email_Unsubscribe, @Email_Click_Link_First, @Email_Subsequent_Click, @SMS_Bounce, @SMS_Unsubscribe)";

                int success = SqlHelper.ExecuteNonQuery(connectionString, true, sCommandText, System.Data.CommandType.Text, sqlParam);

                return success == 0 ? false : true;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(Account_Id.ToString(), "DefaultScore-AddDefaultScore", Account_Id.ToString(), "Error", ex.Message);
                return false;

            }
        }


        public bool UpdateDefaultScore(int Id,Guid Account_Id, short Form_Submit, short Form_Confirmation_Email_Bounce, short Form_Unsubscribe_From_Confirmation_Email, short Form_Acknowledgment_Email_Bounce, short Form_Acknowledgment_Email_First_Open, short Form_Subsequent_Open, short Form_Unsubscribe_From_Acknowledgment_Email, short Form_Click_Link_On_Acknowledgment_Email, short Form_Subsequent_click, short Email_Bounce, short Email_Unsubscribe, short Email_Click_Link_First, short Email_Subsequent_Click, short SMS_Bounce, short SMS_Unsubscribe)
        {
            try
            {
                SqlParameter[] sqlParam =
                {
                  new SqlParameter("@Id", SqlDbType.Int) { Value = Id },
                  new SqlParameter("@Account_Id", SqlDbType.UniqueIdentifier) { Value = Account_Id },
                  new SqlParameter("@Form_Submit", SqlDbType.SmallInt) { Value = Form_Submit },
                  new SqlParameter("@Form_Confirmation_Email_Bounce", SqlDbType.SmallInt) { Value = Form_Confirmation_Email_Bounce },
                  new SqlParameter("@Form_Unsubscribe_From_Confirmation_Email ", SqlDbType.SmallInt) { Value = Form_Unsubscribe_From_Confirmation_Email },
                  new SqlParameter("@Form_Acknowledgment_Email_Bounce", SqlDbType.SmallInt) { Value = Form_Acknowledgment_Email_Bounce },
                  new SqlParameter("@Form_Acknowledgment_Email_First_Open", SqlDbType.SmallInt) { Value = Form_Acknowledgment_Email_First_Open },
                  new SqlParameter("@Form_Subsequent_Open", SqlDbType.SmallInt) { Value = Form_Subsequent_Open },
                  new SqlParameter("@Form_Unsubscribe_From_Acknowledgment_Email ", SqlDbType.SmallInt) { Value = Form_Unsubscribe_From_Acknowledgment_Email },
                  new SqlParameter("@Form_Click_Link_On_Acknowledgment_Email", SqlDbType.SmallInt) { Value = Form_Click_Link_On_Acknowledgment_Email },
                  new SqlParameter("@Form_Subsequent_click", SqlDbType.SmallInt) { Value = Form_Subsequent_click },
                  new SqlParameter("@Email_Bounce", SqlDbType.SmallInt) { Value = Email_Bounce },
                  new SqlParameter("@Email_Unsubscribe", SqlDbType.SmallInt) { Value = Email_Unsubscribe },
                  new SqlParameter("@Email_Click_Link_First", SqlDbType.SmallInt) { Value = Email_Click_Link_First },
                  new SqlParameter("@Email_Subsequent_Click", SqlDbType.SmallInt) { Value = Email_Subsequent_Click },
                  new SqlParameter("@SMS_Bounce", SqlDbType.SmallInt) { Value = SMS_Bounce },
                  new SqlParameter("@SMS_Unsubscribe", SqlDbType.SmallInt) { Value = SMS_Unsubscribe },
              };

                string sCommandText = @"UPDATE Default_Scoring " +
                                "SET Form_Submit = @Form_Submit, " +
                                    "Form_Confirmation_Email_Bounce = @Form_Confirmation_Email_Bounce, " +
                                    "Form_Unsubscribe_From_Confirmation_Email = @Form_Unsubscribe_From_Confirmation_Email, " +
                                    "Form_Acknowledgment_Email_Bounce = @Form_Acknowledgment_Email_Bounce, " +
                                    "Form_Acknowledgment_Email_First_Open = @Form_Acknowledgment_Email_First_Open, " +
                                    "Form_Subsequent_Open = @Form_Subsequent_Open, " +
                                    "Form_Unsubscribe_From_Acknowledgment_Email = @Form_Unsubscribe_From_Acknowledgment_Email, " +
                                    "Form_Click_Link_On_Acknowledgment_Email = @Form_Click_Link_On_Acknowledgment_Email, " +
                                    "Form_Subsequent_click = @Form_Subsequent_click, " +
                                    "Email_Bounce = @Email_Bounce, " +
                                    "Email_Unsubscribe = @Email_Unsubscribe, " +
                                    "Email_Click_Link_First = @Email_Click_Link_First, " +
                                    "Email_Subsequent_Click = @Email_Subsequent_Click, " +
                                    "SMS_Bounce = @SMS_Bounce, " +
                                    "SMS_Unsubscribe = @SMS_Unsubscribe " +
                                    "WHERE Id = @Id AND Account_Id = @Account_Id";

                int success = SqlHelper.ExecuteNonQuery(connectionString, true, sCommandText, System.Data.CommandType.Text, sqlParam);

                return success == 0 ? false : true;
            }
            catch (Exception ex)
            {
                Logger.Logger.WriteLog(Account_Id.ToString(), "DefaultScore-UpdateDefaultScore", Account_Id.ToString(), "Error", ex.Message);
                return false;

            }
        }
    }
}
