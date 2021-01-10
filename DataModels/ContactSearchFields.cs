using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class ContactSearchFields
    {
        public int Id;
        public int SearchId;
        public Guid AccountId;
        public Guid CreatedBy;
        public DateTime CreatedDate;
        public string SearchKey;
        public string SearchOperator;
        public string SearchValue;
        public string SearchLogicalOperator;
    }
}
