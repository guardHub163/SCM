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

namespace SCM.Web.SAR
{
    public partial class _ProductCompare : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string departmentCode = Request.QueryString["DEPARTMENT_CODE"];
                string fromDate = Request.QueryString["FROM_DATE"];
                string totalAmount = Request.QueryString["TOTOAL_AMOUNT"];
                string toDate = Request.QueryString["TO_DATE"];
                if (totalAmount != "" && totalAmount != "0")
                {
                    DataTable dt = GetProductAmountQuantity(departmentCode, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), totalAmount);
                    if (dt.Rows.Count == 0)
                    {
                        dt = ProductDt().Copy();
                        for (int i = dt.Rows.Count; i < 20; i++)
                        {
                            dt.Rows.Add(dt.NewRow());
                        }

                    }
                    else
                    {
                        if (dt.Rows.Count < 20)
                        {
                            for (int i = dt.Rows.Count; i < 20; i++)
                            {
                                dt.Rows.Add(dt.NewRow());
                            }

                        }
                    }
                    if (dt != null)
                    {
                        this.gridView.DataSource = dt;
                        this.gridView.DataBind();
                    }

                }
                else
                {
                    DataTable dt1 = new DataTable();
                    dt1 = ProductDt().Copy();
                    for (int i = dt1.Rows.Count; i < 23; i++)
                    {
                        dt1.Rows.Add(dt1.NewRow());
                    }
                    this.gridView.DataSource = dt1;
                    this.gridView.DataBind();
                }
            }
        }

        public DataTable GetProductAmountQuantity(string departmentCode, DateTime datetime, DateTime todatetime, string amount)
        {
            BSarSalesOrder bll = new BSarSalesOrder();
            DataTable dt = ProductDt();
            DataSet ds = bll.GetProductAmountQuantity(departmentCode, datetime, todatetime);
            DataTable da = ds.Tables[0];
            if (da.Rows.Count == 0)
            {
                return new DataTable();
            }
            foreach (DataRow row in da.Rows)
            {
                DataRow rows = dt.NewRow();
                rows["NUMBER"] = row["NUMBER"];
                rows["NAME"] = row["PRODUCT_NAME"];
                rows["AMOUNT"] = row["PRICE"];
                rows["SORT"] = (CConvert.FormateRate(Convert.ToString(Convert.ToDecimal(row["PRICE"]) / Convert.ToDecimal(amount) * 100))).ToString();
                rows["QUANTITY"] = row["QUANTITY"];
                dt.Rows.Add(rows);
            }
            return dt;

        }

        public DataTable ProductDt()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NUMBER", Type.GetType("System.String"));
            dt.Columns.Add("NAME", Type.GetType("System.String"));
            dt.Columns.Add("AMOUNT", Type.GetType("System.String"));
            dt.Columns.Add("SORT", Type.GetType("System.String"));
            dt.Columns.Add("QUANTITY", Type.GetType("System.String"));
            return dt;
        }
    }
}
