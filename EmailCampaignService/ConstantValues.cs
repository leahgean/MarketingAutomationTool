using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EmailCampaignService
{
    public static class ConstantValues
    {
        public const string Source = "EmailCampaignServices";
        public static string ConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["AutoMarketerConnectionString"].ConnectionString.ToString();
        }

        public static string LogFilePathName()
        {
            string sLogFolderPath = ConfigurationManager.AppSettings["EmailCampaignLogFolderPath"].ToString().Trim();
            string sLogFileNameFormat = ConfigurationManager.AppSettings["EmailCampaignLogFileName"].ToString().Trim();
            string sLogFileName = string.Format(sLogFileNameFormat, System.DateTime.UtcNow.AddHours(8).Year.ToString(), System.DateTime.UtcNow.AddHours(8).Month.ToString(), System.DateTime.UtcNow.AddHours(8).Day.ToString());
            return string.Format(@"{0}\{1}", sLogFolderPath, sLogFileName);
        }

        public enum EmailJobStatus
        {
            Pending = 1,
            Sending = 2,
            Completed = 3,
            ErrorOccurred = 4,
            Updating = 5
        }

        public enum CampaignStatus
        {
            Draft = 0,
            Submitted = 1
        }

        public enum SendingOptions
        {
            SendNow = 0,
            Scheduled = 1
        }

        public enum MessageBodyFormat
        {
            Text = 0,
            HTML = 1
        }
    }
}
