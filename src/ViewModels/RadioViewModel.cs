using EnvironmentControl.Common;
using EnvironmentControl.Domain;
using EnvironmentControl.Services;

namespace EnvironmentControl.ViewModels {
    public class RadioViewModel : ViewModel, IValueItem {
        public RadioViewModel(Variable variable, Value value, bool selected) {
            _variable = variable;
            _value = value;
            _selected = selected;
        }

        private readonly Variable _variable;
        private readonly Value _value;
        private bool _selected;

        public ItemType Type => ItemType.Radio;

        public string Title => _value.Title;

        public string ActualValue => _value.ActualValue;

        public bool Selected {
            get => _selected;
            set {
                if (value)
                    Service.SetVariable(_variable.Name, _value.ActualValue);
                _selected = value;
            }
        }
    }
}