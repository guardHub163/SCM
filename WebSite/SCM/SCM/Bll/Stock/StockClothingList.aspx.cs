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
using System.Drawing;
using System.Text;
using SCM.Web;
using SCM.Model;
using SCM.Common;
using log4net;
using System.Reflection;

namespace SCM.Web.Stock
{
    public partial class StockClothingList : BasePage
    {
        BCommon bCommon = new BCommon();
        DataSet ds = new DataSet();
        BStock bll = new BStock();
        BSize bSize = new BSize();
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateGridViewHeader();
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
                MessageBox.Show(this, "仓库不存在!");
                this.txtWarehouseCode.Text = "";
                this.lblWarehouseName.Text = "";
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
                MessageBox.Show(this, "商品种类不存在!");
                this.lblProductGroupName.Text = "";
                this.txtProductGroupCode.Text = "";
            }
        }

        protected void ClolorCode_Chanage(object sender, EventArgs e)
        {
            if (this.txtColorCode.Text.Trim() == "")
            {
                this.lblColorName.Text = "";
                this.txtColorCode.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_COLOR", txtColorCode.Text.Trim(), "");
            if (table != null)
            {
                this.lblColorName.Text = table.Name;
                this.txtColorCode.Text = table.Code;
            }
            else
            {
                MessageBox.Show(this, "颜色不存在!");
                this.lblColorName.Text = "";
                this.txtColorCode.Text = "";
            }
        }

        protected void StyleCode_Chanage(object sender, EventArgs e)
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
                MessageBox.Show(this, "款式不存在!");
                this.lblStyleName.Text = "";
                this.txtStyleCode.Text = "";
            }
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSearch":
                    Search(sender, e);
                    break;
                case "btnExport":
                    CommonUtil.DataTable2Excel((DataTable)ViewState["dtColumns"]);
                    break;
            }
            return true;
        }





        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        private void Search(object sender, EventArgs e)
        {
            if (!CheckInput())
            {
                return;
            }
            DataTable dt = bll.GetStockClothingList(getConduction()).Tables[0];

            DataTable dtSize = bSize.GetSizeByGroupCode(txtProductGroupCode.Text.Trim()).Tables[0];

            ViewState["SIZE"] = dtSize;

            DataTable dtColumns = CreateGridViewColumns(dtSize);

            CreateGridViewHeader();

            string currentStyleCode = "";
            string currentColorCode = "";
            DataRow row = null;
            decimal total = 0;
            foreach (DataRow dr in dt.Rows)
            {
                string styleCode = CConvert.ToString(dr["STYLE_CODE"]);
                string colorCode = CConvert.ToString(dr["COLOR_CODE"]);

                if (!currentStyleCode.Equals(styleCode) || !currentColorCode.Equals(colorCode))
                {
                    if (row != null)
                    {
                        dtColumns.Rows.Add(row);
                        total = 0;
                    }
                    row = dtColumns.NewRow();
                }

                row["款式"] = dr["STYLE_CODE"];
                row["颜色"] = CConvert.ToString(dr["COLOR_CODE"]) + "|" + CConvert.ToString(dr["COLOR_NAME"]);
                row["名称"] = dr["STYLE_NAME"];
                decimal stock = Math.Floor(CConvert.ToDecimal(dr["STOCK"]));
                row[CConvert.ToString(dr["SIZE_CODE"])] = (stock == 0 ? "" : stock.ToString());
                total += CConvert.ToDecimal(dr["STOCK"]);

                currentStyleCode = styleCode;
                currentColorCode = colorCode;

                row["合计"] = total;
            }

            if (row != null)
            {
                dtColumns.Rows.Add(row);
            }
            ViewState["dtColumns"] = dtColumns;
            if (dtColumns.Rows.Count > 0)
            {
                btnExport.Enabled = true;
            }
            else
            {
                btnExport.Enabled = false;
            }
            string where = "";
            if (rdoThanZero.Checked)
            {
                where = "合计 > 0";
            }
            else if (rdoEqualZero.Checked)
            {
                where = "合计 = 0";
            }
            else if (rdoBelowZero.Checked)
            {
                where = "合计 < 0";
            }

            DataRow[] drArr = dtColumns.Select(where);//查询
            DataTable newDt = dtColumns.Clone();
            for (int i = 0; i < drArr.Length; i++)
            {
                newDt.ImportRow(drArr[i]);
            }

            newDt.DefaultView.Sort = "款式 ASC,颜色 ASC";

            gridView.DataSource = newDt;
            gridView.DataBind();
        }

        #endregion

        #region 查询条件验证
        /// <summary>
        /// 查询条件验证
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            bool ret = true;
            if (string.IsNullOrEmpty(txtProductGroupCode.Text.Trim()))
            {
                ret = false;
                MessageBox.Show(this, "商品种类不能为空。");
                txtProductGroupCode.Focus();
            }

            return ret;
        }

        #endregion

        #region 查询条件
        /// <summary>
        /// 查询条件
        /// </summary>        
        private string getConduction()
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(txtWarehouseCode.Text))
            {
                sb.AppendFormat(" AND BS.WAREHOUSE_CODE = '{0}'", txtWarehouseCode.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtProductGroupCode.Text))
            {
                sb.AppendFormat(" AND BP.GROUP_CODE = '{0}'", txtProductGroupCode.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtStyleCode.Text))
            {
                sb.AppendFormat(" AND BP.STYLE = '{0}'", txtStyleCode.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtColorCode.Text))
            {
                sb.AppendFormat(" AND BP.COLOR = '{0}'", txtColorCode.Text.Trim());
            }
            return sb.ToString();
        }

        #endregion

        #region 创建GridView表头
        /// <summary>
        /// 创建GridView表头
        /// </summary>
        private void CreateGridViewHeader()
        {
            if (ViewState["SIZE"] != null)
            {
                VHeader.Rows.Clear();

                TableRow tr = new TableRow();

                TableCell cell = new TableCell();
                cell.Text = "款式";
                cell.Width = 80;
                tr.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "颜色";
                cell.Width = 80;
                tr.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "名称";
                cell.Width = 120;
                tr.Cells.Add(cell);

                foreach (DataRow dr in ((DataTable)ViewState["SIZE"]).Rows)
                {
                    cell = new TableCell();
                    cell.Text = CConvert.ToString(dr["NAME"]);
                    cell.Width = 50;
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    tr.Cells.Add(cell);
                }

                cell = new TableCell();
                cell.Text = "合计";
                cell.Width = 80;
                cell.HorizontalAlign = HorizontalAlign.Right;
                tr.Cells.Add(cell);
                VHeader.Rows.Add(tr);

                int totalWidth = 360 + ((DataTable)ViewState["SIZE"]).Rows.Count * 50;
                if (totalWidth > 980)
                {
                    cell = new TableCell();
                    cell.Text = "";
                    cell.Width = 17;
                    tr.Cells.Add(cell);
                    VHeader.Rows.Add(tr);
                    totalWidth += 17;
                }
                else
                {
                    cell = new TableCell();
                    cell.Text = "";
                    cell.Width = 1000 - totalWidth;
                    tr.Cells.Add(cell);
                    VHeader.Rows.Add(tr);
                    totalWidth = 1000;
                }

                VHeader.Width = totalWidth;
            }
        }

        #endregion

        #region 创建GridView数据列
        /// <summary>
        /// 创建GridView数据列
        /// </summary>
        private DataTable CreateGridViewColumns(DataTable dtSize)
        {
            DataTable dtColumns = new DataTable();
            dtColumns.Columns.Add("款式", Type.GetType("System.String"));
            dtColumns.Columns.Add("颜色", Type.GetType("System.String"));
            dtColumns.Columns.Add("名称", Type.GetType("System.String"));
            //
            gridView.Columns.Clear();

            BoundField filedName = new BoundField();
            filedName.DataField = "款式";
            filedName.HeaderText = "款式";
            filedName.ItemStyle.Width = Unit.Parse("80px");
            gridView.Columns.Add(filedName);

            filedName = new BoundField();
            filedName.DataField = "颜色";
            filedName.HeaderText = "颜色";
            filedName.ItemStyle.Width = Unit.Parse("80px");
            gridView.Columns.Add(filedName);

            filedName = new BoundField();
            filedName.DataField = "名称";
            filedName.HeaderText = "名称";
            filedName.ItemStyle.Width = Unit.Parse("120px");
            gridView.Columns.Add(filedName);

            foreach (DataRow dr in dtSize.Rows)
            {
                filedName = new BoundField();
                filedName.DataField = CConvert.ToString(dr["CODE"]);
                filedName.HeaderText = CConvert.ToString(dr["NAME"]);
                filedName.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                filedName.ItemStyle.Width = Unit.Parse("50px");
                gridView.Columns.Add(filedName);
                dtColumns.Columns.Add(CConvert.ToString(dr["CODE"]), Type.GetType("System.String"));
            }

            filedName = new BoundField();
            filedName.DataField = "合计";
            filedName.HeaderText = "合计";
            filedName.DataFormatString = "{0:N0}";
            filedName.ItemStyle.Width = Unit.Parse("80px");
            filedName.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            gridView.Columns.Add(filedName);
            dtColumns.Columns.Add("合计", Type.GetType("System.Decimal"));



            return dtColumns;
        }
        #endregion

        #region gridView_RowDataBound
        /// <summary>
        /// gridView_RowDataBound
        /// </summary>
        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                {
                    e.Row.Cells[0].Width = Unit.Parse("80px");
                    e.Row.Cells[1].Width = Unit.Parse("80px");
                    e.Row.Cells[2].Width = Unit.Parse("120px");
                    for (int i = 3; i < gridView.Columns.Count - 1; i++)
                    {
                        e.Row.Cells[i].Width = Unit.Parse("50px");
                    }
                    e.Row.Cells[gridView.Columns.Count - 1].Width = Unit.Parse("80px");
                    gridView.Width = Unit.Parse(360 + ((DataTable)ViewState["SIZE"]).Rows.Count * 50 + "px");
                }
                e.Row.Attributes.Add("OnMouseOver", "c=this.style.backgroundColor;this.style.backgroundColor=mouseOverBackgroundColor;");
                e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor=c;");

                //LinkButton lk = new LinkButton();
                //lk.Text = e.Row.Cells[2].Text;
                //lk.OnClientClick = "javascript:alert(\"" + e.Row.Cells[0].Text + "\");return false;";
                //e.Row.Cells[2].Text = "";
                //e.Row.Cells[2].Controls.Add(lk);
            }
        }
        #endregion
    }//end class
}