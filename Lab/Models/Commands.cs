using Lab.ViewModels;
using System;
using System.Windows.Input;

namespace Lab.Models
{

    public class SelectVideoSourceCommand : ICommand
    {
        private MainPageViewModel _parent;
        public SelectVideoSourceCommand(MainPageViewModel parent)
        {
            _parent = parent;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var devicePreview = parameter as VideoCaptureDeviceViewModel;
            if (devicePreview != null) _parent.SelectedCaptureDevice = devicePreview;
        }
    }

    public class SelectSerialDeviceCommand : ICommand
    {
        private MainPageViewModel _parent;
        public SelectSerialDeviceCommand(MainPageViewModel parent)
        {
            _parent = parent;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var devicePreview = parameter as SerialDeviceViewModel;
            if (devicePreview != null) _parent.SelectedSerialDevice = devicePreview;
        }
    }
}
