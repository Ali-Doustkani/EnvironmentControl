using EnvironmentControl.Services;

namespace EnvironmentControl.ViewModels {
    public class RadioViewModel : IValueItem {
        public RadioViewModel(IService service, Variable variable, Value value, bool selected) {
            _service = service;
            _variable = variable;
            _value = value;
            _selected = selected;
        }

        private readonly IService _service;
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
                    _service.SetVariable(_variable.Name, _value.ActualValue);
                _selected = value;
            }
        }
    }
}