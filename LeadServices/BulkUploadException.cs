using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadServices
{
    public class BulkUploadException
    {
        public static bool CreateException(int Job_Id, int Rec_No, int err_code, string Desc, out string feedback, out int newID)
        {
            feedback = string.Empty;
            bool result = true;
            newID = -1;

            try
            {
                using (DataClasses1DataContext dc = new DataClasses1DataContext(ConstantValues.ConnectionString()))
                {
                    ContactJob_Exception jobE = new ContactJob_Exception();
                    jobE.JOB_ID = Job_Id;
                    jobE.REC_NO = Rec_No;
                    jobE.ERR_CODE = err_code.ToString();
                    jobE.DESCRIPTION = Desc;
                   

                    dc.ContactJob_Exceptions.InsertOnSubmit(jobE);
                    dc.SubmitChanges();

                    newID = jobE.JOB_ID;
                }
            }
            catch (Exception ex)
            {
                result = false;
                feedback = ex.Message;
            }
            return result;
        }
    }
}
