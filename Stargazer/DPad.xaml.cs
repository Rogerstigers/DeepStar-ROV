using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class DPad : UserControl
    {
        public DPad()
        {
            this.InitializeComponent();
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

        private void LeftButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (ButtonPressed != null) {
                if (ButtonPressed.CanExecute("Left"))
                {
                    ButtonPressed.Execute("Left");
                }
            }
        }

        private void LeftButton_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (ButtonReleased != null)
            {
                if (ButtonReleased.CanExecute("Left"))
                {
                    ButtonReleased.Execute("Left");
                }
            }
        }
        private void RightButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (ButtonPressed != null)
            {
                if (ButtonPressed.CanExecute("Right"))
                {
                    ButtonPressed.Execute("Right");
                }
            }

        }

        private void RightButton_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (ButtonReleased != null)
            {
                if (ButtonReleased.CanExecute("Right"))
                {
                    ButtonReleased.Execute("Right");
                }
            }

        }
        private void TopButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (ButtonPressed != null)
            {
                if (ButtonPressed.CanExecute("Top"))
                {
                    ButtonPressed.Execute("Top");
                }
            }

        }

        private void TopButton_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (ButtonReleased != null)
            {
                if (ButtonReleased.CanExecute("Top"))
                {
                    ButtonReleased.Execute("Top");
                }
            }

        }
        private void BottomButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (ButtonPressed != null)
            {
                if (ButtonPressed.CanExecute("Bottom"))
                {
                    ButtonPressed.Execute("Bottom");
                }
            }
        }

        private void BottomButton_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (ButtonReleased != null)
            {
                if (ButtonReleased.CanExecute("Right"))
                {
                    ButtonReleased.Execute("Right");
                }
            }

        }
    }
}
