using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Logger
{
    public enum LogLevels
    {
        Calls,
        ExceptionsOnly,
        ExecutionSteps,
        Information,
        None,
        Parameters,
        Warnings
    }

    public static class EmailCampaignLogging
    {
        public static void WriteLog(string pMessage, bool pInsertEmptyLine, LogLevels pLogLevels)
        {
            try
            {
                string sLogFolderPath = ConfigurationManager.AppSettings["EmailCampaignLogFolderPath"].ToString().Trim();
                string sLogFileNameFormat = ConfigurationManager.AppSettings["EmailCampaignLogFileName"].ToString().Trim();
                string sLogFileName = string.Format(sLogFileNameFormat, System.DateTime.UtcNow.AddHours(8).Year.ToString(), System.DateTime.UtcNow.AddHours(8).Month.ToString(), System.DateTime.UtcNow.AddHours(8).Day.ToString());
                string sLogFilePath = string.Format(@"{0}\{1}", sLogFolderPath, sLogFileName);


                if (!Directory.Exists(sLogFolderPath))
                {
                    Directory.CreateDirectory(sLogFolderPath);
                }

                using (StreamWriter writer = new StreamWriter(sLogFilePath, true))
                {
                    writer.WriteLine("[{0}]\tMessage:{1}\tLogLevel:{2}", System.DateTime.UtcNow.AddHours(8), pMessage, pLogLevels.ToString());
                    if (pInsertEmptyLine)
                    {
                        writer.WriteLine(string.Empty);
                    }
                }
            }
            catch(Exception ex)
            {

            }
           
        }


        public static void WriteLog(Exception e, LogLevels pLogLevels)
        {
            try
            {
                string sLogFolderPath = ConfigurationManager.AppSettings["EmailCampaignLogFolderPath"].ToString().Trim();
                string sLogFileNameFormat = ConfigurationManager.AppSettings["EmailCampaignLogFileName"].ToString().Trim();
                string sLogFileName = string.Format(sLogFileNameFormat, System.DateTime.UtcNow.AddHours(8).Year.ToString(), System.DateTime.UtcNow.AddHours(8).Month.ToString(), System.DateTime.UtcNow.AddHours(8).Day.ToString());
                string sLogFilePath = string.Format(@"{0}\{1}", sLogFolderPath, sLogFileName);


                if (!Directory.Exists(sLogFolderPath))
                {
                    Directory.CreateDirectory(sLogFolderPath);
                }

                using (StreamWriter writer = new StreamWriter(sLogFilePath, true))
                {
                    writer.WriteLine("[{0}]\tMessage:{1}\tLogLevel:{2}", System.DateTime.UtcNow.AddHours(8), e.Message, pLogLevels.ToString());

                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void WriteLog(Exception e, LogLevels pLogLevels,string pModule, string pData)
        {
            try
            {
                string sLogFolderPath = ConfigurationManager.AppSettings["EmailCampaignLogFolderPath"].ToString().Trim();
                string sLogFileNameFormat = ConfigurationManager.AppSettings["EmailCampaignLogFileName"].ToString().Trim();
                string sLogFileName = string.Format(sLogFileNameFormat, System.DateTime.UtcNow.AddHours(8).Year.ToString(), System.DateTime.UtcNow.AddHours(8).Month.ToString(), System.DateTime.UtcNow.AddHours(8).Day.ToString());
                string sLogFilePath = string.Format(@"{0}\{1}", sLogFolderPath, sLogFileName);

                if (!Directory.Exists(sLogFolderPath))
                {
                    Directory.CreateDirectory(sLogFolderPath);
                }

                using (StreamWriter writer = new StreamWriter(sLogFilePath, true))
                {
                    writer.WriteLine("[{0}]\tMessage:{1}\tLogLevel:{2}\tModule:{3}\tData:{4}", System.DateTime.UtcNow.AddHours(8), e.Message, pLogLevels.ToString(), pModule, pData);

                }
            }
            catch (Exception ex)
            {

            }
}
    }
}
