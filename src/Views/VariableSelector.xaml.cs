using EnvironmentControl.ViewModels;
using System.Windows;

namespace EnvironmentControl.Views {
    public partial class VariableSelector {
        public VariableSelector() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) => ViewModel().Load.Execute(null);

        private VariableSelectorViewModel ViewModel() => (VariableSelectorViewModel)DataContext;
    }
}
