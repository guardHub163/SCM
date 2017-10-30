using System;
using System.Collections;
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
using System.IO;

namespace SCM.Web.Productprice
{
    public partial class List : BasePage
    {
        BCommon bCommon = new BCommon();
        BProductprice bll = new BProductprice();
        DataSet ds = new DataSet();
        int currentPage = 1;
        int number = CConstant.DELETE;
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {
                gridView.DataSource = InitDataTable();
                gridView.DataBind();
                btnNew.Attributes.Add("onclick", "return winOpen('Add.aspx?','','380','420');");
                ddlPrice.DataSource = bCommon.GetNames("PRICE_CODE").Tables[0];
                ddlPrice.DataTextField = "NAME"; //dropdownlist的Text的字段 
                ddlPrice.DataValueField = "CODE";//dropdownlist的Value的字段 
                ddlPrice.DataBind();
                ddlPrice.Items.Insert(0, new ListItem("全部", ""));
            }
            this.paging.PageChanged += new PageControl.PageChangedEventHandler(PageChanged);
            this.uploadFile.Process_UploadFile += new UploadFileControl.UploadFileEventHandler(Process_UploadFile);
        }

        protected void PageChanged(object sender, int e)
        {
            BindData();
        }

        private DataTable InitDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", Type.GetType("System.Decimal"));
            dt.Columns.Add("DEPARTMENT_NAME", Type.GetType("System.String"));
            dt.Columns.Add("STYLE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("PARENT_NAME", Type.GetType("System.String"));
            dt.Columns.Add("SALES_PRICE", Type.GetType("System.String"));
            dt.Columns.Add("START_DATE", Type.GetType("System.DateTime"));
            dt.Columns.Add("ORI_PRICE", Type.GetType("System.DateTime"));
            dt.Columns.Add("DISCOUNT_RATE", Type.GetType("System.DateTime"));
            dt.Columns.Add("END_DATE", Type.GetType("System.DateTime"));
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
            int recordCount = bll.GetRecordCount(getConduction());
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

        private string getConduction()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("STATUS_FLAG <>" + CConstant.DELETE);
            if (this.ddlPrice.SelectedItem.Value != "0")
            {
                sb.AppendFormat(" AND PRICE_CODE like '%{0}%'", this.ddlPrice.SelectedItem.Value);
            }
            if (this.txtDepartment_Code.Text.Trim() != "")
            {
                sb.AppendFormat(" AND DEPARTMENT_CODE LIKE '%{0}%'", this.txtDepartment_Code.Text);
            }
            if (this.txtStyleCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND STYLE_CODE LIKE '%{0}%'", this.txtStyleCode.Text.Trim());
            }
            return sb.ToString();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnD = (LinkButton)e.Row.FindControl("btnDelete");
                LinkButton btnS = (LinkButton)e.Row.FindControl("btnShow");
                LinkButton btnM = (LinkButton)e.Row.FindControl("btnModify");
                if (e.Row.Cells[0].Text.Trim() != "&nbsp;" && e.Row.Cells[0].Text.Trim() != "")
                {
                    btnD.Attributes.Add("onclick", "return confirm(\"你确认要删除吗?\")");
                    btnS.Attributes.Add("onclick", "return winOpen('Show.aspx?','id=" + btnS.CommandArgument + "','490','420')");
                    btnM.Attributes.Add("onclick", "return winOpen('Modify.aspx?','id=" + btnM.CommandArgument + "','420','420')");
                    e.Row.Attributes.Add("OnMouseOver", "c=this.style.backgroundColor;this.style.backgroundColor=mouseOverBackgroundColor;");
                    e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor=c;");
                }
                else
                {
                    btnD.Visible = false;
                    btnS.Visible = false;
                    btnM.Visible = false;
                }
            }
        }

        private void BindData()
        {
            string strWhere = getConduction();
            ds = bll.GetListByPage(strWhere, "", (this.paging.CurrentPage - 1) * PageSize + 1, this.paging.CurrentPage * PageSize);
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
                    Search(sender, e);
                    break;
                case "btnModify":
                    BindData();
                    break;
                case "btnImport":
                    import_div.Visible = true;
                    btnImport.Enabled = false;
                    break;
                case "btnDelete":
                    try
                    {
                        LinkButton btn = (LinkButton)sender;
                        bll.Delete(Convert.ToDecimal(btn.CommandArgument));
                        BindData();
                    }
                    catch { }
                    break;
            }
            return true;
        }

        protected void Department_change(object sender, EventArgs e)
        {
            if (this.txtDepartment_Code.Text.Trim() == "")
            {
                this.lblWarehouseName.Text = "";
                this.txtDepartment_Code.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_DEPARTMENT", this.txtDepartment_Code.Text, "");
            if (table != null)
            {
                this.lblWarehouseName.Text = table.Name;
                this.txtDepartment_Code.Text = table.Code;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"部门不存在！\");", true);
                this.lblWarehouseName.Text = "";
                this.txtDepartment_Code.Text = "";
            }
        }
        protected void SysleCode_Chanage(object sender, EventArgs e)
        {
            if (this.txtStyleCode.Text.Trim() == "")
            {
                this.lblStyleName.Text = "";
                this.txtStyleCode.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_STYLE", txtStyleCode.Text.Trim(), "");
            if (table != null)
            {
                this.lblStyleName.Text = table.Name;
                this.txtStyleCode.Text = table.Code;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"款式不存在！\");", true);
                this.lblStyleName.Text = "";
                this.txtStyleCode.Text = "";
            }
        }
        protected void Process_UploadFile(object sender, EventArgs e, HtmlInputFile aFile)
        {
            import_div.Visible = false;
            btnImport.Enabled = true;
            if (aFile == null)
            {
                return;
            }
            HttpPostedFile hpFile = aFile.PostedFile;
            string fileExtension = System.IO.Path.GetExtension(hpFile.FileName);//获取文件扩展名
            if (hpFile.ContentLength <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "alert('请选择导入文件。');", true);
                return;
            }
            if (fileExtension != ".xls" && fileExtension != ".xlsx")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "alert('文件格式错误。');", true);
                return;
            }
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;//新的文件名
            string path = HttpContext.Current.Server.MapPath("~") + "\\" + "Excel" + "\\" + fileName;
            hpFile.SaveAs(path);
            DataTable da = FileOperator.ReadExcel(path, fileName);
            File.Delete(path);
            BaseProductpriceTable bstable = new BaseProductpriceTable();
            BProductprice bst = new BProductprice();
            StringBuilder sb = new StringBuilder();
            _userTable = (BaseUserTable)HttpContext.Current.Session["UserInfo"];
            foreach (DataRow row in da.Rows)
            {
                try
                {
                    if (row[0].ToString() == "")
                    {
                        sb.AppendFormat("{0}", "销售种类不能为空。");
                        continue;
                    }

                    if (row[1].ToString() == "")
                    {
                        sb.AppendFormat("{0}", "商品款式不能为空。");
                        continue;
                    }

                    if (row[2].ToString() == "")
                    {
                        sb.AppendFormat("{0}", "部门不能为空。");
                        continue;
                    }

                    if (row[3].ToString() == "")
                    {
                        sb.AppendFormat("{0}", "销售价格不能为空。");
                        continue;
                    }
                    if (row[4].ToString() == "")
                    {
                        sb.AppendFormat("{0}", "原价不能为空。");
                        continue;
                    }
                    if (row[5].ToString() == "")
                    {
                        sb.AppendFormat("{0}", "折扣不能为空。");
                        continue;
                    }
                    if (row[6].ToString() == "")
                    {
                        sb.AppendFormat("{0}", "是否默认单价不能为空。");
                        continue;
                    }
                    if (row[7].ToString() == "")
                    {
                        sb.AppendFormat("{0}", "开始时间不能为空。");
                        continue;
                    }
                    if (row[8].ToString() == "")
                    {
                        sb.AppendFormat("{0}", "结束时间不能为空。");
                        continue;
                    }
                    else
                    {
                        bstable.PRICE_CODE = row[0].ToString();
                        bstable.STYLE_CODE = row[1].ToString();
                        bstable.DEPARTMENT_CODE = row[2].ToString();
                        bstable.SALES_PRICE = Convert.ToDecimal(row[3].ToString());
                        bstable.ORI_PRICE = Convert.ToDecimal(row[4].ToString());
                        bstable.DISCOUNT_RATE = Convert.ToDecimal(row[5].ToString());
                        bstable.DEFAULT_FLAG = Convert.ToInt32(row[6].ToString());
                        bstable.START_DATE = Convert.ToDateTime(row[7].ToString());
                        bstable.END_DATE = Convert.ToDateTime(row[8].ToString());
                        bstable.ATTRIBUTE1 = row[9].ToString();
                        bstable.ATTRIBUTE2 = row[10].ToString();
                        bstable.ATTRIBUTE3 = row[11].ToString();
                        bstable.STATUS_FLAG = 0;
                        bstable.CREATE_USER = _userTable.USER_ID;
                        bstable.CREATE_DATE_TIME = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));
                        bstable.LAST_UPDATE_TIME = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));
                        bstable.LAST_UPDATE_USER = _userTable.USER_ID;
                        bst.Add(bstable);
                    }
                }
                catch { }

            }
            if (sb.Length > 0)
            {
                HttpContext.Current.Session["ERROR_INFO"] = sb.ToString();
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "window.open('../../LeadingErrorInfo.aspx?');", true);
            }

        }
    }
}
