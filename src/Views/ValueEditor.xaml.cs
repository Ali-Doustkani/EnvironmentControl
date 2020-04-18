using System.Windows;
using System.Windows.Input;
using EnvironmentControl.ViewModels;

namespace EnvironmentControl.Views {
    public partial class ValueEditor : Window {
        public ValueEditor() {
            InitializeComponent();
        }

        private ValueEditorViewModel ViewModel() => (ValueEditorViewModel)DataContext;

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e) {
            await ViewModel().Load();
            Title.Focus();
        }
    }
}
