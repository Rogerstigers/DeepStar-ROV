﻿using Lab.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage.Streams;

namespace Lab.ViewModels
{
    public class MainPageViewModel : NotificationBase
    {
        public MainPageViewModel()
        {
        }

        public async Task LoadDevices()
        {
            foreach (var device in await DeviceManager.LoadVideoCaptureDevices())
            {
                VideoCaptureDevices.Add(new VideoCaptureDeviceViewModel(this, device));
            }

            foreach (var device in await DeviceManager.LoadSerialDevices())
            {
                SerialDevices.Add(new SerialDeviceViewModel(this, device));
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

        VideoCaptureDeviceViewModel _SelectedCaptureDevice;
        public VideoCaptureDeviceViewModel SelectedCaptureDevice
        {
            get { return _SelectedCaptureDevice; }
            set
            {
                SetProperty(ref _SelectedCaptureDevice, value);
                SetMediaCapture();
            }
        }

        SerialDeviceViewModel _SelectedSerialDevice;
        public SerialDeviceViewModel SelectedSerialDevice
        {
            get { return _SelectedSerialDevice; }
            set
            {
                SetProperty(ref _SelectedSerialDevice, value);
                InitializeComms();
            }
        }

        #region ROV Commands

        internal async void AllStop()
        {
            Write("<ALLSTOP>");
        }

        internal async void Ahead()
        {
            Write("<AHEAD>");
        }

        internal async void Astern()
        {
            Write("<ASTEARN>");
        }

        internal async void Port()
        {
            Write("<PORT>");
        }

        internal async void Starboard()
        {
            Write("<STARBOARD>");
        }

        internal async void Ascend()
        {
            Write("<ASCEND>");
        }

        internal async void Dive()
        {
            Write("<DIVE>");
        }

        internal async void LEDOn()
        {
            Write("<LEDON>");
        }

        internal async void LEDOff()
        {
            Write("<LEDOFF>");
        }


        #endregion

        #region Serial Device

        private SerialDevice device = null;

        private async void InitializeComms()
        {
            if (device != null) device.Dispose();

            device = await SerialDevice.FromIdAsync(SelectedSerialDevice.DeviceId);
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
                DeviceInformation d = await DeviceInformation.CreateFromIdAsync(SelectedSerialDevice.DeviceId);
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

        #endregion

        #region Video Capture
        private async void SetMediaCapture()
        {
            if (MediaCapture != null)
            {
                await MediaCapture.StopPreviewAsync();
                MediaCapture.Dispose();
            }

            // Create MediaCapture and its settings
            var _mediaCapture = new MediaCapture();

            var mediaInitSettings = new MediaCaptureInitializationSettings { VideoDeviceId = SelectedCaptureDevice.DeviceId, StreamingCaptureMode = StreamingCaptureMode.Video, MediaCategory = MediaCategory.Media };

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
        #endregion
    }
}
