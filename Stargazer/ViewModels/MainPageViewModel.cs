using Stargazer.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Media.Capture;
using Windows.Media.Core;
using Windows.Media.MediaProperties;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Stargazer.ViewModels
{
    public class MainPageViewModel : NotificationBase
    {
        public MainPageViewModel()
        {
        }

        public async Task Initialize()
        {
            if (CameraId != null) await SetMediaCapture();
            if (ControllerId != null) await InitializeComms();
        }

        #region Settings
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

        public string CameraId
        {
            get { return localSettings.Values["CameraId"] as string; }
        }

        public string ControllerId
        {
            get { return localSettings.Values["ControllerId"] as string; }
        }


        internal async Task ShowSettingsScreen()
        {
            if (MediaCapture != null)
            {
                MediaCapture.Dispose();
                MediaCapture = null;
            }

            var sVM = new SettingsViewModel(this);
            var settings = new ApplicationSettings(sVM);
            await settings.ShowAsync();
            await Initialize();
        }

        ShowSettingsCommand _ShowSettingsScreen;
        public ShowSettingsCommand ShowSettingsScreenCommand
        {
            get
            {
                if (_ShowSettingsScreen == null) _ShowSettingsScreen = new ShowSettingsCommand(this);
                return _ShowSettingsScreen;
            }
        }

        #endregion

        #region ROV Control
        ToggleLightCommand _ToggleLightCommand;
        public ToggleLightCommand ToggleLightCommand
        {
            get
            {
                if (_ToggleLightCommand == null) _ToggleLightCommand = new ToggleLightCommand(this);
                return _ToggleLightCommand;
            }
        }

        DPadButtonPressedCommand _DirectionalButtonPressed;
        public DPadButtonPressedCommand DirectionalButtonPressed
        {
            get
            {
                if (_DirectionalButtonPressed == null) _DirectionalButtonPressed = new DPadButtonPressedCommand(this);
                return _DirectionalButtonPressed;
            }
        }

        DPadButtonReleasedCommand _DirectionalButtonReleased;
        public DPadButtonReleasedCommand DirectionalButtonReleased
        {
            get
            {
                if (_DirectionalButtonReleased == null) _DirectionalButtonReleased = new DPadButtonReleasedCommand(this);
                return _DirectionalButtonReleased;
            }
        }

        private bool LightOn = false;
        internal async Task ToggleLight()
        {
            if (LightOn)
                await LEDOff();
            else
                await LEDOn();
        }

        internal async Task Engage(string direction)
        {
            var d = direction.ToLower();
            await AllStop();
            switch (d)
            {
                case "left":
                    await Port();
                    break;
                case "right":
                    await Starboard();
                    break;
                case "top":
                    await Ahead();
                    break;
                case "bottom":
                    await Astern();
                    break;
                case "up":
                    await Ascend();
                    break;
                case "down":
                    await Dive();
                    break;
                default:
                    break;
            }
        }

        internal async Task Disengage()
        {
            await AllStop();
        }

        internal async Task AllStop()
        {
            await Write("<ALLSTOP>");
        }

        internal async Task Ahead()
        {
            await Write("<AHEAD>");
        }

        internal async Task Astern()
        {
            await Write("<ASTEARN>");
        }

        internal async Task Port()
        {
            await Write("<PORT>");
        }

        internal async Task Starboard()
        {
            await Write("<STARBOARD>");
        }

        internal async Task Ascend()
        {
            await Write("<ASCEND>");
        }

        internal async Task Dive()
        {
            await Write("<DIVE>");
        }

        internal async Task LEDOn()
        {
            await Write("<LEDON>");
            LightOn = true;
        }

        internal async Task LEDOff()
        {
            await Write("<LEDOFF>");
            LightOn = false;
        }
        #endregion

        #region Serial Device

        private SerialDevice device = null;

        private async Task InitializeComms()
        {
            if (device != null) device.Dispose();

            device = await SerialDevice.FromIdAsync(ControllerId);
            if (device != null)
            {
                device.WriteTimeout = TimeSpan.FromMilliseconds(500);
                device.ReadTimeout = TimeSpan.FromMilliseconds(500);
                device.BaudRate = 9600;
                device.Parity = SerialParity.None;
                device.StopBits = SerialStopBitCount.One;
                device.DataBits = 8;

                Listen();
            }
            else
            {
                DeviceInformation d = await DeviceInformation.CreateFromIdAsync(ControllerId);
                var status = d.IsEnabled;
            }
        }

        private async Task Write(string value)
        {
            var writer = new DataWriter(device.OutputStream);
            writer.WriteString(value);
            await writer.StoreAsync();
            writer.DetachStream();
            writer.Dispose();
        }


        CancellationTokenSource ReadCancellationTokenSource = null;
        DataReader dataReaderObject = null;
        private async void Listen()
        {
            try
            {
                if (device != null)
                {
                    dataReaderObject = new DataReader(device.InputStream);
                    ReadCancellationTokenSource = new CancellationTokenSource();
                    // keep reading the serial input
                    while (true)
                    {
                        await ReadAsync(ReadCancellationTokenSource.Token);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        private async Task ReadAsync(CancellationToken cancellationToken)
        {
            Task<UInt32> loadAsyncTask;

            uint ReadBufferLength = 1024;

            // If task cancellation was requested, comply
            cancellationToken.ThrowIfCancellationRequested();

            // Set InputStreamOptions to complete the asynchronous read operation when one or more bytes is available
            dataReaderObject.InputStreamOptions = InputStreamOptions.Partial;

            // Create a task object to wait for data on the serialPort.InputStream
            loadAsyncTask = dataReaderObject.LoadAsync(ReadBufferLength).AsTask(cancellationToken);

            // Launch the task and wait
            UInt32 bytesRead = await loadAsyncTask;
            if (bytesRead > 0)
            {
                var rcvdText = dataReaderObject.ReadString(bytesRead);
            }
        }

        public bool HasActiveController { get { return device != null; } }

        #endregion

        #region Video Capture
        private async Task SetMediaCapture()
        {
            if (MediaCapture != null)
            {
                await MediaCapture.StopPreviewAsync();
                MediaCapture.Dispose();
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

            try
            {
                await _mediaCapture.StartPreviewAsync();
            }
            catch (Exception)
            {
            }
        }

        MediaCapture _MediaCapture;
        public MediaCapture MediaCapture
        {
            get { return _MediaCapture; }
            set { SetProperty(ref _MediaCapture, value); }
        }

        ToggleRecordingCommand _ToggleRecordingCommand;
        public ToggleRecordingCommand ToggleRecordingCommand
        {
            get
            {
                if (_ToggleRecordingCommand == null) _ToggleRecordingCommand = new ToggleRecordingCommand(this);
                return _ToggleRecordingCommand;
            }
        }

        internal async Task ToggleRecording()
        {
            if (Recording)
            {
                await MediaCapture.StopRecordAsync();
                Recording = false;
            }
            else
            {
                var recordFile = await Windows.Storage.KnownFolders.VideosLibrary.CreateFileAsync(string.Format("Stargazer_{0}.mp4", DateTime.Now.ToString("yyyyMMddhhmmss")));
                MediaCapture.Failed += MediaCapture_Failed;
                MediaCapture.RecordLimitationExceeded += MediaCapture_RecordLimitationExceeded;
                await MediaCapture.StartRecordToStorageFileAsync(MediaEncodingProfile.CreateMp4(VideoEncodingQuality.Auto), recordFile);
                Recording = true;
            }
        }

        private void MediaCapture_RecordLimitationExceeded(MediaCapture sender)
        {
            string s = string.Empty;
            Recording = false;
        }

        private void MediaCapture_Failed(MediaCapture sender, MediaCaptureFailedEventArgs errorEventArgs)
        {
            string s = string.Empty;
            Recording = false;
        }

        private bool _Recording = false;
        public bool Recording
        {
            get { return _Recording; }
            set
            {
                SetProperty(ref _Recording, value);
            }
        }
        #endregion

        #region Video Playback
        string _SelectedVideoUrl;
        public string SelectedVideoUrl
        {
            get { return _SelectedVideoUrl; }
            set
            {
                SetProperty(ref _SelectedVideoUrl, value);
            }
        }

        internal async Task LoadSelectedVideo()
        {
            var filePicker = new Windows.Storage.Pickers.FileOpenPicker();
            filePicker.FileTypeFilter.Add(".mp4");
            filePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.VideosLibrary;
            StorageFile file = await filePicker.PickSingleFileAsync();

            if (file != null)
            {
                var mediaSource = MediaSource.CreateFromStorageFile(file);

                SelectedVideo = new MediaElement { AutoPlay = false, AreTransportControlsEnabled = true, TransportControls = new MediaTransportControls() };
                IsPlayingVideo = true;

                SelectedVideo.SetPlaybackSource(mediaSource);
            }
        }

        private MediaElement _SelectedVideo;
        public MediaElement SelectedVideo
        {
            get { return _SelectedVideo; }
            set { SetProperty(ref _SelectedVideo, value); }
        }


        private SelectVideoPlaybackCommand _SelectVideoPlaybackSource;
        public SelectVideoPlaybackCommand SelectVideoPlaybackSource
        {
            get
            {
                if (_SelectVideoPlaybackSource == null)
                    _SelectVideoPlaybackSource = new SelectVideoPlaybackCommand(this);
                return _SelectVideoPlaybackSource;
            }
        }

        private bool _IsPlayingVideo = false;
        public bool IsPlayingVideo
        {
            get { return _IsPlayingVideo; }
            set
            {
                SetProperty(ref _IsPlayingVideo, value);
            }
        }
        #endregion

        ReturnToLiveVideoCommand _ReturnToLiveVideoCommand;
        public ReturnToLiveVideoCommand ReturnToLiveVideoCommand
        {
            get
            {
                if (_ReturnToLiveVideoCommand == null) _ReturnToLiveVideoCommand = new ReturnToLiveVideoCommand(this);
                return _ReturnToLiveVideoCommand;
            }
        }

        internal void GoToLiveVideo()
        {
            IsPlayingVideo = false;
        }
    }
}
