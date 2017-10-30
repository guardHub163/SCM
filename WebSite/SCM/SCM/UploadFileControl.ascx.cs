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
namespace SCM.Web
{
    public partial class UploadFileControl : System.Web.UI.UserControl
    {
 
        //用委托添加添加UploadFile委托
        public delegate void UploadFileEventHandler(object sender, EventArgs e, HtmlInputFile file);
        //用委托改UploadFile添加委托
        public event UploadFileEventHandler Process_UploadFile;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Process_Upload(object sender, EventArgs e)
        {
            if (Process_UploadFile != null)
            {
                Process_UploadFile(sender, e, aFile);
            }
        }

        protected void Process_Cancel(object sender, EventArgs e)
        {
            if (Process_UploadFile != null)
            {
                Process_UploadFile(sender, e, null);
            }
        }
    }//end class
}
