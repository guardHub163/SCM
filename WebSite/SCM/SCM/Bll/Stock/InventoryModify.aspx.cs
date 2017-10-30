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
using System.Text;
namespace SCM.Web.Stock
{
    public partial class InventoryModify : BaseModalDialogPage
    {
        BStock bll = new BStock();
        DataSet ds = new DataSet();
        ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidateRole(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            if (!Page.IsPostBack)
            {
                base._log = _log;
                string type = Request.QueryString["TYPE"];
                string sn = Request.QueryString["SN"];

                if (!string.IsNullOrEmpty(type))
                {
                    if (type == "1")
                    {
                        Search(sn);
                    }
                    else if (type != "1")
                    {
                        string st = Request.QueryString["ST"];
                        string message = "";
                        Hashtable ht = new Hashtable();
                        if (!string.IsNullOrEmpty(st))
                        {
                            string[] AllNumber = st.Split('/');
                            for (int i = 0; i < AllNumber.Length - 1; i++)
                            {
                                string[] AllSamllNumber = AllNumber[i].Split('-');
                                ht.Add(AllSamllNumber[0], AllSamllNumber[1]);
                            }
                            if (type == "2")//暂存
                            {
                                if (bll.UpdateInventory(sn, ht, 0, UserTable.USER_ID) > 0)
                                {
                                    message = "执行成功！";
                                }
                            }
                            else
                            {
                                if (bll.UpdateInventory(sn, ht, 1, UserTable.USER_ID) > 0)//盘点确认
                                {
                                    message = "执行成功！";
                                }
                            }
                            Response.Write(message);
                            Response.End();
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(sn))
                    {
                        BaseInventoryScheduleTable isTable = bll.GetInventoryScheduleMode(Request.QueryString["SN"]);
                        if (isTable != null)
                        {
                            this.lblSlipNumber.Text = isTable.SLIP_NUMBER;
                            this.lblStartDate.Text = isTable.INVENTORY_START_DATE.ToString("yyyy/MM/dd");
                            this.lblWarehouse.Text = isTable.WAREHOUSE_CODE + " " + isTable.WAREHOUSE_NAME;
                            this.lblStatus.Text = isTable.STATUS_NAME;
                            this.lblGroupcode.Text = isTable.GROUP_NAME;
                        }
                    }
                }

            }//end Page.IsPostBack
        }



        //protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        TextBox txtRI = (TextBox)e.Row.FindControl("txtRealInventory");
        //        if (e.Row.Cells[0].Text.Trim() != "&nbsp;" && e.Row.Cells[0].Text.Trim() != "")
        //        {
        //            txtRI.Attributes.Add("onClick", txtRI.ClientID + ".select();");
        //            txtRI.Attributes.Add("onFocus", txtRI.ClientID + ".select();");
        //            try
        //            {
        //                decimal diff = Convert.ToDecimal(e.Row.Cells[7].Text);
        //                if (diff < 0)
        //                {
        //                    e.Row.Cells[7].ForeColor = System.Drawing.Color.Red;

        //                }
        //                else if (diff > 0)
        //                {
        //                    e.Row.Cells[7].ForeColor = System.Drawing.Color.Blue;
        //                }
        //                else
        //                {
        //                    e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
        //                }
        //            }
        //            catch (FormatException fe) { }
        //        }
        //        else
        //        {
        //            txtRI.Visible = false;
        //        }
        //    }
        //}

        private void Search(string sn)
        {
            string str = "";
            ds = bll.GetInventoryInfo(" SLIP_NUMBER = '" + sn + "'");
            if (ds.Tables[0].Rows.Count < 0)
            {
                str = "";
            }
            else
            {
                str = CreateJsonParameters(ds.Tables[0]);
            }
            Response.Write(str);
            Response.End();
        }

        //private void BindData()
        // {
        //     ds = bll.GetInventoryList(GetQustring(), "", (this.paging.CurrentPage - 1) * PageSize + 1, this.paging.CurrentPage * PageSize);
        //     Hashtable ht = (Hashtable)ViewState["LINES"];
        //     if (ht != null)
        //     {
        //         foreach (DataRow row in ds.Tables[0].Rows)
        //         {
        //             object lineNumber = row["LINE_NUMBER"];
        //             if (ht[lineNumber] != null)
        //             {
        //                 row["REAL_INVENTORY"] = Convert.ToDecimal(ht[lineNumber]);
        //                 row["DIFF_QUANTITY"] = Convert.ToDecimal(row["REAL_INVENTORY"]) - Convert.ToDecimal(row["INVENTORY"]);
        //             }
        //         }
        //     }
        //     for (int i = ds.Tables[0].Rows.Count; i < PageSize; i++)
        //     {
        //         ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
        //     }
        //     gridView.DataSource = ds;
        //     gridView.DataBind();
        // }

        //protected void RealInventory_Changed(object sender, EventArgs e)
        //{
        //    TextBox txRI = (TextBox)sender;
        //    decimal realInventory = 0;
        //    try
        //    {
        //        realInventory = Convert.ToDecimal(txRI.Text.Trim());
        //    }
        //    catch (FormatException fe)
        //    {
        //        //非法字符
        //       // ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"实际在库输入格式错误!\");document.getElementById('" + txRI.ClientID + "').value='0';", true);
        //        return;
        //    }
        //    try
        //    {
        //        GridViewRow dr = (GridViewRow)txRI.Parent.Parent;
        //        int lineNumber = (int)gridView.DataKeys[dr.RowIndex].Value;
        //        decimal inventory = Convert.ToDecimal(dr.Cells[5].Text);
        //        decimal diff = realInventory - inventory;
        //        dr.Cells[7].Text = diff.ToString();
        //        if (diff < 0)
        //        {
        //            dr.Cells[7].ForeColor = System.Drawing.Color.Red;

        //        }
        //        else if (diff > 0)
        //        {
        //            dr.Cells[7].ForeColor = System.Drawing.Color.Blue;
        //        }
        //        else
        //        {
        //            dr.Cells[7].ForeColor = System.Drawing.Color.Black;
        //        }
        //        SetViewStatus(lineNumber, realInventory);
        //    }
        //    catch (FormatException fe)
        //    {

        //    }

        //}
        protected void ProductCode_Change(object sender, EventArgs e)
        {

        }

        //private void SetViewStatus(int lineNumber, decimal realInventory)
        //{
        //    Hashtable ht = (Hashtable)ViewState["LINES"];
        //    if (ht == null)
        //    {
        //        ht = new Hashtable();
        //    }
        //    ht.Remove(lineNumber);
        //    if (realInventory != decimal.Parse("0"))
        //    {
        //        ht.Add(lineNumber, realInventory);
        //    }
        //    ViewState["LINES"] = ht;
        //}

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSeach":
                    //  Search();
                    break;
                case "btnSave":
                    // Save(sender, e, 0);
                    break;
                case "btnOK":
                    //  Save(sender, e, 1);
                    break;
            }
            return true;
        }

