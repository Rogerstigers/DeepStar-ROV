using System;
using System.Collections.Generic;
using Windows.Devices.Enumeration;

namespace Lab.Models
{
    public class DeviceManager
    {
        public List<DevicePreview> DevicePreviews { get; set; } = new List<DevicePreview>();

        public DeviceManager() {
            LoadDevicePreviews();
        }

        private async void LoadDevicePreviews()
        {
            var vcd = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);

            foreach (var item in vcd)
            {
                var dp = new DevicePreview { Device = item.Name, DeviceId = item.Id };
                DevicePreviews.Add(dp);
            }
        }

    }

}
