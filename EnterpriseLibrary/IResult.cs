using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseLibrary
{
    public interface IResult
    {
        bool Success { get; set; }
        string ErrorMessage { get; set; }
        Exception ExceptionObj { get; set; }
    }
}
