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

namespace SCM.Web.Productprice
{
    public partial class Modify : BaseModalDialogPage
    {
        BProductprice bll = new BProductprice();
        BCommon bCommon = new BCommon();
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
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    decimal ID = Convert.ToDecimal(Request.Params["id"]);
                    Showinfo(ID);
                }
              
            }
            this.txtDiscount.Text = Convert.ToString(Convert.ToDecimal(this.txtOriPrice.Text) - Convert.ToDecimal(this.txtPrice.Text));
        }
        private void Showinfo(decimal ID)
        {


            BaseProductpriceTable priceTable = bll.GetModel(ID);
            this.lblId.Text = priceTable.ID.ToString();
            this.txtOriPrice.Text =Convert.ToString( priceTable.ORI_PRICE);
            this.txtPrice.Text = priceTable.SALES_PRICE.ToString();
            this.txtDepartment_Code.Text = priceTable.DEPARTMENT_CODE;
            this.ddlPrice.SelectedValue = priceTable.PRICE_CODE;
            this.txtStyleCode.Text = priceTable.STYLE_CODE;
            this.txtStartTime.Text = priceTable.START_DATE.ToString("yyyy/MM/dd");
            this.txtEndTime.Text = priceTable.END_DATE.ToString("yyyy/MM/dd");
            this.txtAttribute1.Text = priceTable.ATTRIBUTE1;
            this.txtAttribute2.Text = priceTable.ATTRIBUTE2;
            this.txtAttribute3.Text = priceTable.ATTRIBUTE3;
            if (priceTable.DEFAULT_FLAG == 0)
            {
                rdo2.Checked = true;
            }
            if (priceTable.DEFAULT_FLAG == 1)
            {
                rdo1.Checked = true;
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
            string message = "";
            if (this.txtPrice.Text.Trim().Length == 0)
            {
                message += "价格不能为空！\\n";
            }else if (!PageValidate.IsDecimal(this.txtPrice.Text.Trim()))
            {
                message += "价格格式不对！\\n";
            }
            if (this.txtDepartment_Code.Text.Trim().Length == 0)
            {
                message += "部门不能为空！\\n";
            }
            if (this.txtStyleCode.Text.Trim().Length == 0)
            {
                message += "样式不能为空！\\n";
            }
            if (this.txtStartTime.Text == "")
            {
                message += "起始时间不能为空！\\n";
            }
            if (this.txtEndTime.Text == "")
            {
                message += "结束时间不能为空！\\n";
            }
            if (this.txtStartTime.Text != "" && this.txtEndTime.Text != "")
            {
                if (Convert.ToDateTime(this.txtStartTime.Text) > Convert.ToDateTime(this.txtEndTime.Text))
                {
                    message += "起始时间不能小于结束时间！\\n";
                }
            }
            BaseProductpriceTable priceTable = new BaseProductpriceTable();
            priceTable.ID = Convert.ToDecimal(this.lblId.Text);
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
            priceTable.START_DATE = Convert.ToDateTime(this.txtStartTime.Text);
            priceTable.END_DATE = Convert.ToDateTime(this.txtEndTime.Text);
            priceTable.PRICE_CODE = this.ddlPrice.SelectedItem.Value;
            priceTable.ATTRIBUTE1 = this.txtAttribute1.Text;
            priceTable.ATTRIBUTE2 = this.txtAttribute2.Text;
            priceTable.ATTRIBUTE3 = this.txtAttribute3.Text;
            priceTable.ORI_PRICE =Convert.ToDecimal( this.txtOriPrice.Text);
            priceTable.DISCOUNT_RATE = Convert.ToDecimal(this.txtDiscount.Text);

            priceTable.LAST_UPDATE_USER = UserTable.USER_ID;

            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }
            if (bll.Update(priceTable))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"修改成功！\");processCloseAndRefreshParent();", true);
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
        protected void StartTime_Changed(object sender, EventArgs e)
        {
            if (txtStartTime.Text.Trim() != "")
            {
                if (!PageValidate.IsDateTime(txtStartTime.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtStartTime.ClientID + "').value='';", true);
                }
                else
                {
                    txtStartTime.Text = Convert.ToDateTime(txtStartTime.Text.Trim()).ToString("yyyy/MM/dd");
                }
            }
        }
        protected void EndTime_Changed(object sender, EventArgs e)
        {
            if (txtEndTime.Text.Trim() != "")
            {
                if (!PageValidate.IsDateTime(txtEndTime.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtEndTime.ClientID + "').value='';", true);
                }
                else
                {
                    txtEndTime.Text = Convert.ToDateTime(txtEndTime.Text.Trim()).ToString("yyyy/MM/dd");
                }
            }
        }
        protected void txtPrice_TextChanged(object sender, EventArgs e)
        {
            this.txtDiscount.Text = Convert.ToString(Convert.ToDecimal(this.txtOriPrice.Text) - Convert.ToDecimal(this.txtPrice.Text));
        }
}
}
