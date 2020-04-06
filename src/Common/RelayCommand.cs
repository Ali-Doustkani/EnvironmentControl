using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EnvironmentControl.Common {
    public class RelayCommand : ICommand {
        public RelayCommand(Func<Task> asyncTodo) {
            _asyncTodo = asyncTodo;
        }

        public RelayCommand(Action todo) {
            _todo = todo;
        }

        private readonly Func<Task> _asyncTodo;
        private readonly Action _todo;

        public bool CanExecute(object parameter) => true;

        public async void Execute(object parameter) {
            if (_asyncTodo != null)
                await _asyncTodo();
            else
                _todo();
        }

        public event EventHandler CanExecuteChanged;
    }
}
