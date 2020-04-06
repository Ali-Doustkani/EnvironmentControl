using EnvironmentControl.Views;
using System.Windows;

namespace EnvironmentControl.Services {
    public class DialogService : IDialogService {
        public void CloseWindow() {
            Application.Current.MainWindow?.Close();
        }
    }
}
