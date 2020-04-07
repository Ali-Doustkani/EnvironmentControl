using EnvironmentControl.Common;
using System.Windows.Input;

namespace EnvironmentControl.ViewModels {
    public class ValueEditorViewModel : ViewModel{
        public ValueEditorViewModel()
        {
            Ok = new RelayCommand(Dialog.Accept);
            Cancel = new RelayCommand(Dialog.Close);
        }

        public string Title { get; set; }
        public string ActualValue { get; set; }

        public ICommand Ok { get; }

        public ICommand Cancel { get; }
    }
}
