using System;
using System.Collections;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SCM.Bll;
using System.Drawing;
using System.Text;
using SCM.Web;
using SCM.Model;
using SCM.Common;
using System.Reflection;
using log4net;

namespace SCM.Web.VipCustomer
{
    public partial class LaseCustomeLog : BaseModalDialogPage
    {
        BSalesOrder bll = new BSalesOrder();
        BVipCustomer bvip = new BVipCustomer();
        DataSet ds = new DataSet();
        int PageSize = 16;
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {
                if (Request.Params["code"] != null && Request.Params["code"].Trim() != "")
                {
                    this.txtCode.Text = Request.Params["code"].ToString();
                    BaseVipCustomerTable bviptable = bvip.GetModel(txtCode.Text);
                    this.txtName.Text = bviptable.NAME;
                }
            }

            this.paging.PageChanged += new PageControl.PageChangedEventHandler(PageChanged);   

            int recordCount = bll.GetLastSalesOrderCount(getConduction());
            if (recordCount > 0)
            {
                panelPage.Visible = true;
            }
            else
            {
                panelPage.Visible = false;
            }
            //将每页显示的数量保存在用户控件
            this.paging.PageSize = PageSize;
            //将数据总条数保存在用户控件
            this.paging.RecorderCount = recordCount;
            BindData();
        }

        private string getConduction()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" 1=1");
            if (this.txtCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND CUSTOMER_CODE = '{0}'", txtCode.Text.Trim());
            }
            return sb.ToString();
        }

        private void BindData()
        {
            string strWhere = getConduction();
            ds = bll.GetLastSalesOrderList(strWhere, "", (this.paging.CurrentPage - 1) * PageSize + 1, this.paging.CurrentPage * PageSize);
            for (int i = ds.Tables[0].Rows.Count; i < PageSize; i++)
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            }
            gridView.DataSource = ds;
            gridView.DataBind();
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            return true;
        }


        protected void PageChanged(object sender, int e)
        {
            BindData();
        }
    }
}
