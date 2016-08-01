using Stargazer.ViewModels;
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

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Stargazer
{
    public sealed partial class ApplicationSettings : ContentDialog
    {
        public ApplicationSettings(SettingsViewModel parent)
        {
            this.InitializeComponent();
            VM = parent;
            VM.LoadDevices();
        }

        public SettingsViewModel VM { get; set; }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            VM.Dispose();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void CaptureElement_Tapped(object sender, TappedRoutedEventArgs e)
        {
            VM.PreviewCaptureElement();
        }
    }
}
