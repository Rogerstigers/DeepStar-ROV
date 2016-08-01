using Stargazer.ViewModels;
using System;
using System.Windows.Input;

namespace Stargazer.Models
{
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
            await _parent.LoadSelectedVideo(parameter.ToString());
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
