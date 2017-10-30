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

namespace SCM.Web
{
    public partial class Main : System.Web.UI.Page
    {
        BNews bn = new BNews();
        BReceipt br = new BReceipt();
        int pageSize = 10;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    BaseUserTable userTable = (BaseUserTable)HttpContext.Current.Session["UserInfo"];

                    if (userTable == null)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "parent.document.location.href='TimeOut.aspx?Flag=Parent';", true);
                        return;
                    }
                }
                catch { }
            }

            InitNewDataTable();

            InitReceiptDataTable();

            gridView2.DataSource = InitDataTable();
            gridView2.DataBind();

            InitNewSystemDataTable();

            InitTransferOutDataTable();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitNewDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", Type.GetType("System.Decimal"));
            dt.Columns.Add("CODE", Type.GetType("System.Decimal"));
            dt.Columns.Add("NEWS_TITLE", Type.GetType("System.String"));
            dt.Columns.Add("PUBLISH_DATE", Type.GetType("System.DateTime"));
            dt.Columns.Add("CREATE_NAME", Type.GetType("System.String"));

            DataRow row;
            DataSet ds = bn.NewsInfo();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                row = dt.NewRow();
                row["CODE"] = (i + 1).ToString();
                row["ID"] = ds.Tables[0].Rows[i]["ID"].ToString();
                row["NEWS_TITLE"] = ds.Tables[0].Rows[i]["NEWS_TITLE"].ToString();
                row["PUBLISH_DATE"] = ds.Tables[0].Rows[i]["PUBLISH_DATE"].ToString();
                row["CREATE_NAME"] = ds.Tables[0].Rows[i]["CREATE_NAME"].ToString();
                dt.Rows.Add(row);
            }
            for (int j = dt.Rows.Count; j < pageSize; j++) 
            {
                dt.Rows.Add(dt.NewRow());
            }

            this.gridView.DataSource = dt;
            this.gridView.DataBind();
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitNewSystemDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", Type.GetType("System.Decimal"));
            dt.Columns.Add("CODE", Type.GetType("System.Decimal"));
            dt.Columns.Add("NEWS_TITLE", Type.GetType("System.String"));
            dt.Columns.Add("PUBLISH_DATE", Type.GetType("System.DateTime"));
            dt.Columns.Add("CREATE_NAME", Type.GetType("System.String"));

            DataRow row;
            DataSet ds = bn.NewsSystemInfo();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                row = dt.NewRow();
                row["CODE"] = (i + 1).ToString();
                row["ID"] = ds.Tables[0].Rows[i]["ID"].ToString();
                row["NEWS_TITLE"] = ds.Tables[0].Rows[i]["NEWS_TITLE"].ToString();
                row["PUBLISH_DATE"] = ds.Tables[0].Rows[i]["PUBLISH_DATE"].ToString();
                row["CREATE_NAME"] = ds.Tables[0].Rows[i]["CREATE_NAME"].ToString();
                dt.Rows.Add(row);
            }
            for (int j = dt.Rows.Count; j < pageSize; j++)
            {
                dt.Rows.Add(dt.NewRow());
            }
            this.gridView3.DataSource = dt;
            this.gridView3.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitReceiptDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CODE", Type.GetType("System.Decimal"));
            dt.Columns.Add("TO_WAREHOUSE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("ARRIVAL_DATE", Type.GetType("System.DateTime"));
            dt.Columns.Add("SUM_QUANTITY", Type.GetType("System.String"));
            DataRow row;
            DataSet ds = br.ReceiptInfo();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                row = dt.NewRow();
                row["CODE"] = (i + 1).ToString();
                row["TO_WAREHOUSE_NAME"] = ds.Tables[0].Rows[i]["TO_WAREHOUSE_NAME"].ToString();
                row["SUM_QUANTITY"] =String.Format("{0:F0}",ds.Tables[0].Rows[i]["SUM_QUANTITY"].ToString());
                row["ARRIVAL_DATE"] = ds.Tables[0].Rows[i]["ARRIVAL_DATE"].ToString();
                dt.Rows.Add(row);
            }
            for (int j = dt.Rows.Count; j < pageSize; j++)
            {
                dt.Rows.Add(dt.NewRow());
            }
            this.gridView1.DataSource = dt;
            this.gridView1.DataBind();

        }

        /// <summary>
        /// 
        /// </summary>
        private void InitTransferOutDataTable() 
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CODE", Type.GetType("System.Decimal"));
            dt.Columns.Add("FROM_WAREHOUSE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("DEPARTUAL_DATE", Type.GetType("System.DateTime"));
            dt.Columns.Add("SUM_QUANTITY", Type.GetType("System.String"));
            DataRow row;
            DataSet ds = br.TransferOutInfo();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                row = dt.NewRow();
                row["CODE"] = (i + 1).ToString();
                row["FROM_WAREHOUSE_NAME"] = ds.Tables[0].Rows[i]["FROM_WAREHOUSE_NAME"].ToString();
                row["DEPARTUAL_DATE"] = ds.Tables[0].Rows[i]["DEPARTUAL_DATE"].ToString();
                row["SUM_QUANTITY"] =String.Format("{0:F0}",ds.Tables[0].Rows[i]["SUM_QUANTITY"].ToString());
                dt.Rows.Add(row);
            }
            for (int j = dt.Rows.Count; j < pageSize; j++)
            {
                dt.Rows.Add(dt.NewRow());
            }
            this.gridView4.DataSource = dt;
            this.gridView4.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DataTable InitDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", Type.GetType("System.String"));
            dt.Columns.Add("DESC", Type.GetType("System.String"));
            dt.Columns.Add("USER_NAME", Type.GetType("System.String"));
            dt.Columns.Add("DATE_TIME", Type.GetType("System.String"));
            dt.Columns.Add("WAREHOUSE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("QUANTITY", Type.GetType("System.String"));
            dt.Columns.Add("SECURE_QUANTITY", Type.GetType("System.String"));



            DataRow row;
            for (int i = 1; i <= 10; i++)
            {
                row = dt.NewRow();
                row["ID"] = i.ToString();
                row["DESC"] = "new and news";
                row["USER_NAME"] = "张三";
                row["DATE_TIME"] = (DateTime.Now.AddDays(-i)).ToString("yyyy/MM/dd");
                row["WAREHOUSE_NAME"] = "北京分店";
                row["QUANTITY"] = "2000";
                row["SECURE_QUANTITY"] = "5000";
                dt.Rows.Add(row);

            }
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnS = (LinkButton)e.Row.FindControl("btnShow");
                if (e.Row.Cells[1].Text.Trim() != "&nbsp;" && e.Row.Cells[1].Text.Trim() != "")
                {
                    btnS.Attributes.Add("onclick", "document.location.href='Base/News/ShowInfo.aspx?id=" + btnS.CommandArgument + "';return false;");

                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnS = (LinkButton)e.Row.FindControl("btnShowSystem");
                if (e.Row.Cells[1].Text.Trim() != "&nbsp;" && e.Row.Cells[1].Text.Trim() != "")
                {
                    btnS.Attributes.Add("onclick", "document.location.href='Base/News/ShowInfo.aspx?id=" + btnS.CommandArgument + "';return false;");

                }
            }
        }

    }
}