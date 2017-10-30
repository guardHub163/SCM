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
using log4net;
using System.Reflection;
using SCM.Common;
using System.IO;

namespace SCM.Web.Common
{
    public partial class UpLoadInfo : BaseModalDialogPage
    {
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        int flag = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (Request.Params["code"] != null && Request.Params["code"].Trim() != "")
            {
                flag =Convert.ToInt32( Request.Params["code"].ToString());
            }

        }

        protected void Upload(object sender, EventArgs e)
        {
                HttpPostedFile hpFile = fileUpload.PostedFile;
                if (hpFile.ContentLength > 0)
                {
                    string fileExtension = System.IO.Path.GetExtension(hpFile.FileName);//获取文件扩展名
                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;//新的文件名
                    string path = HttpContext.Current.Server.MapPath("~") + "\\" + "UploadFiles" + "\\" + "Images" + "\\" + "PRODUCT" + "\\" + fileName;
                    hpFile.SaveAs(path);
                    HttpContext.Current.Session["IMPORT_FILE_NAME"] = path;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "window.close();", true);
                }
                else 
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "alert('请选择上传文件');", true);
                }
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnUpload":
                    Upload(sender, e);
                    break;
            }
            return true;
        }
    }
}
