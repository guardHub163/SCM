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

namespace SCM.Web.ProductItem
{
    public partial class Modify : BaseModalDialogPage
    {
        BProductItem bll = new BProductItem();
        BCommon bCommon = new BCommon();
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {
                if (Request.Params["PRODUCT_CODE"] != null && Request.Params["PRODUCT_CODE"].Trim() != "")
                {
                    this.txtProductCode.Text = Request.Params["PRODUCT_CODE"].ToString();
                    this.txtItemCode.Text = Request.Params["ITEM_CODE"].ToString();
                    showInfo(txtProductCode.Text, txtItemCode.Text);
                }
            }
        }

        private void showInfo(string productcode,string itemcode)
        {
            BaseProductItemTable itemTable = bll.GetModel(productcode, itemcode);
            this.txtProductCode.Text = itemTable.PRODUCT_CODE;
            this.lblProductName.Text = itemTable.PRODUCT_NAME;
            this.txtItemCode.Text = itemTable.ITEM_CODE;
            this.lblItemName.Text = itemTable.ITEM_NAME;
            this.txtSupplierCode.Text = itemTable.SUPPLIER_CODE;
            this.lblSupplierName.Text = itemTable.SUPPLIER_NAME;
            this.txtQuantity.Text = itemTable.QUANTITY.ToString();
            this.txtAttribute1.Text = itemTable.ATTRIBUTE1;
            this.txtAttribute2.Text = itemTable.ATTRIBUTE2;
            this.txtAttribute3.Text = itemTable.ATTRIBUTE3;
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
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"供应商不存在！\");", true);
                this.txtSupplierCode.Text = "";
                this.lblSupplierName.Text = "";
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
            if (this.txtSupplierCode.Text.Trim().Length == 0)
            {
                message += "供应商不能为空！\\n";
            }
            if (this.txtQuantity.Text.Trim().Length == 0)
            {
                message += "数量不能为空！\\n";
            }
            else if (!PageValidate.IsDecimal(this.txtQuantity.Text.Trim()))
            {
                message += "输入的数量的格式不正确！\\n";
            }
            BaseProductItemTable productItem = new BaseProductItemTable();
            productItem.PRODUCT_CODE = this.txtProductCode.Text;
            productItem.SUPPLIER_CODE = this.txtSupplierCode.Text;
            productItem.ITEM_CODE = this.txtItemCode.Text;
            productItem.QUANTITY = Convert.ToDecimal(this.txtQuantity.Text);
            productItem.ATTRIBUTE1 = this.txtAttribute1.Text;
            productItem.ATTRIBUTE2 = this.txtAttribute2.Text;
            productItem.ATTRIBUTE3 = this.txtAttribute3.Text;
            try
            {
                BaseUserTable userTable = (BaseUserTable)Session["UserInfo"];
                productItem.LAST_UPDATE_USER = userTable.USER_ID;
            }
            catch { }
            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }
            if (bll.Update(productItem))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"修改成功！\");processCloseAndRefreshParent()", true);
            }
        }
    }
}
