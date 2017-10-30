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
using SCM.Common;
using SCM.Model.Base;
using System.Reflection;
using log4net;

namespace SCM.Web.Supplier
{
    public partial class Modify : BaseModalDialogPage
    {
        BCommon bCommon = new BCommon();
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {
                if (Request.Params["code"] != null && Request.Params["code"].Trim() != "")
                {
                    string ID = Request.Params["code"];
                    Showinfo(ID);
                }
            }
        }

        private void Showinfo(string CODE)
        {
            BSupplier bll = new BSupplier();
            BaseSupplierTable supplierTable = bll.GetModel(CODE);
            this.txtCode.Text = supplierTable.CODE;
            this.txtName.Text = supplierTable.NAME;
            this.txtName_short.Text = supplierTable.NAME_SHORT;
            this.txtAddress.Text = supplierTable.ADDRESS;
            this.txtPost_code.Text = supplierTable.POST_CODE;
            this.txtTel.Text = supplierTable.TEL;
            this.selInputType.Value = Convert.ToString(supplierTable.TYPE);
            this.txtFax.Text = supplierTable.FAX;
            this.txtContact.Text = supplierTable.CONTACT;
            this.txtEmail.Text = supplierTable.EMAIL;
            this.txtWarehouseCode.Text = supplierTable.WAREHOUSE_CODE;
            this.lblWarehouseName.Text = supplierTable.Warehouse_name;
            this.txtAttribute1.Text = supplierTable.ATTRIBUTE1;
            this.txtAttribute2.Text = supplierTable.ATTRIBUTE2;
            this.txtAttribute3.Text = supplierTable.ATTRIBUTE3;
        }
        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSave":
                    btnSave_Click(sender, e);
                    break;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            BSupplier bll = new BSupplier();
            string message = "";
            if (this.txtName.Text.Trim().Length == 0)
            {
                message += "名称不能为空！\\n";
            }
            if (this.txtName_short.Text.Trim().Length == 0)
            {
                message += "简称不能为空！\\n";
            }
            else if (bll.Exists(this.txtName_short.Text.Trim()))
            {
                message += "简称已经存在！\\n";
            }
            if (this.selInputType.Value.Length == 0)
            {
                message += "类型不能为空！\\n";
            }
            if (this.txtAddress.Text.Trim().Length == 0)
            {
                message += "地址不能为空！\\n";
            }
            if (!PageValidate.IsNumber(this.txtTel.Text.Trim()))
            {
                message += "电话号码只能是数字！\\n";
            }
            if (this.txtContact.Text.Trim().Length == 0)
            if (this.txtEmail.Text.Trim().Length != 0)
            {
                if (!PageValidate.IsEmail1(txtEmail.Text.Trim())) 
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
            supplierTable.EMAIL = this.txtEmail.Text;
            supplierTable.WAREHOUSE_CODE = this.txtWarehouseCode.Text;
            supplierTable.ATTRIBUTE1 = this.txtAttribute1.Text;
            supplierTable.ATTRIBUTE2 = this.txtAttribute2.Text;
            supplierTable.ATTRIBUTE3 = this.txtAttribute3.Text;
            try
            {
                BaseUserTable userTable = (BaseUserTable)Session["UserInfo"];
                supplierTable.LAST_UPDATE_USER = userTable.USER_ID;
            }
            catch { }

            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }
            if (bll.Update(supplierTable))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"编辑成功！\");processCloseAndRefreshParent();", true);
            }
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
