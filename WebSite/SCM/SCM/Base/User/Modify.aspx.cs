using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using SCM.Bll;
using SCM.Model;
using SCM.Common;
using System.IO;
using System.Reflection;
using log4net;

namespace SCM.Web.User
{
    public partial class Modify : BaseModalDialogPage
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
                this.lblPassWord.Text = "123456";
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    int ID = (Convert.ToInt32(Request.Params["id"]));
                    ShowInfo(ID);                  
                }
            }
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSave": //保存
                    Save(sender, e);
                    break;
                case "btnUpdate": //保存
                    Update(sender, e);
                    break;
            }
            return true;
        }

        private void Update(object sender, EventArgs e)
        {
            BaseUserTable usertable = new BaseUserTable();
            usertable.USER_ID = this.lblUSER_ID.Text.Trim();
            usertable.PASSWORD = "123456";
            if (bll.UpdatePassWord(usertable))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"重置密码成功\");", true);
            }
        }


        private void ShowInfo(int ID)
        {
            BaseUserTable uTable = bll.GetModel(ID);
            this.txtId.Text = uTable.ID.ToString();
            this.lblUSER_ID.Text = uTable.USER_ID;
            this.txtTRUE_NAME.Text = uTable.TRUE_NAME;
            if (uTable.SEX == "男")
            {
                sex1.Checked = true;
            }
            else 
            {
                sex2.Checked = true;
            }
            this.txtPHONE.Text = uTable.PHONE;
            this.txtEMAIL.Text = uTable.EMAIL;
            this.txtDepartmentCode.Text = uTable.DEPARTMENT_CODE;
            this.lblDepartmentName.Text = uTable.DEPARTMENT_NAME;
            this.txtSupplierCode.Text = uTable.SUPPLIER_CODE;
            this.lblSupplierName.Text = uTable.SUPPLIER_NAME;
            this.selUserType.SelectedValue = uTable.USER_TYPE;
            this.txtROLES_ID.Text = uTable.ROLES_ID.ToString();
            this.txtSTYLE.Text = uTable.STYLE.ToString();
            this.imgPhoto.Src = "../../Image.aspx?TYPE=USER&FILE_NAME=" + uTable.PHOTO_PATH;

        }

        private void Save(object sender, EventArgs e)
        {
            string strErr = "";
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
            BaseUserTable uTable = bll.GetModel(Convert.ToInt32(this.txtId.Text.Trim()));
            uTable.USER_ID = this.lblUSER_ID.Text;
            uTable.TRUE_NAME = this.txtTRUE_NAME.Text;
            uTable.SEX = SEX;
            uTable.PHONE = this.txtPHONE.Text;
            uTable.EMAIL = this.txtEMAIL.Text;
            uTable.DEPARTMENT_CODE = this.txtDepartmentCode.Text;
            uTable.USER_TYPE = this.selUserType.SelectedItem.Value;
            uTable.ROLES_ID = int.Parse(this.txtROLES_ID.Text);
            uTable.STYLE = int.Parse(this.txtSTYLE.Text);
            uTable.SUPPLIER_CODE = this.txtSupplierCode.Text;
            uTable.LAST_UPDATE_USER_ID = UserTable.USER_ID;           

            if (bll.Update(uTable))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"修正成功!\");processCloseAndRefreshParent();", true);
            }
            else 
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"修正失败\");", true);
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
    }
}
