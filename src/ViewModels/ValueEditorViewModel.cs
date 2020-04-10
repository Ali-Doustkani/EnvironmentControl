using System.Windows;
using EnvironmentControl.Common;
using System.Windows.Input;

namespace EnvironmentControl.ViewModels {
    public class ValueEditorViewModel : ViewModel {
        public ValueEditorViewModel(bool showDelete) {
            _showDelete = showDelete;
            Ok = new RelayCommand(Dialog.Accept);
            Cancel = new RelayCommand(Dialog.Close);
            Delete = new RelayCommand(DoDelete);
        }

        private readonly bool _showDelete;

        public string Title { get; set; }

        public string ActualValue { get; set; }

        public bool Deleted { get; private set; }

        public ICommand Ok { get; }

        public ICommand Cancel { get; }

        public ICommand Delete { get; }

        public Visibility DeleteVisibility => _showDelete ? Visibility.Visible : Visibility.Collapsed;

        private void DoDelete() {
            Deleted = true;
            Dialog.Accept();
        }
    }
}