        #region
        //        protected void Process_UploadFile(object sender, EventArgs e, HtmlInputFile aFile)
        //        {
        //            import_div.Visible = false;
        //            btnImport.Enabled = true;
        //            BaseUserTable b = UserTable;
        //            if (aFile == null)
        //            {
        //                return;
        //            }
        //            HttpPostedFile hpFile = aFile.PostedFile;
        //            string fileExtension = System.IO.Path.GetExtension(hpFile.FileName);//获取文件扩展名
        //            if (hpFile.ContentLength <= 0)
        //            {
        //                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "alert('请选择上传文件');", true);
        //                return;
        //            }
        //            if (fileExtension != ".xls" && fileExtension != ".xlsx")
        //            {
        //                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "alert('上传文件的格式不对');", true);
        //                return;
        //            }
        //            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;//新的文件名
        //            string path = HttpContext.Current.Server.MapPath("~") + "\\" + "Excel" + "\\" + fileName;
        //            hpFile.SaveAs(path);
        //            DataTable dt = FileOperator.ReadExcel(path, fileName);
        //            StringBuilder sb = new StringBuilder();
        //            BProduct bproduct = new BProduct();
        //            DataSet da = bll.GetInventoryMaxLineNumber(this.lblSlipNumber.Text);
        //            int maxlinenumber = Convert.ToInt32(da.Tables[0].Rows[0]["LINE_NUMBER"].ToString());
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                if (row[0].ToString() == null || row[0].ToString() == "")
        //                {
        //                    continue;
        //                }
        //                if (row[1].ToString() == null || row[1].ToString() == "")
        //                {
        //                    continue;
        //                }
        //                if (row[2].ToString() == null || row[2].ToString() == "")
        //                {
        //                    continue;
        //                }
        //                if (PageValidate.IsNumber(row[2].ToString()) == false)
        //                {
        //                    sb.AppendFormat("{0}", "商品编号为" + row[1].ToString() + "的数量的格式不正确。");
        //                }
        //                if (Convert.ToDecimal(row[2].ToString()) <= 0)
        //                {
        //                    sb.AppendFormat("{0}", "商品编号为" + row[1].ToString() + "的数量不能小于0。");
        //                }

        //                if (row[0].ToString() != this.lblSlipNumber.Text.ToString())
        //                {
        //                    sb.AppendFormat("{0}", "商品编号为" + row[1].ToString() + "的数据不是此订单中的数据。");
        //                }
        //                BaseProductTable bproductTable = bproduct.GetModel(row[1].ToString());
        //                if (bproductTable == null)
        //                {
        //                    sb.AppendFormat("{0}", "商品编号为" + row[1].ToString() + "的数据不是有效地商品编号(数据库中不存在)。");
        //                }
        //                DataSet ds = bll.GetInventoryInfo("SLIP_NUMBER='" + row[0].ToString() + "'AND PRODUCT_CODE='" + row[1].ToString() + "'");

