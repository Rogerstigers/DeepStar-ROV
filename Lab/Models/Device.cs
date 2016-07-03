using System.ComponentModel;

namespace Lab.Models
{
    public class Device : INotifyPropertyChanged
    {
        public Device() { }
        
        private string device;

        public string Name
        {
            get { return device; }
            set
            {
                device = value;
                OnPropertyChanged("Name");
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
