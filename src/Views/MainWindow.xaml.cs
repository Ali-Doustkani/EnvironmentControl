using System.ComponentModel;
using System.Windows;
using EnvironmentControl.ViewModels;
using System.Windows.Input;
using EnvironmentControl.ViewModels.States;

namespace EnvironmentControl.Views {
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
            DataContext = new MainViewModel(new StateManager());
        }

        private bool _closeApp;

        private MainViewModel ViewModel() => (MainViewModel)DataContext;

        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e) {
            ViewModel().Load.Execute(null);
            Top = ViewModel().Top;
            Left = ViewModel().Left;
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e) {
            if (!_closeApp) {
                Hide();
                e.Cancel = true;
                return;
            }
            ViewModel().Top = Top;
            ViewModel().Left = Left;
            ViewModel().Closing.Execute(null);
        }

        private void Close_Click(object sender, RoutedEventArgs e) => Hide();

        private void Show(object sender, RoutedEventArgs e) => Show();

        private void MenuItem_OnClick(object sender, RoutedEventArgs e) {
            _closeApp = true;
            Application.Current.Shutdown();
        }
    }
}
