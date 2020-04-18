using EnvironmentControl.Common;
using EnvironmentControl.Domain;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EnvironmentControl.States;
using EditStatus = EnvironmentControl.States.EditStatus;

namespace EnvironmentControl.ViewModels {
    public class MainViewModel : ViewModel {
        public MainViewModel(StateManager stateManager) {
            Load = new RelayCommand(DoLoad);
            Closing = new RelayCommand(DoClosing);
            Edit = new RelayCommand(DoEdit);
            Mediator.Subscribe<VariableDeletedMessage>(VariableDeleted);
            _stateManager = stateManager;
        }

        private readonly StateManager _stateManager;

        public ICommand Load { get; }

        public ICommand Closing { get; }

        public ICommand Edit { get; }

        public ObservableCollection<ITypedViewModel> Items { get; private set; }

        public string EditText => _stateManager.Current.EditStatus == EditStatus.Editing ? "End Editing" : "Edit";

        public double Top { get; set; }

        public double Left { get; set; }

        private async Task DoLoad() {
            var result = await Service.Load();
            Top = result.Top;
            Left = result.Left;
            var list = new List<ITypedViewModel>();
            list.AddRange(result.Variables.Select(x => {
                var vm = new VariableViewModel(_stateManager, x.Name);
                return vm;
            }));
            var addButton = new ButtonViewModel(_stateManager, AddButtonClicked);
            list.Add(addButton);
            Items = new ObservableCollection<ITypedViewModel>(list);
            Notify(nameof(Items), nameof(Top), nameof(Left));
        }

        private async void VariableDeleted(VariableDeletedMessage arg) {
            var deletedItem = Items.Single(x => x is VariableViewModel vm && vm.Name == arg.Name);
            await Service.DeleteVariable(arg.Name);
            Items.Remove(deletedItem);
        }

        private void AddButtonClicked() {
            var result = Dialog.ShowVariableSelector();
            if (result.Accepted) {
                var newVariable = new Variable(result["Name"]);
                newVariable.AddValue(1, "Default", Service.GetValueOf(result["Name"]));
                Items.Insert(Items.Count - 1, new VariableViewModel(_stateManager, newVariable.Name));
                Service.SaveVariable(newVariable);
            }
        }

        private async Task DoClosing() {
            await Service.SaveCoordination(Top, Left);
        }

        private void DoEdit() {
            _stateManager.SetState(Actions.ChangeEditState);
            Notify(nameof(EditText));
        }
    }

    public static class Actions {
        public static AppState ChangeEditState(AppState current) {
            return new AppState(current.EditStatus == EditStatus.Normal ? EditStatus.Editing : EditStatus.Normal);
        }
    }
}
