using Lab.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;

namespace Lab.ViewModels
{
    public class MainPageViewModel : NotificationBase
    {
        public MainPageViewModel()
        {
            deviceManager = new DeviceManager();
            foreach (var device in deviceManager.DevicePreviews)
            {
                DevicePreviews.Add(new DevicePreviewViewModel(this, device));
            }
        }

        DeviceManager deviceManager;

        ObservableCollection<DevicePreviewViewModel> _DevicePreviews = new ObservableCollection<DevicePreviewViewModel>();
        public ObservableCollection<DevicePreviewViewModel> DevicePreviews
        {
            get { return _DevicePreviews; }
            set { SetProperty(ref _DevicePreviews, value); }
        }

        DevicePreviewViewModel _SelectedDevice;
        public DevicePreviewViewModel SelectedDevice
        {
            get { return _SelectedDevice; }
            set
            {
                SetProperty(ref _SelectedDevice, value);
                SetMediaCapture();
            }
        }

        private async void SetMediaCapture()
        {
            if (MediaCapture != null)
            {
                await MediaCapture.StopPreviewAsync();
                MediaCapture.Dispose();
            }

            // Create MediaCapture and its settings
            var _mediaCapture = new MediaCapture();

            var mediaInitSettings = new MediaCaptureInitializationSettings { VideoDeviceId = SelectedDevice.DeviceId, StreamingCaptureMode = StreamingCaptureMode.Video, MediaCategory = MediaCategory.Media };

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
