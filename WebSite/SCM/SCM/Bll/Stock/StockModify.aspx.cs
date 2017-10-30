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
using log4net;
using System.Reflection;
using System.IO;
using System.Text;

namespace SCM.Web.Stock
{
    public partial class StockModify : BasePage
    {
        BStock bll = new BStock();
        DataSet ds = new DataSet();
        BCommon bCommon = new BCommon();
        int flag = 1;
        ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            ValidateRole(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            if (!Page.IsPostBack)
            {
                // btnImport.Attributes.Add("onclick", "winOpen('../../Common/UpLoadInfo.aspx?code=" + flag + "','','150','400')");
                ddlPrice.DataSource = bCommon.GetNames("STOCK_REASON").Tables[0];
                ddlPrice.DataTextField = "NAME"; //dropdownlist的Text的字段 
                ddlPrice.DataValueField = "CODE";//dropdownlist的Value的字段   
                ddlPrice.DataBind();
                try
                {
                    BaseUserTable UT = (BaseUserTable)Session["UserInfo"];
                    this.txtName.Text = UT.USER_ID + " | " + UT.TRUE_NAME;
                }
                catch { }
                this.txtDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            }
            this.uploadFile.Process_UploadFile += new UploadFileControl.UploadFileEventHandler(Process_UploadFile);
            btnProduct.Attributes.Add("onclick", "return processMasterClickByServer('PRODUCT','" + txtProductCode.ClientID + "','" + lblProductName.ClientID + "');");
            btnWarehouse.Attributes.Add("onclick", "return processMasterClickByServer('WAREHOUSE','" + txtWarehouseCode.ClientID + "','" + lblWarehouseName.ClientID + "');");
        }

