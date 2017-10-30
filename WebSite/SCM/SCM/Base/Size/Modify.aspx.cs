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

namespace SCM.Web.Size
{
    public partial class Modify : BaseModalDialogPage
    {
        BSize bll = new BSize();
        BCommon bCommon = new BCommon();
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {
                if (Request.Params["code"] != null && Request.Params["code"].Trim() != "")
                {
                    string code = Request.Params["code"];
                    string groupCode = Request.Params["gcode"];
                    showInfo(code, groupCode);
                }
            }
        }

        private void showInfo(string code,string groupCode)
        {
            BaseSizeTable sizeTable = bll.GetModel(code, groupCode);
            this.lblCode.Text = sizeTable.CODE;
            this.txtSizeName.Text = sizeTable.NAME;
            this.txtReference.Text = Convert.ToString(sizeTable.REFERENCE_PERCENTAGE);
            this.txtProductGroupCode.Text = sizeTable.PRODUCT_GROUP_CODE;
            this.lblProductGroupName.Text = sizeTable.PRODUCT_GROUP_NAME;
            this.txtAttribute1.Text = sizeTable.ATTRIBUTE1;
            this.txtAttribute2.Text = sizeTable.ATTRIBUTE2;
            this.txtAttribute3.Text = sizeTable.ATTRIBUTE3;
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
            if (this.txtSizeName.Text.Trim().Length == 0)
            {
                message += "尺码不能为空！\\n";
            }

            BaseSizeTable sizetable = new BaseSizeTable();
            sizetable.CODE = this.lblCode.Text;
            sizetable.NAME = this.txtSizeName.Text;
            sizetable.REFERENCE_PERCENTAGE = Convert.ToDecimal(this.txtReference.Text);
            sizetable.ATTRIBUTE1 = this.txtAttribute1.Text;
            sizetable.ATTRIBUTE2 = this.txtAttribute2.Text;
            sizetable.ATTRIBUTE3 = this.txtAttribute3.Text;
            sizetable.PRODUCT_GROUP_CODE = this.txtProductGroupCode.Text;
            sizetable.LAST_UPDATE_USER = UserTable.USER_ID;


            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }
            if (bll.Update(sizetable))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"修改成功！\");processCloseAndRefreshParent();", true);
            }
        }

    }
}
