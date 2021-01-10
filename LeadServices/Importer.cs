using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterpriseLibrary.Logging;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Web;
using EnterpriseLibrary.LeadService.FileAsset;
using EnterpriseLibrary.MailSender;
using EnterpriseLibrary.SysKey;
using ClosedXML.Excel;
using System.Configuration;

namespace LeadServices
{
    public class Importer
    {
        public const string MatchEmailPattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                              @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                              @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public bool ProcessFile(ContactJob job)
        {
            string sFilePath = string.Empty;
            string sFullPath = string.Empty;
            string strFileExtension = string.Empty;
            sFilePath= Path.Combine(FileAssetManager.AssetsDirectory(), FileAssetManager.LeadServiceDirectory()); 
            sFullPath = Path.Combine(sFilePath, job.FileName);
            
            DatabaseLog db = new DatabaseLog(ConstantValues.ConnectionString());


            if (!File.Exists(sFullPath))
            {
                db.AddLog(ConstantValues.Source, "ERROR", "ERROR", string.Format("File does not exist: {0}", job.FileName), job.JobId.ToString(), "Importer-ProcessFile", null, null);
                add_error(job.JobId, 0, (int)ConstantValues.Error_Code.File_Does_Not_Exist);
                return false;
            }
            else
            {
                if (string.IsNullOrEmpty(job.JobStarted.ToString()))
                {
                    string feedback = string.Empty;
                    if (!BulkUpload.UpdateJob(job.JobId, DateTime.UtcNow, null, (int)ConstantValues.ContactJobStatus.PARSINGEXCEL, 0, 0, string.Empty, out feedback))
                    {
                        db.AddLog(ConstantValues.Source, "ERROR", "ERROR", feedback, job.JobId.ToString(), "Importer-ProcessFile", null, null);
                        return false;
                    }
                    else{

                        strFileExtension = System.IO.Path.GetExtension(job.FileName).Remove(0, 1);
                        if (string.Compare(strFileExtension, ConstantValues.ContactListJobFileValidExtension.ToString()) != 0)
                        {
                            db.AddLog(ConstantValues.Source, "ERROR", "ERROR", "Invalid file format.", job.JobId.ToString(), "Importer-ProcessFile", null, null);
                            return false;
                        }
                        else
                        {
                            ProcessExcelFile(job,sFullPath, 0);
                        }
                    }
                }
            }
            return true;
        }


        public bool ReProcessFile(ContactJob job, int lastrowprocessed)
        {
            string sFilePath = string.Empty;
            string sFullPath = string.Empty;
            string strFileExtension = string.Empty;
            sFilePath = Path.Combine(FileAssetManager.AssetsDirectory(), FileAssetManager.LeadServiceDirectory());
            sFullPath = Path.Combine(sFilePath, job.FileName);

            DatabaseLog db = new DatabaseLog(ConstantValues.ConnectionString());


            if (!File.Exists(sFullPath))
            {
                db.AddLog(ConstantValues.Source, "ERROR", "ERROR", string.Format("File does not exist: {0}", job.FileName), job.JobId.ToString(), "Importer-ProcessFile", null, null);
                add_error(job.JobId, 0, (int)ConstantValues.Error_Code.File_Does_Not_Exist);
                return false;
            }
            else
            {
                if (!string.IsNullOrEmpty(job.JobStarted.ToString()))
                {
                    string feedback = string.Empty;
                    if (!BulkUpload.UpdateJob(job.JobId, DateTime.UtcNow, null, (int)ConstantValues.ContactJobStatus.PARSINGEXCEL, 0, 0, string.Empty, out feedback))
                    {
                        db.AddLog(ConstantValues.Source, "ERROR", "ERROR", feedback, job.JobId.ToString(), "Importer-ProcessFile", null, null);
                        return false;
                    }
                    else
                    {

                        strFileExtension = System.IO.Path.GetExtension(job.FileName).Remove(0, 1);
                        if (string.Compare(strFileExtension, ConstantValues.ContactListJobFileValidExtension.ToString()) != 0)
                        {
                            db.AddLog(ConstantValues.Source, "ERROR", "ERROR", "Invalid file format.", job.JobId.ToString(), "Importer-ProcessFile", null, null);
                            return false;
                        }
                        else
                        {
                            ProcessExcelFile(job, sFullPath, lastrowprocessed);
                        }
                    }
                }
            }
            return true;
        }

