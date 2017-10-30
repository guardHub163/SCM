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
using System.IO;
namespace SCM.Web.Common
{
    public partial class ShowImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["PC"] != null && Request.QueryString["PC"] != "")
                {
                    txtProductCode.Text = Request.QueryString["PC"];
                }
                btnDelete.Attributes.Add("onClick", "return confirm(\"你确认要删除吗?\")");
                init();
            }           
        }

        private void init()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SRC", Type.GetType("System.String"));
            dt.Columns.Add("BIG_SRC", Type.GetType("System.String"));
            dt.Columns.Add("FILE_NAME", Type.GetType("System.String"));
            string path = HttpContext.Current.Server.MapPath("~") + "\\" + "UploadFiles" + "\\" + "Images" + "\\" + "PRODUCT" + "\\" + txtProductCode.Text + "\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string[] strFiles = Directory.GetFiles(path, "s_*");
            bool flag = true;
            maxImg.Src = "";
            txtCurrentImage.Text = "";
            foreach (string file in strFiles)
            {
                DataRow dr = dt.NewRow();
                dr["FILE_NAME"] = file.Substring(file.LastIndexOf('\\') + 3);
                dr["SRC"] = "../Image.aspx?TYPE=PRODUCT&PRODUCT_CODE=" + txtProductCode.Text + "&FILE_NAME=" + file.Substring(file.LastIndexOf('\\') + 1);
                dr["BIG_SRC"] = "../Image.aspx?TYPE=PRODUCT&PRODUCT_CODE=" + txtProductCode.Text + "&FILE_NAME=" + file.Substring(file.LastIndexOf('\\') + 3);
                dt.Rows.Add(dr);
                if (flag)
                {
                    flag = false;
                    maxImg.Src = Convert.ToString(dr["BIG_SRC"]);
                    txtCurrentImage.Text = Convert.ToString(dr["FILE_NAME"]);
                }
            }
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
            int width = strFiles.Length * 82;
            if (width < 802)
            {
                width = 802;
            }
            this.myUl.Style.Add("width", width + "px;");
        }

        protected void Repeater_ItemDataBound(object sender, EventArgs e)
        {

        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            string path = HttpContext.Current.Server.MapPath("~") + "\\" + "UploadFiles" + "\\" + "Images" + "\\" + "PRODUCT" + "\\" + txtProductCode.Text + "\\";
            if (this.txtCurrentImage.Text.Trim() != "")
            {
                File.Delete(path + txtCurrentImage.Text.Trim());
                File.Delete(path + "s_" + txtCurrentImage.Text.Trim());
                init();
            }
            else 
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"没有可删除的图片!\");", true);
            }
        }

    }//end class
}
