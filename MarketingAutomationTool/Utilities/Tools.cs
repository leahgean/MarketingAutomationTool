using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketingAutomationTool.Utilities
{
    public static class Tools
    {
        public static string PadLeadID(string pLeadID)
        {
            if (pLeadID.Length < 6)
            {
                switch (pLeadID.Length)
                {
                    case 1:
                        pLeadID = "00000" + pLeadID.ToString();
                        break;
                    case 2:
                        pLeadID = "0000" + pLeadID.ToString();
                        break;
                    case 3:
                        pLeadID = "000" + pLeadID.ToString();
                        break;
                    case 4:
                        pLeadID = "00" + pLeadID.ToString();
                        break;
                    case 5:
                        pLeadID = "0" + pLeadID.ToString();
                        break;

                }
            }

            return pLeadID;
        }
    }
}