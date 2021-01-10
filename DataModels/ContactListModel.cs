using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public  class ContactListModel
    {
        public int ID;
        public Guid AccountID;
        public string ListName;
        public string ListDescription;
        public DateTime DateCreated;
        public Guid CreatedBy;
        public DateTime? ModifiedDate;
        public Guid? ModifiedBy;
    }
}
