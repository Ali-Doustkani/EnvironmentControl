using EnvironmentControl.Common;
using EnvironmentControl.Domain;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EnvironmentControl.ViewModels {
    public class VariableViewModel : ViewModel {
        public VariableViewModel(Variable variable) {
            _variable = variable;
        }

        private readonly Variable _variable;

        public string Name => _variable.Name;

        private ObservableCollection<IValueItem> _values;
        public ObservableCollection<IValueItem> Values {
            get {
                if (_values == null) {
                    var list = new List<IValueItem>();
                    var selectedValue = Service.GetVariable(_variable.Name);
                    list.AddRange(_variable.Values.Select(x => new RadioViewModel(_variable, x, x.ActualValue == selectedValue)));
                    var button = new ButtonViewModel();
                    button.ValueApproved += ValueApproved;
                    list.Add(button);
                    _values = new ObservableCollection<IValueItem>(list);
                }
                return _values;
            }
        }

        public void SetState(State state) {
            foreach (var value in Values) {
                value.SetState(state);
            }
        }

        private void ValueApproved(Value newValue) {
            var item = new RadioViewModel(_variable, newValue, false);
            _variable.Values.Add(newValue);
            _values.Insert(_values.Count - 1, item);
            Service.SaveVariable(_variable);
        }
    }

}