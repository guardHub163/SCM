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

namespace SCM.Web.Productprice
{
    public partial class Add : BaseModalDialogPage
    {
        BCommon bCommon = new BCommon();
        BProductprice bll = new BProductprice();
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {
                ddlPrice.DataSource = bCommon.GetNames("PRICE_CODE").Tables[0];
                ddlPrice.DataTextField = "NAME"; //dropdownlist的Text的字段 
                ddlPrice.DataValueField = "CODE";//dropdownlist的Value的字段   
                ddlPrice.DataBind();
                this.txtFromDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                this.txtToDate.Text = DateTime.Now.ToString("yyyy/MM/dd");

            }
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
            if (this.txtPrice.Text.Trim().Length == 0)
            {
                message += "价格不能为空！\\n";
            }
            else if (!PageValidate.IsNumber(this.txtPrice.Text.Trim()))
            {
                message += "价格格式不对！\\n";
            }
            else if (Convert.ToDecimal(txtPrice.Text) < 0)
            {
                message += "价格不能小于0！\\n";
            }
            if (this.txtOriPrice.Text.Trim().Length == 0)
            {
                message += "原价不能为空！\\n";
            }
            if (this.txtDepartment_Code.Text.Trim().Length == 0)
            {
                message += "部门不能为空！\\n";
            }
            if (this.txtStyleCode.Text.Trim().Length == 0)
            {
                message += "样式不能为空！\\n";
            }
            if (this.txtFromDate.Text == "")
            {
                message += "起始时间不能为空！\\n";
            }
            if (this.txtToDate.Text == "")
            {
                message += "结束时间不能为空！\\n";
            }
            if (this.txtFromDate.Text != "" && this.txtToDate.Text != "")
            {
                if (Convert.ToDateTime(this.txtFromDate.Text) > Convert.ToDateTime(this.txtToDate.Text))
                {
                    message += "起始时间不能小于结束时间！\\n";
                }
            }

            BaseProductpriceTable priceTable = new BaseProductpriceTable();
            priceTable.SALES_PRICE = Convert.ToDecimal(this.txtPrice.Text);
            priceTable.DEPARTMENT_CODE = this.txtDepartment_Code.Text;
            priceTable.STYLE_CODE = this.txtStyleCode.Text;
            if (rdo1.Checked)
            {
                priceTable.DEFAULT_FLAG = 1;
            }
            if (rdo2.Checked)
            {
                priceTable.DEFAULT_FLAG = 0;
            }
            priceTable.START_DATE = Convert.ToDateTime(this.txtFromDate.Text);
            priceTable.END_DATE = Convert.ToDateTime(this.txtToDate.Text);
            priceTable.PRICE_CODE = this.ddlPrice.SelectedItem.Value;
            priceTable.ATTRIBUTE1 = this.txtAttribute1.Text;
            priceTable.ATTRIBUTE2 = this.txtAttribute2.Text;
            priceTable.ATTRIBUTE3 = this.txtAttribute3.Text;
            priceTable.ORI_PRICE = Convert.ToDecimal(this.txtOriPrice.Text);
            priceTable.DISCOUNT_RATE = Convert.ToDecimal(this.txtDiscount.Text);

            priceTable.CREATE_USER = UserTable.USER_ID;
            priceTable.LAST_UPDATE_USER = priceTable.CREATE_USER;

            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }
            if (bll.Add(priceTable) > 0)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"添加成功！\");processCloseAndRefreshParent();", true);
            }
        }
        protected void Department_change(object sender, EventArgs e)
        {

            if (this.txtDepartment_Code.Text.Trim() == "")
            {
                this.lblWarehouseName.Text = "";
                this.txtDepartment_Code.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_DEPARTMENT", this.txtDepartment_Code.Text, "");
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
        protected void SysleCode_Chanage(object sender, EventArgs e)
        {
            if (this.txtStyleCode.Text.Trim() == "")
            {
                this.lblStyleName.Text = "";
                this.txtStyleCode.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_STYLE", txtStyleCode.Text.Trim(), "");
            if (table != null)
            {
                this.lblStyleName.Text = table.Name;
                this.txtStyleCode.Text = table.Code;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"款式不存在！\");", true);
                this.lblStyleName.Text = "";
                this.txtStyleCode.Text = "";
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
            }
        }
        protected void ToDate_Changed(object sender, EventArgs e)
        {
            if (txtToDate.Text.Trim() != "")
            {
                if (!PageValidate.IsDateTime(txtToDate.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtToDate.ClientID + "').value='';", true);
                }
                else
                {
                    txtToDate.Text = Convert.ToDateTime(txtToDate.Text.Trim()).ToString("yyyy/MM/dd");
                }
            }
        }

        protected void txtPrice_TextChanged(object sender, EventArgs e)
        {
            this.txtDiscount.Text = Convert.ToString(Convert.ToDecimal(this.txtOriPrice.Text) - Convert.ToDecimal(this.txtPrice.Text));
        }
    }
}
