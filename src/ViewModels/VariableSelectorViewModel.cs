using System.Windows.Input;
using EnvironmentControl.Services;
using EnvironmentControl.ViewModels.Common;

namespace EnvironmentControl.ViewModels {
    public class VariableSelectorViewModel : ViewModel {
        public VariableSelectorViewModel() {
            Load = new RelayCommand(DoLoad);
        }

        public ICommand Load { get; }

        public WindowsVariable[] Variables { get; private set; }

        private WindowsVariable _selectedVariable;
        public WindowsVariable SelectedVariable {
            get => _selectedVariable;
            set {
                _selectedVariable = value;
                Dialog.Accept();
            }
        }

        private void DoLoad() {
            Variables = Service.GetVariables();
            Notify(nameof(Variables));
        }
    }
}
