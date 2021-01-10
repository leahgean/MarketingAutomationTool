using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BaseEntity
    {
        public string ConnectionString { get; protected set; }
        DataClasses1DataContext _dataContext = null;
        public DataClasses1DataContext DataClasses1DataContext
        {
            get
            {
                return GetDataContext();
            }
            protected set 
            {
                _dataContext = value;
            }
        }

        //Workflow.WorkflowDataContext _workflowDataContext = null;
        //public Workflow.WorkflowDataContext WorkflowDataContext
        //{
        //    get
        //    {
        //        return GetWorkflowDataContext();
        //    }
        //    protected set
        //    {
        //        _workflowDataContext = value;
        //    }
        //}

        protected DAL.DataClasses1DataContext GetDataContext()
        {
            if (_dataContext == null)
                _dataContext = new DAL.DataClasses1DataContext(ConnectionString);
            return _dataContext;
        }

        //protected DAL.Workflow.WorkflowDataContext GetWorkflowDataContext()
        //{
        //    if (_workflowDataContext == null)
        //        _workflowDataContext = new DAL.Workflow.WorkflowDataContext(ConnectionString);
        //    return _workflowDataContext;
        //}

        public string GetUserName(string userID)
        {
            DAL.DataClasses1DataContext dc = GetDataContext();

            return dc.ExecuteQuery<string>("select UserName from USR_USER where UserId={0}", new Guid(userID)).SingleOrDefault();
        }
    }
}
