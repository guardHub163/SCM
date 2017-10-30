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
    public partial class Modify : BaseModalDialogPage
    {
        BNews bll = new BNews();
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {
                if (Request.Params["ID"] != null && Request.Params["ID"].Trim() != "")
                {
                    decimal ID = Convert.ToDecimal(Request.Params["ID"]);
                    Showinfo(ID);
                }
            }
        }

        private void Showinfo(decimal ID)
        {
            BaseNewsTable newTable = bll.GetModel(ID);
            this.lblTitle.Text = newTable.NEWS_TITLE;
            this.lblType.Text = newTable.TYPE_NAME;
            this.txtNewsContent.Value = newTable.NEWS_CONTENT;
            this.lblId.Text = newTable.ID.ToString();
            this.lblTypeCode.Text = newTable.NEWS_TYPE.ToString();
            this.lblTime.Text = newTable.PUBLISH_DATE.ToString("yyyy/MM/dd");
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
            if (this.txtNewsContent.Value.Trim().Length == 0)
            {
                message += "新闻内容不能为空！";
            }
            BaseNewsTable newTable = new BaseNewsTable();
            newTable.ID = Convert.ToDecimal(this.lblId.Text);
            newTable.NEWS_CONTENT = this.txtNewsContent.Value.Trim();
            newTable.NEWS_TITLE = this.lblTitle.Text;
            newTable.NEWS_TYPE = Convert.ToInt32(this.lblTypeCode.Text);
            newTable.PUBLISH_DATE = Convert.ToDateTime(this.lblTime.Text);

                newTable.LAST_UPDATE_USER = UserTable.USER_ID;

            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }
            if (bll.Update(newTable))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"修改成功！\");processCloseAndRefreshParent();", true);
            }
        }
    }
}
