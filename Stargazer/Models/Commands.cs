using Stargazer.ViewModels;
using System;
using System.Windows.Input;

namespace Stargazer.Models
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

    public class SelectVideoPlaybackCommand: ICommand
    {
        private MainPageViewModel _parent;
        public SelectVideoPlaybackCommand(MainPageViewModel parent)
        {
            _parent = parent;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await _parent.LoadSelectedVideo(parameter.ToString());
        }
    }
}
