using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseLibrary.Logging
{
    public class DatabaseLog
    {
        private static string _connectionstring;

        public DatabaseLog(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        private static string FormatException(Exception ex)
        {
            return string.Format("Time:{0}\r\n\r\nError Message:{1}\r\n\r\nStackTrace:{2}\r\n\r\nInner Exception Message:{3}\r\n\r\nSource:{4}",
                                     DateTime.UtcNow.ToString("hh:mm:ss:ms"),
                                     ex.Message,
                                     ex.StackTrace,
                                     (ex.InnerException != null) ? ex.InnerException.Message : string.Empty,
                                     ex.Source);
        }

        public  Result AddLog(string logSource, Exception ex, string logCode, string entityID, string entityType, Guid? userID, string ipAddress)
        {
            return AddLog(logSource, "Exception", logCode, FormatException(ex), entityID, entityType, userID, ipAddress);
        }

        public  Result AddLog(string logSource, string logType, string logData, Nullable<Guid> userID, string ipAddress)
        {
            return AddLog(logSource, logType, string.Empty, logData, string.Empty, string.Empty, userID, ipAddress);
        }

        public  Result AddLog(string logSource, string logType, string logCode, string logData, string entityID, string entityType, Guid? userID, string ipAddress)
        {
            Result result = new Result(false, string.Empty,null);
            try
            {
                DataClasses1DataContext dc = new DataClasses1DataContext(_connectionstring);
                Sys_Log s = new Sys_Log();
                s.Entity_Id = entityID;
                s.Entity_Type = entityType;
                s.Log_Source = logSource;
                s.Log_Type = logType;
                s.Log_Code = logCode;
                s.Log_Data = logData;
                s.User_Id = userID;
                s.Ip_Address = ipAddress;
                dc.Sys_Logs.InsertOnSubmit(s);
                dc.SubmitChanges();

                result.Success = true;
            }
            catch(Exception ex)
            {
                string sMessage = ex.Message;
                result.Success = false;
                result.ErrorMessage = sMessage;
                result.ExceptionObj = ex;
            }
            
            return result;
        }
    }
}
