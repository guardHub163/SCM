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
using log4net;
using System.Reflection;
using System.IO;

namespace SCM.Web.Style
{
    public partial class List : BasePage
    {

        BStyle bll = new BStyle();
        DataSet ds = new DataSet();
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {
                gridView.DataSource = InitDataTable();
                gridView.DataBind();
                btnNew.Attributes.Add("onclick", "return winOpen('Add.aspx?','','200','420');");
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

        private string getConduction()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("STATUS_FLAG <>" + CConstant.DELETE);
            if (this.txtStyleName.Text.Trim() != "")
            {
                sb.AppendFormat(" AND NAME like '%{0}%'", this.txtStyleName.Text.Trim());
            }
            if (this.txtCode.Text.Trim() != "") 
            {
                sb.AppendFormat(" AND CODE LIKE '%{0}%'", this.txtCode.Text.Trim());
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
                    btnS.Attributes.Add("onclick", "return winOpen('Show.aspx?','code=" + btnS.CommandArgument + "','300','420')");
                    btnM.Attributes.Add("onclick", "return winOpen('Modify.aspx?','code=" + btnM.CommandArgument + "','200','420')");
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
                        bll.Delete(btn.CommandArgument);
                        BindData();
                    }
                    catch { }
                    break;
            }
            return true;
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
            BaseStyleTable bstable = new BaseStyleTable();
            BStyle bst = new BStyle();
            StringBuilder sb = new StringBuilder();
            _userTable = (BaseUserTable)HttpContext.Current.Session["UserInfo"];
            foreach (DataRow row in da.Rows)
            {
                try
                {
                    if (row[0].ToString() == "")
                    {
                        sb.AppendFormat("{0}", "编号不能为空。");
                        continue;
                    }
                    if (bst.Exists(row[0].ToString()))
                    {
                        sb.AppendFormat("{0}", "编号为" + row[0] + "的已存在。");
                        continue;
                    }
                    if (row[1].ToString() == "")
                    {
                        sb.AppendFormat("{0}", "编号为" + row[0] + "的名称不能为空。");
                        continue;
                    }
                    else
                    {
                        bstable.CODE = row[0].ToString();
                        bstable.NAME = row[1].ToString();
                        bstable.STATUS_FLAG = 0;
                        bstable.ATTRIBUTE1 = row[2].ToString();
                        bstable.ATTRIBUTE2 = row[3].ToString();
                        bstable.ATTRIBUTE3 = row[4].ToString();
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