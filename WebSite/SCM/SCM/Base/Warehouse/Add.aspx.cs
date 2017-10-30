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
using System.Reflection;
using log4net;

namespace SCM.Web.Warehouse
{
    public partial class Add : BaseModalDialogPage
    {
        private Int32 FileLength = 0;
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
            BWarehouse bll = new BWarehouse();
            string message = "";
            if (this.txtCode.Text.Trim().Length == 0)
            {
                message += "编号不能为空！\\n";
            }
            else if (bll.Exists(this.txtCode.Text.Trim()))
            {
                message += "编号已经存在！\\n";
            }
            if (this.txtName.Text.Trim().Length == 0)
            {
                message += "门店名称不能为空！\\n";
            }
            if (this.txtDepartment_Code.Text.Trim().Length == 0)
            {
                message += "部门不能为空！\\n";
            }
            BaseWarehouseTable warehouseTable = new BaseWarehouseTable();
            warehouseTable.CODE = this.txtCode.Text;
            warehouseTable.NAME = this.txtName.Text;
            warehouseTable.TYPE = Convert.ToInt32(this.txtType.Value);
            warehouseTable.ATTRIBUTE1 = this.txtAttribute1.Text;
            warehouseTable.ATTRIBUTE2 = this.txtAttribute2.Text;
            warehouseTable.ATTRIBUTE2 = this.txtAttribute3.Text;
            warehouseTable.DEPARTMENT_CODE = this.txtDepartment_Code.Text;
            try
            {
                BaseUserTable userTable = (BaseUserTable)Session["UserInfo"];
                warehouseTable.CREATE_USER = userTable.USER_ID;
                warehouseTable.LAST_UPDATE_USER = userTable.USER_ID;
            }
            catch { }
            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
                Clear();
            }
            if (bll.Add(warehouseTable) > 0)
            {

                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"添加成功！\");processCloseAndRefreshParent();", true);
            }

        }
        public void Clear()
        {
            this.txtCode.Text = "";
            this.txtName.Text = "";
            this.txtAttribute1.Text = "";
            this.txtAttribute2.Text = "";
            this.txtAttribute3.Text = "";
        }

        protected void Wearhouse_change(object sender, EventArgs e)
        {
            BCommon bCommon = new BCommon();
            if (this.txtDepartment_Code.Text.Trim() == "")
            {
                this.lblWarehouseName.Text = "";
                this.txtDepartment_Code.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_DEPARTMENT", txtDepartment_Code.Text, "");
            if (table != null)
            {
                this.lblWarehouseName.Text = table.Name;
                this.txtDepartment_Code.Text = table.Code;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"部门不存在！\");", true);
                this.lblWarehouseName.Text = "";
                this.txtDepartment_Code.Text = "";
            }
        }
    }
}
