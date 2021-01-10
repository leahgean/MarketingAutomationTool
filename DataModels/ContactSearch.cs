using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class ContactSearch
    {
        public string Name;
        public int Id;
        public Guid SearchUID;
        public Guid AccountId;
        public Guid CreatedBy;
        public DateTime CreatedDate;
        public Guid? ModifiedBy;
        public DateTime? ModifiedDate;
    }
}
