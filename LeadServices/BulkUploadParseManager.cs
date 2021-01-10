using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using EnterpriseLibrary.Logging;
using System.Configuration;

namespace LeadServices
{
    
    public class BulkUploadParseManager
    {
        private Timer timer1 = null;
        private bool _isRunning = false;
        private int _interval = 60000;
        public int Interval
        {
            get { return _interval; }
            set { this._interval = value; }
        }
        public void Start()
        {
            DatabaseLog db = new DatabaseLog(ConstantValues.ConnectionString());
            db.AddLog(ConstantValues.Source, "Information", "Bulk Upload Service has been started", null, null);


            timer1 = new Timer();
            this.timer1.Interval = Interval;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Tick);
            timer1.Enabled = true;

            //for testing purposes only
            ProcessImportedFiles();
        }
        private void timer1_Tick(object sender, ElapsedEventArgs e)
        {
            if (_isRunning) return;
            ProcessImportedFiles();
        }

        public void Stop()
        {
            timer1.Enabled = false;
            //Library.WriteErrorLog("Test window service stopped");
        }

        private void ProcessImportedFiles()
        {
            _isRunning = true;

            List<ContactJob> unfinishedJobs = BulkUpload.GetUnfinishedJob();
            foreach (ContactJob unfinishedJob in unfinishedJobs)
            {
                ImportJob(unfinishedJob);
            }

            List<ContactJob> interrupedExcelParsingJobs = BulkUpload.ResumeExcelParsing();
            foreach (ContactJob interruptedExcelParsingJob in interrupedExcelParsingJobs)
            {
                int lastrowprocessed = 0;

                if (interruptedExcelParsingJob.CurrentRowParsedInExcel.HasValue)
                    lastrowprocessed = interruptedExcelParsingJob.CurrentRowParsedInExcel.Value;
                ResumeExcelParse(interruptedExcelParsingJob, lastrowprocessed);
            }

            List<ContactJob> interruptedJobs = BulkUpload.ResumeInteruptedJob();
            foreach (ContactJob interruptedJob in interruptedJobs)
            {
                ImportInterruptedJobs(interruptedJob);
            }
            _isRunning = false;
        }

        private void ImportInterruptedJobs(ContactJob job)
        {
            try
            {
                object lockObject = new object();
                lock (lockObject)
                {

                    DatabaseLog db = new DatabaseLog(ConstantValues.ConnectionString());
                    db.AddLog(ConstantValues.Source, "Trace", string.Empty, "Resuming Processing", job.JobId.ToString(), "ContactJob-JobID", null, null);

                    Importer bulkupload = new Importer();
                    bulkupload.ResumeExcel(job);
                }
            }
            catch (Exception ex)
            {
                DatabaseLog db = new DatabaseLog(ConstantValues.ConnectionString());
                db.AddLog(ConstantValues.Source, ex,  string.Empty, job.JobId.ToString(), "ContactJob-JobID", null, null);
            }
        }

        private void ImportJob(ContactJob job)
        {
            try
            {
                object lockObject = new object();
                lock (lockObject)
                {
                    DatabaseLog db = new DatabaseLog(ConstantValues.ConnectionString());
                    db.AddLog(ConstantValues.Source, "Trace",string.Empty, "Start Processing", job.JobId.ToString(), "ContactJob-JobID", null, null);

                    Importer bulkupload = new Importer();
                    bulkupload.ProcessFile(job);
                }
            }
            catch (Exception ex)
            {
                DatabaseLog db = new DatabaseLog(ConstantValues.ConnectionString());
                db.AddLog(ConstantValues.Source, ex, string.Empty, job.JobId.ToString(), "ContactJob-JobID", null, null);
            }
        }

        private void ResumeExcelParse(ContactJob job, int lastrowprocessed)
        {
            try
            {
                object lockObject = new object();
                lock (lockObject)
                {
                    DatabaseLog db = new DatabaseLog(ConstantValues.ConnectionString());
                    db.AddLog(ConstantValues.Source, "Trace", string.Empty, "Resume Parse Excel", job.JobId.ToString(), "ContactJob-JobID", null, null);

                    Importer bulkupload = new Importer();
                    bulkupload.ReProcessFile(job, lastrowprocessed);
                }
            }
            catch (Exception ex)
            {
                DatabaseLog db = new DatabaseLog(ConstantValues.ConnectionString());
                db.AddLog(ConstantValues.Source, ex, string.Empty, job.JobId.ToString(), "ContactJob-JobID", null, null);
            }
        }
    }
}
