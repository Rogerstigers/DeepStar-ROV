using Stargazer.Models;
using System;
using System.Linq;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;

namespace Stargazer.ViewModels
{
    public class VideoCaptureDeviceViewModel : NotificationBase<Device>
    {
        public VideoCaptureDeviceViewModel(MainPageViewModel parent, Device dp = null) : base(dp)
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
