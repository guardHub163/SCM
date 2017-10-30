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
using System.Reflection;
using log4net;

namespace SCM.Web.Size
{
    public partial class Add : BaseModalDialogPage
    {
        BSize bll = new BSize();
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
            else if (this.txtProductGroupCode.Text.Trim().Length == 0)
            {
                message += "种类不能为空！\\n";
            }
            else if (bll.Exists(txtCode.Text.Trim(), txtProductGroupCode.Text.Trim()))
            {
                message += "编号已经存在！\\n";
            }
            if (this.txtName.Text.Trim().Length == 0)
            {
                message += "尺码不能为空！\\n";
            }
            
            BaseSizeTable sizetable = new BaseSizeTable();
            sizetable.CODE = this.txtCode.Text;
            sizetable.NAME = this.txtName.Text;
            sizetable.PRODUCT_GROUP_CODE = this.txtProductGroupCode.Text;
            sizetable.REFERENCE_PERCENTAGE = CConvert.ToDecimal(this.txtReference.Text);
            sizetable.ATTRIBUTE1 = this.txtAttribute1.Text;
            sizetable.ATTRIBUTE2 = this.txtAttribute2.Text;
            sizetable.ATTRIBUTE3 = this.txtAttribute3.Text;

            sizetable.CREATE_USER = UserTable.USER_ID;
            sizetable.LAST_UPDATE_USER = sizetable.CREATE_USER;

            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }
            if (bll.Add(sizetable) > 0)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"添加成功！\");processCloseAndRefreshParent();", true);
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
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"种类不存在！\");", true);
                this.lblProductGroupName.Text = "";
                this.txtProductGroupCode.Text = "";
            }
        }
    }
}
