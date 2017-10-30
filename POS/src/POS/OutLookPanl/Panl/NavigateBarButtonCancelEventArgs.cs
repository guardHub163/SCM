using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace OutLookPanl
{
    #region Class : NavigateBarButtonEventArgs
    /// <summary>
    /// CancelEventArgs for NavigateBar.OnNavigateBarButtonSelecting event
    /// </summary>
    public class NavigateBarButtonCancelEventArgs : CancelEventArgs
    {

        #region Selected
        NavigateBarButton selected;
        /// <summary>
        /// New Selected NavigateBarButton
        /// <para>get</para>
        /// </summary>
        public NavigateBarButton Selected
        {
            get { return selected; }
        }
        #endregion

        #region PreviousSelected
        NavigateBarButton previousSelected;
        /// <summary>
        /// Previous Selected NavigateBarButton
        /// <para>Get</para>
        /// </summary>
        public NavigateBarButton PreviousSelected
        {
            get { return previousSelected; }
        }
        #endregion

        public NavigateBarButtonCancelEventArgs(NavigateBarButton tSelected, NavigateBarButton tPreviousSelected)
        {
            selected = tSelected;
            previousSelected = tPreviousSelected;
            this.Cancel = false;
        }
    }
    #endregion
}
