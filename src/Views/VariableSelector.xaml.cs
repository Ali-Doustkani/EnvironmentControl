using EnvironmentControl.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace EnvironmentControl.Views {
    public partial class VariableSelector {
        public VariableSelector() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) => ViewModel().Load.Execute(null);

        private VariableSelectorViewModel ViewModel() => (VariableSelectorViewModel)DataContext;

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e) => Close();
    }
}
