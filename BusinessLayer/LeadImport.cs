using System;
using System.Data;
using DataModels;

namespace BusinessLayer
{
    public class LeadImport
    {
        public DataTable GetExistingImports(Guid AccountID, int PageNum, int PageSize, string SortColumn, string SortOrder, out int MinItem, out int MaxItem, out int TotalRows)
        {
            DataAccessLayer.Controller.LeadImport cLead = new DataAccessLayer.Controller.LeadImport();
            return cLead.GetExistingImports(AccountID, PageNum, PageSize, SortColumn, SortOrder, out MinItem, out MaxItem, out TotalRows);
        }

        public ContactJob GetContactJob(int JobId, Guid AccountId)
        {
            DataAccessLayer.Controller.LeadImport cLead = new DataAccessLayer.Controller.LeadImport();
            return cLead.GetContactJob(JobId, AccountId);
        }
    }
}
