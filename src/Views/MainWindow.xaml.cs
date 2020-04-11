using System.ComponentModel;
using System.Windows;
using EnvironmentControl.ViewModels;
using System.Windows.Input;

namespace EnvironmentControl.Views {
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
        }

        private MainViewModel ViewModel() => (MainViewModel)DataContext;

        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e) {
            ViewModel().Load.Execute(null);
            Top = ViewModel().Top;
            Left = ViewModel().Left;
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e) {
            ViewModel().Top = Top;
            ViewModel().Left = Left;
            ViewModel().Closing.Execute(null);
        }

        private void Close_Click(object sender, RoutedEventArgs e) => Close();
    }
}
