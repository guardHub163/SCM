using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace POS
{
    /// <summary>
    /// DataGridView
    /// </summary>
    public partial class MyDataGrid : DataGridView
    {
        //private Image m_Image;

        public MyDataGrid()
        {
            InitializeComponent();
            //m_Image = global::POS.Properties.Resources.footer;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
        ///<summary>
        ///重写PaintBackground
        ///</summary> 
        protected override void PaintBackground(Graphics graphics, Rectangle clipBounds, Rectangle gridBounds)
        {
            //base.PaintBackground(graphics, clipBounds, gridBounds);
            //graphics.DrawImage(this.m_Image, gridBounds);
            base.PaintBackground(graphics, clipBounds, gridBounds);
        }
        /// <summary>
        /// 设置背景图片
        /// </summary>       
        //public Image BackImage
        //{
        //    get { return this.m_Image; }
        //    set { this.m_Image = value; base.Refresh(); }  // 重新加载
        //}

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            base.OnCellPainting(e);
            if (e.ColumnIndex == -1 && e.RowIndex == -1)
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(e.CellBounds, Color.FromArgb(46, 108, 150),
                    Color.FromArgb(153, 190, 213), LinearGradientMode.ForwardDiagonal))
                {
                    e.Graphics.FillRectangle(brush, e.CellBounds);
                    Rectangle border = e.CellBounds;
                    border.Offset(new Point(-1, -1));
                    e.Graphics.DrawRectangle(Pens.Gray, border);
                }
                e.PaintContent(e.CellBounds);
                e.Handled = true;
            }
            else if (e.RowIndex == -1)
            {
                //标题行
                using (LinearGradientBrush brush = new LinearGradientBrush(e.CellBounds, Color.FromArgb(153, 190, 213), 
                    Color.FromArgb(46, 108, 150),LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, e.CellBounds);
                    Rectangle border = e.CellBounds;
                    border.Offset(new Point(-1, -1));
                    e.Graphics.DrawRectangle(Pens.Gray, border);
                }
                e.PaintContent(e.CellBounds);
                e.Handled = true;
            }
            else if (e.ColumnIndex == -1)
            {
                //标题列
                using (LinearGradientBrush brush = new LinearGradientBrush(e.CellBounds, Color.LightGray,
                    Color.White, LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, e.CellBounds);
                    Rectangle border = e.CellBounds;
                    border.Offset(new Point(-1, -1));
                    e.Graphics.DrawRectangle(Pens.Gray, border);
                }
                e.PaintContent(e.CellBounds);
                e.Handled = true;
            }
        }
    }//END CLASS    
}
