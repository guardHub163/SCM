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

namespace SCM.Web.Productgroup
{
    public partial class Modify : BaseModalDialogPage
    {
        BProductGroup bll = new BProductGroup();
        BCommon bCommon = new BCommon();
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {
                if (Request.Params["code"] != null && Request.Params["code"].Trim() != "")
                {
                    string ID = Request.Params["code"];
                    Showinfo(ID);
                }
            }

        }

        private void Showinfo(string CODE)
        {
            BProductGroup bll = new BProductGroup();
            BaseProductGroupTable productgroupTable = bll.GetModel(CODE);
            this.txtCode.Text = productgroupTable.CODE;
            this.txtName.Text = productgroupTable.NAME;
            this.txtProductGroupCode.Text = productgroupTable.PARENT_CODE;
            this.lblProductGroupName.Text = productgroupTable.Group_name;
            this.txtAttribute1.Text = productgroupTable.ATTRIBUTE1;
            this.txtAttribute2.Text = productgroupTable.ATTRIBUTE2;
            this.txtAttribute3.Text = productgroupTable.ATTRIBUTE3;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string message = "";
            if (this.txtName.Text.Trim().Length == 0)
            {
                message += "种类名称不能为空！\\n";
            }
            if (this.txtProductGroupCode.Text.Trim().Length == 0)
            {
                message += "种类不能为空！\\n";
            }
            BaseProductGroupTable productgroup = new BaseProductGroupTable();
            productgroup.CODE = this.txtCode.Text.Trim();
            productgroup.NAME = this.txtName.Text.Trim();
            productgroup.PARENT_CODE = this.txtProductGroupCode.Text;
            productgroup.ATTRIBUTE1 = this.txtAttribute1.Text;
            productgroup.ATTRIBUTE2 = this.txtAttribute2.Text;
            productgroup.ATTRIBUTE3 = this.txtAttribute3.Text;
            productgroup.LAST_UPDATE_USER = UserTable.TRUE_NAME;

            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }
            if (bll.Update(productgroup))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"修改成功！\");processCloseAndRefreshParent();", true);
            }

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

        protected void ProductGroupCode_Chanage(object sender, EventArgs e)
        {
            if (txtProductGroupCode.Text.Trim() == "")
            {
                this.lblProductGroupName.Text = "";
                this.txtProductGroupCode.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_PRODUCT_GROUP", txtProductGroupCode.Text.Trim(), "");
            if (table != null)
            {
                this.lblProductGroupName.Text = table.Name;
                this.txtProductGroupCode.Text = table.Code;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"种类不存在！\");", true);
                this.lblProductGroupName.Text = "";
                this.txtProductGroupCode.Text = "";
            }
        }
    }
}
