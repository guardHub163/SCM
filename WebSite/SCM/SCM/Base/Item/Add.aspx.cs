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

namespace SCM.Web.Item
{
    public partial class Add : BaseModalDialogPage
    {
        BItem bll = new BItem();
        BCommon bCommon = new BCommon();
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
                message += "名称不能为空！\\n";
            }
            if (this.txtUnitCode.Text.Trim().Length == 0) 
            {
                message += "单位不能为空！\\n";
            }
            BaseItemTable productTable = new BaseItemTable();
            productTable.CODE = this.txtCode.Text;
            productTable.NAME = this.txtName.Text;
            productTable.UNIT_CODE = this.txtUnitCode.Text;
            productTable.SPEC = this.txtItemspec.Text;
            productTable.ATTRIBUTE1 = this.txtAttribute1.Text;
            productTable.ATTRIBUTE2 = this.txtAttribute2.Text;
            productTable.ATTRIBUTE3 = this.txtAttribute3.Text;
            productTable.ATTRIBUTE4 = this.txtAttribute4.Text;
            productTable.ATTRIBUTE5 = this.txtAttribute5.Text;

                productTable.CREATE_USER = UserTable.USER_ID;
                productTable.LAST_UPDATE_USER = productTable.CREATE_USER;

            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }

            if (bll.Add(productTable) > 0)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"添加成功！\");processCloseAndRefreshParent();", true);
            }
        }
        protected void UnitCode_Chanage(object sender, EventArgs e)
        {
            if (this.txtUnitCode.Text.Trim() == "")
            {
                this.lblUnitName.Text = "";
                this.txtUnitCode.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_UNIT", txtUnitCode.Text.Trim(), "");
            if (table != null)
            {
                this.lblUnitName.Text = table.Name;
                this.txtUnitCode.Text = table.Code;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"单位不存在！\");", true);
                this.lblUnitName.Text = "";
                this.txtUnitCode.Text = "";
            }
        }
    }
}
