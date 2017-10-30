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
using System;
using System.Reflection;
using log4net;

namespace SCM.Web.Stock
{

    public partial class StockDetails : BaseModalDialogPage
    {
        BStock bll = new BStock();
        decimal Stock = 0;
        ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            ValidateRole(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            if (!Page.IsPostBack)
            {
                if (Request.Params["WAREHOUSE_CODE"] != null && Request.Params["WAREHOUSE_CODE"].Trim() != "")
                {
                    this.txtWarehouseCode.Text = Request.Params["WAREHOUSE_CODE"].ToString();
                    this.txtProductCode.Text = Request.Params["PRODUCT_CODE"].ToString();
                    showInfo(txtWarehouseCode.Text, txtProductCode.Text);
                }
                DateBand();
            }
        }

        private void showInfo(string warehouse, string product)
        {
            BllStockTable bst = bll.GetModel(warehouse, product);
            this.txtProductCode.Text = bst.PRODUCT_CODE;
            this.lblProductName.Text = bst.PRODUCT_NAME;
            this.lblWarehouseName.Text = bst.WAREHOUSE_NAME;
            this.txtColorCode.Text = bst.COLOR_CODE;
            this.lblColorName.Text = bst.COLOR_NAME;
            this.txtSizeCode.Text = bst.SIZE_CODE;
            this.lblSizeName.Text = bst.SIZE_NAME;
            this.txtStyleCode.Text = bst.STYLE_CODE;
            this.lblStyleName.Text = bst.STYLE_NAME;
            Stock = bst.QUANTITY;
        }

        private void DateBand()
        {

            DataTable dt = new DataTable();
            DataSet ds = bll.Show(txtWarehouseCode.Text, txtProductCode.Text);
            dt.Columns.Add("OPT_DATE", Type.GetType("System.DateTime"));
            dt.Columns.Add("TYPE", Type.GetType("System.String"));
            dt.Columns.Add("NAME", Type.GetType("System.String"));
            dt.Columns.Add("QUANTITY", Type.GetType("System.Decimal"));
            dt.Columns.Add("OUTNUMBER", Type.GetType("System.Decimal"));
            dt.Columns.Add("ENTERNUMBER", Type.GetType("System.Decimal"));
            decimal stock = Stock;
            DataRow row = dt.NewRow();
            row["OPT_DATE"] = DateTime.Now.ToString("yyyy/MM/dd");
            row["TYPE"] = "当前库存";
            row["QUANTITY"] = stock;
            dt.Rows.Add(row);
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                row = dt.NewRow();
                row["OPT_DATE"] = item["OPT_DATE"];
                if (item["TYPE"] != null && item["TYPE"].ToString() == "2")
                {
                    row["TYPE"] = "出库预定";
                    row["OUTNUMBER"] = item["QUANTITY"];
                    stock = stock - Convert.ToDecimal(item["QUANTITY"]);
                }
                else
                {
                    row["TYPE"] = "入库预定";
                    row["ENTERNUMBER"] = item["QUANTITY"];
                    stock = stock + Convert.ToDecimal(item["QUANTITY"]);
                }
                row["QUANTITY"] = stock;
                row["NAME"] = item["NAME"];
                dt.Rows.Add(row);
            }
            for (int i = dt.Rows.Count; i < PageSize; i++)
            {
                dt.Rows.Add(dt.NewRow());
            }
            this.gridView.DataSource = dt;
            this.gridView.DataBind();
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
