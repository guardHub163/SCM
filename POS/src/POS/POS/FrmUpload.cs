using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POS.Bll;
using POS.Model;
using POS.Common;
using UserCache;
using System.Collections;

namespace POS
{
    public partial class FrmUpload : Form
    {
        WebServieceOperate operate = new WebServieceOperate();
        public FrmUpload()
        {
            InitializeComponent();
        }

        private void prcCustomer_Click(object sender, EventArgs e)
        {

            string[] names = { "CUSTOMER" };
            GetInfo(names);
        }

        private void picCash_Click(object sender, EventArgs e)
        {
            string[] names = { "CASH" };
            GetInfo(names);
        }

        private void picSalesOrder_Click(object sender, EventArgs e)
        {
            string[] names = { "SALES" };
            GetInfo(names);
        }

        private void GetInfo(string[] names)
        {
            try
            {
                Hashtable ht = operate.SendDate(names);
                foreach (DictionaryEntry de in ht)
                {
                    listBox.Items.Insert(0, "" + DateTime.Now.ToString() + "" + "  " + "" + de.Value + "");
                }

            }
            catch { }
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
