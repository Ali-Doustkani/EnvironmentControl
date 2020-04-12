using System.Windows;
using System.Windows.Input;

namespace EnvironmentControl.Views {
    public partial class ValueEditor : Window {
        public ValueEditor() {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) => Title.Focus();
    }
}
