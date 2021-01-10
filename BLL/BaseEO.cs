using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using Enterprise.Library;
using DAL;

namespace BLL
{
    [Serializable()]
    abstract public class BaseEO : BaseBO
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseEO()
            : base()
        {
            //Default the status to New.
            BOStatus = BOStatusEnum.IsNew;
        }

        #endregion Constructor

        #region Properties

        #region ID
        string _id { get; set; }
        public string ID
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
                ApplyIdToChilds();
            }
        }
        public bool HasID
        {
            get
            {
                return !string.IsNullOrEmpty(this.ID);
            }
        }

        public int ID_AsInteger
        {
            get
            {
                return int.Parse(this.ID);
            }
        }

        public long ID_AsLong
        {
            get
            {
                return long.Parse(this.ID);
            }
        }

        public Guid ID_AsGuid
        {
            get
            {
                return new Guid(this.ID);
            }
        }

        public byte ID_AsByte
        {
            get
            {
                return byte.Parse(this.ID);
            }
        }

        #endregion

        #region AccountID

        string _accountID = null;
        public virtual string AccountID
        {
            get
            {
                return _accountID;
            }
            set
            {
                _accountID = value;
                ApplyAccountIdToChilds();
            }
        }
        public bool HasAccountID
        {
            get
            {
                return !string.IsNullOrEmpty(this.AccountID);
            }
        }

        public int? AccountID_AsInteger
        {
            get
            {
                if (string.IsNullOrEmpty(this.AccountID)) return null;
                return int.Parse(this.AccountID);
            }
        }

        #endregion

        public string UserID { get; set; }

        public string DeleteUserID = string.Empty;
        public string UpdateUserID = string.Empty;

        public DateTime? DateCreated { get; set; }
        public string DateCreated_Formatted { get { return FromatDate(DateCreated); } }

        public DateTime? DateUpdated { get; set; }
        public string DateUpdated_Formatted { get { return FromatDate(DateUpdated); } }

        public DateTime? DateDeleted { get; set; }
        public string DateDeleted_Formatted { get { return FromatDate(DateDeleted); } }

        public Binary Version;

        public string CreatedBy
        {
            get
            {
                return GetUserName(UserID);
            }
        }

        public string DeletedBy
        {
            get
            {
                return GetUserName(DeleteUserID);
            }
        }

        public string UpdatedBy
        {
            get
            {
                return GetUserName(UpdateUserID);
            }
        }

        #endregion Properties

        #region Abstract Methods

        protected abstract Result LoadCustomProperties(IEntity entity);

        protected abstract DAL.BaseEntity GetDataObject();

        /// <summary>
        /// Apply account id to child objects which require this paramter
        /// </summary>
        protected abstract Result ApplyAccountIdToChilds();

        /// <summary>
        /// Apply ID to child objects which require this paramter
        /// </summary>
        protected abstract Result ApplyIdToChilds();

        /// <summary>
        /// Get the record from the database and load the object's properties
        /// </summary>
        /// <returns>Returns true if the record is found.</returns>
        protected abstract Result LoadImp(int? accountID, string entityID);

        /// <summary>
        /// This method will add or update a record.
        /// </summary>
        protected abstract Result SaveImp(DAL.DataClasses1DataContext dataContext, int accountID, Guid? userId);

        /// <summary>
        /// This method validates the object's data before trying to save the record.  If there is a validation error
        /// the validationErrors will be populated with the error message.
        /// </summary>
        public abstract void Validate(ref Result result);

        /// <summary>
        /// This should call the business object's data class to delete the record.  The only method that should call this 
        /// is the virtual method "Delete(SqlTransaction tn, ref ValidationErrorAL validationErrors, int id)" in this class.
        /// </summary>
        protected abstract Result DeleteImp(DAL.DataClasses1DataContext dataContext, Guid? userId);

        /// <summary>
        /// This method validates the object's data before trying to delete the record.  If there is a validation error
        /// the validationErrors will be populated with the error message.
        /// </summary>
        protected abstract void ValidateDelete(ref Result result);

        /// <summary>
        /// This will load the object with the default properties.
        /// </summary>
        public abstract Result Init();

        /// <summary>
        /// This will initialize the object as a new object
        /// </summary>
        protected abstract Result CreateNewImp();

        #endregion Abstract Methods

        #region Public Methods

        /// <summary>
        /// This method will load all the properties of the object from the entity.
        /// </summary> 
        internal Result LoadProperties(IEntity entity)
        {
            Result result = new Result();
            if (entity != null)
            {
                if (entity.USER_ID.HasValue)
                    this.UserID = entity.USER_ID.Value.ToString();

                if (entity.ACCOUNT_ID.HasValue)
                    this.AccountID = entity.ACCOUNT_ID.Value.ToString();

                this.DateCreated = entity.DATE_CREATED;
                this.DateUpdated = entity.DATE_UPDATED;
                this.DateDeleted = entity.DATE_DELETED;

                this.DeleteUserID = entity.DELETE_USER_ID.ToString();
                this.UpdateUserID = entity.UPDATE_USER_ID.ToString();

                this.Version = entity.VERSION;

                result = this.LoadCustomProperties(entity);

                if (!result.Successful) return result;

                this.BOStatus = BOStatusEnum.IsSaved;
            }
            return result;
        }

        public void ApplyId_AccountId_ToChilds()
        {
            ApplyIdToChilds();
            ApplyAccountIdToChilds();
        }

        public Result CreateNew()
        {
            return CreateNewImp();
        }

        public Result ReLoad()
        {
            return Load(this.ID);
        }

        public Result Load(string entityID)
        {
            Result result = LoadImp(this.AccountID_AsInteger, entityID);
            if (result.Successful)
                this.BOStatus = BOStatusEnum.IsSaved;
            return result;
        }

        public Result Delete(Guid? userId)
        {
            return Delete(null, userId);
        }

        public Result Delete(DAL.DataClasses1DataContext dataContext, Guid? userId)
        {
            Result result = new Result();
            ValidateDelete(ref result);

            if (BOStatus == BOStatusEnum.IsNew)
            {
                BOStatus = BOStatusEnum.IsDeleted;
                return result;
            }

            BOStatus = BOStatusEnum.IsDeleted;
            if (result.Successful)
            {
                result = DeleteImp(dataContext, userId);
            }
            return result;
        }

        public Result Save(int accountID, Guid? userId)
        {
            return Save(null, accountID, userId);
        }

        public Result Save(DAL.DataClasses1DataContext dataContext, int accountID, Guid? userId)
        {
            Result result = new Result();
            Validate(ref result);
            if (result.Successful)
            {
                result = SaveImp(dataContext, accountID, userId);

                if (result.Successful)
                {
                    BOStatus = BOStatusEnum.IsSaved;
                }
            }
            return result;
        }

        public bool CheckDuplicateByName(string value, string fieldName, string tableName, string ID_Name, string ID_Value)
        {
            if (string.IsNullOrEmpty(ID_Value)) { ID_Value = "-1"; }

            string cmd = string.Format("if exists(select 1 from {0} where {1} = '{2}' and {3} <> {4}) select 1 else select 0; ", tableName, fieldName, value, ID_Name, ID_Value);
            int res = GetDataObject().DataContext.ExecuteQuery<int>(cmd).Single();
            return res == 1;
        }

        public int GetCount(string fieldName, string tableName)
        {
            string cmd = string.Format("select IsNull(Count({0}),0) from {1}", fieldName, tableName);
            int res = GetDataObject().DataContext.ExecuteQuery<int>(cmd).Single();
            return res;
        }

        #endregion Public Methods

        #region Private Methods
        private string GetUserName(string userID)
        {
            if (string.IsNullOrEmpty(userID)) return string.Empty;

            return GetDataObject().GetUserName(userID);
        }
        private string FromatDate(DateTime? dt)
        {
            if (!dt.HasValue) return string.Empty;

            return dt.Value.ToString("dd/MM/yyyy");
        }

        #endregion
    }
}
