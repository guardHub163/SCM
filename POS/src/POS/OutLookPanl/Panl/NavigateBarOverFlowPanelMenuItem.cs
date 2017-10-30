using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace OutLookPanl
{
    /// <summary>
    /// ContextMenu item for Overflowpanel
    /// </summary>
    class NavigateBarOverFlowPanelMenuItem : ToolStripMenuItem
    {

        #region NavigateBarButton
        readonly NavigateBarButton navigateBarButton = null;
        public NavigateBarButton NavigateBarButton
        {
            get { return navigateBarButton; }
        }
        #endregion

        #region Yapıcı Method

        public NavigateBarOverFlowPanelMenuItem(NavigateBarButton tNavigateBarButton, bool tCheckMenu)
        {
            navigateBarButton = tNavigateBarButton;

            if (navigateBarButton == null)
                return;

            this.Text = navigateBarButton.Caption;
            this.Image = navigateBarButton.Image;

            if (tCheckMenu)
            {
                this.CheckOnClick = true;
                this.CheckState = tNavigateBarButton.IsDisplayed ? CheckState.Checked : CheckState.Unchecked;
            }

        }
        #endregion

    }
}
