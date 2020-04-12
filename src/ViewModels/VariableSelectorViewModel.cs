using EnvironmentControl.Common;
using System.Windows.Input;

namespace EnvironmentControl.ViewModels {
    public class VariableSelectorViewModel : ViewModel {
        public VariableSelectorViewModel() {
            Load = new RelayCommand(DoLoad);
        }

        public ICommand Load { get; }

        public string[] Variables { get; private set; }

        private string _selectedName;
        public string SelectedName {
            get => _selectedName;
            set {
                _selectedName = value;
                Dialog.Accept();
            }
        }

        private void DoLoad() {
            Variables = Service.GetVariables();
            Notify(nameof(Variables));
        }
    }
}
