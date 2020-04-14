using EnvironmentControl.Common;
using System;
using System.Windows;
using System.Windows.Input;

namespace EnvironmentControl.ViewModels {
    public class ButtonViewModel : ViewModel, ITypedViewModel {
        public ButtonViewModel(StateManager stateManager, Action onClick) {
            _stateManager = stateManager;
            _stateManager.StateChanged += StateChanged;
            Command = new RelayCommand(onClick);
        }

        private readonly StateManager _stateManager;

        public int Type => 2;

        public ICommand Command { get; }

        public Visibility Visibility {
            get {
                if (_stateManager.Current.State == State.Editing)
                    return Visibility.Visible;
                return Visibility.Collapsed;
            }
        }

        private void StateChanged(AppState obj) => Notify(nameof(Visibility));
    }
}
