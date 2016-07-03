using Lab.ViewModels;
using Windows.UI.Xaml.Controls;
using Microsoft.Maker.RemoteWiring;
using Microsoft.Maker.Serial;
using System;

using System.Collections.ObjectModel;
using Windows.Devices.SerialCommunication;
using Windows.Devices.Enumeration;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Lab
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            VM = new MainPageViewModel();

            VM.LoadDevices();
        }

        public MainPageViewModel VM { get; set; }

        public ObservableCollection<TypeWrapper> Ports { get; set; } = new ObservableCollection<TypeWrapper>();

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            VM.LEDOn();
        }

        private void Button_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            VM.LEDOff();

        }

        private void Button_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            VM.LEDOff();
        }

        private void Button_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            VM.LEDOn();
        }

        private void Button_Holding(object sender, Windows.UI.Xaml.Input.HoldingRoutedEventArgs e)
        {
            if (e.HoldingState == Windows.UI.Input.HoldingState.Started)
                VM.Port();
            else
                VM.AllStop();
        }

        private void Button_Holding1(object sender, Windows.UI.Xaml.Input.HoldingRoutedEventArgs e)
        {
            if (e.HoldingState == Windows.UI.Input.HoldingState.Started)
                VM.Starboard();
            else
                VM.AllStop();
        }

        private void Button_Holding2(object sender, Windows.UI.Xaml.Input.HoldingRoutedEventArgs e)
        {
            if (e.HoldingState == Windows.UI.Input.HoldingState.Started)
                VM.Ascend();
            else
                VM.AllStop();
        }

        private void Button_Holding3(object sender, Windows.UI.Xaml.Input.HoldingRoutedEventArgs e)
        {
            if (e.HoldingState == Windows.UI.Input.HoldingState.Started)
                VM.Dive();
            else
                VM.AllStop();
        }

        private void Button_Holding4(object sender, Windows.UI.Xaml.Input.HoldingRoutedEventArgs e)
        {
            if (e.HoldingState == Windows.UI.Input.HoldingState.Started)
                VM.Ahead();
            else
                VM.AllStop();
        }

        private void Button_Holding5(object sender, Windows.UI.Xaml.Input.HoldingRoutedEventArgs e)
        {
            if (e.HoldingState == Windows.UI.Input.HoldingState.Started)
                VM.Astern();
            else
                VM.AllStop();
        }
    }

    public class TypeWrapper
    {
        public string String { get; set; }
    }

    class DeepStarController
    {
        public DeepStarController()
        {

        }

        public void LED_ON()
        {

        }

    }
}
