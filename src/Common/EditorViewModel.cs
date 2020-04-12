using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace EnvironmentControl.Common {
    public abstract class EditorViewModel : ViewModel {
        protected EditorViewModel(bool showDelete) {
            _showDelete = showDelete;
            Ok = new RelayCommand(Dialog.Accept);
            Cancel = new RelayCommand(Dialog.Close);
            Delete = new RelayCommand(DoDelete);
        }

        private readonly bool _showDelete;

        public ICommand Ok { get; }

        public ICommand Cancel { get; }

        public ICommand Delete { get; }

        public bool Deleted { get; private set; }

        public Visibility DeleteVisibility => _showDelete ? Visibility.Visible : Visibility.Collapsed;

        public abstract Dictionary<string, string> ToDictionary();

        private void DoDelete() {
            Deleted = true;
            Dialog.Accept();
        }
    }
}