        private void ProcessExcelFile(ContactJob job,string sFullPath, int lastrowprocessed)
        {
            DatabaseLog db = new DatabaseLog(ConstantValues.ConnectionString());
            
            try
            {
                DataTable dt = new DataTable();
                string feedback = string.Empty;

                //Open the Excel file
                using (XLWorkbook workBook = new XLWorkbook(sFullPath))
                {
                    //Read the first Sheet from Excel file.
                    IXLWorksheet workSheet = workBook.Worksheet(1);
                    string sCellRange = ConfigurationManager.AppSettings["cellrange"].ToString();

                    //Loop through the Worksheet rows.
                    bool firstRow = true;
                    foreach (IXLRow row in workSheet.Rows())
                    {
                        //Use the first row to add columns to DataTable.
                        if (firstRow)
                        {
                            foreach (IXLCell cell in row.Cells(sCellRange))
                            {
                                dt.Columns.Add(cell.Value.ToString());
                            }
                            firstRow = false;
                        }
                        else
                        {
                            //Add rows to DataTable.
                            dt.Rows.Add();
                            int i = 0;
                            foreach (IXLCell cell in row.Cells(sCellRange))
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                                i++;
                            }
                        }
                    }
                }

                string title = string.Empty, 
                    first_name = string.Empty, 
                    last_name = string.Empty, 
                    gender = string.Empty,
                    email = string.Empty, 
                    mobile = string.Empty,
                    phone_no = string.Empty,
                    company_name = string.Empty,
                    website = string.Empty,
                    position_title = string.Empty, 
                    address1 = string.Empty, 
                    address2 = string.Empty,
                    city = string.Empty,
                    state = string.Empty,
                    country = string.Empty,
                    postcode = string.Empty;

                    int? countryid = 0;
                    bool subscribedtoemail =true;
                    int? contacttype=null;
                    int? contactstatus = null;
                    int? leadstatus = null;
                    string subsribedtoemailvia="ExcelImport";
                    int? formid=null;
                    int? filterid=null;

                    int counter = 0;
                    int uploaded = 0;


                int maxrow = dt.Rows.Count;
                if (maxrow > 0)
                    maxrow = maxrow - 1;

                if (lastrowprocessed == 0)
                {
                    counter = 0;//start with 0 based index
                }
                else
                {
                    counter = lastrowprocessed;//used saved row because this is the next row
                    uploaded = lastrowprocessed-1;
                }

                for(int i= counter; i<=maxrow; i++)
                {
                    DataRow dr = dt.Rows[i];
                    counter++;//increment counter because current row is +1 due to 0 based index
                   
                    if (dt.Columns.Contains("TITLE"))
                        title = dr["TITLE"].ToString().Trim();

                    if (dt.Columns.Contains("FIRST_NAME"))
                        first_name = dr["FIRST_NAME"].ToString().Trim();

                    if (dt.Columns.Contains("LAST_NAME"))
                        last_name = dr["LAST_NAME"].ToString().Trim();

                    if (dt.Columns.Contains("GENDER"))
                        gender = dr["GENDER"].ToString().Trim();

                    if (dt.Columns.Contains("EMAIL"))
                        email = dr["EMAIL"].ToString().Trim();

                    if (dt.Columns.Contains("MOBILE"))
                        mobile = dr["MOBILE"].ToString().Trim();

                    if (dt.Columns.Contains("PHONE_NO"))
                        phone_no = dr["PHONE_NO"].ToString().Trim();

                    if (dt.Columns.Contains("COMPANY_NAME"))
                        company_name = dr["COMPANY_NAME"].ToString().Trim();

                    if (dt.Columns.Contains("POSITION_TITLE"))
                        position_title = dr["POSITION_TITLE"].ToString().Trim();

                    if (dt.Columns.Contains("ADDRESS1"))
                        address1 = dr["ADDRESS1"].ToString().Trim();

                    if (dt.Columns.Contains("ADDRESS2"))
                        address2 = dr["ADDRESS2"].ToString().Trim();

                    if (dt.Columns.Contains("WEBSITE"))
                        website = dr["WEBSITE"].ToString().Trim();

                    if (dt.Columns.Contains("CITY"))
                        city = dr["CITY"].ToString().Trim();
                    
                    if (dt.Columns.Contains("STATE"))
                        state = dr["STATE"].ToString().Trim();

                    if (dt.Columns.Contains("COUNTRY"))
                        country = dr["COUNTRY"].ToString().Trim();

                    if (dt.Columns.Contains("POSTCODE"))
                        postcode = dr["POSTCODE"].ToString().Trim();

                    gender = gender.ToLower();

                    if (gender == "male" || gender == "m")
                    {
                        gender = "M";
                    }
                    else if (gender == "female" || gender == "f")
                    {
                        gender = "F";
                    }
                    else
                    {
                        gender = "";
                    }

                    string[] Records = new string[]
                        {
                            first_name,
                            last_name,
                            email,
                            mobile
                        };

                    int err_code = CheckValidation(Records);

                    if (err_code > 0)
                    {
                        add_error(job.JobId, counter, err_code);
                        continue;
                    }


                    //Check for unique email
                    if (TempContact.IsUniqueEmail(job.User.AccountID.Value, email))
                    {
                        string strGender = gender;
                        char chrGender = ' ';
                        if (strGender != string.Empty)
                        {
                            chrGender = strGender.ToCharArray()[0];
                        }

                        long nid;

                        countryid = TempContact.GetCountryId(country);

                        

                        if (TempContact.Create(
                            job.JobId,
                            job.User.AccountID.Value,
                            first_name,
                            last_name,
                            email,
                            mobile,
                            title,
                            subscribedtoemail,
                            contacttype,
                            contactstatus,
                            leadstatus,
                            chrGender,
                            company_name,
                            website,
                            position_title,
                            phone_no,
                            address1,
                            address2,
                            city,
                            state,
                            countryid,
                            postcode,
                            subsribedtoemailvia,
                            job.User.UserID,
                            job.IPAddress,
                            formid,
                            job.ContactListId,
                            filterid,
                            out feedback,
                            out nid))
                        {
                            uploaded++;
                            BulkUpload.UpdateJob(job.JobId, counter);
                        }
                        else
                        {
                            string feedbackx = string.Empty;
                            string Msg = string.Empty;
                            int oi = 0;
                            BulkUploadException.CreateException(job.JobId, counter, (int)ConstantValues.Error_Code.Contact_Insert, feedback, out feedbackx, out oi);
                        }
                    }
                    else
                    {
                        //Create Log Exception
                        add_error(job.JobId, counter, (int)ConstantValues.Error_Code.Email_Duplicate);
                        continue;
                    }

                }

                dt = null;


                if (!BulkUpload.UpdateJob(job.JobId, DateTime.UtcNow, null, (int)ConstantValues.ContactJobStatus.RUNNINGIMPORT, counter, uploaded, string.Empty, out feedback))
                {
                    db.AddLog(ConstantValues.Source, "ERROR", "ERROR", "Error Updating JobStatus To Running Import", job.JobId.ToString(), "ContactJob-JobID", null, null);
                }
                else
                {
                    if (TempContact.SaveParsedRecordXLS_Min(job.JobId) || uploaded == 0)
                    {
                        //Set Job Status to Finished.
                        if (string.IsNullOrEmpty(job.JobStarted.ToString()))
                            BulkUpload.UpdateJob(job.JobId, null, DateTime.UtcNow, (byte)ConstantValues.ContactJobStatus.COMPLETED, counter, uploaded, string.Empty, out feedback);

                        SendNotification(job);
                    }
                    else
                    {
                        //Set Error and Set Job Status to Finished.
                        if (string.IsNullOrEmpty(job.JobStarted.ToString()))
                            BulkUpload.UpdateJob(job.JobId, null, DateTime.UtcNow, (int)ConstantValues.ContactJobStatus.ERROROCCURED, counter, uploaded, "Error in saving parsed Records", out feedback);
                    }
                }
                
            }
            catch (Exception ex)
            {
                db.AddLog("Bulk Upload Service", "Error", string.Empty, ex.Message, job.JobId.ToString(), "Bulk Upload Job ID", null, null);
            }
        }

        


