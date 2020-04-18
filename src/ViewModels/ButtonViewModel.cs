using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using EnvironmentControl.ViewModels.Common;
using EnvironmentControl.ViewModels.States;
using EditStatus = EnvironmentControl.ViewModels.States.EditStatus;

namespace EnvironmentControl.ViewModels {
    public class ButtonViewModel : ViewModel, ITypedViewModel {
        public ButtonViewModel(StateManager stateManager, Func<Task> onClick) {
            _stateManager = stateManager;
            _stateManager.StateChanged += StateChanged;
            Command = new RelayCommand(onClick);
        }

        private readonly StateManager _stateManager;

        public int Type => 2;

        public ICommand Command { get; }

        public Visibility Visibility {
            get {
                if (_stateManager.Current.EditStatus == EditStatus.Editing)
                    return Visibility.Visible;
                return Visibility.Collapsed;
            }
        }

        private void StateChanged(AppState obj) => Notify(nameof(Visibility));
    }
}
