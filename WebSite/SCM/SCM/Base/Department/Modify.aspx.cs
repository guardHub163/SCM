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
using log4net;
using System.Reflection;

namespace SCM.Web.Department
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
                    showInfo(ID);
                }

            }
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
            BDepartment bll = new BDepartment();
            string message = "";
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
            try
            {
                BaseUserTable userTable = (BaseUserTable)Session["UserInfo"];
                departTable.LAST_UPDATE_USER = userTable.USER_ID;
            }
            catch { }
            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }
            if (bll.Update(departTable)) 
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"编辑成功！\");processCloseAndRefreshParent();", true);
            }
        }
        private void showInfo(string CODE) 
        {
            BDepartment bll = new BDepartment();
            BaseDepartmentTable departable = bll.GetModel(CODE);
            this.txtCode.Text = departable.CODE;
            this.txtName.Text = departable.NAME;
            this.txtDepartment_Code.Text = departable.PARENT_CODE;
            this.lblWarehouseName.Text = departable.NAME;
            this.txtAttribute1.Text = departable.ATTRIBUTE1;
            this.txtAttribute2.Text = departable.ATTRIBUTE2;
            this.txtAttribute3.Text = departable.ATTRIBUTE3;
        }
        protected void Department_Change(object sender, EventArgs e)
        {
            BCommon bCommon = new BCommon();
            if (txtDepartment_Code.Text.Trim() == "") 
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
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"上级部门不存在！\");", true);
                this.lblWarehouseName.Text = "";
                this.txtDepartment_Code.Text = "";
            }
        }
}
}
