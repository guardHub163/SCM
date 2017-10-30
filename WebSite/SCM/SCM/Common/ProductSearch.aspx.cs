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
using SCM.Web;
using SCM.Bll;
using SCM.Common;
using System.Text;
using SCM.Model;

namespace SCM.Web.Common
{
    public partial class ProductSearch : BaseModalDialogPage
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
                    dt.Columns.Add("STYLE_NAME", Type.GetType("System.String"));
                    dt.Columns.Add("COLOR_NAME", Type.GetType("System.String"));
                    dt.Columns.Add("SIZE_NAME", Type.GetType("System.String"));
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
                if (e.Row.Cells[0].Text != "&nbsp;" && e.Row.Cells[0].Text != "")
                {
                    e.Row.Attributes.Add("id", "row_" + e.Row.RowIndex.ToString());
                    e.Row.Attributes.Add("onClick", "processRowClick('row_" + e.Row.RowIndex.ToString() + "');");
                }
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
                    this.btnOK.Disabled = true;
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

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSearch":
                    Search(sender, e);
                    break;
            }
            return true;
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
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"款式不存在！\");", true);
                this.lblStyleName.Text = "";
                this.txtStyleCode.Text = "";
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
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"颜色不存在！\");", true);
                this.lblColorName.Text = "";
                this.txtColorCode.Text = "";
            }
        }
        protected void SizeCode_Chanage(object sender, EventArgs e)
        {

            if (this.txtSizeCode.Text.Trim() == "")
            {
                this.lblSizeName.Text = "";
                this.txtSizeCode.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_SIZE", txtSizeCode.Text.Trim(), "");
            if (table != null)
            {
                this.lblSizeName.Text = table.Name;
                this.txtSizeCode.Text = table.Code;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"尺码不存在！\");", true);
                this.lblSizeName.Text = "";
                this.txtSizeCode.Text = "";
            }
        }
    }
}
