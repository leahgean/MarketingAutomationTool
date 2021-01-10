using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace LeadServices
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if (DEBUG)
            BulkUploadParseManager service = new BulkUploadParseManager();
            service.Start();
#else
           ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new BulkUploadService()
            };
            ServiceBase.Run(ServicesToRun);
#endif

        }
    }
}
