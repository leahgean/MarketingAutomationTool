using Enterprise.Library;
using System;
using System.Collections.Generic;
using BLL.Accounts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Messages
{
    internal enum SystemFields
    {
        Sys_User_First_Name = -10001,
        Sys_User_Last_Name = -10002,
        Sys_User_Email = -10003,
        Sys_User_Mobile = -10004,
        Sys_User_UserName = -10005,
        Sys_User_ChangePassID = -10006,

        Sys_Forward_Message_Id = -10101,
        Sys_Forward_Sender_Id = -10102,
        Sys_Forward_Sender_First_Name = -10103,
        Sys_Forward_Sender_Last_Name = -10104,
        Sys_Forward_Sender_Email = -10105,
        Sys_Forward_Sender_Description = -10106,

        Sys_Score_Lead_ID = -10201,
        Sys_Score_Lead_Score = -10202,
        Sys_Score_Lead_First_Name = -10203,
        Sys_Score_Lead_Last_Name = -10204,
        Sys_Score_Lead_Email = -10205,
        Sys_Score_Lead_Mobile = -10206,
        Sys_Score_Campaign = -10207,

        Sys_Account_BusinessName = -10301,
        Sys_Account_Email = -10302,
        Sys_Account_Mobile = -10303,
        Sys_Account_Tel = -10304,

        Sys_New_Lead_Id = -10401,
        Sys_New_Lead_First_Name = -10402,
        Sys_New_Lead_Last_Name = -10403,
        Sys_New_Lead_Email = -10404,
        Sys_New_Lead_Mobile = -10405

    }

    public enum SystemMessageType
    {
        ForwardEmail = 1,
        NotifyScore = 2,
        NotifyNewAccount = 3,
        ConfirmNewAccount = 4,
        ForgotPassword = 5,
        ImportLead = 6,
        AfterSubmission = 7,
        AfterConfirmation = 8
    }

    public class SystemMessage
    {
        public static Result ImportLead(string receipientUserId, int accountId)
        {
            int jobId;
            int count;

            Result result = new Result();

            MembershipEO userInfo = new MembershipEO();
            result = userInfo.Load(receipientUserId);
            if (!result.Successful) return result;

            MessageEO systemMessage = new MessageEO();
            result = systemMessage.LoadByEntityID(accountId, (int)SystemMessageType.ImportLead);
            if (!result.Successful) return result;

            try
            {
                SB.Communication.B.Message dispatcher = new SB.Communication.B.Message();
                // MON 10 Jan 2014
                // changed from false to true the force insufficient
                dispatcher.Dispatch(null, systemMessage.UserID, accountId, true, int.Parse(systemMessage.ID), DateTime.Now, 0, -1, null, true,
                    new KeyValuePair<string, string>[] {
                                                            new KeyValuePair<string, string>(SystemFields.Sys_User_First_Name.ToString(), userInfo.FirstName),
                                                            new KeyValuePair<string, string>(SystemFields.Sys_User_Last_Name.ToString(), userInfo.LastName),
                                                            new KeyValuePair<string, string>(SystemFields.Sys_User_Email.ToString(), userInfo.Email),
                                                            new KeyValuePair<string, string>(SystemFields.Sys_User_Mobile.ToString(), userInfo.Mobile)
                                                       }
                    , out jobId, out count);
            }
            catch (Exception exp)
            {
                result.UpdateError(exp);
            }

            return result;
        }
    }
}