        private void SendNotification(ContactJob job)
        {
            Mail mail = new Mail();
            string sBody = string.Format("Dear {0}," +
                "<p>Your import from {1} has successfully finished.</p>" +
                "<p>&nbsp;</p>" +
                "<p>&nbsp;</p>" +
                "<p>Sincerely,</p>" +
                "<p>Marketing Automation Tool Team</p>",
               job.User.FirstName, job.OriginalFileName);

            string sSMTPserver = SysKey.GetKey("smtpclient");
            string sPort = SysKey.GetKey("port"); 
            string sUserName = SysKey.GetKey("smtpusername"); 
            string sPassword = SysKey.GetKey("smtppassword"); 
            string sFromAddress = SysKey.GetKey("fromaddress"); 

            if (mail.SendEmail(sSMTPserver, int.Parse(sPort), sUserName, sPassword, sFromAddress, job.User.EmailAddress, "Import Notification", true, sBody, System.Net.Mail.MailPriority.Normal))
            {
                
            }
            else
            {
                
            }
        }

        public void ResumeExcel(ContactJob job)
        {
            string feedback;
            if (TempContact.SaveParsedRecordXLS_Min(job.JobId))
            {
                 if (string.IsNullOrEmpty(job.JobFinished.ToString()))
                 {
                    BulkUpload.UpdateJob(job.JobId, null, DateTime.UtcNow, (byte)ConstantValues.ContactJobStatus.COMPLETED, 0, 0, string.Empty, out feedback);
                 }

                 SendNotification(job);
            }
            else
            {
                if (string.IsNullOrEmpty(job.JobStarted.ToString()))
                    BulkUpload.UpdateJob(job.JobId, null, DateTime.UtcNow, (int)ConstantValues.ContactJobStatus.ERROROCCURED, 0, 0, "Error in saving parsed Records", out feedback);
               
            }
        }

