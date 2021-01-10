using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using EnterpriseLibrary.Logging;

namespace LeadServices
{
    public class BulkUpload
    {
        static public List<ContactJob> GetUnfinishedJob()
        {
            DataClasses1DataContext dc = new DataClasses1DataContext(ConstantValues.ConnectionString());
            return dc.ContactJobs.Where(j => j.JobFinished == null && j.JobStatusId == (int)ConstantValues.ContactJobStatus.PENDING).ToList();
        }

        static public List<ContactJob> ResumeInteruptedJob()
        {
            DataClasses1DataContext dc = new DataClasses1DataContext(ConstantValues.ConnectionString());
            return dc.ContactJobs.Where(j => j.JobFinished == null && j.JobStatusId == (int)ConstantValues.ContactJobStatus.RUNNINGIMPORT).ToList();
        }

        static public List<ContactJob> ResumeExcelParsing()
        {
            DataClasses1DataContext dc = new DataClasses1DataContext(ConstantValues.ConnectionString());
            return dc.ContactJobs.Where(j => j.JobFinished == null && j.JobStatusId == (int)ConstantValues.ContactJobStatus.PARSINGEXCEL).ToList();
        }

        static public ContactJob GetJob(DataClasses1DataContext datacontext, int job_id)
        {
            DataClasses1DataContext dc = (datacontext == null) ? new DataClasses1DataContext() : datacontext;
            return dc.ContactJobs.Where(j => j.JobId == job_id).FirstOrDefault();
        }

        static public bool UpdateJob(int Job_Id, DateTime? Job_Started, DateTime? Job_Finished, byte Job_Status_Id, int Total_Contacts, int Uploaded_Contacts, string Error, out string feedback)
        {
            feedback = string.Empty;
            bool result = true;
            try
            {
                using (DataClasses1DataContext dc = new DataClasses1DataContext(ConstantValues.ConnectionString()))
                {
                    ContactJob job = BulkUpload.GetJob(dc, Job_Id);

                    if (Job_Started != null) job.JobStarted = Job_Started;
                    if (Job_Finished != null) job.JobFinished = Job_Finished;

                    if (Total_Contacts > 0)
                        job.TotalContacts = Total_Contacts;
                    if (Uploaded_Contacts > 0)
                        job.UploadedContacts = Uploaded_Contacts;

                    job.JobStatusId = Job_Status_Id;

                    if (Error != string.Empty)
                        job.Error = Error;

                    dc.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                result = false;
                feedback = ex.Message;
            }
            return result;
        }

        static public bool UpdateJob(int Job_Id,  int CurrentRowParsedInExcel)
        {
            
            bool result = true;
            try
            {
                using (DataClasses1DataContext dc = new DataClasses1DataContext(ConstantValues.ConnectionString()))
                {
                    ContactJob job = BulkUpload.GetJob(dc, Job_Id);

                    if (CurrentRowParsedInExcel > 0)
                        job.CurrentRowParsedInExcel = CurrentRowParsedInExcel;
                    

                    dc.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }




    }
}
