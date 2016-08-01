using Stargazer.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;

namespace Stargazer.ViewModels
{
    public class SettingsViewModel : NotificationBase
    {
        public SettingsViewModel(MainPageViewModel parent) : base()
        {
            MVM = parent;
        }

        MainPageViewModel MVM;

        public async Task LoadDevices()
        {
            foreach (var device in await DeviceManager.LoadVideoCaptureDevices())
            {
                VideoCaptureDevices.Add(new VideoCaptureDeviceViewModel(MVM, device));
            }

            foreach (var device in await DeviceManager.LoadSerialDevices())
            {
                SerialDevices.Add(new SerialDeviceViewModel(MVM, device));
            }

            RaisePropertyChanged("ControllerId");
            RaisePropertyChanged("CameraId");
            if (CameraId != null) PreviewCaptureElement();
        }

        internal void Dispose()
        {
            if(MediaCapture != null)
            {
                MediaCapture.Dispose();
            }
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
                if (value != null)
                {
                    SetLocalSettingProperty("CameraId", value);
                    PreviewCaptureElement();
                }
            }
        }

        ObservableCollection<VideoCaptureDeviceViewModel> _VideoCaptureDevices = new ObservableCollection<VideoCaptureDeviceViewModel>();
        public ObservableCollection<VideoCaptureDeviceViewModel> VideoCaptureDevices
        {
            get { return _VideoCaptureDevices; }
            set { SetProperty(ref _VideoCaptureDevices, value); }
        }

        ObservableCollection<SerialDeviceViewModel> _SerialDevices = new ObservableCollection<SerialDeviceViewModel>();
        public ObservableCollection<SerialDeviceViewModel> SerialDevices
        {
            get { return _SerialDevices; }
            set { SetProperty(ref _SerialDevices, value); }
        }

        internal async Task PreviewCaptureElement()
        {
            if (MediaCapture != null)
            {
                await MediaCapture.StopPreviewAsync();
                MediaCapture.Dispose();
                MediaCapture = null;
            }

            // Create MediaCapture and its settings
            var _mediaCapture = new MediaCapture();

            var mediaInitSettings = new MediaCaptureInitializationSettings { VideoDeviceId = CameraId, StreamingCaptureMode = StreamingCaptureMode.Video, MediaCategory = MediaCategory.Media };

            // Initialize MediaCapture
            try
            {
                await _mediaCapture.InitializeAsync(mediaInitSettings);
            }
            catch (UnauthorizedAccessException)
            {
            }
            catch (Exception ex)
            {
            }

            var maxRes = _mediaCapture.VideoDeviceController
                                    .GetAvailableMediaStreamProperties(MediaStreamType.VideoPreview)
                                    .Select(x => x as VideoEncodingProperties)
                                    .OrderByDescending(x => x.Height * x.Width)
                                    .FirstOrDefault();

            await _mediaCapture.VideoDeviceController.SetMediaStreamPropertiesAsync(MediaStreamType.VideoPreview, maxRes);
            MediaCapture = _mediaCapture;
            await _mediaCapture.StartPreviewAsync();
        }

        MediaCapture _MediaCapture;
        public MediaCapture MediaCapture
        {
            get { return _MediaCapture; }
            set { SetProperty(ref _MediaCapture, value); }
        }
    }
}
