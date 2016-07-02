using Lab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Enumeration;
using Windows.Media.Capture;

namespace Lab.ViewModels
{
    public class DevicePreviewViewModel : NotificationBase<DevicePreview>
    {
        public DevicePreviewViewModel(MainPageViewModel parent, DevicePreview dp = null) : base(dp)
        {
            SelectVideoSourceCommand = new SelectVideoSourceCommand(parent);
        }

        SelectVideoSourceCommand _SelectVideoSourceCommand;
        public SelectVideoSourceCommand SelectVideoSourceCommand
        {
            get { return _SelectVideoSourceCommand; }
            set { SetProperty(ref _SelectVideoSourceCommand, value); }
        }

        public string Device
        {
            get { return This.Device; }
            set { SetProperty(This.Device, value, () => this.Device = value); }
        }

        public string DeviceId
        {
            get { return This.DeviceId; }
            set { SetProperty(This.DeviceId, value, () => this.DeviceId = value); }
        }
    }

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
            var devicePreview = parameter as DevicePreviewViewModel;
            if (devicePreview != null) _parent.SelectedDevice = devicePreview;
        }
    }
}
