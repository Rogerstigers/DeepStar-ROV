using Stargazer.ViewModels;
using System;
using System.Windows.Input;

namespace Stargazer.Models
{
    public class MotorTestCommand : ICommand
    {
        private MainPageViewModel _parent;
        public MotorTestCommand(MainPageViewModel parent)
        {
            _parent = parent;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await _parent.MotorTest();
        }
    }
    public class ToggleLightCommand : ICommand
    {
        private MainPageViewModel _parent;
        public ToggleLightCommand(MainPageViewModel parent)
        {
            _parent = parent;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await _parent.ToggleLight();
        }
    }

    public class ToggleRecordingCommand : ICommand
    {
        private MainPageViewModel _parent;
        public ToggleRecordingCommand(MainPageViewModel parent)
        {
            _parent = parent;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await _parent.ToggleRecording();
        }
    }

    public class PublishToYouTubeCommand : ICommand
    {
        private MainPageViewModel _parent;
        public PublishToYouTubeCommand(MainPageViewModel parent)
        {
            _parent = parent;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await _parent.PublishToYouTube();
        }
    }

    public class ShowSettingsCommand : ICommand
    {
        private MainPageViewModel _parent;
        public ShowSettingsCommand(MainPageViewModel parent)
        {
            _parent = parent;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return !_parent.Recording;
        }

        public async void Execute(object parameter)
        {
            await _parent.ShowSettingsScreen();
        }
    }

    public class SelectVideoPlaybackCommand: ICommand
    {
        private MainPageViewModel _parent;
        public SelectVideoPlaybackCommand(MainPageViewModel parent)
        {
            _parent = parent;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await _parent.LoadSelectedVideo();
        }
    }

    public class ReturnToLiveVideoCommand : ICommand
    {
        private MainPageViewModel _parent;
        public ReturnToLiveVideoCommand(MainPageViewModel parent)
        {
            _parent = parent;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _parent.GoToLiveVideo();
        }
    }

    public class DPadButtonPressedCommand: ICommand
    {
        private MainPageViewModel _parent;
        public DPadButtonPressedCommand(MainPageViewModel parent)
        {
            _parent = parent;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _parent.HasActiveController;
        }

        public async void Execute(object parameter)
        {
            await _parent.Engage(parameter as string);
        }
    }

    public class DPadButtonReleasedCommand : ICommand
    {
        private MainPageViewModel _parent;
        public DPadButtonReleasedCommand(MainPageViewModel parent)
        {
            _parent = parent;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await _parent.Disengage();
        }
    }
}
