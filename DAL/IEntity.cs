using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IEntity
    {
        int? ACCOUNT_ID { get; set; }
        Guid? USER_ID { get; set; }

        Guid? DELETE_USER_ID { get; set; }
        Guid? UPDATE_USER_ID { get; set; }

        DateTime DATE_CREATED { get; set; }
        DateTime? DATE_UPDATED { get; set; }
        DateTime? DATE_DELETED { get; set; }

        Binary VERSION { get; set; }
    }
}
