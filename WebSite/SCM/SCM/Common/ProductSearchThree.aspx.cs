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
using SCM.Model;
using SCM.Bll;

namespace SCM.Web.Common
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProductSearchThree : System.Web.UI.Page    //BaseModalDialogPage
    {
        BCommon bCommon = new BCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TemplateField customField = new TemplateField();
                customField.ShowHeader = true;
                customField.ItemStyle.Width = new Unit(60);
                customField.HeaderTemplate = new GridViewTemplate(DataControlRowType.Header, "S");//添加的列标题  
                GridViewTemplate gvt = new GridViewTemplate(DataControlRowType.DataRow, "txtQuantity_S", "");//空白列  
                customField.ItemTemplate = gvt;
                gridView.Columns.Add(customField);

                DataTable dt = new DataTable();
                dt.Columns.Add("COLOR_CODE", Type.GetType("System.String"));
                dt.Columns.Add("COLOR_NAME", Type.GetType("System.String"));
                dt.Columns.Add("txtQuantity_S", Type.GetType("System.String"));
                DataRow row = dt.NewRow();
                row["COLOR_CODE"] = "01";
                row["COLOR_NAME"] = "红色";
                dt.Rows.Add(row);
                row = dt.NewRow();
                row["COLOR_CODE"] = "02";
                row["COLOR_NAME"] = "白色";
                dt.Rows.Add(row);

                gridView.DataSource = dt;
                gridView.DataBind();
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
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"款式不存在！\");", true);
                this.lblStyleName.Text = "";
                this.txtStyleCode.Text = "";
            }
        }

    }//end class

    /// <summary>
    /// 
    /// </summary>
    public class GridViewTemplate : ITemplate
    {
        public delegate void EventHandler(object sender, EventArgs e);
        public event EventHandler eh;
        private DataControlRowType templateType;
        private string columnName;
        private string controlID;
        public GridViewTemplate(DataControlRowType type, string colname)
        {
            templateType = type;
            columnName = colname;
        }
        public GridViewTemplate(DataControlRowType type, string controlID, string colname)
        {
            templateType = type;
            this.controlID = controlID;
            columnName = colname;
        }
        public void InstantiateIn(System.Web.UI.Control container)
        {
            switch (templateType)
            {
                case DataControlRowType.Header:
                    Literal lc = new Literal();
                    lc.Text = columnName;
                    container.Controls.Add(lc);
                    break;
                case DataControlRowType.DataRow://可以定义自己想显示的控件以及绑定事件
                    TextBox text = new TextBox();
                    text.ID = controlID;
                    text.Attributes.Add("onkeydown", "processKeyPress();");
                    container.Controls.Add(text);
                    break;
                default:
                    break;
            }
        }

    }//end class
}
