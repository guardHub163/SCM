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
using System.Reflection;
using log4net;

namespace SCM.Web.Size
{
    public partial class Show : BaseModalDialogPage
    {
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        BSize bll = new BSize();
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

        private void showInfo(string code, string groupCode)
        {
            BaseSizeTable sizeTable = bll.GetModel(code, groupCode);
            this.lblCode.Text = sizeTable.CODE;
            this.lblName.Text = sizeTable.NAME;
            this.lblRefence.Text = sizeTable.REFERENCE_PERCENTAGE.ToString();
            this.lblProeuctgroupname.Text = sizeTable.PRODUCT_GROUP_NAME;
            this.lblAttribute1.Text = sizeTable.ATTRIBUTE1;
            this.lblAttribute2.Text = sizeTable.ATTRIBUTE2;
            this.lblAttribute3.Text = sizeTable.ATTRIBUTE3;
            this.lblCreate_date_time.Text = sizeTable.CREATE_DATE_TIME.ToString("yyyy/MM/dd");
            this.lblCreate_user.Text = sizeTable.User_name;
            this.lblLast_update_time.Text = sizeTable.LAST_UPDATE_TIME.ToString("yyyy/MM/dd");
            this.lblLast_update_user.Text = sizeTable.Update_name;
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            return true;
        }
    }
}
