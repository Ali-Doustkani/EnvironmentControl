using EnvironmentControl.Common;
using EnvironmentControl.Domain;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EnvironmentControl.ViewModels {
    public class VariableViewModel : ViewModel, ITypedViewModel {
        public VariableViewModel(Variable variable) {
            _variable = variable;
            DeleteVariable = new RelayCommand(() => Mediator.Publish(new VariableDeletedMessage(Name)));
            Mediator.Subscribe<ValueDeletedMessage>(ValueDeleted);
        }

        private readonly Variable _variable;
        private State _state;

        public string Name => _variable.Name;

        public Visibility Visibility => _state == State.Editing ? Visibility.Visible : Visibility.Collapsed;

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

        public void SetState(State state) {
            _state = state;
            foreach (var value in Values) {
                value.SetState(state);
            }
            Notify(nameof(Visibility));
        }

        private void FillValues() {
            var list = new List<ITypedViewModel>();
            var selectedValue = Service.GetVariable(_variable.Name);
            RadioViewModel CreateRadio(Value x) {
                var ret = new RadioViewModel(_variable, x, x.ActualValue == selectedValue);
                return ret;
            }
            list.AddRange(_variable.Values.Select(CreateRadio));
            list.Add( new ButtonViewModel(AddButtonClicked));
            _values = new ObservableCollection<ITypedViewModel>(list);
            Notify(nameof(Values));
        }

        private void AddButtonClicked() {
            var result = Dialog.ShowValueEditor();
            if (result.Accepted) {
                var newValue = new Value(result["Title"], result["ActualValue"]);
                var item = new RadioViewModel(_variable, newValue, false);
                _variable.Values.Add(newValue);
                _values.Insert(_values.Count - 1, item);
                Service.SaveVariable(_variable);
            }
        }

        private void ValueDeleted(ValueDeletedMessage msg) {
            var toDelete = _variable.Values.Single(x => x.Title == msg.Title);
            _variable.Values.Remove(toDelete);
            FillValues();
            Service.SaveVariable(_variable);
            Notify(nameof(Values));
        }
    }

}