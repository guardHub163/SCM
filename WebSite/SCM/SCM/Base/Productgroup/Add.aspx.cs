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

namespace SCM.Web.Productgroup
{
    public partial class Add : BaseModalDialogPage
    {
        BCommon bCommon = new BCommon();
        BProductGroup bll = new BProductGroup();
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
            string where = "1=1";
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
                message += "品种名称不能为空！\\n";
            }
            if (bll.GetRecordCount(where) > 0)
            {
                if (this.txtProductGroupCode.Text.Trim().Length == 0)
                {
                    message += "种类不能为空！\\n";
                }
            }
            BaseProductGroupTable productgroup = new BaseProductGroupTable();
            productgroup.CODE = this.txtCode.Text.Trim();
            productgroup.NAME = this.txtName.Text.Trim();
            productgroup.PARENT_CODE = Convert.ToString(this.txtProductGroupCode.Text.Trim());
            productgroup.ATTRIBUTE1 = this.txtAttribute1.Text;
            productgroup.ATTRIBUTE2 = this.txtAttribute2.Text;
            productgroup.ATTRIBUTE3 = this.txtAttribute3.Text;

                productgroup.CREATE_USER = UserTable.USER_ID;
                productgroup.LAST_UPDATE_USER = productgroup.CREATE_USER;

            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }
            if (bll.Add(productgroup) > 0)
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