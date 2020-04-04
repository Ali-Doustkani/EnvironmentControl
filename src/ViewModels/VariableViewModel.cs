using EnvironmentControl.Services;

namespace EnvironmentControl.ViewModels
{
    public class VariableViewModel {
        public VariableViewModel(IService service, Variable variable, Value selectedValue) {
            _service = service;
            _variable = variable;
            _selectedValue = selectedValue;
        }

        private readonly IService _service;
        private readonly Variable _variable;

        public string Name => _variable.Name;

        private Value _selectedValue;
        public Value SelectedValue {
            get => _selectedValue;
            set {
                _selectedValue = value;
                _service.SetVariable(_variable.Name, value.ActualValue);
            }
        }

        public Value[] Values => _variable.Values;
    }
}