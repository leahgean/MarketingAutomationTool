using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketingAutomationTool.Utilities
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

        public static string ContactListJobFileValidExtension = "xlsx";

        //Table: CampaignEmailFormat
        public enum MessageBodyFormat
        {
            Text=0,
            HTML=1
        }

        public static string LeadsExportColumnsToExclude = "ContactID|AccountID|IsDeletedNum|RowNum";

        public enum ContactAction
        {
            SUBSCRIBE=1,
            UNSUBSCRIBE=2,
            UPDATECONTACTDETAILS=3,
            DELETE=4
        }

        //Table: CampaignType
        public enum CampaignType
        {
            Email=1
        }

        public enum CampaignStatus
        {
            Draft=0,
            Submitted=1
        }

        public enum SendingOptions
        {
            SendNow = 0,
            Scheduled = 1
        }


        public enum CampaignActiveStep
        {
            CampaignDetails = 0,
            Message = 1,
            Recipients=2,
            SendingOption=3
        }

        public enum LeadActiveList
        {
            Shortcuts=0,
            ViewRecentSearches=1,
            Lists=2
        }

        public enum EmailJobStatus
        {
            Pending = 1,
            Sending = 2,
            Completed = 3,
            ErrorOccurred = 4,
            Updating = 5
        }

    }
}