        //                if (ds.Tables[0].Rows.Count == 0)
        //                {
        //                    maxlinenumber++;
        //                    BllInventoryTable Intable = new BllInventoryTable();
        //                    Intable.SLIP_NUMBER = this.lblSlipNumber.Text;
        //                    Intable.LINE_NUMBER = maxlinenumber;
        //                    Intable.PRODUCT_CODE = row[1].ToString();
        //                    Intable.UNIT_CODE = bproductTable.UNIT_CODE.ToString();
        //                    Intable.INVENTORY = 0;
        //                    Intable.REAL_INVENTORY = Convert.ToDecimal(row[2].ToString());
        //                    Intable.STATUS_FLAG = 0;
        //                    Intable.CREATE_USER = UserTable.USER_ID;
        //                    Intable.LAST_UPDATE_USER = Intable.CREATE_USER;
        //                    bll.AddInventory(Intable);
        //                }
        //                else
        //                {
        //                    if (ds.Tables[0].Rows[0]["REAL_INVENTORY"] == row[2])
        //                    {
        //                        continue;
        //                    }
        //                    int linNumber = Convert.ToInt32(ds.Tables[0].Rows[0]["LINE_NUMBER"].ToString());
        //                    SetViewStatus(linNumber, Convert.ToDecimal(row[2]));
        //                }

        //                BindData();
        //            }
        //            if (sb.Length > 0)
        //            {
        //                HttpContext.Current.Session["ERROR_INFO"] = sb.ToString();
        //                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "window.open('../../LeadingErrorInfo.aspx?');", true);
        //            }
        //        }
        #endregion
        private void Save(object sender, EventArgs e, int statusFlag)
        {
            Hashtable ht = (Hashtable)ViewState["LINES"];
            if (bll.UpdateInventory(lblSlipNumber.Text.Trim(), ht, statusFlag, UserTable.USER_ID) > 0)
            {

                //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"执行成功!\");processCloseAndRefreshParent();", true);
            }
            else
            {
                // ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"执行失败!\");", true);
            }
        }

        //public string GetQustring()
        //{
        //    StringBuilder str = new StringBuilder();
        //    str.AppendFormat(" SLIP_NUMBER = '{0}'", lblSlipNumber.Text.Trim());
        //    if (rdostock1.Checked)
        //    {
        //        str.Append(" AND 1=1 ");
        //    }
        //    else if (rdostock2.Checked)
        //    {
        //        str.Append(" AND INVENTORY<>0 ");
        //    }
        //    else if (rdostock3.Checked)
        //    {
        //        str.Append(" AND INVENTORY=0 ");
        //    }
        //    if (rdoAll.Checked)
        //    {
        //        str.Append(" AND 1=1 ");
        //    }
        //    else if (rdoInventory1.Checked)
        //    {
        //        str.Append(" AND REAL_INVENTORY=0 ");
        //    }
        //    else if (rdoInventory2.Checked)
        //    {
        //        str.Append(" AND  REAL_INVENTORY<>0 AND REAL_INVENTORY=INVENTORY ");
        //    }
        //    else if (rdoInventory3.Checked)
        //    {
        //        str.Append(" AND  REAL_INVENTORY<>0  AND REAL_INVENTORY<>INVENTORY ");
        //    }
        //    return str.ToString();
        //}



        //public string GetQustring()
        //{
        //    StringBuilder str = new StringBuilder();
        //    str.AppendFormat(" SLIP_NUMBER = '{0}'", lblSlipNumber.Text.Trim());
        //    if (rdostock1.Checked)
        //    {
        //        str.Append(" AND 1=1 ");
        //    }
        //    else if (rdostock2.Checked)
        //    {
        //        str.Append(" AND INVENTORY<>0 ");
        //    }
        //    else if (rdostock3.Checked)
        //    {
        //        str.Append(" AND INVENTORY=0 ");
        //    }
        //    if (rdoAll.Checked)
        //    {
        //        str.Append(" AND 1=1 ");
        //    }
        //    else if (rdoInventory1.Checked)
        //    {
        //        str.Append(" AND REAL_INVENTORY=0 ");
        //    }
        //    else if (rdoInventory2.Checked)
        //    {
        //        str.Append(" AND  REAL_INVENTORY<>0 AND REAL_INVENTORY=INVENTORY ");
        //    }
        //    else if (rdoInventory3.Checked)
        //    {
        //        str.Append(" AND  REAL_INVENTORY<>0  AND REAL_INVENTORY<>INVENTORY ");
        //    }
        //    return str.ToString();
        //}

        /// <summary>
        /// 转换
        /// </summary>
        private string CreateJsonParameters(DataTable dt)
        {
            StringBuilder JsonString = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("{root:[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{ ");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                    }
                }
                JsonString.Append("]}");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }




    }//end class
}
