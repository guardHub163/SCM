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
using SCM.Model;
using System.Text;
using SCM.Common;
using log4net;
using System.Reflection;

namespace SCM.Web.News
{
    public partial class ShowInfo : BasePage
    {
        BNews bll = new BNews();
        BCommon bCommon = new BCommon();
        DataSet ds = new DataSet();
        int PageSize = 20;
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    decimal CODE = Convert.ToDecimal(Request.Params["id"]);
                    Showinfo(CODE);
                    ds = bll.UserPhoto(this.lblName.Text);
                    string photo = ds.Tables[0].Rows[0]["PHOTO_PATH"].ToString();
                    this.PhotoCreate.Src = "../../Image.aspx?TYPE=USER&FILE_NAME=" + photo;
                }
                int recordCount = bll.GetNewsCount(getConduction());
                if (recordCount > 0)
                {
                    if (recordCount > PageSize)
                    {
                        panelPage.Visible = true;
                    }
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
            this.paging.PageChanged += new PageControl.PageChangedEventHandler(PageChanged);
        }

        protected void PageChanged(object sender, int e)
        {
            BindData();
        }

        private DataTable InitDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CREATE_USER", Type.GetType("System.String"));
            dt.Columns.Add("NEWS_CONTENT", Type.GetType("System.String"));
            dt.Columns.Add("CREATE_DATE_TIME", Type.GetType("System.DateTime"));
            for (int i = 1; i <= PageSize; i++)
            {
                dt.Rows.Add(dt.NewRow());

            }
            return dt;
        }
        private string getConduction()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("STATUS_FLAG <>" + CConstant.DELETE);
            sb.Append(" AND PARENT_ID=" + Convert.ToDecimal(this.Labelid.Text));
            return sb.ToString();
        }

        private void BindData()
        {
            string strWhere = getConduction();
            ds = bll.GetNewsListByPage(strWhere, "", (this.paging.CurrentPage - 1) * PageSize + 1, this.paging.CurrentPage * PageSize);
            gridView.DataSource = ds;
            gridView.DataBind();
        }

        private void Showinfo(decimal ID)
        {
            BNews bll = new BNews();
            BaseNewsTable newTable = bll.GetModel(ID);
            this.lblTime.Text = newTable.PUBLISH_DATE.ToString();
            this.Labelid.Text = newTable.ID.ToString();
            this.lblName.Text = newTable.CREAT_NAME;
            this.lblTitle.Text = newTable.NEWS_TITLE;
            this.lblContent.Text = newTable.NEWS_CONTENT;
            this.lblType.Text = newTable.NEWS_TYPE.ToString();
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnReturn":
                    Search(sender, e);
                    break;
            }
            return true;
        }

        private void Search(object sender, EventArgs e)
        {
            string message = "";
            if (this.txtNewsContent.Value.Trim().Length == 0)
            {
                message += "回复内容不能为空！";
            }
            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }
            BaseNewsTable newTable = new BaseNewsTable();
            newTable.PUBLISH_DATE = Convert.ToDateTime(DateTime.Now.ToString());
            newTable.PARENT_ID = Convert.ToDecimal(this.Labelid.Text);
            newTable.NEWS_TYPE = Convert.ToInt32(this.lblType.Text);
            newTable.NEWS_CONTENT = this.txtNewsContent.Value;
            try
            {
                newTable.CREATE_USER = _userTable.USER_ID;
                newTable.LAST_UPDATE_USER = _userTable.USER_ID;
            }
            catch { }
            if (bll.Add(newTable) > 0)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "processCloseAndRefreshParent();", true);
                this.txtNewsContent.Value = "";
            }
            int recordCount = bll.GetNewsCount(getConduction());
            if (recordCount > 0)
            {
                if (recordCount > PageSize)
                {
                    panelPage.Visible = true;
                }
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
}
