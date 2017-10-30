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

namespace SCM.Web.Productprice
{
    public partial class Show : BaseModalDialogPage
    {
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    decimal ID = Convert.ToDecimal(Request.Params["id"]);
                    Showinfo(ID);
                }
            }
        }
        private void Showinfo(decimal ID)
        {
            BProductprice bll = new BProductprice();
            BaseProductpriceTable priceTable = bll.GetModel(ID);
            this.lblId.Text = priceTable.ID.ToString();
            this.lblOriPrice.Text = Convert.ToString(priceTable.ORI_PRICE);
            this.lblDricount.Text = Convert.ToString(priceTable.DISCOUNT_RATE);
            this.lblPrice.Text = priceTable.SALES_PRICE.ToString();
            this.lblDepartment.Text = priceTable.Department_name;
            this.lblType.Text = priceTable.Price_name;
            this.lblStyle.Text = priceTable.Style_name;
            this.lblStartTime.Text = priceTable.START_DATE.ToString("yyyy/MM/dd");
            this.lblEndTime.Text = priceTable.END_DATE.ToString("yyyy/MM/dd");
            this.lblAttribute1.Text = priceTable.ATTRIBUTE1;
            this.lblAttribute2.Text = priceTable.ATTRIBUTE2;
            this.lblAttribute3.Text = priceTable.ATTRIBUTE3;
            this.lblCreate_user.Text = priceTable.Creat_name;
            this.lblCreate_date_time.Text = priceTable.CREATE_DATE_TIME.ToString("yyyy/MM/dd");
            this.lblLast_update_user.Text = priceTable.Update_name;
            this.lblLast_update_time.Text = priceTable.LAST_UPDATE_TIME.ToString("yyyy/MM/dd");
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            return true;
        }
    }
}
