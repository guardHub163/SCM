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
using log4net;
using System.Reflection;

namespace SCM.Web.SalesPromotion
{
    public partial class Modify : BaseModalDialogPage
    {
        BSalesPromotion bsales = new BSalesPromotion();
        BCommon bCommon = new BCommon();
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {
                if (Request.Params["code"] != null && Request.Params["code"].Trim() != "")
                {
                    string CODE = Request.Params["code"];
                    showInfo(CODE);
                }
            }
        }

        private void showInfo(string CODE)
        {
            BaseSalesPromotionTable bstable = bsales.GetModel(CODE);
            this.lblCode.Text = bstable.CODE;
            this.lblDepartmentName.Text = bstable.DEPARTMENT_NAME;
            this.txtDepartmentCode.Text = bstable.DEPARTMENT_CODE;
            this.txtName.Text = bstable.NAME;
            this.txtFromDate.Text = bstable.START_TIME.ToString("yyyy/MM/dd");
            this.txtToDate.Text = bstable.END_TIME.ToString("yyyy/MM/dd");
            this.txtProperty1.Text = bstable.PROPERTY1;
            this.txtProperty2.Text = bstable.PROPERTY2;
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
            if (txtDepartmentCode.Text.Trim().Length == 0)
            {
                message += "部门不能为空！\\n";
            }
            if (txtName.Text.Trim().Length == 0)
            {
                message += "名称不能为空！\\n";
            }
            if (txtProperty1.Text.Trim().Length == 0)
            {
                message += "满额不能为空！\\n";
            }
            else if (!PageValidate.IsNumber(txtProperty1.Text.Trim()))
            {
                message += "满额格式不对！\\n";
            }
            if (txtProperty2.Text.Trim().Length == 0)
            {
                message += "减免不能为空！\\n";
            }
            else if (!PageValidate.IsNumber(txtProperty2.Text.Trim()))
            {
                message += "减免格式不对！\\n";
            }
            if (txtFromDate.Text.Trim().Length == 0)
            {
                message += "开始时间不能为空！\\n";
            }
            if (txtToDate.Text.Trim().Length == 0)
            {
                message += "结束时间不能为空！\\n";
            }
            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }
            BaseSalesPromotionTable promotion = new BaseSalesPromotionTable();
            promotion.CODE = this.lblCode.Text.Trim();
            promotion.NAME = this.txtName.Text.Trim();
            promotion.DEPARTMENT_CODE = this.txtDepartmentCode.Text.Trim();
            promotion.PROPERTY1 = this.txtProperty1.Text.Trim();
            promotion.PROPERTY2 = this.txtProperty2.Text.Trim();
            promotion.START_TIME = Convert.ToDateTime(this.txtFromDate.Text.Trim());
            promotion.END_TIME = Convert.ToDateTime(this.txtToDate.Text.Trim());

            promotion.CREATE_USER = UserTable.USER_ID;
            promotion.LAST_UPDATE_USER = promotion.CREATE_USER;


            if (bsales.Update(promotion))
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
        protected void txtProperty2_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtProperty2.Text.Trim()) > Convert.ToDecimal(txtProperty1.Text.Trim()))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"减免不能大于满额\");", true);
                this.txtProperty2.Text = "";
                return;
            }
        }
        protected void ToDate_Changed(object sender, EventArgs e)
        {
            if (txtToDate.Text.Trim() != "")
            {
                if (!PageValidate.IsDateTime(txtToDate.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtToDate.ClientID + "').value='';", true);
                    return;
                }
                else
                {
                    txtToDate.Text = Convert.ToDateTime(txtToDate.Text.Trim()).ToString("yyyy/MM/dd");
                }
                if (this.txtFromDate.Text.Trim() != "" && this.txtToDate.Text.Trim() != "")
                {
                    if (Convert.ToDateTime(txtToDate.Text) < Convert.ToDateTime(txtFromDate.Text))
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"开始时间不能大于结束时间!\");document.getElementById('" + txtToDate.ClientID + "').value='';", true);
                    }
                }
            }
        }
        protected void FromDate_Changed(object sender, EventArgs e)
        {
            if (txtFromDate.Text.Trim() != "")
            {
                if (!PageValidate.IsDateTime(txtFromDate.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtFromDate.ClientID + "').value='';", true);
                }
                else
                {
                    txtFromDate.Text = Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("yyyy/MM/dd");
                }
                if (this.txtFromDate.Text.Trim() != "" && this.txtToDate.Text.Trim() != "")
                {
                    if (Convert.ToDateTime(txtToDate.Text) < Convert.ToDateTime(txtFromDate.Text))
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"开始时间不能大于结束时间!\");document.getElementById('" + txtToDate.ClientID + "').value='';", true);
                    }
                }
            }
        }
    }
}
