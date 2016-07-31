using System;
using System.Collections.Generic;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using System.Threading.Tasks;

namespace Stargazer.Models
{
    public static class DeviceManager
    {
        public static async Task<List<Device>> LoadSerialDevices()
        {
            List<Device> SerialDevices = new List<Device>();
            string aqs = SerialDevice.GetDeviceSelector();
            var devices = await DeviceInformation.FindAllAsync(aqs);

            foreach (var item in devices)
            {
                var dp = new Device { Name = item.Name, DeviceId = item.Id };
                SerialDevices.Add(dp);
            }

            return SerialDevices;
        }

        public static async Task<List<Device>> LoadVideoCaptureDevices()
        {
            List<Device> VideoCaptureDevices = new List<Device>();
            var devices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);

            foreach (var item in devices)
            {
                var dp = new Device { Name = item.Name, DeviceId = item.Id };
                VideoCaptureDevices.Add(dp);
            }

            return VideoCaptureDevices;
        }


    }

}
