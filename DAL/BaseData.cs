using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enterprise.Library;

namespace DAL
{
    public abstract class BaseData<T> : BaseEntity where T : IEntity
    {
        //MGONZALES 20130808
        #region Properties
        public int RowCount { get; set; }
        #endregion 
        #region Constructors

        public BaseData(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public BaseData(DataClasses1DataContext dataContext)
        {
            this.DataClasses1DataContext = dataContext;
        }
        // MON 04 Oct 2013 added this part since every datacontext will use this part 
        //if this method will destroy the current implementation of BLL kindly remove this part
        public BaseData()
        {
            this.ConnectionString = ConnectionStrings.Default;
            if (this.DataClasses1DataContext == null)
            {
                this.DataClasses1DataContext = new DataClasses1DataContext(ConnectionStrings.Default);
            }
        }
        #endregion

        #region AbstractMethods

        public abstract MultipleResult<T> Select(int? accountID);

        public abstract SingleResult<T> SelectByID(int? accountID, string id);

        public abstract Result Delete(string id, int? accountID, Guid userId);

        #endregion

        #region Common Functions

        protected static bool IsDuplicate(DAL.DataClasses1DataContext db, string tableName, string fieldName,
            string fieldNameId, string value, string id)
        {
            string sql =
                "SELECT COUNT(" + fieldNameId + ") AS DuplicateCount " +
                  "FROM " + tableName +
                " WHERE " + fieldName + " = {0}" +
                  " AND " + fieldNameId + " <> {1}";

            var result = db.ExecuteQuery<DuplicateCheck>(sql, new object[] { value, id });

            List<DuplicateCheck> list = result.ToList();

            return (list[0].DuplicateCount > 0);
        }

        protected static bool IsDuplicate(DAL.DataClasses1DataContext db, string tableName, string fieldName,
            string fieldNameId, DateTime value, string id)
        {
            string sql =
                "SELECT COUNT(" + fieldNameId + ") AS DuplicateCount " +
                  "FROM " + tableName +
                " WHERE " + fieldName + " = {0}" +
                  " AND " + fieldNameId + " <> {1}";

            var result = db.ExecuteQuery<DuplicateCheck>(sql, new object[] { value, id });

            List<DuplicateCheck> list = result.ToList();

            return (list[0].DuplicateCount > 0);
        }

        #endregion Common Functions

    }
}
