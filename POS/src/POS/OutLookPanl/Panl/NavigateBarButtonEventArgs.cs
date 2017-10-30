using System;
using System.Collections.Generic;
using System.Text;

namespace OutLookPanl
{
    #region Class : NavigateBarButtonEventArgs

    /// <summary>
    /// NavigateBarButton EventArgs
    /// </summary>
    public sealed class NavigateBarButtonEventArgs : EventArgs
    {

        #region NavigateBarButton
        NavigateBarButton navigateBarButton;
        /// <summary>
        /// Selected NavigateBarButton
        /// </summary>
        public NavigateBarButton NavigateBarButton
        {
            get { return navigateBarButton; }
        }
        #endregion

        public NavigateBarButtonEventArgs(NavigateBarButton tNavigateBarButton)
        {
            if (tNavigateBarButton == null)
                throw new NullReferenceException("Cannot null tNavigateBarButton");

            navigateBarButton = tNavigateBarButton;
        }

    }
    #endregion
}
