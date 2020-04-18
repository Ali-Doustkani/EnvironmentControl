using System.Windows;
using EnvironmentControl.ViewModels;

namespace EnvironmentControl.Views {
    public partial class Variable {
        public Variable() {
            InitializeComponent();
        }

        private VariableViewModel ViewModel() => (VariableViewModel)DataContext;

        private async void OnLoaded(object sender, RoutedEventArgs e) {
            await ViewModel().Load();
        }
    }
}
