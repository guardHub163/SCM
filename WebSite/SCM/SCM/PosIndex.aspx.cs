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
using System.Text;

namespace SCM.Web
{
    public partial class PosIndex : System.Web.UI.Page
    {
        BCommon bCommon = new BCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                init();
            }
        }

        private void init()
        {
            try
            {
                BaseUserTable userTable = (BaseUserTable)Session["UserInfo"];

                DataSet ds = bCommon.GetMenu(userTable.USER_TYPE);

                StringBuilder sb = new StringBuilder();

                sb.Append("<script>");
                sb.Append(" var parentArr = new Array();");
                sb.Append(" var childArr = new Array();");
                int cId = Convert.ToInt32(ds.Tables[0].Rows[0]["CATEGORY_ID"]);
                sb.Append("childArr.cDesc = '" + Convert.ToString(ds.Tables[0].Rows[0]["C_DESC"]) + "';");
                int i = 0;
                int j = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    int categoryId = Convert.ToInt32(row["CATEGORY_ID"]);
                    if (cId != categoryId)
                    {
                        cId = categoryId;
                        j = 0;
                        sb.AppendFormat(" parentArr[{0}] = childArr;", i++);
                        sb.Append("childArr = new Array();");
                        sb.AppendFormat("childArr.cDesc = '{0}';", Convert.ToString(row["C_DESC"]));
                    }
                    sb.Append("var menu = new Object();");
                    sb.AppendFormat(" menu.pDesc = '{0}';", Convert.ToString(row["P_DESC"]));
                    sb.AppendFormat(" menu.pUrl= '{0}';", Convert.ToString(row["FUNCTION_URL"]));
                    sb.AppendFormat(" childArr[{0}] = menu;", j++);
                }
                if (i != 0)
                {
                    sb.AppendFormat(" parentArr[{0}] = childArr;", i++);
                }
                sb.Append("</script>");
                Response.Write(sb.ToString());
            }
            catch (Exception ex) { }
        }

    }//end class
}
