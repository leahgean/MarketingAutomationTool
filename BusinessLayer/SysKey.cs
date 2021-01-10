using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer
{
    public class SysKey
    {
        public string GetKey(string KeyName)
        {
            DataAccessLayer.Controller.SysKey syskey = new DataAccessLayer.Controller.SysKey();
            string strKeyValue = syskey.GetKey(KeyName);
            return strKeyValue;

        }
    }
}