        /// <summary>
        /// 
        /// </summary>
        protected void Warehouse_Change(object sender, EventArgs e)
        {
            if (txtWarehouseCode.Text.Trim() == "")
            {
                this.txtWarehouseCode.Text = "";
                this.lblWarehouseName.Text = "";
                this.lblNowStock.Text = "";
                this.txtUpdateStock.Text = "";
                this.lblUpdatelastStock.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_WAREHOUSE", txtWarehouseCode.Text.Trim(), "");
            if (table != null)
            {
                this.txtWarehouseCode.Text = table.Code;
                this.lblWarehouseName.Text = table.Name;
                this.lblNowStock.Text = "";
                this.txtUpdateStock.Text = "";
                this.lblUpdatelastStock.Text = "";
                showInfo();
            }
            else
            {
                this.lblWarehouseName.Text = "";
                this.lblNowStock.Text = "";
                this.txtUpdateStock.Text = "";
                this.lblUpdatelastStock.Text = "";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"仓库不存在!\");document.getElementById('" + txtWarehouseCode.ClientID + "').value='';", true);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected void Product_Changed(object sender, EventArgs e)
        {
            if (txtProductCode.Text.Trim() == "")
            {
                this.txtProductCode.Text = "";
                this.lblProductName.Text = "";
                this.txtColorCode.Text = "";
                this.txtSizeCode.Text = "";
                this.txtStyleCode.Text = "";
                this.lblUnit.Text = "";
                this.lblColorName.Text = "";
                this.lblProductName.Text = "";
                this.lblSizeName.Text = "";
                this.lblStyleName.Text = "";
                this.lblNowStock.Text = "";
                this.txtUpdateStock.Text = "";
                this.lblUpdatelastStock.Text = "";
                return;
            }

            BaseProductTable productTable = new BProduct().GetModel(txtProductCode.Text.Trim());
            if (productTable != null)
            {
                this.txtProductCode.Text = productTable.CODE;
                this.lblProductName.Text = productTable.NAME;
                this.txtColorCode.Text = productTable.COLOR;
                this.txtSizeCode.Text = productTable.SIZE;
                this.txtStyleCode.Text = productTable.STYLE;
                this.lblUnit.Text = productTable.UNIT_CODE;
                this.lblColorName.Text = productTable.COLOR_NAME;
                this.lblSizeName.Text = productTable.SIZE_NAME;
                this.lblStyleName.Text = productTable.STYLE_NAME;
                this.lblNowStock.Text = "";
                this.txtUpdateStock.Text = "";
                this.lblUpdatelastStock.Text = "";
                showInfo();
            }
            else
            {
                this.txtProductCode.Text = "";
                this.lblProductName.Text = "";
                this.txtColorCode.Text = "";
                this.txtSizeCode.Text = "";
                this.txtStyleCode.Text = "";
                this.lblUnit.Text = "";
                this.lblColorName.Text = "";
                this.lblProductName.Text = "";
                this.lblSizeName.Text = "";
                this.lblStyleName.Text = "";
                this.lblNowStock.Text = "";
                this.txtUpdateStock.Text = "";
                this.lblUpdatelastStock.Text = "";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"商品不存在!\");document.getElementById('" + txtProductCode.ClientID + "').value='';", true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void showInfo()
        {
            string warehouseCode = this.txtWarehouseCode.Text.Trim();
            string productCode = this.txtProductCode.Text.Trim();

            if (warehouseCode != "" && productCode != "")
            {
                BllStockTable bst = bll.GetModel(warehouseCode, productCode);
                if (bst != null)
                {
                    lblNowStock.Text = Math.Floor(bst.QUANTITY).ToString();
                }
                else
                {
                    lblNowStock.Text = "0";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected void UpdateStock_Change(object sender, EventArgs e)
        {
            decimal UpdateStock = Convert.ToDecimal(this.txtUpdateStock.Text);
            decimal NowStock = Convert.ToDecimal(this.lblNowStock.Text);
            if (this.txtUpdateStock.Text != "")
            {
                if ((UpdateStock + NowStock) >= 0)
                {
                    this.lblUpdatelastStock.Text = Math.Floor(UpdateStock + NowStock).ToString();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"修改库存输入有误!\");", true);
                    this.txtUpdateStock.Text = "";
                    this.lblUpdatelastStock.Text = "";
                }
            }
        }


        protected void Process_UploadFile(object sender, EventArgs e, HtmlInputFile aFile)
        {
            import_div.Visible = false;
            btnImport.Enabled = true;
            if (aFile == null)
            {
                return;
            }
            HttpPostedFile hpFile = aFile.PostedFile;
            string fileExtension = System.IO.Path.GetExtension(hpFile.FileName);//获取文件扩展名
            if (hpFile.ContentLength <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "alert('请选择上传文件');", true);
                return;
            }
            if (fileExtension != ".xls" && fileExtension != ".xlsx")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "alert('上传文件的格式不对');", true);
                return;
            }
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;//新的文件名
            string path = HttpContext.Current.Server.MapPath("~") + "\\" + "Excel" + "\\" + fileName;
            hpFile.SaveAs(path);
            DataTable da = FileOperator.ReadExcel(path, fileName);
            File.Delete(path);
            BProduct bproduct = new BProduct();
            BWarehouse bwarehouse = new BWarehouse();
            StringBuilder sb = new StringBuilder();
            _userTable = (BaseUserTable)HttpContext.Current.Session["UserInfo"];
            foreach (DataRow row in da.Rows)
            {
                try
                {
                    //if (row[2].ToString() != null && Convert.ToDecimal(row[2]) < 0)
                    //{
                    //    sb.AppendFormat("{0}", "商品编号为" + row[1] + "：此数据的库存小于0。");
                    //    continue;
                    //}
                    //if (row[2].ToString() != null && PageValidate.IsNumber(row[2].ToString()) == false)
                    //{
                    //    sb.AppendFormat("{0}", "商品编号为" + row[1] + "：此数据的库存格式不对。");
                    //    continue;
                    //}
                    if (row[0].ToString() != null && row[0].ToString() == "")
                    {
                        continue;
                    }
                    if (row[1].ToString() != null && row[1].ToString() == "")
                    {
                        continue;
                    }

                    if (bwarehouse.Exists(row[0].ToString()) == false)
                    {
                        sb.AppendFormat("{0}", "商品编号为" + row[1] + "：此数据的仓库编号不存在。");
                        continue;
                    }
                    if (bproduct.Exists(row[1].ToString()) == false)
                    {
                        sb.AppendFormat("{0}", "商品编号为" + row[1] + "：此数据的商品编号不存在。");
                        continue;
                    }

                    BaseProductTable bptable = bproduct.GetModel(row[1].ToString());
                    string unit = bptable.UNIT_CODE;
                    if (bll.Exists(row[0].ToString(), row[1].ToString()))
                    {
                        BllStockTable bst = bll.GetModel(row[0].ToString(), row[1].ToString());
                        if (bst.QUANTITY != Convert.ToDecimal(row[2].ToString()))
                        {
                            BllStockTable stock = new BllStockTable();
                            stock.PRODUCT_CODE = row[1].ToString();
                            stock.WAREHOUSE_CODE = row[0].ToString();
                            stock.QUANTITY = bst.QUANTITY;
                            stock.Toquantity = Convert.ToDecimal(row[2].ToString());
                            stock.Reason = row[3].ToString();
                            stock.Lastquantity = Convert.ToDecimal(row[2].ToString()) + Convert.ToDecimal(bst.QUANTITY.ToString());
                            stock.UNIT_CODE = unit;
                            stock.Creat_name = _userTable.USER_ID;
                            bll.Update(stock);
                        }
                    }
                    else
                    {
                        BllStockTable stock = new BllStockTable();
                        stock.PRODUCT_CODE = row[1].ToString();
                        stock.WAREHOUSE_CODE = row[0].ToString();
                        stock.QUANTITY = Convert.ToDecimal(row[2].ToString());
                        stock.Lastquantity = Convert.ToDecimal(row[2].ToString());
                        stock.Reason = row[3].ToString();
                        stock.Toquantity = 0;
                        stock.UNIT_CODE = unit;
                        stock.Creat_name = _userTable.USER_ID;
                        bll.Update(stock);
                    }
                }


                catch { }

            }
            if (sb.Length > 0)
            {
                HttpContext.Current.Session["ERROR_INFO"] = sb.ToString();
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "window.open('../../LeadingErrorInfo.aspx?');", true);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {

            switch (btnId)
            {
                case "btnSave":
                    btnSave_Click(sender, e);
                    break;
                case "btnProduct":
                    Product_Changed(sender, e);
                    break;
                case "btnWarehouse":
                    Warehouse_Change(sender, e);
                    break;
                case "btnImport":
                    import_div.Visible = true;
                    btnImport.Enabled = false;
                    break;
            }
            return true;
        }

        private void Import(object sender, EventArgs e)
        {
            //    BProduct bproduct = new BProduct();
            //    BWarehouse bwarehouse = new BWarehouse();
            //    if (HttpContext.Current.Session["IMPORT_FILE_NAME"] != null)
            //    {
            //        string Path = HttpContext.Current.Session["IMPORT_FILE_NAME"].ToString();
            //        HttpContext.Current.Session.Remove("IMPORT_FILE_NAME");
            //        int index = Path.LastIndexOf("\\");
            //        string name = Path.Substring(index + 1);
            //        DataTable da = FileOperator.ReadExcel(Path, name);
            //        File.Delete(Path);
            //        StringBuilder sb = new StringBuilder();
            //        foreach (DataRow row in da.Rows)
            //        {
            //            try
            //            {
            //                if (row[2].ToString() != null && Convert.ToDecimal(row[2]) < 0)
            //                {
            //                    sb.AppendFormat("{0}", "商品编号为" + row[1] + "：此数据的库存小于0。");
            //                    continue;
            //                }
            //                if (row[2].ToString() != null && PageValidate.IsNumber(row[2].ToString()) == false)
            //                {
            //                    sb.AppendFormat("{0}", "商品编号为" + row[1] + "：此数据的库存格式不对。");
            //                    continue;
            //                }
            //                if (row[0].ToString() != null && row[0].ToString() == "")
            //                {
            //                    continue;
            //                }
            //                if (row[1].ToString() != null && row[1].ToString() == "")
            //                {
            //                    continue;
            //                }

            //                if (bwarehouse.Exists(row[0].ToString()) == true)
            //                {
            //                    sb.AppendFormat("{0}", "商品编号为" + row[1] + "：此数据的仓库编号不存在。");
            //                    continue;
            //                }
            //                if (bproduct.Exists(row[1].ToString()) == true)
            //                {
            //                    sb.AppendFormat("{0}", "商品编号为" + row[1] + "：此数据的商品编号不存在。");
            //                    continue;
            //                }

            //                BaseProductTable bptable = bproduct.GetModel(row[0].ToString());
            //                string unit = bptable.UNIT_CODE;
            //                BllStockTable bst = bll.GetModel(row[0].ToString(), row[1].ToString());
            //                if (bst.QUANTITY.ToString() != row[2].ToString())
            //                {
            //                    BllStockTable stock = new BllStockTable();
            //                    stock.PRODUCT_CODE = row[1].ToString();
            //                    stock.WAREHOUSE_CODE = row[0].ToString();
            //                    stock.QUANTITY = bst.QUANTITY;
            //                    stock.Lastquantity = Convert.ToDecimal(row[2].ToString());
            //                    stock.Reason = row[3].ToString();
            //                    stock.Toquantity = Convert.ToDecimal(row[2].ToString()) - Convert.ToDecimal(bst.QUANTITY.ToString());
            //                    stock.UNIT_CODE = unit;
            //                    stock.Creat_name = userTable.USER_ID;
            //                    bll.Update(stock);
            //                }
            //            }
            //            catch { }

            //        }
            //        if (sb.Length > 0)
            //        {
            //            HttpContext.Current.Session["ERROR_INFO"] = sb.ToString();
            //            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "window.open('../../LeadingErrorInfo.aspx?');", true);
            //        }

            //    }
        }

        /// <summary>
        /// 
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            string message = "";
            if (this.txtUpdateStock.Text.Trim().Length == 0)
            {
                message += "修改库存不能为空！\\n";
            }
            else if (int.Parse(this.txtUpdateStock.Text) == 0)
            {
                message += "修改库存不能为0！\\n";
            }
            else if (!PageValidate.IsNumberSign(this.txtUpdateStock.Text))
            {
                message += "输入的库存格式错误！\\n";
            }
            if (string.IsNullOrEmpty(txtProductCode.Text.Trim()))
            {
                message += "商品编号不能为空！\\n";
            }
            if (string.IsNullOrEmpty(txtWarehouseCode.Text.Trim()))
            {
                message += "仓库编号不能为空！\\n";
            }
            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }
            BllStockTable bsta = new BllStockTable();
            bsta.WAREHOUSE_CODE = this.txtWarehouseCode.Text.Trim();
            bsta.PRODUCT_CODE = this.txtProductCode.Text.Trim();
            bsta.Toquantity = Convert.ToDecimal(this.txtUpdateStock.Text.Trim());
            bsta.Reason = this.ddlPrice.SelectedItem.Value;
            bsta.Lastquantity = Convert.ToDecimal(this.lblUpdatelastStock.Text.Trim());
            bsta.QUANTITY = Convert.ToDecimal(this.lblNowStock.Text.Trim());
            bsta.UNIT_CODE = this.lblUnit.Text;
            try
            {
                bsta.Creat_name = _userTable.USER_ID;
            }
            catch { }

            if (bll.Update(bsta) > 0)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"修改成功！\");", true);
                Clear();
            }
        }

        private void Clear()
        {
            this.lblProductName.Text = "";
            this.txtProductCode.Text = "";
            this.txtColorCode.Text = "";
            this.txtSizeCode.Text = "";
            this.txtStyleCode.Text = "";
            this.lblUnit.Text = "";
            this.lblColorName.Text = "";
            this.lblProductName.Text = "";
            this.lblSizeName.Text = "";
            this.lblStyleName.Text = "";
            this.lblNowStock.Text = "";
            this.txtUpdateStock.Text = "";
            this.lblUpdatelastStock.Text = "";
            this.txtWarehouseCode.Text = "";
            this.lblWarehouseName.Text = "";
        }

    }//end class
}
