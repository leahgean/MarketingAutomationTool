using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BusinessLayer
{
   public class LoginHistory
   {
        public DataTable GetLoginHistory(Guid UserId, DateTime? LoginDateTimeFrom, DateTime? LoginDateTimeTo, DateTime? LogoutDateTimeFrom, DateTime? LogoutDateTimeTo, string IPAddress, int PageNum, int MaxRows, out int MinItem , out int MaxItem , out int TotalRows)
        {
            DataAccessLayer.Controller.LoginHistory usr = new DataAccessLayer.Controller.LoginHistory();
            DataTable dtLoginHistory = usr.GetLoginHistory(UserId, LoginDateTimeFrom, LoginDateTimeTo, LogoutDateTimeFrom, LogoutDateTimeTo, IPAddress, PageNum, MaxRows, out MinItem, out MaxItem, out TotalRows);
            return dtLoginHistory;
        }
    }
}
