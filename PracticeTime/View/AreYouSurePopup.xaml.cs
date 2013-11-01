using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace PracticeTime.View
{
    public sealed partial class AreYouSurePopup : UserControl
    {
        public bool IsOk { get; set; }
        public string Message
        {
            set { this.DialogMessage.Text = value; }
        }

        public AreYouSurePopup()
        {
            this.InitializeComponent();
        }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            IsOk = false;
            (this.Parent as Popup).IsOpen = false;
        }

        private void ButtonOk_OnClick(object sender, RoutedEventArgs e)
        {
            IsOk = true;
            (this.Parent as Popup).IsOpen = false;
        }
    }
}
