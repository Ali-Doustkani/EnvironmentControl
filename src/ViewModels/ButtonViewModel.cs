using EnvironmentControl.Common;
using System;
using System.Windows;
using System.Windows.Input;

namespace EnvironmentControl.ViewModels {
    public class ButtonViewModel : ViewModel, ITypedViewModel {
        public ButtonViewModel(Action onClick) {
            Command = new RelayCommand(onClick);
            _state = State.Normal;
        }

        private State _state;

        public int Type => 2;

        public ICommand Command { get; }

        public Visibility Visibility {
            get {
                if (_state == State.Editing)
                    return Visibility.Visible;
                return Visibility.Collapsed;
            }
        }

        public void SetState(State state) {
            _state = state;
            Notify(nameof(Visibility));
        }
    }
}
