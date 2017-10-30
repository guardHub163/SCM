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
using System.Reflection;
using log4net;

namespace SCM.Web.Style
{
    public partial class Modify : BaseModalDialogPage
    {
        BStyle bll = new BStyle();
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
            BaseStyleTable styleTable = bll.GetModel(CODE);
            this.lblCode.Text = styleTable.CODE;
            this.txtStyleName.Text = styleTable.NAME;
            this.txtAttribute1.Text = styleTable.ATTRIBUTE1;
            this.txtAttribute2.Text = styleTable.ATTRIBUTE2;
            this.txtAttribute3.Text = styleTable.ATTRIBUTE3;
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
            if (this.txtStyleName.Text.Trim().Length == 0)
            {
                message += "样式不能为空！\\n";
            }
            BaseStyleTable styletable = new BaseStyleTable();
            styletable.CODE = this.lblCode.Text;
            styletable.NAME = this.txtStyleName.Text;
            styletable.ATTRIBUTE1 = this.txtAttribute1.Text;
            styletable.ATTRIBUTE2 = this.txtAttribute2.Text;
            styletable.ATTRIBUTE3 = this.txtAttribute3.Text;
            styletable.LAST_UPDATE_USER = UserTable.TRUE_NAME;


            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }
            if (bll.Update(styletable))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"修改成功！\");processCloseAndRefreshParent();", true);
            }
        }
    }
}