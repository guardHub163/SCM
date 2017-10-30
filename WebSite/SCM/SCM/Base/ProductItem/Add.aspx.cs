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
    public partial class Add : BaseModalDialogPage
    {
        BCommon bCommon = new BCommon();
        BProductItem bll = new BProductItem();
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            btnProduct.Attributes.Add("onclick", "processMasterClickByServer('PRODUCT','" + txtProductCode.ClientID + "','" + lblProductName.ClientID + "');");
            if (!Page.IsPostBack)
            {
                if (Request.Params["PRODUCT_CODE"] != null && Request.Params["PRODUCT_CODE"].Trim() != "")
                {
                    this.txtProductCode.Text = Request.Params["PRODUCT_CODE"].ToString();
                    BaseProductTable productTable = new BProduct().GetModel(txtProductCode.Text.Trim());
                    this.lblProductName.Text = productTable.NAME;
                    //this.txtProductCode.Enabled = false;
                    //this.btnProduct.Enabled = false;

                }
            }
        }
        protected void Product_Change(object sender, EventArgs e)
        {
            if (txtProductCode.Text.Trim() == "")
            {
                this.lblProductName.Text = "";
                return;
            }
            BaseProductTable productTable = new BProduct().GetModel(txtProductCode.Text.Trim());
            if (productTable != null)
            {
                this.lblProductName.Text = productTable.NAME;
            }
            else
            {

                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"商品不存在!\");", true);
                this.txtProductCode.Text = "";
                this.lblProductName.Text = "";
            }
        }
        protected void Item_Change(object sender, EventArgs e)
        {

            if (txtItemCode.Text.Trim() == "")
            {
                this.txtItemCode.Text = "";
                this.lblItemName.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_ITEM", txtItemCode.Text.Trim(), "");
            if (table != null)
            {
                this.txtItemCode.Text = table.Code;
                this.lblItemName.Text = table.Name;
            }
            else
            {

                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"原料不存在!\");", true);
                this.txtItemCode.Text = "";
                this.lblItemName.Text = "";
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
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"供应商不存在！\");", true);
                this.txtSupplierCode.Text = "";
                this.lblSupplierName.Text = "";
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
            if (this.txtProductCode.Text.Trim().Length == 0 && this.txtItemCode.Text.Trim().Length == 0)
            {
                message += "商品以及原料不能为空！\\n";
            }
            else if (bll.Exists(txtProductCode.Text.Trim(),txtItemCode.Text.Trim()))
            {
                message += "商品的原料已经存在！\\n";
            }
            if (this.txtSupplierCode.Text.Trim().Length == 0)
            {
                message += "供应商不能为空！\\n";
            }
            if (this.txtQuantity.Text.Trim().Length == 0)
            {
                message += "数量不能为空！\\n";
            }
            else if (!PageValidate.IsDecimal(this.txtQuantity.Text))
            {
                message += "输入的数量的格式不正确！\\n";
            }
            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }
            BaseProductItemTable itemTable = new BaseProductItemTable();
            itemTable.PRODUCT_CODE = this.txtProductCode.Text;
            itemTable.ITEM_CODE = this.txtItemCode.Text;
            itemTable.SUPPLIER_CODE = this.txtSupplierCode.Text;
            itemTable.QUANTITY = Convert.ToDecimal(this.txtQuantity.Text.Trim());
            itemTable.ATTRIBUTE1 = this.txtAttribute1.Text;
            itemTable.ATTRIBUTE2 = this.txtAttribute2.Text;
            itemTable.ATTRIBUTE3 = this.txtAttribute3.Text;

            itemTable.CREATE_USER = UserTable.USER_ID;
            itemTable.LAST_UPDATE_USER = itemTable.CREATE_USER;
           
         
            if (bll.Add(itemTable) > 0)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"添加成功！\");processCloseAndRefreshParent();", true);    
            }
           
        }
    }
}