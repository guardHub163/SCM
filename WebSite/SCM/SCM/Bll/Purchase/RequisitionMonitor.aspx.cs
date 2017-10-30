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
using System.Collections.Generic;
using log4net;
using System.Reflection;

namespace SCM.Web.Purchase
{
    public partial class RequisitionMonitor : BaseModalDialogPage
    {
        BPurchaseRequisition bll = new BPurchaseRequisition();
        DataSet ds = new DataSet();
        ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["SN"] != null && Request.QueryString["SN"] != "")
                {

                    Show(Request.QueryString["SN"]);
                }
            }
        }

        private void Show(string slipNumber)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("尺码", Type.GetType("System.String"));
            for (int i = 1; i <= 7; i++)
            {
                dt.Rows.Add(dt.NewRow());
            }
            dt.Rows[0][0] = "店面现有库存数";
            dt.Rows[1][0] = "预计到货前销售";
            dt.Rows[2][0] = "到货后同期销售量";
            dt.Rows[3][0] = "本次申请数";
            dt.Rows[4][0] = "到货后库存数";
            dt.Rows[5][0] = "申请后各码数比例";
            dt.Rows[6][0] = "参考标准比例";
            ds = bll.GetMonitorData(slipNumber);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                dt.Columns.Add(row["NAME"].ToString(), Type.GetType("System.String"));
                dt.Rows[0][row["NAME"].ToString()] = String.Format("{0:F0}",row["SHOP_STOCK"]);
                dt.Rows[1][row["NAME"].ToString()] = String.Format("{0:F0}",row["BEFORE_SALES_QUANTITY"]);
                dt.Rows[2][row["NAME"].ToString()] = String.Format("{0:F0}",row["AFTER_SALES_QUANTITY"]);
                dt.Rows[3][row["NAME"].ToString()] = String.Format("{0:F0}",row["REQUISTION_QUANTITY"]);
                dt.Rows[4][row["NAME"].ToString()] = String.Format("{0:F0}",Convert.ToDecimal(row["SHOP_STOCK"]) - Convert.ToDecimal(row["BEFORE_SALES_QUANTITY"]) + Convert.ToDecimal(row["REQUISTION_QUANTITY"]));
                //dt.Rows[5][row["NAME"].ToString()] = row["SHOP_STOCK"];
                dt.Rows[6][row["NAME"].ToString()] = row["REFERENCE_PERCENTAGE"].ToString() + "%";
            }
            dt.Columns.Add("合计", Type.GetType("System.String"));
            for (int i = 0; i < 6; i++)
            {
                DataRow row = dt.Rows[i];
                object[] objs = row.ItemArray;
                decimal total = 0;
                for (int j = 1; j < objs.Length - 1; j++)
                {
                    if (i < 5)
                    {
                        try
                        {
                            total += Convert.ToDecimal(objs[j]);
                        }
                        catch { }
                    }
                    else
                    {
                        DataRow dr = dt.Rows[i - 1];
                        try
                        {
                            row[j] = String.Format("{0:F2}", Convert.ToDecimal(dr[j]) / Convert.ToDecimal(dr["合计"])*100) + "%";
                        }
                        catch
                        {
                            row[j] = "0%";
                        }

                    }
                }
                if (i < 5)
                {
                    row["合计"] = String.Format("{0:F0}", total);
                }
            }
            GridViewBind(this.gridView, dt);
        }


        private void GridViewBind(GridView gdv, DataTable dt)
        {
            gdv.Columns.Clear();
            gdv.AutoGenerateColumns = false;
            gdv.DataSource = dt;
            for (int i = 0; i < dt.Columns.Count; i++)   //绑定普通数据列
            {
                BoundField bfColumn = new BoundField();
                bfColumn.DataField = dt.Columns[i].ColumnName;
                bfColumn.HeaderText = dt.Columns[i].Caption;
                gdv.Columns.Add(bfColumn);
            }
            gdv.DataBind();
        }


        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
