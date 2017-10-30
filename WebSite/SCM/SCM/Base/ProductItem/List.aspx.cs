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

namespace SCM.Web.ProductItem
{
    public partial class List : BasePage
    {
        BProductItem bll = new BProductItem();
        DataSet ds = new DataSet();
        BCommon bCommon = new BCommon();
        bool falg = false;
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {
                gridView.DataSource = InitDataTable();
                gridView.DataBind();
            }
            if (this.txtProductCode.Text.Trim().Length == 0 && this.lblProductName.Text.Trim().Length == 0)
            {
                btnNew.Attributes.Add("onclick", "return winOpen('Add.aspx?','','260','420');");
            }
            else
            {
                string parm = "PRODUCT_CODE=" + txtProductCode.Text;
                btnNew.Attributes.Add("onclick", "return winOpen('Add.aspx?','" + parm + "','260','420');");
            }
            btnProduct.Attributes.Add("onclick", "return processMasterClickByServer('PRODUCT','" + txtProductCode.ClientID + "','" + lblProductName.ClientID + "');");
            this.paging.PageChanged += new PageControl.PageChangedEventHandler(PageChanged);
        }

        protected void PageChanged(object sender, int e)
        {
            BindData();
        }

        private DataTable InitDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PRODUCT_CODE", Type.GetType("System.String"));
            dt.Columns.Add("PRODUCT_NAME", Type.GetType("System.String"));
            dt.Columns.Add("ITEM_CODE", Type.GetType("System.String"));
            dt.Columns.Add("ITEM_NAME", Type.GetType("System.String"));
            dt.Columns.Add("QUANTITY", Type.GetType("System.String"));
            dt.Columns.Add("SUPPLIER_NAME", Type.GetType("System.String"));
            dt.Columns.Add("ATTRIBUTE1", Type.GetType("System.String"));
            dt.Columns.Add("ATTRIBUTE2", Type.GetType("System.String"));
            dt.Columns.Add("ATTRIBUTE3", Type.GetType("System.String"));
            for (int i = 1; i <= PageSize; i++)
            {
                dt.Rows.Add(dt.NewRow());

            }
            return dt;
        }

        private void Search(object sender, EventArgs e)
        {
            if (!falg)
            {
                if (this.txtProductCode.Text.Trim() != "")
                {
                    int recordCount = bll.GetCount(getConduction());
                    if (recordCount > 0)
                    {
                        panelPage.Visible = true;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"您查询的信息不存在！\");processCloseAndRefreshParent();", true);
                        panelPage.Visible = false;
                    }
                    //将每页显示的数量保存在用户控件
                    this.paging.PageSize = PageSize;
                    //将数据总条数保存在用户控件
                    this.paging.RecorderCount = recordCount;
                    BindData();

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"商品编号不能为空！\");", true);
                    gridView.DataSource = InitDataTable();
                    gridView.DataBind();
                    return;
                }
            }
            else 
            {
                int recordCount = bll.GetCount(getConduction());
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
        }

        private string getConduction()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("STATUS_FLAG <>" + CConstant.DELETE); 
            if (this.txtProductCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND PRODUCT_CODE='{0}'", this.txtProductCode.Text.Trim());
            }
            return sb.ToString();

        }
        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnD = (LinkButton)e.Row.FindControl("btnDelete");
                LinkButton btnM = (LinkButton)e.Row.FindControl("btnModify");
                string[] sArray = btnM.CommandArgument.ToString().Split('|');
                string productcode = Convert.ToString(sArray[0]);
                string itemcode = Convert.ToString(sArray[1]);
                string param = "PRODUCT_CODE=" + productcode;
                param += "&ITEM_CODE=" + itemcode;
                if (e.Row.Cells[0].Text.Trim() != "&nbsp;" && e.Row.Cells[0].Text.Trim() != "")
                {
                    btnD.Attributes.Add("onclick", "return confirm(\"你确认要删除吗?\")");
                    btnM.Attributes.Add("onclick", "return winOpen('Modify.aspx?','" + param + " ','260','420')");
                    e.Row.Attributes.Add("OnMouseOver", "c=this.style.backgroundColor;this.style.backgroundColor=mouseOverBackgroundColor;");
                    e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor=c;");
                }
                else
                {
                    btnD.Visible = false;
                    btnM.Visible = false;
                }
            }
        }

        private void BindData()
        {
            string strWhere = getConduction();
            ds = bll.GetList(strWhere, "", (this.paging.CurrentPage - 1) * PageSize + 1, this.paging.CurrentPage * PageSize);
            for (int i = ds.Tables[0].Rows.Count; i < PageSize; i++)
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            }
            gridView.DataSource = ds;
            gridView.DataBind();

        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSearch":
                    Search(sender, e);
                    break;
                case "btnNew":
                    falg = true;
                    Search(sender, e);
                    break;
                case "btnModify":
                    BindData();
                    break;
                case "btnDelete":
                    try
                    {
                        LinkButton btn = (LinkButton)sender;
                        string[] sArray = btn.CommandArgument.ToString().Split('|');
                        string productcode = Convert.ToString(sArray[0]);
                        string itemcode = Convert.ToString(sArray[1]);
                        bll.Delete(productcode, itemcode);
                        BindData();
                    }
                    catch { }
                    break;
            }
            return true;
        }


        protected void Product_Changed(object sender, EventArgs e)
        {
            if (txtProductCode.Text.Trim() == "")
            {
                this.lblProductName.Text = "";
                return;
            }
            BaseProductTable productTable = new BProduct().GetModel(txtProductCode.Text.Trim());
            if (productTable != null)
            {
                this.lblProductName.Text = productTable.NAME;
            }
            else
            {
                this.lblProductName.Text = "";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"商品不存在!\");document.getElementById('" + txtProductCode.ClientID + "').value='';", true);
            }
        }
    }
}