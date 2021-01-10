using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class ContactJob
    {
        public int JobId;
        public string JobName;
        public int JobStatusId;
        public Guid AccountID;
        public Guid CreatedBy;
        public string FileFormat;
        public string OriginalFileName;
        public string FileName;
        public int? ContactListId;
        public DateTime DateCreated;
        public DateTime? JobStarted;
        public DateTime? JobFinished;
        public string Error;
        public int? TotalContacts;
        public int? UploadedContacts;
        public int? CurrentRowParsedInExcel;
    }
}
