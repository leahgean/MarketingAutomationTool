using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class ContactSearchExport
    {
        public int SearchID;
        public Guid AccountID;
        public Guid CreatedBy;
        public DateTime CreatedDate;
        public Guid ModifiedBy;
        public DateTime ModifiedDate;
        public string FileTimeStamp;
        public string FileName;
    }
}
