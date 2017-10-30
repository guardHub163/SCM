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

namespace SCM.Web.Item
{
    public partial class Modify : BaseModalDialogPage
    {
        BCommon bCommon = new BCommon();
        BItem bll = new BItem();
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {
                if (Request.Params["code"] != null && Request.Params["code"].Trim() != "")
                {
                    string CODE = Request.Params["code"];
                    Showinfo(CODE);
                }
            }
        }

        private void Showinfo(string CODE)
        {
            BItem bll = new BItem();
            BaseItemTable Ptable = bll.GetModel(CODE);
            this.lblCode.Text = Ptable.CODE;
            this.txtName.Text = Ptable.NAME;
            this.txtItemspec.Text = Ptable.SPEC;
            this.lblUnitName.Text = Ptable.Unit_name;
            this.txtUnitCode.Text = Ptable.UNIT_CODE;
            this.txtAttribute1.Text = Ptable.ATTRIBUTE1;
            this.txtAttribute2.Text = Ptable.ATTRIBUTE2;
            this.txtAttribute3.Text = Ptable.ATTRIBUTE3;
            this.txtAttribute4.Text = Ptable.ATTRIBUTE4;
            this.txtAttribute5.Text = Ptable.ATTRIBUTE5;

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
                message += "名称不能为空！\\n";
            }
            if (this.txtUnitCode.Text.Trim().Length == 0) 
            {
                message += "单位不能为空！\\n";
            }
            BaseItemTable productTable = new BaseItemTable();
            productTable.CODE = this.lblCode.Text;
            productTable.NAME = this.txtName.Text;
            productTable.UNIT_CODE = this.txtUnitCode.Text;
            productTable.SPEC = this.txtItemspec.Text;
            productTable.ATTRIBUTE1 = this.txtAttribute1.Text;
            productTable.ATTRIBUTE2 = this.txtAttribute2.Text;
            productTable.ATTRIBUTE3 = this.txtAttribute3.Text;
            productTable.ATTRIBUTE4 = this.txtAttribute4.Text;
            productTable.ATTRIBUTE5 = this.txtAttribute5.Text;

                productTable.LAST_UPDATE_USER = UserTable.USER_ID;

            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }

            if (bll.Update(productTable))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"修改成功！\");processCloseAndRefreshParent();", true);
            }
        }
        protected void UnitCode_Chanage(object sender, EventArgs e)
        {
            if (this.txtUnitCode.Text.Trim() == "")
            {
                this.lblUnitName.Text = "";
                this.txtUnitCode.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_UNIT", txtUnitCode.Text.Trim(), "");
            if (table != null)
            {
                this.lblUnitName.Text = table.Name;
                this.txtUnitCode.Text = table.Code;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"单位不存在！\");", true);
                this.lblUnitName.Text = "";
                this.txtUnitCode.Text = "";
            }
        }
}
}