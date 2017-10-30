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
using System.Reflection;
using log4net;


namespace SCM.Web.VipCustomer
{
    public partial class Add : BaseModalDialogPage
    {
        BVipCustomer bll = new BVipCustomer();
        BCommon bCommon = new BCommon();
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            this.txtSalesTime.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.txtPoints.Text = "0";
            this.txtLevel.Text = "0";
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
            if (this.txtDepartmentCode.Text.Trim().Length == 0)
            {
                message += "门店不能为空！\\n";
            }
           
            BaseVipCustomerTable VipTable = new BaseVipCustomerTable();
            VipTable.CODE = this.txtCode.Text.Trim();
            VipTable.NAME = this.txtName.Text.Trim();
            VipTable.DEPARTMENT_CODE = this.txtDepartmentCode.Text.Trim();
            VipTable.ADDRESS = this.txtAdress.Text.Trim();
            VipTable.QQ = this.txtQQ.Text.Trim();
            VipTable.WW = this.txtWW.Text.Trim();
            VipTable.EMAIL = this.txtEmail.Text.Trim();
            VipTable.BIRTH_DATE = Convert.ToDateTime(txtBirthDate.Text.Trim());
            VipTable.LAST_SALES_DATE = Convert.ToDateTime(this.txtSalesTime.Text.Trim());
            VipTable.DISCOUNT_RATE = Convert.ToDecimal(this.txtDiscount.Text.Trim());
            VipTable.POINTS = Convert.ToInt32(this.txtPoints.Text.Trim());
            VipTable.VIP_LEVEL = Convert.ToInt32(this.txtLevel.Text.Trim());

            VipTable.CREATE_USER = UserTable.USER_ID;
            VipTable.LAST_UPDATE_USER = VipTable.CREATE_USER;

            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }
            if (bll.Add(VipTable) > 0)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"添加成功！\");processCloseAndRefreshParent();", true);
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
        protected void BirthDate_Changed(object sender, EventArgs e)
        {

            if (!PageValidate.IsDateTime(txtBirthDate.Text.Trim()))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtBirthDate.ClientID + "').value='';", true);
            }
            else
            {
                txtBirthDate.Text = Convert.ToDateTime(txtBirthDate.Text.Trim()).ToString("yyyy/MM/dd");
            }
        }
}
}
