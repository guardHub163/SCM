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

namespace SCM.Web.Product
{
    public partial class Add : BaseModalDialogPage
    {
        BCommon bCommon = new BCommon();
        BProduct bll = new BProduct();
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
                message += "姓名不能为空！\\n";
            }
            if (this.txtStyleCode.Text.Trim().Length == 0) 
            {
                message += "款式不能为空！\\n";
            }
            if (this.txtProductGroupCode.Text.Trim().Length == 0) 
            {
                message += "种类不能为空！\\n";
            }
            if (this.txtSizeCode.Text.Trim().Length == 0) 
            {
                message += "尺码不能为空！\\n";
            }
            if (this.txtUnitCode.Text.Trim().Length == 0) 
            {
                message += "单位不能为空！\\n";
            }
            if (this.txtColorCode.Text.Trim().Length ==0) 
            {
                message += "颜色不能为空！\\n";
            }
            BaseProductTable productTable = new BaseProductTable();
            productTable.CODE = this.txtCode.Text;
            productTable.NAME = this.txtName.Text;
            productTable.STYLE = this.txtStyleCode.Text;
            productTable.GROUP_CODE = this.txtProductGroupCode.Text;
            productTable.COLOR = this.txtColorCode.Text;
            productTable.SIZE = this.txtSizeCode.Text;
            productTable.UNIT_CODE = this.txtUnitCode.Text;
            productTable.ATTRIBUTE1 = this.txtAttribute1.Text;
            productTable.ATTRIBUTE2 = this.txtAttribute2.Text;
            productTable.ATTRIBUTE3 = this.txtAttribute3.Text;
            productTable.PRODUCT_SPEC = this.txtProduct_spec.Text;

                productTable.CREATE_USER = UserTable.USER_ID;
                productTable.LAST_UPDATE_USER = productTable.CREATE_USER;

            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
                Clear();
            }

            if (bll.Add(productTable) > 0) 
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"添加成功！\");processCloseAndRefreshParent();", true);
                Clear();
            }
        }

        private void Clear()
        {
            this.txtCode.Text = "";
            this.txtName.Text = "";
            this.txtProductGroupCode.Text = "";
            this.txtStyleCode.Text = "";
            this.txtSizeCode.Text = "";
            this.txtColorCode.Text = "";
            this.txtUnitCode.Text = "";
            this.txtAttribute1.Text = "";
            this.txtAttribute2.Text = "";
            this.txtAttribute3.Text = "";
            this.txtProduct_spec.Text = "";
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
        protected void StyleCode_Chanage(object sender, EventArgs e)
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
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"款式不存在！\");", true);
                this.lblStyleName.Text = "";
                this.txtStyleCode.Text = "";
            }
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
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"颜色不存在！\");", true);
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
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"尺码不存在！\");", true);
                this.lblSizeName.Text = "";
                this.txtSizeCode.Text = "";
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
