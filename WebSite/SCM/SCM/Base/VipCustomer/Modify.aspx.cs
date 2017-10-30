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

namespace SCM.Web.VipCustomer
{
    public partial class Modify : BaseModalDialogPage
    {
        BVipCustomer bll = new BVipCustomer();
        BCommon bCommon = new BCommon();
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {
                if (UserTable.ROLES_ID == 9) 
                {
                    txtPoints.Enabled = true;
                }
                if (Request.Params["code"] != null && Request.Params["code"].Trim() != "")
                {
                    string CODE = Request.Params["code"];
                    showInfo(CODE);
                }
            }
        }

        private void showInfo(string CODE)
        {  
            BaseVipCustomerTable VipTable = bll.GetModel(CODE);
            this.lblCode.Text = VipTable.CODE;
            this.txtName.Text = VipTable.NAME;
            this.lblLevel.Text = VipTable.VIP_LEVEL.ToString();
            this.txtAdress.Text = VipTable.ADDRESS;
            this.txtQQ.Text = VipTable.QQ;
            this.txtWW.Text = VipTable.WW;
            this.txtEmail.Text = VipTable.EMAIL;
            this.txtDepartmentCode.Text = VipTable.DEPARTMENT_CODE;
            this.lblDepartmentName.Text = VipTable.Department;
            this.lblSalesTime.Text = VipTable.LAST_SALES_DATE.ToString("yyyy/MM/dd");
            this.txtbirth.Text = VipTable.BIRTH_DATE.ToString("yyyy/MM/dd");
            this.txtDiscount.Text = VipTable.DISCOUNT_RATE.ToString();
            this.txtPoints.Text = VipTable.POINTS.ToString();
            this.txtAttribute1.Text = VipTable.ATTRIBUTE1;
            this.txtAttribute2.Text = VipTable.ATTRIBUTE2;
            this.txtAttribute3.Text = VipTable.ATTRIBUTE3;
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
            string message = "";
            if (this.txtName.Text.Trim().Length == 0)
            {
                message += "姓名不能为空！\\n";
            }
            if (this.txtDepartmentCode.Text.Trim().Length == 0)
            {
                message += "门店不能为空！\\n";
            }
            //if (this.txtAdress.Text.Trim().Length == 0)
            //{
            //    message += "地址不能为空！\\n";
            //}
            //if (this.txtQQ.Text.Trim().Length == 0)
            //{
            //    message += "QQ不能为空！\\n";
            //}
            //if (this.txtWW.Text.Trim().Length == 0)
            //{
            //    message += "旺旺不能为空！\\n";
            //}
            //if (this.txtEmail.Text.Trim().Length == 0)
            //{
            //    message += "电子邮件不能为空！\\n";
            //}
            //else if (!PageValidate.IsEmail1(this.txtEmail.Text.Trim()))
            //{
            //    message += "电子邮件格式有误！\\n";
            //}
            //if (this.txtbirth.Text.Trim().Length == 0)
            //{
            //    message += "生日不能为空！\\n";
            //}
            //if (this.txtDiscount.Text.Trim().Length == 0)
            //{
            //    message += "折扣不能为空！\\n";
            //}
            BaseVipCustomerTable VipTable = new BaseVipCustomerTable();
            VipTable.CODE = this.lblCode.Text.Trim();
            VipTable.NAME = this.txtName.Text.Trim();
            VipTable.DEPARTMENT_CODE = this.txtDepartmentCode.Text.Trim();
            VipTable.ADDRESS = this.txtAdress.Text.Trim();
            VipTable.QQ = this.txtQQ.Text.Trim();
            VipTable.WW = this.txtWW.Text.Trim();
            VipTable.EMAIL = this.txtEmail.Text.Trim();
            VipTable.BIRTH_DATE = CConvert.ToDateTime(this.txtbirth.Text.Trim());
            VipTable.LAST_SALES_DATE = CConvert.ToDateTime(this.lblSalesTime.Text.Trim());
            VipTable.DISCOUNT_RATE = CConvert.ToDecimal(this.txtDiscount.Text.Trim());
            VipTable.POINTS = CConvert.ToInt32(this.txtPoints.Text.Trim());
            VipTable.VIP_LEVEL = CConvert.ToInt32(this.lblLevel.Text.Trim());

            VipTable.LAST_UPDATE_USER = UserTable.USER_ID;

            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }
            if (bll.Update(VipTable))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"修改成功！\");processCloseAndRefreshParent();", true);
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
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"部门不存在！\");", true);
                this.lblDepartmentName.Text = "";
                this.txtDepartmentCode.Text = "";
            }
        }
        protected void txtbirth_TextChanged(object sender, EventArgs e)
        {
            if (!PageValidate.IsDateTime(txtbirth.Text.Trim()))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtbirth.ClientID + "').value='';", true);
            }
            else
            {
                txtbirth.Text = CConvert.ToDateTime(txtbirth.Text.Trim()).ToString("yyyy/MM/dd");
            }
        }
}
}
