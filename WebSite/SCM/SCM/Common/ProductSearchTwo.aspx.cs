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
using System.Text;
using SCM.Common;
using SCM.Bll;

namespace SCM.Web.Common
{
    public partial class ProductSearchTwo : BaseModalDialogPage
    {
        BCommon bCommon = new BCommon();
        DataSet ds = new DataSet();
        int PageSize = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("CODE", Type.GetType("System.String"));
                    dt.Columns.Add("NAME", Type.GetType("System.String"));
                    dt.Columns.Add("STYLE", Type.GetType("System.String"));
                    dt.Columns.Add("COLOR", Type.GetType("System.String"));
                    dt.Columns.Add("SIZE", Type.GetType("System.String"));
                    dt.Columns.Add("STYLE_NAME", Type.GetType("System.String"));
                    dt.Columns.Add("COLOR_NAME", Type.GetType("System.String"));
                    dt.Columns.Add("SIZE_NAME", Type.GetType("System.String"));
                    dt.Columns.Add("UNIT_CODE", Type.GetType("System.String"));
                    dt.Columns.Add("UNIT_NAME", Type.GetType("System.String"));
                    dt.Columns.Add("QUANTITY", Type.GetType("System.Decimal"));
                    for (int i = 0; i < PageSize; i++)
                    {
                        dt.Rows.Add(dt.NewRow());
                    }
                    gridView.DataSource = dt;
                    gridView.DataBind();
                }
                catch { }
            }
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("chk");
                TextBox qty = (TextBox)e.Row.FindControl("txtQuantity");
                if (e.Row.Cells[1].Text != "&nbsp;" && e.Row.Cells[1].Text != "")
                {
                    //e.Row.Attributes.Add("id", "row_" + e.Row.RowIndex.ToString());
                    //e.Row.Attributes.Add("onClick", "processRowClick('row_" + e.Row.RowIndex.ToString() + "');");
                }
                else
                {
                    chk.Visible = false;
                    qty.Visible = false;
                }
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
            }
        }

        private void Search(object sender, EventArgs e)
        {
            ds = bCommon.GetProductList(getConduction());
            DataTable dt = ds.Tables[0];
            try
            {
                if (dt.Rows.Count == 0)
                {
                    this.btnOK.Enabled = false;
                }
                else
                {
                    this.btnOK.Enabled = true;
                }
            }
            catch { }
            for (int i = dt.Rows.Count; i < PageSize; i++)
            {
                dt.Rows.Add(dt.NewRow());
            }
            gridView.DataSource = dt;
            gridView.DataBind();
        }

        private string getConduction()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("STATUS_FLAG <> " + CConstant.DELETE);
            if (this.txtProductGroupCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND GROUP_CODE = '{0}'", this.txtProductGroupCode.Text.Trim());
            }
            if (this.txtStyleCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND STYLE = '{0}'", this.txtStyleCode.Text.Trim());
            }
            if (this.txtColorCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND COLOR = '{0}'", this.txtColorCode.Text.Trim());
            }
            if (this.txtSizeCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND SIZE = '{0}'", this.txtSizeCode.Text.Trim());
            }
            if (this.txtProductName.Text.Trim() != "")
            {
                sb.AppendFormat(" AND SIZE = '{0}%'", this.txtProductName.Text.Trim());
            }
            return sb.ToString();
        }

        protected void Quantity_Changed(object sender, EventArgs e)
        {
            TextBox qty = (TextBox)sender;
            if (!PageValidate.IsDecimal(qty.Text.Trim()))
             {
                 ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "alert(\"数量格式输入错误！\");document.getElementById ('"+qty.ClientID+"').value=1;", true); 
             }
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSearch":
                    Search(sender, e);
                    break;
                case "btnOK":
                    createData();
                    break;
            }
            return true;
        }

        private void createData()
        {
            int j = 0;
            string str = "var arr = new Array();";
            str += "var obj = new Object();";
            foreach (GridViewRow row in gridView.Rows)
            {
                if (row.Cells[1].Text != "&nbsp;" && row.Cells[1].Text != "")
                {
                    CheckBox chk = (CheckBox)row.FindControl("chk");
                    TextBox qty = (TextBox)row.FindControl("txtQuantity");
                    if (chk.Checked && Convert.ToDecimal(qty.Text) > 0)
                    {
                        str += "obj = new Object();";
                        str += "obj.code = '" + row.Cells[1].Text + "';";
                        str += "obj.name = '" + row.Cells[2].Text + "';";
                        str += "obj.styleCode = '" + row.Cells[3].Text + "';";
                        str += "obj.styleName = '" + row.Cells[4].Text + "';";
                        str += "obj.colorCode = '" + row.Cells[5].Text + "';";
                        str += "obj.colorName = '" + row.Cells[6].Text + "';";
                        str += "obj.sizeCode = '" + row.Cells[7].Text + "';";
                        str += "obj.sizeName = '" + row.Cells[8].Text + "';";
                        str += "obj.unitCode = '" + row.Cells[9].Text + "';";
                        str += "obj.unitName = '" + row.Cells[10].Text + "';";
                        str += "obj.quantity = '" + qty.Text + "';";
                        str += "arr[" + (j++) + "] = obj;";
                    }
                }
                else
                {
                    break;
                }
            }
            str += "window.returnValue = arr;window.close();";
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", str, true);
        }
    }
}
