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
namespace POS
{
    public partial class FrmProduct : Form
    {
        DataTable productGrouplst;
        public string retProductCode = "";
        string productCode = "";
        DataSet ds = new DataSet();
        BCommon bCommon = new BCommon();
        BProduct bProduct = new BProduct();
        ItemList item = null;
        bool _flag = false;

        public FrmProduct()
        {
            InitializeComponent();
        }

        public FrmProduct(string productCode)
        {
            this.productCode = productCode;
            InitializeComponent();
        }

        private void FrmProduct_Load(object sender, EventArgs e)
        {

            //衣服种类的建立
            createProductTree();

            //设置树展开
            productGroup.ExpandAll();

            //商品的建立           
            this.txtSearchKey.Text = productCode;

            try
            {
                ds = bCommon.GetNames("PRODUCT_KEY");
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    cmbProduct.Items.Add(new ItemList(Convert.ToString(row["CODE"]), Convert.ToString(row["NAME"])));
                }
                cmbProduct.SelectedIndex = 0;
            }
            catch { }
            if (txtSearchKey.Text.Trim() != "")
            {
                btnSearch_Click(null, EventArgs.Empty);
            }
            _flag = true;
        }

        private void createProductTree()
        {
            BProductGroup bProductGroup = new BProductGroup();
            DataTable productGrouplst = bProductGroup.GetList(" status_flag<>9 ").Tables[0];
            Bind_Tv(productGrouplst, productGroup.Nodes, null, "CODE", "PARENT_CODE", "NAME");
        }

        #region 绑定TreeView
        /// <summary>
        /// 绑定TreeView（利用TreeNodeCollection）
        /// </summary>
        /// <param name="tnc">TreeNodeCollection（TreeView的节点集合）</param>
        /// <param name="pid_val">父id的值</param>
        /// <param name="id">数据库 id 字段名</param>
        /// <param name="pid">数据库 父id 字段名</param>
        /// <param name="text">数据库 文本 字段值</param>
        private void Bind_Tv(DataTable dt, TreeNodeCollection tnc, string pid_val, string id, string pid, string text)
        {
            DataView dv = new DataView(dt);//将DataTable存到DataView中，以便于筛选数据
            TreeNode tn;//建立TreeView的节点（TreeNode），以便将取出的数据添加到节点中
            //以下为三元运算符，如果父id为空，则为构建“父id字段 is null”的查询条件，否则构建“父id字段=父id字段值”的查询条件
            string filter = string.IsNullOrEmpty(pid_val) ? pid + "=''" : string.Format(pid + "='{0}'", pid_val);
            dv.RowFilter = filter;//利用DataView将数据进行筛选，选出相同 父id值 的数据
            foreach (DataRowView drv in dv)
            {
                tn = new TreeNode();//建立一个新节点（学名叫：一个实例）
                tn.Tag = drv[id].ToString();//节点的Value值，一般为数据库的id值
                tn.Text = drv[text].ToString();//节点的Text，节点的文本显示
                tnc.Add(tn);//将该节点加入到TreeNodeCollection（节点集合）中
                Bind_Tv(dt, tn.Nodes, tn.Tag.ToString(), id, pid, text);//递归（反复调用这个方法，直到把数据取完为止）
            }
        }

        private void productGroup_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //页面初始化的时候不走
            if (_flag)
            {
                TreeNode tn = e.Node;
                string sWhere = " AND P.GROUP_CODE='" + tn.Tag.ToString() + "' ";
                Bind_DataGrid(sWhere);
            }
            else
            {
                _flag = true;
            }
        }

        #endregion

        #region 绑定dataGrid
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void Bind_DataGrid(string sWhere)
        {
            DataTable productDataTable = bProduct.GetProductList(sWhere).Tables[0];
            productGridView.DataSource = productDataTable;
            this.Show();
            this.productGridView.Focus();
        }

        /// <summary>
        /// PANEL_边框线
        /// </summary>
        private void panelTitle_Paint(object sender, PaintEventArgs e)
        {
            //ControlPaint.DrawBorder(e.Graphics,
            //                    this.panelTitle.ClientRectangle,
            //                    Color.LightSeaGreen,//7f9db9
            //                    0,
            //                    ButtonBorderStyle.Solid,
            //                    Color.LightSeaGreen,
            //                    0,
            //                    ButtonBorderStyle.Solid,
            //                    Color.LightSeaGreen,
            //                    0,
            //                    ButtonBorderStyle.Solid,
            //                    Color.White,
            //                    1,
            //                    ButtonBorderStyle.Solid);
        }

        #endregion

        /// <summary>
        /// 查询
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (txtSearchKey.Text.Trim() != "")
            {
                item = (ItemList)(this.cmbProduct.SelectedItem);
                switch (item.Value)
                {
                    case "1":
                        sb.AppendFormat("AND( P.CODE LIKE '{0}%' OR STYLE LIKE '{1}%')", txtSearchKey.Text.Trim(), txtSearchKey.Text.Trim());
                        break;
                    case "2":
                        sb.AppendFormat("AND P.CODE LIKE '{0}%'", txtSearchKey.Text.Trim());
                        break;
                    case "3":
                        sb.AppendFormat("AND COLOR LIKE '{0}%'", txtSearchKey.Text.Trim());
                        break;
                    case "4":
                        sb.AppendFormat("AND NAME LIKE '%{0}%'", txtSearchKey.Text.Trim());
                        break;
                    case "5":
                        sb.AppendFormat("AND SIZE LIKE '{0}%'", txtSearchKey.Text.Trim());
                        break;
                    case "6":
                        sb.AppendFormat("AND STYLE LIKE '{0}%'", txtSearchKey.Text.Trim());
                        break;
                }
            }
            Bind_DataGrid(sb.ToString());
            productGridView.Focus();
        }

        private void txtSeachKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        #region 确定
        /// <summary>
        /// 确定
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (productGridView.RowCount > 0)
            {
                retProductCode = productGridView.SelectedRows[0].Cells[0].Value.ToString();
            }
            this.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        private void productGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK_Click(null, EventArgs.Empty);
            }
        }

        private void productGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnOK_Click(null, EventArgs.Empty);
        }
        #endregion

        /// <summary>
        /// 取消
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void txtEnter(object sender, EventArgs e)
        //{
        //    ((TextBox)sender).Focus();
        //    ((TextBox)sender).SelectAll();
        //}

    }
}
