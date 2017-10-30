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
using log4net;
using System.Reflection;

namespace SCM.Web.Department
{
    public partial class Add : BaseModalDialogPage
    {
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
            BDepartment bll = new BDepartment();
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
                message += "部门名称不能为空！\\n";
            }
            BaseDepartmentTable departTable = new BaseDepartmentTable();
            departTable.CODE = this.txtCode.Text;
            departTable.NAME = this.txtName.Text;
            departTable.PARENT_CODE = this.txtDepartment_Code.Text;
            departTable.ATTRIBUTE1 = this.txtAttribute1.Text;
            departTable.ATTRIBUTE2 = this.txtAttribute2.Text;
            departTable.ATTRIBUTE3 = this.txtAttribute3.Text;
                departTable.CREATE_USER = UserTable.USER_ID;
                departTable.LAST_UPDATE_USER = departTable.CREATE_USER;

            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
                Clear();
            }
            if (bll.Add(departTable)>0) 
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"添加成功！\");processCloseAndRefreshParent();", true);
            }
        }

        private void Clear() 
        {
            this.txtCode.Text = "";
            this.txtName.Text = "";
            this.txtDepartment_Code.Text = "";
            this.txtAttribute1.Text = "";
            this.txtAttribute2.Text = "";
            this.txtAttribute3.Text = "";
        }
        protected void Department_change(object sender, EventArgs e)
        {
            BCommon bCommon = new BCommon();
            if (this.txtDepartment_Code.Text.Trim() == "") 
            {
                this.lblWarehouseName.Text = "";
                this.txtDepartment_Code.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_DEPARTMENT",this.txtDepartment_Code.Text, "");
            if (table != null)
            {
                this.lblWarehouseName.Text = table.Name;
                this.txtDepartment_Code.Text = table.Code;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"上级部门不存在！\");", true);
                this.lblWarehouseName.Text = "";
                this.txtDepartment_Code.Text = "";
            }
        }
}
}