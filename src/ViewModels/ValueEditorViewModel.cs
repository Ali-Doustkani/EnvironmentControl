using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using EnvironmentControl.Common;

namespace EnvironmentControl.ViewModels {
    public class ValueEditorViewModel : ViewModel {
        public ValueEditorViewModel(bool showDelete) {
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

        private void DoDelete() {
            Deleted = true;
            Dialog.Accept();
        }

        public string Title { get; set; }

        public string ActualValue { get; set; }

        public Dictionary<string, string> ToDictionary() =>
            new Dictionary<string, string>
            {
                {nameof(Title), Title},
                {nameof(ActualValue), ActualValue}
            };
    }
}
