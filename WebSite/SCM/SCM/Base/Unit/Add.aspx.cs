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


namespace SCM.Web.Unit
{
    public partial class Add : BaseModalDialogPage
    {
        BUnit bll = new BUnit();
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
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
            if (this.txtCode.Text.Trim().Length == 0)
            {
                message += "编号不能为空！\\n";
            }
            else if (bll.Exists(txtCode.Text.Trim()))
            {
                message += "编号已经存在！\\n";
            }
            if (this.txtName.Text.Trim().Length == 0)
            {
                message += "尺码不能为空！\\n";
            }
            BaseUnitTable untable = new BaseUnitTable();
            untable.CODE = this.txtCode.Text;
            untable.NAME = this.txtName.Text;
            untable.ATTRIBUTE1 = this.txtAttribute1.Text;
            untable.ATTRIBUTE2 = this.txtAttribute2.Text;
            untable.ATTRIBUTE3 = this.txtAttribute3.Text;

            untable.CREATE_USER = UserTable.USER_ID;
            untable.LAST_UPDATE_USER = untable.CREATE_USER;

            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }
            if (bll.Add(untable) > 0)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"添加成功！\");processCloseAndRefreshParent();", true);
            }
        }
    }
}