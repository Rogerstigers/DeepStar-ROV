using Stargazer.Models;
using System;
using System.Linq;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;

namespace Stargazer.ViewModels
{
    public class SettingsViewModel : NotificationBase
    {
        public SettingsViewModel() : base()
        {

        }

        public string ControllerId
        {
            get
            {
                return localSettings.Values["ControllerId"] as string;
            }
            set
            {
                SetLocalSettingProperty("ControllerId", value);
            }
        }

        public string CameraId
        {
            get
            {
                return localSettings.Values["CameraId"] as string;
            }
            set
            {
                SetLocalSettingProperty("CameraId", value);
            }
        }
    }
}
