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

namespace SCM.Web.Common
{
    public partial class ItemSearch : BaseModalDialogPage
    {
        BCommon bCommon = new BCommon();
        DataSet ds = new DataSet();
        int PageSize = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CODE", Type.GetType("System.String"));
                dt.Columns.Add("NAME", Type.GetType("System.String"));
                dt.Columns.Add("SPEC", Type.GetType("System.String"));
                for (int i = 0; i < PageSize; i++)
                {
                    dt.Rows.Add(dt.NewRow());
                }
                gridView.DataSource = dt;
                gridView.DataBind();
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
            ds = bCommon.GetItemList(this.txtItemName.Text.Trim());
            DataTable dt = ds.Tables[0];
            for (int i = dt.Rows.Count; i < PageSize; i++)
            {
                dt.Rows.Add(dt.NewRow());
            }
            gridView.DataSource = dt;
            gridView.DataBind();
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
    }
}
