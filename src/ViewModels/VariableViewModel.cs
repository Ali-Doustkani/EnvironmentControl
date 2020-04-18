using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using EnvironmentControl.ViewModels.Common;
using EnvironmentControl.ViewModels.States;
using EditStatus = EnvironmentControl.ViewModels.States.EditStatus;

namespace EnvironmentControl.ViewModels {
    public class VariableViewModel : ViewModel, ITypedViewModel {
        public VariableViewModel(StateManager stateManager, string variableName) {
            Name = variableName;
            DeleteVariable = new RelayCommand(() => Mediator.Publish(new VariableDeletedMessage(Name)));
            Mediator.Subscribe<ValueDeletedMessage>(ValueDeleted);
            _stateManager = stateManager;
            stateManager.StateChanged += StateChanged;
        }

        private readonly StateManager _stateManager;

        public string Name { get; }

        public Visibility Visibility => _stateManager.Current.EditStatus == EditStatus.Editing ? Visibility.Visible : Visibility.Collapsed;

        public ICommand DeleteVariable { get; }

        public ObservableCollection<ITypedViewModel> Values { get; private set; }

        public int Type => 1;

        public async Task Load() {
            await FillValues();
        }

        private async Task FillValues() {
            var list = new List<ITypedViewModel>();
            RadioViewModel CreateRadio(dynamic x) {
                var ret = new RadioViewModel(_stateManager, Service.IsSet(Name, x.ActualValue), Name, x.Id, x.Title, x.ActualValue);
                return ret;
            }
            list.AddRange((await Service.GetValuesOf(Name)).Select(CreateRadio));
            list.Add(new ButtonViewModel(_stateManager, AddButtonClicked));
            Values = new ObservableCollection<ITypedViewModel>(list);
            Notify(nameof(Values));
        }

        private async Task AddButtonClicked() {
            var result = Dialog.ShowValueEditor(Name);
            if (result.Accepted) {
                await FillValues();
            }
        }

        private async Task ValueDeleted(ValueDeletedMessage msg) {
            if (msg.VariableName != Name)
                return;

            await Service.DeleteValue(Name, msg.Id);
            await FillValues();
            Notify(nameof(Values));
        }

        private void StateChanged(AppState obj) {
            Notify(nameof(Visibility));
        }
    }
}