using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class UserAccessLog
    {
        public int Access_Log_Id;
        public Guid User_Id;
        public string IP_Address;
        public DateTime Log_In;
        public DateTime Log_Out;
        public string Session_Id;
    }
}
