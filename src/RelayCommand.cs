using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EnvironmentControl {
    public class RelayCommand : ICommand {
        public RelayCommand(Func<Task> todo) {
            _todo = todo;
        }

        private readonly Func<Task> _todo;

        public bool CanExecute(object parameter) => true;

        public async void Execute(object parameter) => await _todo();

        public event EventHandler CanExecuteChanged;
    }
}
