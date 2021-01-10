using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Business;

namespace EnterpriseLibrary.SysKey
{
   public class SysKey
    {
        public static string GetKey(string KeyName)
        {
            return Common.Business.SysKey.GetKey(KeyName);
        }
    }

}
