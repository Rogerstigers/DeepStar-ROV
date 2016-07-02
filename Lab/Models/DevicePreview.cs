using System.ComponentModel;

namespace Lab.Models
{
    public class DevicePreview : INotifyPropertyChanged
    {
        public DevicePreview() { }
        
        private string device;

        public string Device
        {
            get { return device; }
            set
            {
                device = value;
                OnPropertyChanged("Device");
            }
        }


        private string deviceId;

        public string DeviceId
        {
            get { return deviceId; }
            set
            {
                deviceId = value;
                OnPropertyChanged("DeviceId");
            }
        }
        
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
    }
}
