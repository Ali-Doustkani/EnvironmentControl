using System.Windows;
using System.Windows.Input;
using EnvironmentControl.Common;
using EnvironmentControl.Domain;

namespace EnvironmentControl.ViewModels {

    public delegate void ValueApprovedDelegate(Value newValue);

    public class ButtonViewModel : ViewModel, IValueItem {
        public ButtonViewModel() {
            NewValue = new RelayCommand(ShowEditor);
            _state = State.Normal;
        }

        private State _state;

        public ItemType Type => ItemType.Button;

        public ICommand NewValue { get; }

        public event ValueApprovedDelegate ValueApproved;

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

        private void ShowEditor() {
            var result = Dialog.ShowValueEditor();
            if (result.Accepted) {
                ValueApproved?.Invoke(new Value(result.Title, result.ActualValue));
            }
        }
    }
}
