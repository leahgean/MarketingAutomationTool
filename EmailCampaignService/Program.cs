using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace EmailCampaignService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if (DEBUG)
            QueService service = new QueService();
            service.Start();
            //Console.WriteLine("Press any key to stop program");
            //Console.Read();
            //service.Stop();
#else
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new QueService()
                };
                ServiceBase.Run(ServicesToRun);
#endif
        }



    }
}
