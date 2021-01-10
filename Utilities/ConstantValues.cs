using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class ConstantValues
    {
        public enum ContactJobStatus
        {
            PENDING = 100,
            PARSINGEXCEL = 101,
            RUNNINGIMPORT = 102,
            COMPLETED = 103,
            CANCELLED = 104,
            CORRUPTED_FILE = 105,
            ERROROCCURED = 106
        }
    }
}
