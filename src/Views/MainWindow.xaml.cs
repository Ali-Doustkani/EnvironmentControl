using System.Windows;
using System.Windows.Input;

namespace EnvironmentControl.Views {
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
        }

        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e) => DragMove();
    }
}
