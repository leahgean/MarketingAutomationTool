using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace BusinessLayer
{
    public class LeadReport
    {
        public DataTable NewLeadsStatistics(DateTime? FromDate, DateTime? ToDate, string Source, int? TypeID, int? StatusID, Guid AccountID, out int iTotalLeadCount)
        {
            DataAccessLayer.LeadReport oLR = new DataAccessLayer.LeadReport();
            return oLR.NewLeadsStatistics(FromDate, ToDate, Source, TypeID, StatusID, AccountID, out iTotalLeadCount);
        }


        public DataTable LeadsListing(DateTime? FromDate, DateTime? ToDate, string Source, int? TypeID, int? StatusID, Guid AccountID, out int iTotalLeadCnt)
        {
            DataAccessLayer.LeadReport oLR = new DataAccessLayer.LeadReport();
            return oLR.LeadsListing(FromDate, ToDate, Source, TypeID, StatusID, AccountID, out iTotalLeadCnt);
        }

        public DataTable DeletedLeadsListing(DateTime? FromDate, DateTime? ToDate, int? TypeID, int? StatusID, Guid AccountID, out int iTotalLeadCnt)
        {
            DataAccessLayer.LeadReport oLR = new DataAccessLayer.LeadReport();
            return oLR.DeletedLeadsListing(FromDate, ToDate, TypeID, StatusID, AccountID, out iTotalLeadCnt);
        }

        public DataTable UnsubscribedLeadsListing(DateTime? FromDate, DateTime? ToDate, string Source, int? TypeID, int? StatusID, Guid AccountID, out int iTotalLeadCnt)
        {
            DataAccessLayer.LeadReport oLR = new DataAccessLayer.LeadReport();
            return oLR.UnsubscribedLeadsListing(FromDate, ToDate, Source, TypeID, StatusID, AccountID, out iTotalLeadCnt);
        }

        public DataTable DuplicateLeadsListing(Guid AccountID, out int iTotalLeadCnt)
        {
            DataAccessLayer.LeadReport oLR = new DataAccessLayer.LeadReport();
            return oLR.DuplicateLeadsListing(AccountID, out iTotalLeadCnt);
        }
    }
}
