using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POS.Model;
using POS.Bll;
using UserCache;
using POS.Common;
using System.Collections;

namespace POS
{
    public partial class FrmDownload : Form
    {
        public FrmDownload()
        {
            InitializeComponent();
        }
        WebServieceOperate operate = new WebServieceOperate();
        private bool isDownLoad = false;

        //全部获得
        private void PicAllInfo_Click(object sender, EventArgs e)
        {

            string[] tableNames = { "PRODUCT_GROUP", "PRODUCT", "STYLE", "COLOR", "SIZE", "UNIT", "PRODUCT_PRICE", "VIP_CUSTOMER", "USER", "SALES_PROMOTION" ,"NAMES"};
            GetInfo(tableNames);

        }

        //用户获得
        private void PicUser_Click(object sender, EventArgs e)
        {

            string[] tableNames = { "USER" };
            GetInfo(tableNames);
        }

        //客户获得
        private void PicVipCounst_Click(object sender, EventArgs e)
        {

            string[] tableNames = { "VIP_CUSTOMER" };
            GetInfo(tableNames);
        }

        //商品获得
        private void PicProduct_Click(object sender, EventArgs e)
        {

            string[] tableNames = { "PRODUCT" };
            GetInfo(tableNames);
        }

        //单位获得
        private void PicUnit_Click(object sender, EventArgs e)
        {

            string[] tableNames = { "UNIT" };
            GetInfo(tableNames);
        }

        //尺码获得
        private void PicSize_Click(object sender, EventArgs e)
        {

            string[] tableNames = { "SIZE" };
            GetInfo(tableNames);
        }

        //商品种类获得
        private void PicProductGroup_Click(object sender, EventArgs e)
        {

            string[] tableNames = { "PRODUCT_GROUP" };
            GetInfo(tableNames);
        }

        //款式获得
        private void PicStyle_Click(object sender, EventArgs e)
        {

            string[] tableNames = { "STYLE" };
            GetInfo(tableNames);
        }

        //单价获得
        private void PicPrice_Click(object sender, EventArgs e)
        {
            string[] tableNames = { "PRODUCT_PRICE" };
            GetInfo(tableNames);

        }

        //颜色获得
        private void PicColor_Click(object sender, EventArgs e)
        {

            string[] tableNames = { "COLOR" };
            GetInfo(tableNames);
        }


        //促销的获得
        private void PicSalesPromotion_Click(object sender, EventArgs e)
        {
            string[] tableNames = { "SALES_PROMOTION" };
            GetInfo(tableNames);
        }

        //积分方式的取得
        private void picPoints_Click(object sender, EventArgs e)
        {
            string[] tableNames = { "NAMES" };
            GetInfo(tableNames);
        }


        private void GetInfo(string[] tableNames)
        {
            if (isDownLoad)
            {
                MessageBox.Show("", this.Text);
                return;
            }
            isDownLoad = true;
            try
            {
                Hashtable ht = operate.GetDataInfo(tableNames);
                if (ht.Count == 0)
                {
                    listBox.Items.Insert(0, "" + DateTime.Now.ToString() + "" + " " + "没有可下载的信息！");
                }
                else
                {
                    foreach (DictionaryEntry de in ht)
                    {
                        listBox.Items.Insert(0, "" + DateTime.Now.ToString() + "" + " " + "" + de.Value + "");
                    }
                }
            }
            catch (Exception ex)
            {

            }
            isDownLoad = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Pic_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            pic.BackgroundImage = global::POS.Properties.Resources.边框;
        }

        private void Pic_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            pic.BackgroundImage = null;
        }

    }
}
