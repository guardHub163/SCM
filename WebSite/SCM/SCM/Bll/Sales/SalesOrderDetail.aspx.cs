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
using log4net;
using System.Reflection;

namespace SCM.Web.Sales
{
    public partial class SalesOrderDetail : BaseModalDialogPage
    {
        string slipnumber = "";
        BSalesOrder bll = new BSalesOrder();
        DataSet ds = new DataSet();
        ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["code"] != null && Request.Params["code"].Trim() != "")
                {
                    slipnumber = Request.Params["code"].ToString();

                }

            }
            DataSet da = bll.GetSlipNumberInfo(slipnumber);
            foreach (DataRow row in da.Tables[0].Rows)
            {
                string[] AMOUTN = row["AMOUNT"].ToString().Split('.');
                this.lblAmount.Text = AMOUTN[0];
                this.lblpoints.Text = row["POINTS"].ToString();
                string[] QUANTITY = row["QUANTITY"].ToString().Split('.');
                this.lblQuantity.Text = QUANTITY[0];
            }
            int recordCount = bll.GetSalesOrderCount(getConduction());
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
            if (slipnumber != "")
            {
                sb.AppendFormat(" AND SLIP_NUMBER='{0}'", slipnumber);
            }
            return sb.ToString();
        }

        private void BindData()
        {
            string strWhere = getConduction();
            ds = bll.GetSalesOrderList(strWhere, "", (this.paging.CurrentPage - 1) * PageSize + 1, this.paging.CurrentPage * PageSize);
            for (int i = ds.Tables[0].Rows.Count; i < PageSize; i++)
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            }
            gridView.DataSource = ds;
            gridView.DataBind();
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
