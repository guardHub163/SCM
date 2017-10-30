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
using System.Reflection;
using log4net;

namespace SCM.Web.Warehouse
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
            BWarehouse bll = new BWarehouse();
            string message = "";
            if (this.txtName.Text.Trim().Length == 0)
            {
                message += "门店名称不能为空！\\n"; 
            }
            if (this.txtDepartmentCode.Text.Trim().Length == 0)
            {
                message += "部门编号不能为空！\\n"; 
            }
            BaseWarehouseTable housertable = new BaseWarehouseTable();
            housertable.CODE = this.txtCode.Text;
            housertable.NAME = this.txtName.Text;
            housertable.TYPE =int.Parse(this.txtType.Value);
            housertable.DEPARTMENT_CODE = this.txtDepartmentCode.Text;
            housertable.ATTRIBUTE1 = this.txtAttribute1.Text;
            housertable.ATTRIBUTE2 = this.txtAttribute2.Text;
            housertable.ATTRIBUTE3 = this.txtAttribute3.Text;
            try
            {
                BaseUserTable userTable = (BaseUserTable)Session["UserInfo"];
                housertable.LAST_UPDATE_USER = userTable.USER_ID;
            }
            catch { }
            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }
            if (bll.Update(housertable))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"修改成功！\");processCloseAndRefreshParent();", true);
            }
        }

        private void showInfo(string CODE)
        {
            BWarehouse bll = new BWarehouse();
            BaseWarehouseTable housertable = bll.GetModel(CODE);
            this.txtCode.Text = housertable.CODE;
            this.txtName.Text = housertable.NAME;
            this.txtType.Value = housertable.TYPE.ToString();
            this.txtDepartmentCode.Text =housertable.DEPARTMENT_CODE;
            this.lblDepartmentName.Text = housertable.DEPARTMENT_NAME;
            this.txtAttribute1.Text = housertable.ATTRIBUTE1;
            this.txtAttribute2.Text = housertable.ATTRIBUTE2;
            this.txtAttribute3.Text = housertable.ATTRIBUTE3;
        }
        protected void Department_Change(object sender, EventArgs e)
        {
            if (this.txtDepartmentCode.Text.Trim() == "") 
            {
                this.lblDepartmentName.Text = "";
                this.txtDepartmentCode.Text = "";
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_DEPARTMENT", txtDepartmentCode.Text, "");
            if (table != null)
            {
                this.lblDepartmentName.Text = table.Name;
                this.txtDepartmentCode.Text = table.Code;
            }
            else 
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"部门不存在！\");", true);
                this.lblDepartmentName.Text = "";
                this.txtDepartmentCode.Text = "";
            }
        }
    }//end classs
}