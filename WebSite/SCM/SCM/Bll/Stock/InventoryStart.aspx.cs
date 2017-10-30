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
using System.Reflection;
using log4net;

namespace SCM.Web.Stock
{
    public partial class InventoryStart : BaseModalDialogPage
    {
        BStock bll = new BStock();
        BCommon bCommon = new BCommon();
        ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            ValidateRole(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            this.txtFromDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
        }

        protected void Warehouse_Change(object sender, EventArgs e)
        {
            if (txtWarehouseCode.Text.Trim() == "")
            {
                this.txtWarehouseCode.Text = "";
                this.lblWarehouseName.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_WAREHOUSE", txtWarehouseCode.Text.Trim(), "");
            if (table != null)
            {
                this.txtWarehouseCode.Text = table.Code;
                this.lblWarehouseName.Text = table.Name;
            }
            else
            {
                this.lblWarehouseName.Text = "";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"盘点仓库不存在!\");document.getElementById('" + txtWarehouseCode.ClientID + "').value='';", true);
            }
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId) 
            {
                case "btnSave":
                    Save(sender, e);
                    break;
            }
            return true;
        }

        private void Save(object sender, EventArgs e)
        {
            if (this.txtWarehouseCode.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"盘点仓库不能为空!\");", true);
                return;
            }
            if (this.txtProductGroupCode.Text.Trim() == "") 
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"商品种类不能为空!\");", true);
                return;
            }

            if (bll.InsertInventory(txtWarehouseCode.Text.Trim(),txtProductGroupCode.Text.Trim(), UserTable.USER_ID) == 0)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"保存失败！\");", true);
            }
            else 
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"保存成功！\");processCloseAndRefreshParent();", true);
            }
        }

        protected void ProductGroupCode_Chanage(object sender, EventArgs e)
        {
            if (this.txtProductGroupCode.Text.Trim() == "")
            {
                this.lblProductGroupName.Text = "";
                this.txtProductGroupCode.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_PRODUCT_GROUP", txtProductGroupCode.Text.Trim(), "");
            if (table != null)
            {
                this.lblProductGroupName.Text = table.Name;
                this.txtProductGroupCode.Text = table.Code;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"种类不存在！\");", true);
                this.lblProductGroupName.Text = "";
                this.txtProductGroupCode.Text = "";
            }
        }
    }
}
