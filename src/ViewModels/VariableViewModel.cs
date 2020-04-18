using EnvironmentControl.Common;
using EnvironmentControl.Domain;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using EnvironmentControl.States;
using EditStatus = EnvironmentControl.States.EditStatus;

namespace EnvironmentControl.ViewModels {
    public class VariableViewModel : ViewModel, ITypedViewModel {
        public VariableViewModel(StateManager stateManager, Variable variable) {
            _variable = variable;
            DeleteVariable = new RelayCommand(() => Mediator.Publish(new VariableDeletedMessage(Name)));
            Mediator.Subscribe<ValueDeletedMessage>(ValueDeleted);
            _stateManager = stateManager;
            stateManager.StateChanged += StateChanged;
        }

        private readonly StateManager _stateManager;
        private readonly Variable _variable;

        public string Name => _variable.Name;

        public Visibility Visibility => _stateManager.Current.EditStatus == EditStatus.Editing ? Visibility.Visible : Visibility.Collapsed;

        public ICommand DeleteVariable { get; }

        private ObservableCollection<ITypedViewModel> _values;
        public ObservableCollection<ITypedViewModel> Values {
            get {
                if (_values == null) {
                    FillValues();
                }
                return _values;
            }
        }

        public int Type => 1;

        private void FillValues() {
            var list = new List<ITypedViewModel>();
            var selectedValue = Service.GetValueOf(_variable.Name);
            RadioViewModel CreateRadio(Value x) {
                var ret = new RadioViewModel(_stateManager, _variable, x, x.ActualValue == selectedValue);
                return ret;
            }
            list.AddRange(_variable.Values.Select(CreateRadio));
            list.Add(new ButtonViewModel(_stateManager, async () => await AddButtonClicked()));
            _values = new ObservableCollection<ITypedViewModel>(list);
            Notify(nameof(Values));
        }

        private async Task AddButtonClicked() {
            var result = Dialog.ShowValueEditor(_variable.Name);
            if (result.Accepted) {
                var newValue = await Service.CreateValue(_variable, result["Title"], result["ActualValue"]);
                var item = new RadioViewModel(_stateManager, _variable, newValue, false);
                _variable.Values.Add(newValue);
                _values.Insert(_values.Count - 1, item);
                await Service.SaveVariable(_variable);
            }
        }

        private void ValueDeleted(ValueDeletedMessage msg) {
            if (msg.VariableName != _variable.Name)
                return;

            var toDelete = _variable.Values.Single(x => x.Title == msg.Title);
            _variable.Values.Remove(toDelete);
            FillValues();
            Service.SaveVariable(_variable);
            Notify(nameof(Values));
        }

        private void StateChanged(AppState obj) {
            Notify(nameof(Visibility));
        }
    }
}