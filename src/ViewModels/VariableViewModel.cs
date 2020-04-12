using EnvironmentControl.Common;
using EnvironmentControl.Domain;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EnvironmentControl.ViewModels {
    public class VariableViewModel : ViewModel, ITypedViewModel {
        public VariableViewModel(Variable variable) {
            _variable = variable;
        }

        private readonly Variable _variable;

        public string Name => _variable.Name;

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
            foreach (var value in Values) {
                value.SetState(state);
            }
        }

        private void CommandMade() {
            var result = Dialog.ShowValueEditor();
            if (result.Accepted) {
                var newValue = new Value(result["Title"], result["ActualValue"]);
                var item = new RadioViewModel(_variable, newValue, false);
                _variable.Values.Add(newValue);
                _values.Insert(_values.Count - 1, item);
                Service.SaveVariable(_variable);
            }
        }

        private void FillValues() {
            if (_values != null) {
                foreach (var i in _values.Take(_values.Count - 1).Cast<RadioViewModel>()) {
                    i.ValueDeleted -= ValueDeleted;
                }
                ((ButtonViewModel)_values.Last()).CommandMade -= CommandMade;
            }
            var list = new List<ITypedViewModel>();
            var selectedValue = Service.GetVariable(_variable.Name);
            RadioViewModel CreateRadio(Value x) {
                var ret = new RadioViewModel(_variable, x, x.ActualValue == selectedValue);
                ret.ValueDeleted += ValueDeleted;
                return ret;
            }
            list.AddRange(_variable.Values.Select(CreateRadio));
            var button = new ButtonViewModel();
            button.CommandMade += CommandMade;
            list.Add(button);
            _values = new ObservableCollection<ITypedViewModel>(list);
            Notify(nameof(Values));
        }

        private void ValueDeleted(Value deletedValue) {
            _variable.Values.Remove(deletedValue);
            FillValues();
            Service.SaveVariable(_variable);
            Notify(nameof(Values));
        }
    }

}