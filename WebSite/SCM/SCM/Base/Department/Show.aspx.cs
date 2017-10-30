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
using log4net;
using System.Reflection;

namespace SCM.Web.Department
{

    public partial class Show : BaseModalDialogPage
    {
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
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
            BDepartment bll = new BDepartment();
            BaseDepartmentTable departable = bll.GetModel(CODE);
            this.lblCode.Text = departable.CODE;
            this.lblName.Text = departable.NAME;
            this.lblDerartment_code.Text = departable.Parent_name;
            this.lblAttribute1.Text = departable.ATTRIBUTE1;
            this.lblAttribute2.Text = departable.ATTRIBUTE2;
            this.lblAttribute3.Text = departable.ATTRIBUTE3;
            this.lblCreate_user.Text = departable.Creat_user_name; ;
            this.lblCreate_date_time.Text = departable.CREATE_DATE_TIME.ToString("yyyy/MM/dd");
            this.lblLast_update_user.Text = departable.Update_user_name; ;
            this.lblLast_update_time.Text = departable.LAST_UPDATE_TIME.ToString("yyyy/MM/dd");
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            return true;
        }
    }
}