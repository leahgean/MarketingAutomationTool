using System;

namespace DataModels
{
    public class AccountStatusHistory
    {
        public int Id;
        public Guid AccountID;
        public string Status;
        public DateTime DateChanged;
        public string IP;
        public Guid ChangedBy;
    }
}
