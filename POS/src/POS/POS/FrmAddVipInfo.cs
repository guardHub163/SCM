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
using System.Threading;

namespace POS
{
    public partial class FrmAddVipInfo : Form
    {
        BVipCustomer bll = new BVipCustomer();
        DataSet ds = new DataSet();
        private BaseUserTable _userTable;
        private string _customerCode;
        public bool flag = false;

        #region init
        public FrmAddVipInfo()
        {
            InitializeComponent();
        }

        public FrmAddVipInfo(BaseUserTable _user)
        {
            InitializeComponent();
            _userTable = _user;
        }

        public FrmAddVipInfo(BaseUserTable userTable, string customerCode)
        {
            InitializeComponent();
            _userTable = userTable;
            _customerCode = customerCode;
        }

        private void FrmAddVipInfo_Load(object sender, EventArgs e)
        {
            if (_customerCode != "")
            {
                txtCode.Text = _customerCode;
                this.txtCode.Enabled = false;
            }
            else
            {
                this.txtCode.Enabled = true;
            }
        }

        #endregion

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.txtCode.Text.Trim().Length == 0)
            {
                MessageBox.Show("编号不能为空!");
                return;
            }
            else if (bll.Exists(txtCode.Text.Trim()))
            {
                 MessageBox.Show("编号已经存在!");
                 return;
            }
            if (this.txtName.Text.Trim().Length == 0)
            {
                MessageBox.Show("名称不能为空！");
                return;
            }
            BaseVipCustomerTable vipTable = new BaseVipCustomerTable();
            vipTable.CODE = txtCode.Text.Trim();
            vipTable.NAME = txtName.Text.Trim();
            vipTable.ADDRESS = txtAddress.Text.Trim();
            vipTable.QQ = txtQQ.Text.Trim();
            vipTable.WW = txtWW.Text.Trim();
            vipTable.BIRTH_DATE = Convert.ToDateTime(txtBirth.Value.ToString("yyyy/MM/dd"));
            vipTable.EMAIL = txtEmail.Text;
            vipTable.LAST_SALES_DATE = DateTime.Now;
            vipTable.DEPARTMENT_CODE = Cache.DEPARTMENT_CODE;
            vipTable.VIP_LEVEL = 0;
            vipTable.DISCOUNT_RATE = 0;
            vipTable.POINTS = 0;
            vipTable.USED_POINTS = 0;
            vipTable.STATUS_FLAG = 0;
            vipTable.ATTRIBUTE1 = "";
            vipTable.ATTRIBUTE2 = "";
            vipTable.ATTRIBUTE3 = "";
            vipTable.CREATE_USER = _userTable.USER_ID;
            vipTable.LAST_UPDATE_USER = _userTable.USER_ID;
            if (bll.Add(vipTable) > 0)
            {
                Thread thread = new Thread(SendVipDate);
                thread.Start();
                this.DialogResult = DialogResult.OK;
                flag = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("新建客户失败！", this.Text);
            }
        }

        private void btnCanel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAddVipInfo_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    btnOk_Click(this, EventArgs.Empty);
                    break;

                case Keys.Escape:
                    btnCanel_Click(this, EventArgs.Empty);
                    break;
               
            }
        }
        //吧新建的信息传回SCM
        public void SendVipDate()
        {
            try
            {
                DataSet ds = bll.GetAllInfo(StrWhere());
                DataTable da = ds.Tables[0];
                string tableName = "CUSTOMER";
                string Datasetxml = Data.GetDataSetXml(tableName, da);
                POS.CZZD.SCM.WebService webService = new POS.CZZD.SCM.WebService();
                string webServiceKey = tableName + Datasetxml.Length + Common.Constant.WEBSERVICE_POS2SCM_KEY;
                string retXml = webService.SetDataInfo(tableName, Datasetxml, Common.DESEncrypt.Encrypt(webServiceKey));
                DataSet dst = Data.ConvertXMLToDataSet(retXml);
                foreach (DataRow row in dst.Tables[0].Rows)
                {
                    if (row["STATUS"].ToString() == Constant.SUCCESS)
                    {
                        bll.UpdateFlag(Constant.NORMAL, row["SLIP_NUMBER"].ToString());
                    }
                    else
                    {
                        bll.UpdateFlag(Constant.INIT, row["SLIP_NUMBER"].ToString());
                       
                    }
                }
            }
            catch { }
        }

        public string StrWhere()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("1=1");
            if (this.txtCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND CODE='{0}'", this.txtCode.Text.Trim());
            }
            return sb.ToString();
        }

    }//end class
}
