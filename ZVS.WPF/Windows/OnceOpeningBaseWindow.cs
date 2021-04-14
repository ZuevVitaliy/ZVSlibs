using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ZVS.WPF.Libs.Windows
{
    /// <summary>
    /// Представляет окно, в котором можно открыть лишь один экземпляр для конкретного типа дочернего окна.
    /// </summary>
    public class OnceOpeningBaseWindow : Window
    {
        #region Commented

        //protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        //{
        //    var viewModel = DataContext as BaseWindowViewModel;
        //    if (sizeInfo.WidthChanged)
        //    {
        //        this.Height = sizeInfo.NewSize.Width / viewModel.ActualWindowRatio;
        //    }
        //    else
        //    {
        //        this.Width = sizeInfo.NewSize.Height * viewModel.ActualWindowRatio;
        //    }
        //}

        #endregion Commented

        protected HashSet<Window> OpenedChildWindows { get; } = new HashSet<Window>();

        protected OnceOpeningBaseWindow ParentWindow { get; }

        protected virtual void OpenWindow(Window openingWindow)
        {
            var sameOpenedWindow = OpenedChildWindows.FirstOrDefault(x => x.GetType() == openingWindow.GetType());
            if (sameOpenedWindow == null)
            {
                OpenedChildWindows.Add(openingWindow);
                openingWindow.Show();
                openingWindow.Closed += OnClosedActions;
            }
            else { sameOpenedWindow.Focus(); }
        }

        protected virtual void OpenDialogWindow(Window openingDialogWindow)
        {
            var sameOpenedWindow = OpenedChildWindows.FirstOrDefault(x => x.GetType() == openingDialogWindow.GetType());
            if (sameOpenedWindow == null)
            {
                OpenedChildWindows.Add(openingDialogWindow);
                openingDialogWindow.ShowDialog();
                OpenedChildWindows.Remove(openingDialogWindow);
            }
            else { sameOpenedWindow.Focus(); }
        }

        protected virtual void OnClosedActions(object sender, EventArgs e)
        {
            OpenedChildWindows.Remove(sender as Window);
        }
    }
}