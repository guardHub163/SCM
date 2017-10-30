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
using SCM.Common;
using SCM.Model;
using System.IO;
using SCM.Web;
using log4net;
using System.Reflection;

namespace SCM.Web.News
{
    public partial class Add : BaseModalDialogPage
    {
        BNews bll = new BNews();
        BCommon bCommon = new BCommon();
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            //selNewsType.DataSource = bCommon.GetNames("NEW_TYPE").Tables[0];
            //selNewsType.DataTextField = "NAME"; //dropdownlist的Text的字段 
            //selNewsType.DataValueField = "CODE";//dropdownlist的Value的字段 
            //selNewsType.DataBind();

        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSave": //保存
                    btnSave_Click(sender, e);
                    break;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string message = "";
            if (this.txtTitle.Text.Trim().Length == 0)
            {
                message += "新闻标题不能为空！\\n";
            }
            if (this.txtType.Value.Trim().Length == 0)
            {
                message += "类型不能为空！\\n";
            }
            if (this.txtNewsContent.Value.Trim().Length == 0)
            {
                message += "新闻内容不能为空！\\n";
            }
            BaseNewsTable newTable = new BaseNewsTable();
            newTable.PUBLISH_DATE = Convert.ToDateTime(DateTime.Now.ToString());
            newTable.NEWS_TITLE = this.txtTitle.Text.Trim();
            newTable.NEWS_CONTENT = this.txtNewsContent.Value.Trim();
            newTable.NEWS_TYPE = Convert.ToInt32(this.txtType.Value);

                newTable.CREATE_USER = UserTable.USER_ID;
                newTable.LAST_UPDATE_USER = newTable.CREATE_USER;

            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }
            if (bll.Add(newTable) > 0)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"添加成功！\");processCloseAndRefreshParent();", true);
            }
        }
    }
}