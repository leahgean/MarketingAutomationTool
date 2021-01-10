using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data;

namespace Common.Business
{
    public class SysKey
    {
        public static string GetKey(string KeyName)
        {
            Common.Data.SysKey sys = new Common.Data.SysKey();
            string result =sys.GetKey(KeyName);
            return result;
        }
    }
}
