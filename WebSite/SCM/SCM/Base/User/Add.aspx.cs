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

namespace SCM.Web.User
{
    public partial class Add : BaseModalDialogPage
    {
        private Int32 FileLength = 0;
        BUser bll = new BUser();
        BCommon bCommon = new BCommon();
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack) 
            {
                selUserType.DataSource = bCommon.GetNames("USER_TYPE").Tables[0];
                selUserType.DataTextField = "NAME"; //dropdownlist的Text的字段 
                selUserType.DataValueField = "CODE";//dropdownlist的Value的字段 
                selUserType.DataBind();
            }
        }


        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSave": //保存
                    Save(sender, e);
                    break;
            }
            return true;
        }

        private void Save(object sender, EventArgs e)
        {
            string strErr = "";
            this.txtSTYLE.Text = "1";
            if (this.txtUSER_ID.Text.Trim().Length == 0)
            {
                strErr += "用户名不能为空！\\n";
            }
            if (this.txtPASSWORD.Value.Trim().Length == 0)
            {
                strErr += "密码不能为空！\\n";
            }
            if (this.txtTRUE_NAME.Text.Trim().Length == 0)
            {
                strErr += "真实姓名不能为空！\\n";
            }
            if (this.txtDepartmentCode.Text.Trim().Length == 0)
            {
                strErr += "所属部门不能为空！\\n";
            }
            if (!PageValidate.IsNumber(txtROLES_ID.Text))
            {
                strErr += "权限编号格式错误！\\n";
            }
            if (!PageValidate.IsNumber(txtSTYLE.Text))
            {
                strErr += "页面风格编号格式错误！\\n";
            }
            if (bll.Exists(this.txtUSER_ID.Text.Trim()))
            {
                strErr = this.txtUSER_ID.Text.Trim() + "用户己经存在! \\n";
            }
            if (this.txtPASSWORD.Value.Trim() != txtRePASSWORD.Value.Trim())
            {
                strErr += "两次输入密码不一致! \\n";
            }

            if (strErr != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"" + strErr + "\");", true);
                return;
            }            
            string SEX = this.sex1.Text;
            if (this.sex2.Checked)
            {
                SEX = this.sex2.Text;
            }

            BaseUserTable uTable = new BaseUserTable();
            uTable.USER_ID = this.txtUSER_ID.Text;
            uTable.PASSWORD = this.txtPASSWORD.Value;
            uTable.TRUE_NAME = this.txtTRUE_NAME.Text;
            uTable.SEX = SEX;
            uTable.PHONE = this.txtPHONE.Text;
            uTable.EMAIL = this.txtEMAIL.Text;
            uTable.DEPARTMENT_CODE = this.txtDepartmentCode.Text;
            uTable.USER_TYPE = this.selUserType.SelectedItem.Value;
            uTable.ROLES_ID = int.Parse(this.txtROLES_ID.Text);
            uTable.STYLE = int.Parse(this.txtSTYLE.Text);
            uTable.CREATE_USER_ID = UserTable.USER_ID;
            uTable.LAST_UPDATE_USER_ID = uTable.CREATE_USER_ID;
            uTable.SUPPLIER_CODE = this.txtSupplierCode.Text;       

            if (bll.Add(uTable) > 0)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"保存成功!\");processCloseAndRefreshParent();", true);
            }
            else 
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"保存失败\");", true);
            }
        }

        protected void Department_Change(object sender, EventArgs e)
        {
            if (this.txtDepartmentCode.Text.Trim() == "")
            {
                this.lblDepartmentName.Text = "";
                this.txtDepartmentCode.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_DEPARTMENT", this.txtDepartmentCode.Text, "");
            if (table != null)
            {
                this.lblDepartmentName.Text = table.Name;
                this.txtDepartmentCode.Text = table.Code;
            }
            else
            {
                this.lblDepartmentName.Text = "";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"部门不存在！\");document.getElementById('" + txtDepartmentCode.ClientID + "').value='';", true);
            }
        }

        protected void Supplier_Change(object sender, EventArgs e)
        {
            if (txtSupplierCode.Text.Trim() == "")
            {
                this.txtSupplierCode.Text = "";
                this.lblSupplierName.Text = "";
                return;
            }

            BaseMaster table = bCommon.GetBaseMaster("BASE_SUPPLIER", txtSupplierCode.Text.Trim(), "");
            if (table != null)
            {
                this.txtSupplierCode.Text = table.Code;
                this.lblSupplierName.Text = table.Name;
            }
            else
            {
                this.lblSupplierName.Text = "";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"供应商不存在!\");document.getElementById('" + txtSupplierCode.ClientID + "').value='';", true);
            }
        }

    }// end class
}
