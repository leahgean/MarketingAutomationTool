using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Logger;
using EnterpriseLibrary.Logging;
using System.Configuration;


namespace EmailCampaignService
{
    public partial class QueService : ServiceBase
    {
        private Thread _WatchThread;
        private bool _blnStopping = false;
        EmailCampaignService.Process p = new EmailCampaignService.Process();

        public QueService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
           EmailCampaignLogging.WriteLog("Campaign Email Service begins to start.",false,LogLevels.ExecutionSteps);
            try
            {
                #region Create Watch thread
                if (Int32.Parse(ConfigurationManager.AppSettings["INTERVAL_WATCH_MIN"].ToString()) > 0)
                {
                    _WatchThread = new Thread(new ThreadStart(Watch));
                    _WatchThread.SetApartmentState(ApartmentState.STA);
                    _WatchThread.Priority = System.Threading.ThreadPriority.AboveNormal;
                    _WatchThread.Name = ConfigurationManager.AppSettings["NAME_SERVICE"] + "--WATCH--SERVICE-";
                    _WatchThread.Start();
                }
                EmailCampaignLogging.WriteLog("Watch thread created.", false, LogLevels.ExecutionSteps);
                #endregion

                #region Create the working STA thread
                Thread t = new Thread(new ThreadStart(StartProcessing));
                t.SetApartmentState(ApartmentState.STA);
                t.Priority = System.Threading.ThreadPriority.Normal;
                t.Name = ConfigurationManager.AppSettings["NAME_SERVICE"] + "-Processing-Service";
                t.Start();
                #endregion
                EmailCampaignLogging.WriteLog(string.Format("{0} {1}", ConfigurationManager.AppSettings["NAME_SERVICE"].ToString(), " service is started"), false, LogLevels.ExecutionSteps);
            }
            catch (Exception e)
            {
                EmailCampaignLogging.WriteLog(e, LogLevels.ExceptionsOnly);
                //throw e;
            }

            DatabaseLog db = new DatabaseLog(ConstantValues.ConnectionString());
            db.AddLog(ConstantValues.Source, "Information", "Campaign Email Que Service has been started.",  null, null);
        }


        #region Watch
        /// <summary>
        /// Monitor Suspicious conditions in the Application and raise exceptions if required
        /// </summary>
        private void Watch()
        {
            while (!_blnStopping)
            {
                
                EmailCampaignLogging.WriteLog("Watch kicks in", false, LogLevels.ExecutionSteps);
                try
                {
                    #region Monitor Log file size
                    System.IO.FileInfo fi;
                    if (System.IO.File.Exists(ConstantValues.LogFilePathName()))
                    {
                        fi = new System.IO.FileInfo(ConstantValues.LogFilePathName());
                        if (fi.Length / 1024 > Int32.Parse(ConfigurationManager.AppSettings["ALERTS_LOG_FILE_SIZE_KB_THRESHHOLD"]))
                        {
                            EmailCampaignLogging.WriteLog(string.Format("{0} {1} {2} {3} {4}", "Log file size has reached", (fi.Length / 1024).ToString(), "Kb that exceeds", ConfigurationManager.AppSettings["ALERTS_LOG_FILE_SIZE_KB_THRESHHOLD"], "kb threshold."), true, LogLevels.Warnings);
                        }
                    }
                    #endregion

                    #region Watch memory used by the process
                    long lngNonPaged = System.Diagnostics.Process.GetCurrentProcess().NonpagedSystemMemorySize64 / 1024;
                    long lngPaged = System.Diagnostics.Process.GetCurrentProcess().PagedSystemMemorySize64 / 1024;
                    
                    EmailCampaignLogging.WriteLog(string.Format("{0} {1} {2} {3} {4}", "Process' NonPaged memory=", lngNonPaged.ToString(), "K, Paged memory=", lngPaged.ToString(), "K"), true, LogLevels.Warnings);

                    if (lngNonPaged > Int32.Parse(ConfigurationManager.AppSettings["PROCESS_MEMORY_KB_THRESHHOLD"]))
                    {
                        EmailCampaignLogging.WriteLog(string.Format("{0} {1} {2}", "Process has exceeded Memory threshhold. Currently NonPaged memory=", lngNonPaged.ToString(), "K, Paged memory="), true, LogLevels.Warnings);
                    }
                    #endregion
                }
                catch (Exception e)
                {
                    EmailCampaignLogging.WriteLog(e, LogLevels.ExceptionsOnly);
                    //throw e;
                }
                Thread.Sleep(Int32.Parse(ConfigurationManager.AppSettings["INTERVAL_WATCH_MIN"].ToString()) * 60000);
            }
        }
        #endregion

        protected override void OnStop()
        {
        
            EmailCampaignLogging.WriteLog("Attempting to stop the service", true, LogLevels.ExecutionSteps);
            _blnStopping = true;
            try
            {
                _WatchThread.Abort();
                p.Stop();
            }
            catch { }
            try
            {
                EmailCampaignLogging.WriteLog(string.Format("{0} {1}", ConfigurationManager.AppSettings["NAME_SERVICE"].ToString() ,"service is stopped\r\n\r\n\r\n\r\n"), true, LogLevels.Warnings);
            }
            catch (Exception e)
            {
                EmailCampaignLogging.WriteLog(e, LogLevels.ExceptionsOnly);
                //throw e;
            }
        }

        public void StartProcessing()
        {
            p.Start();
        }

        public void Start()
        {
            OnStart(null);
        }
        public void Stop()
        {
            OnStop();
        }
    }
}
