using System;
using System.Web.UI;

namespace MarketingAutomationTool.UserControls
{
    public partial class Pager : System.Web.UI.UserControl
    {
        private System.Delegate _delPageMethod;
        public Delegate CallingPageMethod
        {
            set { _delPageMethod = value; }
        }

        public int TotalRows
        {
            get
            {
                if (ViewState["TotalRows"] != null)
                {
                    return Convert.ToInt32(ViewState["TotalRows"].ToString());
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                ViewState["TotalRows"] = value;
            }

        }

        public int MaxPages
        {
            get
            {
                if (ViewState["MaxPages"] != null)
                {
                    return Convert.ToInt32(ViewState["MaxPages"].ToString());
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                ViewState["MaxPages"] = value;
            }

        }

        public int MinItem
        {
            get
            {
                if (ViewState["MinItem"] != null)
                {
                    return Convert.ToInt32(ViewState["MinItem"].ToString());
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                ViewState["MinItem"] = value;
            }

        }

        public int MaxItem
        {
            get
            {
                if (ViewState["MaxItem"] != null)
                {
                    return Convert.ToInt32(ViewState["MaxItem"].ToString());
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                ViewState["MaxItem"] = value;
            }

        }

        public int PageNum
        {
            get
            {
                if  (ViewState["PageNum"] != null)
                {
                    return Convert.ToInt32(ViewState["PageNum"].ToString());
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                ViewState["PageNum"] = value;
                txtPageNum.Text = value.ToString();
            }
        }

       


        public int CurMaxRows
        {
            get
            {
                   return  Convert.ToInt32(ddlMaxRows.SelectedItem.Value);
            }
        }

        public int PrevMaxRows
        {
            get
            {
                if (ViewState["PrevMaxRows"] != null)
                {
                    return Convert.ToInt32(ViewState["PrevMaxRows"].ToString());
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                ViewState["PrevMaxRows"] = value;
            }
        }

        public string SortBy
        {
            get
            {
                if (ViewState["SortBy"] != null)
                {
                    return (ViewState["SortBy"].ToString());
                }
                else
                {
                    return "Id";
                }
            }
            set
            {
                ViewState["SortBy"] = value;
            }
        }

        public string SortDirection
        {
            get
            {
                if (ViewState["SortDirection"] != null)
                {
                    return (ViewState["SortDirection"].ToString());
                }
                else
                {
                    return "ASC";
                }
            }
            set
            {
                ViewState["SortDirection"] = value;
            }
        }

        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtPageNum.Text = PageNum.ToString();
                PrevMaxRows = CurMaxRows;
            }

        }

        private void CallFunction()
        {
            object[] obj = new object[1];
            obj[0] = txtPageNum.Text as object;
            _delPageMethod.DynamicInvoke(obj);
        }

        protected void btnFirst_Click(object sender, EventArgs e)
        {
            PageNum = 1;
            txtPageNum.Text = PageNum.ToString();
            CallFunction();
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (PageNum > 1) PageNum = PageNum - 1;
            txtPageNum.Text = PageNum.ToString();
            CallFunction();
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (PageNum < MaxPages) PageNum = PageNum + 1;
            txtPageNum.Text = PageNum.ToString();
            CallFunction();
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            PageNum = MaxPages;
            txtPageNum.Text = PageNum.ToString();
            CallFunction();
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            if (CurMaxRows != PrevMaxRows)
            {
                PageNum = 1;
                txtPageNum.Text = PageNum.ToString(); //reset page num to 1
                PrevMaxRows = CurMaxRows; //track new maxrows
            }
            
            if (Convert.ToInt32(txtPageNum.Text) >= 1 && Convert.ToInt32(txtPageNum.Text) <= Convert.ToInt32(MaxPages))
                {
                    PageNum = Convert.ToInt32(txtPageNum.Text);
                    CallFunction();
                }
                else
                {
                    if (Convert.ToInt32(txtPageNum.Text) < 1) txtPageNum.Text = "1";
                    if (Convert.ToInt32(txtPageNum.Text) > MaxPages) txtPageNum.Text = MaxPages.ToString();
                }
            
            
        }
    }
}