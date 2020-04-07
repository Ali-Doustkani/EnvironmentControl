using System.Windows.Input;
using EnvironmentControl.Common;
using EnvironmentControl.Domain;
using EnvironmentControl.Services;

namespace EnvironmentControl.ViewModels {

    public delegate void ValueApprovedDelegate(Value newValue);

    public class ButtonViewModel : ViewModel, IValueItem {
        public ButtonViewModel() {
            NewValue = new RelayCommand(ShowEditor);
        }

        public ItemType Type => ItemType.Button;

        public ICommand NewValue { get; }

        public event ValueApprovedDelegate ValueApproved;

        private void ShowEditor() {
            var result = Dialog.ShowValueEditor();
            if (result.Accepted) {
                ValueApproved?.Invoke(result.Value);
            }
        }
    }
}
