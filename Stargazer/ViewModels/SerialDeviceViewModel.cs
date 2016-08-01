using Stargazer.Models;

namespace Stargazer.ViewModels
{
    public class SerialDeviceViewModel : NotificationBase<Device>
    {
        public SerialDeviceViewModel(MainPageViewModel parent, Device dp = null) : base(dp)
        {
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
