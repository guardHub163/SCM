using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using SCM.Bll;
using SCM.Model;
using log4net;
using System.Reflection;

namespace SCM.Web.User
{
    public partial class Show : BaseModalDialogPage
    {
        BCommon bCommon = new BCommon();
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {
                selUserType.DataSource = bCommon.GetNames("USER_TYPE").Tables[0];
                selUserType.DataTextField = "NAME"; //dropdownlist的Text的字段 
                selUserType.DataValueField = "CODE";//dropdownlist的Value的字段 
                selUserType.DataBind();
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    string strid = Request.Params["id"];
                    int ID = (Convert.ToInt32(strid));
                    ShowInfo(ID);
                }
            }
        }

        private void ShowInfo(int ID)
        {
            BUser bll = new BUser();
            BaseUserTable userTable = bll.GetModel(ID);
            this.lblUSER_ID.Text = userTable.USER_ID;            
            this.lblTRUE_NAME.Text = userTable.TRUE_NAME;
            this.lblSEX.Text = userTable.SEX;
            this.lblPHONE.Text = userTable.PHONE;
            this.lblEMAIL.Text = userTable.EMAIL;
            this.txtDepartmentCode.Text = userTable.DEPARTMENT_CODE;
            this.lblDepartmentName.Text = userTable.DEPARTMENT_NAME;
            this.txtSupplierCode.Text = userTable.SUPPLIER_CODE;
            this.lblSupplierName.Text = userTable.SUPPLIER_NAME;
            this.selUserType.SelectedValue = userTable.USER_TYPE;
            this.lblROLES_ID.Text = userTable.ROLES_ID.ToString();
            this.lblSTYLE.Text = userTable.STYLE.ToString();
            this.imgPhoto.Src = "../../Image.aspx?TYPE=USER&FILE_NAME=" + userTable.PHOTO_PATH;
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            return true;
        }
    }
}
