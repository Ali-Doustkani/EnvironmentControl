using System.Collections.Generic;
using EnvironmentControl.Common;
using EnvironmentControl.Domain;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EnvironmentControl.ViewModels {
    public class MainViewModel : ViewModel {
        public MainViewModel() {
            Load = new RelayCommand(DoLoad);
            Closing = new RelayCommand(DoClosing);
            Edit = new RelayCommand(DoEdit);
            _state = State.Normal;
        }

        private State _state;

        public ICommand Load { get; }

        public ICommand Closing { get; }

        public ICommand Edit { get; }

        public ObservableCollection<ITypedViewModel> Items { get; private set; }

        public string EditText => _state == State.Editing ? "End Editing" : "Edit";

        public double Top { get; set; }

        public double Left { get; set; }

        private async Task DoLoad() {
            var result = await Service.Load();
            Top = result.Top;
            Left = result.Left;
            var list = new List<ITypedViewModel>();
            list.AddRange(result.Variables.Select(x => new VariableViewModel(x)));
            var addButton = new ButtonViewModel();
            addButton.CommandMade += CommandMade;
            list.Add(addButton);
            Items = new ObservableCollection<ITypedViewModel>(list);
            Notify(nameof(Items), nameof(Top), nameof(Left));
        }

        private void CommandMade() {
            var result = Dialog.ShowVariableSelector();
            if (result.Accepted) {
                var newVariable = new Variable(result["Name"]);
                newVariable.AddValue("Default", Service.GetVariable(result["Name"]));
                var vm = new VariableViewModel(newVariable);
                vm.SetState(_state);
                Items.Insert(Items.Count - 1, vm);
                Service.SaveVariable(newVariable);
            }
        }

        private async Task DoClosing() {
            await Service.SaveCoordination(Top, Left);
        }

        private void DoEdit() {
            _state = _state == State.Normal ? State.Editing : State.Normal;
            foreach (var item in Items) {
                item.SetState(_state);
            }
            Notify(nameof(EditText));
        }
    }
}
