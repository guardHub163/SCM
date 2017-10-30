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
using System.Windows.Forms;
namespace SCM.Web.Common
{
    public partial class LoadImage : System.Web.UI.Page
    {
        UpLoadAndSaveImage img = new UpLoadAndSaveImage();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["PC"] != null && Request.QueryString["PC"] != "") 
                {
                    txtProductCode.Text = Request.QueryString["PC"];
                }
            }
        }
       

        protected void Upload(object sender, EventArgs e)
        {
            string path = HttpContext.Current.Server.MapPath("~") + "\\" + "UploadFiles" + "\\" + "Images" + "\\" + "PRODUCT" + "\\" + txtProductCode.Text.Trim()+"\\";
            img.FormFile = (HtmlInputFile)this.form1.FindControl("txtFile");
            img.SavePath = path;
            img.IsDraw = true;
            img.Upload();
            switch (img.Error) // Error返回值，1、没有上传的文件。2、类型不允许。3、大小超限。4、未知错误。0、上传成功。 
            {
                case 0 :
                    Response.Write("<script> window.parent.document.getElementById('divImgList').style.visibility = 'visible';</script> ");
                    Response.Write("<script> window.parent.document.getElementById('divOperate').style.visibility = 'visible';</script> ");
                    Response.Write("<script> window.parent.document.getElementById('divUpLoadImg').style.visibility = 'hidden';</script> ");                    
                    Response.Write("<script> window.parent.processAddLi('" + txtProductCode.Text.Trim() + "','" + img.OutFileName + "');</script>");
                    break;
                case 1:
                    Response.Write("<script>alert('没有上传的文件');</script> ");
                    break;
                case 2:
                    Response.Write("<script>alert('类型不允许');</script> ");
                    break;
                case 3:
                    Response.Write("<script>alert('大小超限');</script> ");
                    break;
                case 4:
                    Response.Write("<script>alert('未知错误');</script> ");
                    break;
            }
        }
    }
}
