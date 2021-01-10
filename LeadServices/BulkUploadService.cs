using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace LeadServices
{
    public partial class BulkUploadService : ServiceBase
    {
        public BulkUploadService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            BulkUploadParseManager service = new BulkUploadParseManager();
            service.Start();
        }

        protected override void OnStop()
        {
        }
    }
}
