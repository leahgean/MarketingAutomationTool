using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseLibrary
{
    public class Result : IResult
    {
        public Result()
        {

        }

        public Result(bool success, string errormessage, Exception ex)
        {
            this.Success = success;
            this.ErrorMessage = errormessage;
            this.ExceptionObj = ex;

        }

        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public Exception ExceptionObj { get; set; }


    }
}
