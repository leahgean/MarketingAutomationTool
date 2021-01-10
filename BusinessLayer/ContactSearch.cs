using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;

namespace BusinessLayer
{
    public class ContactSearch
    {
        public bool AddContactSearch(Guid AccountId, Guid CreatedBy, string SearchJsonString, List<SearchFieldsItems> lstSearchFieldsItems, string SearchType, out int SearchId, out Guid SearchUID)
        {
            SearchId = 0;
            SearchUID = Guid.NewGuid();
            DataAccessLayer.Controller.ContactSearch cContactSearch = new DataAccessLayer.Controller.ContactSearch();
            return cContactSearch.AddContactSearch(AccountId, CreatedBy, SearchJsonString, lstSearchFieldsItems, SearchType, out SearchId, out SearchUID);
        }

        public bool UpdateContactSearch(Guid AccountId, Guid ModifiedBy, string SearchJsonString, List<SearchFieldsItems> lstSearchFieldsItems, int SearchId)
        {
            DataAccessLayer.Controller.ContactSearch cContactSearch = new DataAccessLayer.Controller.ContactSearch();
            return cContactSearch.UpdateContactSearch(AccountId, ModifiedBy, SearchJsonString,lstSearchFieldsItems,SearchId);
        }

        public bool DeleteContactSearchFields(int SearchId, Guid AccountId)
        {
            DataAccessLayer.Controller.ContactSearch cContactSearch = new DataAccessLayer.Controller.ContactSearch();
            return cContactSearch.DeleteContactSearchFields(SearchId, AccountId);
        }

        public bool AddContactSearchFields(int SearchId, Guid AccountId, Guid CreatedBy, string SearchKey, string SearchOperator, string SearchValue, string SearchLogicalOperator)
        {
            SearchId = 0;
            DataAccessLayer.Controller.ContactSearch cContactSearch = new DataAccessLayer.Controller.ContactSearch();
            return cContactSearch.AddContactSearchFields(SearchId,AccountId, CreatedBy, SearchKey,SearchOperator,SearchValue,SearchLogicalOperator);
        }

        public DataTable GetSearchResult(int SearchID, Guid AccountId, Guid CreatedBy, int MaxRows, string SortBy, string SortDirection, string ExcludeList, int PageNum, out int NewPageNum, out int MinItem, out int MaxItem, out int TotalRows, out decimal MaxPages)
        {
            DataAccessLayer.Controller.ContactSearch cContactSearch = new DataAccessLayer.Controller.ContactSearch();
            return cContactSearch.GetSearchResult(SearchID, AccountId, CreatedBy, MaxRows, SortBy, SortDirection, ExcludeList, PageNum, out NewPageNum, out MinItem, out MaxItem, out TotalRows, out MaxPages);
        }

       
        public DataTable GetExcludeList(int SearchID, Guid AccountId, Guid CreatedBy, int MaxRows, string SortBy, string SortDirection, string ExcludeList, int PageNum, out int NewPageNum, out int MinItem, out int MaxItem, out int TotalRows, out decimal MaxPages)
        {
            DataAccessLayer.Controller.ContactSearch cContactSearch = new DataAccessLayer.Controller.ContactSearch();
            return cContactSearch.GetExcludeList(SearchID, AccountId, CreatedBy,  MaxRows, SortBy, SortDirection, ExcludeList, PageNum, out NewPageNum,out MinItem, out MaxItem, out TotalRows, out MaxPages);
        }

        public string GetSearchCriteriaDisplay(int SearchID, Guid AccountId, Guid UserId)
        {
            DataAccessLayer.Controller.ContactSearch cContactSearch = new DataAccessLayer.Controller.ContactSearch();
            return cContactSearch.GetSearchCriteriaDisplay(SearchID,AccountId, UserId);
        }

        public string GetSearchJsonString(int SearchID, Guid AccountId, Guid UserId)
        {
            DataAccessLayer.Controller.ContactSearch cContactSearch = new DataAccessLayer.Controller.ContactSearch();
            return cContactSearch.GetSearchJsonString(SearchID, AccountId, UserId);
        }

        public DataTable ViewRecentSearches(Guid AccountId, Guid CreatedBy)
        {
            DataAccessLayer.Controller.ContactSearch cContactSearch = new DataAccessLayer.Controller.ContactSearch();
            return cContactSearch.ViewRecentSearches(AccountId, CreatedBy);
        }

        public string GetSearchID(Guid SearchUID, Guid AccountID, Guid CreatedBy)
        {
            DataAccessLayer.Controller.ContactSearch cContactSearch = new DataAccessLayer.Controller.ContactSearch();
            return cContactSearch.GetSearchID(SearchUID, AccountID, CreatedBy);
        }

        public string GetSearchUID(int SearchID, Guid AccountID, Guid UserId)
        {
            DataAccessLayer.Controller.ContactSearch cContactSearch = new DataAccessLayer.Controller.ContactSearch();
            return cContactSearch.GetSearchUID(SearchID, AccountID, UserId);
        }


        public string GetSearchUIDByCampaignUID(Guid CampaignUID, Guid AccountID, Guid UserId)
        {
            DataAccessLayer.Controller.ContactSearch cContactSearch = new DataAccessLayer.Controller.ContactSearch();
            return cContactSearch.GetSearchUIDByCampaignUID(CampaignUID, AccountID, UserId);
        }

        public string GetSearchID_BySearchType(Guid CreatedBy, Guid AccountID, string SearchType)
        {
            DataAccessLayer.Controller.ContactSearch cContactSearch = new DataAccessLayer.Controller.ContactSearch();
            return cContactSearch.GetSearchID_BySearchType(CreatedBy, AccountID, SearchType);
        }

    }
}
