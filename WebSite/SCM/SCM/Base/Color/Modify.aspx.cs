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
using SCM.Model;
using SCM.Common;
using log4net;
using System.Reflection;

namespace SCM.Web.Color
{
    public partial class Modify : BaseModalDialogPage
    {
        BColor bll = new BColor();
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {
                if (Request.Params["code"] != null && Request.Params["code"].Trim() != "")
                {
                    string ID = Request.Params["code"];
                    showInfo(ID);
                }
            }
        }
        private void showInfo(string CODE) 
        {
            BaseColorTable colorTable = bll.GetModel(CODE);
            this.txtCode.Text = colorTable.CODE;
            this.txtName.Text = colorTable.NAME;
            this.txtAttribute1.Text = colorTable.ATTRIBUTE1;
            this.txtAttribute2.Text = colorTable.ATTRIBUTE2;
            this.txtAttribute3.Text = colorTable.ATTRIBUTE3;
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSave":
                    btnSave_Click(sender, e);
                    break;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string message = "";
            if (this.txtName.Text.Trim().Length == 0)
            {
                message += "颜色不能为空！\\n";
            }
            BaseColorTable colortable = new BaseColorTable();
            colortable.CODE = this.txtCode.Text;
            colortable.NAME = this.txtName.Text;
            colortable.ATTRIBUTE1 = this.txtAttribute1.Text;
            colortable.ATTRIBUTE2 = this.txtAttribute2.Text;
            colortable.ATTRIBUTE3 = this.txtAttribute3.Text;
                colortable.LAST_UPDATE_USER = UserTable.TRUE_NAME;

            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }
            if (bll.Update(colortable)) 
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"修改成功！\");processCloseAndRefreshParent();", true);
            }
        }
    }
}
