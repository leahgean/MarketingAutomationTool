using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ContactSearchExport
    {
        public bool AddContactSearchExportFiles(int SearchID, Guid AccountID, Guid CreatedBy, string FileTimeStamp, string FileName)
        {
            DataAccessLayer.Controller.ContactSearchExport cContactSearchExport = new DataAccessLayer.Controller.ContactSearchExport();
            return cContactSearchExport.AddContactSearchExportFiles(SearchID,AccountID, CreatedBy,FileTimeStamp,FileName);
        }


        public string GetExportFile(int SearchID, Guid AccountID, string FileTimeStamp)
        {
            DataAccessLayer.Controller.ContactSearchExport cContactSearchExport = new DataAccessLayer.Controller.ContactSearchExport();
            return cContactSearchExport.GetExportFile(SearchID,AccountID,  FileTimeStamp);
        }
    }
}
