using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

using OutLookPanl.Design;

namespace OutLookPanl
{
    /// <summary>
    /// Show related control on this panel
    /// </summary>
    class NavigateBarControlPanel : UserControl
    {

        #region NavigateBar
        NavigateBar navigateBar = null;
        public NavigateBar NavigateBar
        {
            get { return navigateBar; }
            set
            {
                navigateBar = value;
                Invalidate();
            }
        }
        #endregion

        public NavigateBarControlPanel()
        {

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw, true);
        }

        #region Overrided Methodlar

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (this.Controls.Count == 0 && !this.DesignMode)
            {
                base.OnPaintBackground(e);

                NavigateBarHelper.PaintGradientControl(this, e.Graphics,
                    navigateBar.NavigateBarColorTable.ButtonNormalBegin,
                    navigateBar.NavigateBarColorTable.ButtonNormalEnd,
                    navigateBar.NavigateBarColorTable.PaintAngle);
            }
        }
        #endregion
    }
}
