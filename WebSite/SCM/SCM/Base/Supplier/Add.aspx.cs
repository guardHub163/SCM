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
using SCM.Model.Base;
using System.Reflection;
using log4net;

namespace SCM.Web.Supplier
{
    public partial class Add : BaseModalDialogPage
    {
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
            BSupplier bll = new BSupplier();
            string message = "";
            if (this.txtCode.Text.Trim().Length == 0)
            {
                message += "编号不能为空！\\n";
            }else if (bll.Exists(this.txtCode.Text.Trim()))
            {
                message += "编号已经存在！\\n";
            }
            if (this.txtName.Text.Trim().Length == 0)
            {
                message += "名称不能为空！\\n";
            }
            if (this.selInputType.Value.Length == 0)
            {
                message += "类型不能为空！\\n";
            }
            if (this.txtAddress.Text.Trim().Length == 0)
            {
                message += "地址不能为空！\\n";
            }
            if (this.txtWarehouseCode.Text.Trim().Length == 0) 
            {
                message += "仓库不能为空！\\n";
            }
            //if (this.txtPost_code.Text.Trim().Length == 0)
            //{
            //    message += "邮政编码不能为空！\\n";
            //}
            if (!PageValidate.IsNumber(this.txtTel.Text.Trim()))
            {
                message += "电话号码只能是数字！\\n";
            }
            if (this.txtEmail.Text.Trim().Length != 0)
            {
                if(!PageValidate.IsEmail1(txtEmail.Text.Trim()))
                {
                    message += "电子邮件格式不对！\\n";
                }
            }
            BaseSupplierTable supplierTable = new BaseSupplierTable();
            supplierTable.CODE = this.txtCode.Text;
            supplierTable.NAME = this.txtName.Text;
            supplierTable.NAME_SHORT = this.txtName_short.Text;
            supplierTable.ADDRESS = this.txtAddress.Text;
            supplierTable.POST_CODE = this.txtPost_code.Text;
            supplierTable.TEL = this.txtTel.Text;
            supplierTable.FAX = this.txtFax.Text;
            supplierTable.CONTACT = this.txtContact.Text;
            supplierTable.TYPE = Convert.ToInt32(this.selInputType.Value);
            supplierTable.WAREHOUSE_CODE = this.txtWarehouseCode.Text;
            supplierTable.EMAIL = this.txtEmail.Text;
            supplierTable.ATTRIBUTE1 = this.txtAttribute1.Text;
            supplierTable.ATTRIBUTE2 = this.txtAttribute2.Text;
            supplierTable.ATTRIBUTE3 = this.txtAttribute3.Text;
            try
            {
                BaseUserTable userTable = (BaseUserTable)Session["UserInfo"];
                supplierTable.CREATE_USER = userTable.USER_ID;
                supplierTable.LAST_UPDATE_USER = userTable.USER_ID;
            }
            catch { }

            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
                Clear();
            }
            if (bll.Add(supplierTable) > 0)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"添加成功！\");processCloseAndRefreshParent();", true);
                //Clear();
            }
        }

        private void Clear()
        {
            this.txtCode.Text = "";
            this.txtName.Text = "";
            this.txtName_short.Text = "";
            this.txtAddress.Text = "";
            this.txtPost_code.Text = "";
            this.txtTel.Text = "";
            this.txtFax.Text = "";
            this.txtContact.Text = "";
            this.txtEmail.Text = "";
            this.txtAttribute1.Text = "";
            this.txtAttribute2.Text = "";
            this.txtAttribute3.Text = "";
        }
        protected void Warehouse_Change(object sender, EventArgs e)
        {
            if (txtWarehouseCode.Text.Trim() == "")
            {
                this.txtWarehouseCode.Text = "";
                this.lblWarehouseName.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_WAREHOUSE", txtWarehouseCode.Text.Trim(), "");
            if (table != null)
            {
                this.txtWarehouseCode.Text = table.Code;
                this.lblWarehouseName.Text = table.Name;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"出库仓库不存在!\");", true);
                this.txtWarehouseCode.Text = "";
                this.lblWarehouseName.Text = "";
            }
        }
}
}
