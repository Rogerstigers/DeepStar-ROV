using Lab.Models;

namespace Lab.ViewModels
{
    public class VideoCaptureDeviceViewModel : NotificationBase<Device>
    {
        public VideoCaptureDeviceViewModel(MainPageViewModel parent, Device dp = null) : base(dp)
        {
            SelectVideoSourceCommand = new SelectVideoSourceCommand(parent);
        }

        SelectVideoSourceCommand _SelectVideoSourceCommand;
        public SelectVideoSourceCommand SelectVideoSourceCommand
        {
            get { return _SelectVideoSourceCommand; }
            set { SetProperty(ref _SelectVideoSourceCommand, value); }
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
