using System.IO;
using System.Web;
using System.Configuration;

namespace Logger
{
    public static class Logger
    {
        public static void WriteLog(string loggeduser, string module, string id, string type, string message)
        {

            string sLogFolder = ConfigurationManager.AppSettings["LogFolder"].ToString().Trim();
            string sLogFileNameFormat = ConfigurationManager.AppSettings["LogFileName"].ToString().Trim();

            string sLogFileName = string.Format(sLogFileNameFormat, System.DateTime.UtcNow.AddHours(8).Year.ToString(), System.DateTime.UtcNow.AddHours(8).Month.ToString(), System.DateTime.UtcNow.AddHours(8).Day.ToString());

            string sLogFolderPath = HttpContext.Current.Server.MapPath(string.Format("~/{0}", sLogFolder));
            string sLogFilePath=HttpContext.Current.Server.MapPath(string.Format("~/{0}/{1}", sLogFolder,sLogFileName));


            if (!Directory.Exists(sLogFolderPath))
            {
                Directory.CreateDirectory(sLogFolderPath);
            }

            using (StreamWriter writer = new StreamWriter(sLogFilePath, true))
            {
                writer.WriteLine("[{0}]\tLoggedUser:{1}\tModule:{2}\tId:{3}\tType:{4}\tMessage:{5}", System.DateTime.UtcNow.AddHours(8), loggeduser, module, id, type, message);
            }
        }



       
    }
}
