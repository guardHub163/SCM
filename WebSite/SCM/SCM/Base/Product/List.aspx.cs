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
using System.IO;

namespace SCM.Web.Product
{
    public partial class List : BasePage
    {

        private DataSet ds = new DataSet();
        private BProduct bProduct = new BProduct();
        private BCommon bCommon = new BCommon();
        private BStyle bStyle = new BStyle();
        private BColor bColor = new BColor();
        private BSize bSize = new BSize();
        private BUnit bUnit = new BUnit();
        private BProductGroup bProductGroup = new BProductGroup();
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {
                gridView.DataSource = InitDataTable();
                gridView.DataBind();
                btnNew.Attributes.Add("onclick", "return winOpen('Add.aspx?','','360','420');");
            }
            this.uploadFile.Process_UploadFile += new UploadFileControl.UploadFileEventHandler(Process_UploadFile);
            this.paging.PageChanged += new PageControl.PageChangedEventHandler(PageChanged);
        }

        protected void PageChanged(object sender, int e)
        {
            BindData();
        }

        private DataTable InitDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CODE", Type.GetType("System.String"));
            dt.Columns.Add("NAME", Type.GetType("System.String"));
            dt.Columns.Add("STYLE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("PRODUCT_SPEC", Type.GetType("System.String"));
            dt.Columns.Add("PRODUCT_GROUP_NAME", Type.GetType("System.String"));
            dt.Columns.Add("COLOR_NAME", Type.GetType("System.String"));
            dt.Columns.Add("SIZE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("UNIT_NAME", Type.GetType("System.String"));
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
            int recordCount = bProduct.GetRecordCount(getConduction());
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
            if (this.txtProductName.Text != "")
            {
                sb.AppendFormat(" AND NAME like '%{0}%'", this.txtProductName.Text);
            }
            if (this.txtProductGroupCode.Text != "")
            {
                sb.AppendFormat(" AND GROUP_CODE = '{0}'", this.txtProductGroupCode.Text);
            }
            if (this.txtStyleCode.Text != "")
            {
                sb.AppendFormat(" AND STYLE like '%{0}%'", this.txtStyleCode.Text);
            }
            if (this.txtColorCode.Text != "")
            {
                sb.AppendFormat(" AND COLOR LIKE '%{0}%'", this.txtColorCode.Text.Trim());
            }
            if (this.txtSizeCode.Text != "")
            {
                sb.AppendFormat(" AND SIZE like '%{0}%'", this.txtSizeCode.Text.Trim());
            }
            if (this.txtUnitCode.Text != "")
            {
                sb.AppendFormat(" AND UNIT_CODE like '%{0}%'", this.txtUnitCode.Text.Trim());
            }
            if (this.txtCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND CODE LIKE '%{0}%'", this.txtCode.Text.Trim());
            }
            return sb.ToString();

        }

        private void BindData()
        {
            string strWhere = getConduction();
            ds = bProduct.GetListByPage(strWhere, "", (this.paging.CurrentPage - 1) * PageSize + 1, this.paging.CurrentPage * PageSize);
            for (int i = ds.Tables[0].Rows.Count; i < PageSize; i++)
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            }
            gridView.DataSource = ds;
            gridView.DataBind();
        }
        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnD = (LinkButton)e.Row.FindControl("btnDelete");
                LinkButton btnS = (LinkButton)e.Row.FindControl("btnShow");
                LinkButton btnM = (LinkButton)e.Row.FindControl("btnModify");
                LinkButton btnP = (LinkButton)e.Row.FindControl("btnPhoto");
                if (e.Row.Cells[0].Text.Trim() != "&nbsp;" && e.Row.Cells[0].Text.Trim() != "")
                {
                    btnD.Attributes.Add("onclick", "return confirm(\"你确认要删除吗?\")");
                    btnP.Attributes.Add("onclick", "winOpen('../../Common/ShowImage.aspx?','PC=" + btnP.CommandArgument + "','768','815');return false;");
                    btnS.Attributes.Add("onclick", "return winOpen('Show.aspx?','code=" + btnS.CommandArgument + "','430','420')");
                    btnM.Attributes.Add("onclick", "return winOpen('Modify.aspx?','code=" + btnM.CommandArgument + "','390','420')");
                    e.Row.Attributes.Add("OnMouseOver", "c=this.style.backgroundColor;this.style.backgroundColor=mouseOverBackgroundColor;");
                    e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor=c;");
                }
                else
                {
                    btnD.Visible = false;
                    btnS.Visible = false;
                    btnM.Visible = false;
                    btnP.Visible = false;
                }
            }
        }

        protected void ProductGroupCode_Chanage(object sender, EventArgs e)
        {
            if (this.txtProductGroupCode.Text.Trim() == "")
            {
                this.lblProductGroupName.Text = "";
                this.txtProductGroupCode.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_PRODUCT_GROUP", txtProductGroupCode.Text.Trim(), "");
            if (table != null)
            {
                this.lblProductGroupName.Text = table.Name;
                this.txtProductGroupCode.Text = table.Code;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"种类不存在！\");", true);
                this.lblProductGroupName.Text = "";
                this.txtProductGroupCode.Text = "";
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
                case "btnImport":
                    import_div.Visible = true;
                    btnImport.Enabled = false;
                    break;
                case "btnModify":
                    BindData();
                    break;
                case "btnDelete":
                    try
                    {
                        LinkButton btn = (LinkButton)sender;
                        bProduct.Delete(btn.CommandArgument);
                        BindData();
                    }
                    catch { }
                    break;
            }
            return true;
        }

        protected void ClolorCode_Chanage(object sender, EventArgs e)
        {
            if (this.txtColorCode.Text.Trim() == "")
            {
                this.lblColorName.Text = "";
                this.txtColorCode.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_COLOR", txtColorCode.Text.Trim(), "");
            if (table != null)
            {
                this.lblColorName.Text = table.Name;
                this.txtColorCode.Text = table.Code;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"颜色不存在！\");", true);
                this.lblColorName.Text = "";
                this.txtColorCode.Text = "";
            }
        }
        protected void SizeCode_Chanage(object sender, EventArgs e)
        {
            if (this.txtSizeCode.Text.Trim() == "")
            {
                this.lblSizeName.Text = "";
                this.txtSizeCode.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_SIZE", txtSizeCode.Text.Trim(), "");
            if (table != null)
            {
                this.lblSizeName.Text = table.Name;
                this.txtSizeCode.Text = table.Code;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"尺码不存在！\");", true);
                this.lblSizeName.Text = "";
                this.txtSizeCode.Text = "";
            }
        }
        protected void UnitCode_Chanage(object sender, EventArgs e)
        {
            if (this.txtSizeCode.Text.Trim() == "")
            {
                this.lblUnitName.Text = "";
                this.txtUnitCode.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_UNIT", txtUnitCode.Text, "");
            if (table != null)
            {
                this.lblUnitName.Text = table.Name;
                this.txtUnitCode.Text = table.Code;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"单位不存在！\");", true);
                this.lblUnitName.Text = "";
                this.txtUnitCode.Text = "";
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "alert('请选择上传文件');", true);
                return;
            }
            if (fileExtension != ".xls" && fileExtension != ".xlsx")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "alert('上传文件的格式不对');", true);
                return;
            }
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;//新的文件名
            string path = HttpContext.Current.Server.MapPath("~") + "\\" + "Excel" + "\\" + fileName;
            hpFile.SaveAs(path);
            DataTable da = FileOperator.ReadExcel(path, fileName);
            File.Delete(path);
            BaseProductTable pTable = new BaseProductTable();
            StringBuilder sb = new StringBuilder();
            _userTable = (BaseUserTable)HttpContext.Current.Session["UserInfo"];
            foreach (DataRow row in da.Rows)
            {
                try
                {
                    if (string.IsNullOrEmpty(CConvert.ToString(row[0])))
                    {
                        sb.Append("商品编号不能为空。");
                    }
                    else if (bProduct.Exists(row[0].ToString()))
                    {
                        sb.Append("商品编号[" + CConvert.ToString(row[0]) + "]已存在。");
                    }
                    else if (row[1].ToString() == "")
                    {
                        sb.Append("商品编号为[" + CConvert.ToString(row[0]) + "]的名称不能为空。");
                    }                   
                    else if (!bStyle.Exists(CConvert.ToString(row[3])))
                    {
                        sb.Append("商品编号为[" + CConvert.ToString(row[0]) + "]的款式不存在。");
                    }
                    else if (!bColor.Exists(CConvert.ToString(row[4])))
                    {
                        sb.Append("商品编号为[" + CConvert.ToString(row[0]) + "]的颜色不存在。");
                    }
                    else if (!bProductGroup.Exists(CConvert.ToString(row[7])))
                    {
                        sb.Append("商品编号为[" + CConvert.ToString(row[0]) + "]的种类不存在。");
                    }
                    else if (!bSize.Exists(CConvert.ToString(row[5]), CConvert.ToString(row[7])))
                    {
                        sb.Append("商品编号为[" + CConvert.ToString(row[0]) + "]的尺码不存在。");
                    }
                    else if (!bUnit.Exists(CConvert.ToString(row[6])))
                    {
                        sb.Append("商品编号为[" + CConvert.ToString(row[0]) + "]的单位不存在。");
                    }
                   
                    else
                    {
                        pTable.CODE = CConvert.ToString(row[0]);
                        pTable.NAME = CConvert.ToString(row[1]);
                        pTable.PRODUCT_SPEC = CConvert.ToString(row[2]);
                        pTable.STYLE = CConvert.ToString(row[3]);
                        pTable.COLOR = CConvert.ToString(row[4]);
                        pTable.SIZE = CConvert.ToString(row[5]);
                        pTable.UNIT_CODE = CConvert.ToString(row[6]);
                        pTable.GROUP_CODE = CConvert.ToString(row[7]);
                        pTable.ATTRIBUTE1 = CConvert.ToString(row[8]);
                        pTable.ATTRIBUTE2 = CConvert.ToString(row[9]);
                        pTable.ATTRIBUTE3 = CConvert.ToString(row[10]);
                        pTable.STATUS_FLAG = CConstant.INIT; ;
                        pTable.CREATE_USER = _userTable.USER_ID;
                        pTable.LAST_UPDATE_USER = _userTable.USER_ID;

                        bProduct.Add(pTable);
                    }
                }
                catch (Exception ex)
                {
                    _log.Error("商品导入：", ex);
                }

            }
            if (sb.Length > 0)
            {
                HttpContext.Current.Session["ERROR_INFO"] = sb.ToString();
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "window.open('../../LeadingErrorInfo.aspx?');", true);
            }

        }
    }
}