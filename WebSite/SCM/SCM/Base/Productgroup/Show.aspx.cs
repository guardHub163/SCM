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

namespace SCM.Web.Productgroup
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
            BProductGroup bll = new BProductGroup();
            BaseProductGroupTable productgroupTable = bll.GetModel(CODE);
            this.lblCode.Text = productgroupTable.CODE;
            this.lblName.Text = productgroupTable.NAME;
            this.lblProductGroupCode.Text = productgroupTable.Group_name;
            this.lblAttribute1.Text = productgroupTable.ATTRIBUTE1;
            this.lblAttribute2.Text = productgroupTable.ATTRIBUTE2;
            this.lblAttribute3.Text = productgroupTable.ATTRIBUTE3;
            this.lblCreate_date_time.Text = productgroupTable.CREATE_DATE_TIME.ToString("yyyy/MM/dd");
            this.lblCreate_user.Text = productgroupTable.Creat_name;
            this.lblLast_update_time.Text = productgroupTable.LAST_UPDATE_TIME.ToString("yyyy/MM/dd");
            this.lblLast_update_user.Text = productgroupTable.Update_name;
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            return true;
        }
    }
}
