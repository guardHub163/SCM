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

namespace SCM.Web.Unit
{
    public partial class Modify : BaseModalDialogPage
    {
        BUnit bll = new BUnit();
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
            BaseUnitTable unitTable = bll.GetModel(CODE);
            this.lblCode.Text = unitTable.CODE;
            this.txtUnitName.Text = unitTable.NAME;
            this.txtAttribute1.Text = unitTable.ATTRIBUTE1;
            this.txtAttribute2.Text = unitTable.ATTRIBUTE2;
            this.txtAttribute3.Text = unitTable.ATTRIBUTE3;
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
            if (this.txtUnitName.Text.Trim().Length == 0)
            {
                message += "尺码不能为空！\\n";
            }
            BaseUnitTable untable = new BaseUnitTable();
            untable.CODE = this.lblCode.Text;
            untable.NAME = this.txtUnitName.Text;
            untable.ATTRIBUTE1 = this.txtAttribute1.Text;
            untable.ATTRIBUTE2 = this.txtAttribute2.Text;
            untable.ATTRIBUTE3 = this.txtAttribute3.Text;

            untable.LAST_UPDATE_USER = UserTable.USER_ID;


            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }
            if (bll.Update(untable))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"修改成功！\");processCloseAndRefreshParent();", true);
            }
        }
    }
}
