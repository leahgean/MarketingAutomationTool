using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ConnectionStrings
    {
        public static string Default
        {
            get
            {
                return GetConnectionString("ConnectionString_Default");
            }
        }
        //WF
        public static string WorkflowConnectionString
        {
            get
            {
                return GetConnectionString("Ent_ConnectionString_Workflow");
            }
        }
        //Tracking
        public static string TrackingConnectionString
        {
            get
            {
                return GetConnectionString("Ent_ConnectionString_Tracking");
            }
        }

        static string GetConnectionString(string key)
        {
            return Enterprise.Library.SysKeys.FetchKeyValue(key).Entity;
        }



    }
}
