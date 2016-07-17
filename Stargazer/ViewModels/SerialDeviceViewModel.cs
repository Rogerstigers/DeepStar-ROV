using Lab.Models;

namespace Lab.ViewModels
{
    public class SerialDeviceViewModel : NotificationBase<Device>
    {
        public SerialDeviceViewModel(MainPageViewModel parent, Device dp = null) : base(dp)
        {
            SelectSerialDeviceCommand = new SelectSerialDeviceCommand(parent);
        }

        SelectSerialDeviceCommand _SelectSerialDeviceCommand;
        public SelectSerialDeviceCommand SelectSerialDeviceCommand
        {
            get { return _SelectSerialDeviceCommand; }
            set { SetProperty(ref _SelectSerialDeviceCommand, value); }
        }

        public string Name
        {
            get { return This.Name; }
            set { SetProperty(This.Name, value, () => this.Name = value); }
        }

        public string DeviceId
        {
            get { return This.DeviceId; }
            set { SetProperty(This.DeviceId, value, () => this.DeviceId = value); }
        }
    }
}
