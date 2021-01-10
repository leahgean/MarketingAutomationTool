using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ContactList
    {
        public bool UniqueListName(string ListName, Guid AccountId, int? ContactListId)
        {
            DataAccessLayer.Controller.ContactList objLead = new DataAccessLayer.Controller.ContactList();
            return objLead.UniqueListName(ListName, AccountId, ContactListId);
        }

        public bool CreateContactList(Guid AccountID,
                string ListName,
                string ListDescription,
                Guid CreatedBy,
                 out int ContactListId)
        {
            DataAccessLayer.Controller.ContactList cContactList = new DataAccessLayer.Controller.ContactList();
            return cContactList.CreateContactList(AccountID, ListName, ListDescription, CreatedBy, out ContactListId);
        }

        public bool UpdateContactList(Guid AccountID,
                string ListName,
                string ListDescription,
                Guid CreatedBy,
                 int ContactListId)
        {
            DataAccessLayer.Controller.ContactList cContactList = new DataAccessLayer.Controller.ContactList();
            return cContactList.UpdateContactList(AccountID, ListName, ListDescription, CreatedBy,  ContactListId);
        }

        public bool DeleteContactList(Guid AccountID,
                                  Guid ModifiedBy,
                                  int ContactListId)
        {
            DataAccessLayer.Controller.ContactList cContactList = new DataAccessLayer.Controller.ContactList();
            return cContactList.DeleteContactList(AccountID,ModifiedBy, ContactListId);
        }

        public DataModels.ContactListModel SelectContactList(Guid AccountID, int Id)
        {
            DataAccessLayer.Controller.ContactList cContactList = new DataAccessLayer.Controller.ContactList();
            return cContactList.SelectContactList(AccountID, Id);
        }

        public DataTable GetLists(Guid AccountID, bool isDeleted, int PageSize, string SortBy, string SortDirection, int PageNum, out int NewPageNum, out int MinItem, out int MaxItem, out int TotalRows, out decimal MaxPages)
        {
            DataAccessLayer.Controller.ContactList cContactList = new DataAccessLayer.Controller.ContactList();
            return cContactList.GetLists(AccountID, isDeleted, PageSize, SortBy, SortDirection, PageNum, out NewPageNum, out MinItem, out MaxItem, out TotalRows, out MaxPages);
        }

        public DataTable GetListNames(Guid AccountID, bool isDeleted)
        {
            DataAccessLayer.Controller.ContactList cContactList = new DataAccessLayer.Controller.ContactList();
            return cContactList.GetListNames(AccountID, isDeleted);
        }

        public bool CreateContactListJob(string JobName,
            int JobStatusId,
            Guid AccountID,
            Guid CreatedBy,
            string FileFormat,
            string OriginalFileName,
            string FileName,
            int? ContactListId,
            string IPAddress)
        {
            DataAccessLayer.Controller.ContactList cContactList = new DataAccessLayer.Controller.ContactList();
            return cContactList.CreateContactListJob(JobName, JobStatusId, AccountID, CreatedBy, FileFormat, OriginalFileName, FileName, ContactListId,IPAddress);
        }

        public bool AddMemberToList(int SearchID,
                                   Guid AccountID,
                                   Guid CreatedBy,
                                   string ExcludeList,
                                   int ContactListId)
        {
            DataAccessLayer.Controller.ContactList cContactList = new DataAccessLayer.Controller.ContactList();
            return cContactList.AddMemberToList(SearchID, AccountID, CreatedBy, ExcludeList, ContactListId);
        }


        public DataTable GetListMembers(int ContactListID, Guid AccountID, Guid CreatedBy, int MaxRows, string SortBy, string SortDirection, string ExcludeList, int PageNum, out int NewPageNum, out int MinItem, out int MaxItem, out int TotalRows, out decimal MaxPages)
        {
            DataAccessLayer.Controller.ContactList cContactList = new DataAccessLayer.Controller.ContactList();
            return cContactList.GetListMembers(ContactListID, AccountID, CreatedBy, MaxRows, SortBy, SortDirection, ExcludeList, PageNum, out NewPageNum, out MinItem, out MaxItem, out TotalRows, out MaxPages);
        }

        public DataTable GetRemovedListMembers(int ContactListID, Guid AccountID, Guid CreatedBy, int MaxRows, string SortBy, string SortDirection, string RemoveList, int PageNum, out int NewPageNum, out int MinItem, out int MaxItem, out int TotalRows, out decimal MaxPages)
        {
            DataAccessLayer.Controller.ContactList cContactList = new DataAccessLayer.Controller.ContactList();
            return cContactList.GetRemovedListMembers(ContactListID, AccountID, CreatedBy, MaxRows, SortBy, SortDirection, RemoveList, PageNum, out NewPageNum, out MinItem, out MaxItem, out TotalRows, out MaxPages);
        }

        public bool ModifyMemberList(int ContactListId,
                                   Guid AccountID,
                                   Guid UserId,
                                   string RemoveList)
        {
            DataAccessLayer.Controller.ContactList cContactList = new DataAccessLayer.Controller.ContactList();
            return cContactList.ModifyMemberList(ContactListId, AccountID, UserId, RemoveList);
        }
    }
}
