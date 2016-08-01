using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
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

namespace Stargazer
{
    public sealed partial class UpDownToggle : UserControl
    {
        public UpDownToggle()
        {
            this.InitializeComponent();

            UpButton.AddHandler(PointerPressedEvent, new PointerEventHandler(UpButton_PointerPressed), true);
            UpButton.AddHandler(PointerReleasedEvent, new PointerEventHandler(UpButton_PointerReleased), true);
            DownButton.AddHandler(PointerPressedEvent, new PointerEventHandler(DownButton_PointerPressed), true);
            DownButton.AddHandler(PointerReleasedEvent, new PointerEventHandler(DownButton_PointerReleased), true);
        }

        public ICommand ButtonPressed
        {
            get { return (ICommand)GetValue(ButtonPressedProperty); }
            set { SetValue(ButtonPressedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftButtonPressed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonPressedProperty =
            DependencyProperty.Register("ButtonPressed", typeof(ICommand), typeof(DPad), new PropertyMetadata(null));

        public ICommand ButtonReleased
        {
            get { return (ICommand)GetValue(ButtonReleasedProperty); }
            set { SetValue(ButtonReleasedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonReleased.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonReleasedProperty =
            DependencyProperty.Register("ButtonReleased", typeof(ICommand), typeof(DPad), new PropertyMetadata(null));
        
        private void DownButton_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (ButtonReleased != null)
            {
                if (ButtonReleased.CanExecute("Down"))
                {
                    ButtonReleased.Execute("Down");
                }
            }
        }

        private void DownButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (ButtonPressed != null)
            {
                if (ButtonPressed.CanExecute("Down"))
                {
                    ButtonPressed.Execute("Down");
                }
            }
        }

        private void UpButton_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (ButtonReleased != null)
            {
                if (ButtonReleased.CanExecute("Up"))
                {
                    ButtonReleased.Execute("Up");
                }
            }
        }

        private void UpButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (ButtonPressed != null)
            {
                if (ButtonPressed.CanExecute("Up"))
                {
                    ButtonPressed.Execute("Up");
                }
            }
        }
    }
}
