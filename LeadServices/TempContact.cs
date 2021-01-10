using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadServices
{
    public class TempContact
    {
        public static bool IsUniqueEmail(Guid Account_Id, string strEmail)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext(ConstantValues.ConnectionString());
            string ans = dc.isUniqueEmail(Account_Id, strEmail);
            if (ans == "0")
                return false;
            else
                return true;
        }

        public static int? GetCountryId( string countryName)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext(ConstantValues.ConnectionString());
            int? countryid = dc.fn_Country_GetCountryId(countryName);
            return countryid;
        }

        static public bool Create(
            int Job_Id,
            Guid account_id,
            string first_name,
            string last_name,
            string email,
            string mobile,
            string title,
            bool subscribedtoemail,
            int? contacttype,
            int? contactstatus,
            int? leadstatus,
            char gender,
            string company_name,
            string website,
            string position_title,
            string phone_no,
            string address1,
            string address2,
            string city,
            string state,
            int? countryid,
            string postalcode,
            string subscribedtoemailvia,
            Guid createdby,
            string subscribedtoipaddress,
            int? formid,
            int? contactlistid,
            int? filterid,
            out string feedback,
            out long newID)
        {

            feedback = string.Empty;
            bool result = true;
            newID = -1;

            try
            {
                using (DataClasses1DataContext dc = new DataClasses1DataContext(ConstantValues.ConnectionString()))
                {
                     dc.SaveRecordToTempXLS(
                        Job_Id,
                        account_id,
                        first_name,
                        last_name,
                        email,
                        mobile,
                        title,
                        subscribedtoemail,
                        contacttype,
                        contactstatus,
                        leadstatus,
                        gender,
                        company_name,
                        website,
                        position_title,
                        phone_no,
                        address1,
                        address2,
                        city,
                        state,
                        countryid,
                        postalcode,
                        subscribedtoemailvia,
                        createdby,
                        subscribedtoipaddress,
                        formid,
                        contactlistid,
                        filterid
                        );
                    
                }
            }
            catch (Exception ex)
            {
                result = false;
                feedback = ex.Message;
            }
            return result;
        }

        static public bool SaveParsedRecordXLS_Min(int Job_Id)
        {
            try
            {
                using (DataClasses1DataContext dc = new DataClasses1DataContext(ConstantValues.ConnectionString()))
                {
                    foreach (var item in dc.S_CON_GET_TEMP_CONTACT(Job_Id))
                    {
                        dc.S_CON_ADD_RECORDS_PARSER_XLS_MIN(Job_Id, item.ID);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
