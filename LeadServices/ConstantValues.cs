using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace LeadServices
{
    public static class ConstantValues
    {
        public const string Source = "LeadServices";
        public static string ConnectionString() {
            return ConfigurationManager.ConnectionStrings["AutoMarketerConnectionString"].ConnectionString.ToString();
        }

        public enum ContactJobStatus
        {
            PENDING = 100,
            PARSINGEXCEL = 101,
            RUNNINGIMPORT=102,
            COMPLETED = 103,
            CANCELLED = 104,
            CORRUPTED_FILE = 105,
            ERROROCCURED = 106
        }

        public static string ContactListJobFileValidExtension = "xlsx";

        public enum Error_Code
        {
            Success = 0,
            File_Does_Not_Exist = 10001,
            File_Can_Not_Open = 10002,
            File_Too_Big = 10003,
            Parser_Err = 10004,
            Invalid_Name = 10005,
            Invalid_Email = 10006,
            Email_Duplicate = 10007,
            Excede_Max_Names = 10008,
            Invalid_Phone = 10009,
            Invalid_Mobile = 100010,
            Contact_Insert = 100011
        }

        public enum ContactAction
        {
            SUBSCRIBE = 1,
            UNSUBSCRIBE = 2,
            UPDATECONTACTDETAILS = 3,
            DELETE = 4
        }
    }
}
