using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;

namespace Services_WebAPI.Controller
{
    public class LeadServicesController : ApiController
    {
        public IHttpActionResult Get()
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = ConfigurationManager.AppSettings["LeadServices"].ToString().Trim();
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
                return Ok();
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
