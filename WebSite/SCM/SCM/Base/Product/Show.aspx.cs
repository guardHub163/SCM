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

namespace SCM.Web.Product
{
    public partial class Show : BaseModalDialogPage
    {
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
            BProduct bll = new BProduct();
            BaseProductTable productTable = bll.GetModel(CODE);
            this.lblCode.Text = productTable.CODE;
            this.lblName.Text = productTable.NAME;
            this.lblStyleCode.Text = productTable.STYLE_NAME;
            this.lblProductGroupCode.Text = productTable.PRODUCT_GROUP_NAME;
            this.lblSizeCode.Text = productTable.SIZE_NAME;
            this.lblColorCode.Text = productTable.COLOR_NAME;
            this.lblUnitCode.Text = productTable.UNIT_NAME;
            this.lblAttribute1.Text = productTable.ATTRIBUTE1;
            this.lblAttribute2.Text = productTable.ATTRIBUTE2;
            this.lblAttribute3.Text = productTable.ATTRIBUTE3;
            this.lblCreate_date_time.Text = productTable.CREATE_DATE_TIME.ToString("yyyy/MM/dd");
            this.lblCreate_user.Text = productTable.CREATE_USER_NAME;
            this.lblLast_update_time.Text = productTable.LAST_UPDATE_TIME.ToString("yyyy/MM/dd");
            this.lblLast_update_user.Text = productTable.UPDATE_USER_NAME;
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            return true;
        }
    }
}