        private int CheckValidation(string[] Record)
        {
            string First_Name, Last_Name, Email, Mobile;

            int err_code = 0;

            First_Name = Record[0];
            Last_Name = Record[1];
            Email = Record[2];
            Mobile = Record[3];

            if (First_Name.Length > 100 || Last_Name.Length > 100)
            {
                err_code = (int)ConstantValues.Error_Code.Invalid_Name;
            }
            else if (Email.Length > 256)
            {
                err_code = (int)ConstantValues.Error_Code.Invalid_Email;
            }
            else if (Mobile.Length > 20)
            {
                err_code = (int)ConstantValues.Error_Code.Invalid_Mobile;
            }
            else if (!IsValidEmail(Email))
            {
                err_code = (int)ConstantValues.Error_Code.Invalid_Email;
            }
            return err_code;
        }

        private bool IsValidEmail(string email)
        {
            if (email != null)
            {

                bool res = Regex.IsMatch(email, MatchEmailPattern);
                if (res)
                {
                    try
                    {
                        var x = new System.Net.Mail.MailAddress(email);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
                else
                    return res;
            }
            else return false;
        }

        private void add_error(int Job_Id, int Rec_No, int Err_Code)
        {
            int new_id = 0;
            string feedback = string.Empty;
            string Msg = string.Empty;
            switch (Err_Code)
            {
                case (int)ConstantValues.Error_Code.File_Does_Not_Exist:
                    {
                        Msg = "File Does Not Exist";
                        break;
                    }
                case (int)ConstantValues.Error_Code.File_Can_Not_Open:
                    {
                        Msg = "File can not open";
                        break;
                    }
                case (int)ConstantValues.Error_Code.Invalid_Name:
                    {
                        Msg = "Invalid Name or Name too long";
                        break;
                    }
                case (int)ConstantValues.Error_Code.Invalid_Email:
                    {
                        Msg = "Invalid Email or Email too long.";
                        break;
                    }
                case (int)ConstantValues.Error_Code.Invalid_Mobile:
                    {
                        Msg = "Invalid Mobile or Mobile too long.";
                        break;
                    }
                case (int)ConstantValues.Error_Code.File_Too_Big:
                    {
                        Msg = "Invalid File Size: File is too big";
                        break;
                    }
                case (int)ConstantValues.Error_Code.Parser_Err:
                    {
                        Msg = "Error In File Parse";
                        break;
                    }
                case (int)ConstantValues.Error_Code.Email_Duplicate:
                    {
                        Msg = "Duplicate Email Address";
                        break;
                    }
            }
            BulkUploadException.CreateException(Job_Id, Rec_No, Err_Code, Msg, out feedback, out new_id);
        }

    }
}
