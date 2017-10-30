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
using SCM.Common;
using SCM.Bll;
using SCM.Model;
using System.Reflection;
using log4net;

namespace SCM.Web.User
{
    public partial class UpdatePassWord : BaseModalDialogPage
    {
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        BaseUserTable busertable = new BaseUserTable();
        BUser bll = new BUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSave": //保存
                    Save(sender, e);
                    break;
            }
            return true;
        }

        private void Save(object sender, EventArgs e)
        {
            string message = "";
            if (this.txtOldPassWord.Text.Trim().Length == 0)
            {
                message += "原密码不能为空\\n";
            }
            else if (DESEncrypt.Encrypt(this.txtOldPassWord.Text.Trim()) != UserTable.PASSWORD)
            {
                message += "原密码不正确\\n";
            }
            if (this.txtNewPassWord.Text.Trim().Length == 0)
            {
                message += "新密码不能为空\\n";
            }
            else if (this.txtNewPassWord.Text.Trim().Length < 6)
            {
                message += "密码长度不能小于6位\\n";
            }
            if (this.txtRePassWord.Text.Trim().Length == 0)
            {
                message += "确认密码不能为空\\n";
            }
            else if (this.txtRePassWord.Text != this.txtNewPassWord.Text)
            {
                message += "两次输入密码不一致";
            }
            if (message != "")
            {
                UserInfoClear();
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }
            busertable.USER_ID = UserTable.USER_ID;
            busertable.PASSWORD = this.txtNewPassWord.Text;
            if (bll.UpdatePassWord(busertable))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"密码修改成功!\");processCloseAndRefreshParent();", true);
                                                                                                //return winOpen('Modify.aspx?','code=" + btnM.CommandArgument + "','200','420')"
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"密码修改失败\");", true);
            }


        }

        private void UserInfoClear()
        {
            this.txtNewPassWord.Text = "";
            this.txtOldPassWord.Text = "";
            this.txtRePassWord.Text = "";
        }
    }
}
