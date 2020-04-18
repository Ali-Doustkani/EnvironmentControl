using EnvironmentControl.Common;
using EnvironmentControl.Domain;
using EnvironmentControl.States;
using EditStatus = EnvironmentControl.States.EditStatus;

namespace EnvironmentControl.ViewModels {
    public class RadioViewModel : ViewModel, ITypedViewModel {
        public RadioViewModel(StateManager stateManager, Variable variable, Value value, bool selected) {
            _stateManager = stateManager;
            _variable = variable;
            _value = value;
            _selected = selected;
        }

        private readonly StateManager _stateManager;
        private readonly Variable _variable;
        private Value _value;
        private bool _selected;


        public string VariableName => _variable.Name;

        public int Type => 1;

        public string Title => _value.Title;

        public string ActualValue => _value.ActualValue;

        public bool Selected {
            get => _selected;
            set {
                if (_stateManager.Current.EditStatus == EditStatus.Editing) {
                    var result = Dialog.ShowValueEditor(_variable.Name, _value);
                    if (result.Accepted) {
                        if (result.Status == Common.EditStatus.Edited) {
                            if (_selected && result["ActualValue"] != _value.ActualValue) {
                                _selected = false;
                            }
                            _value = _variable.UpdateValue(_value, result["Title"], result["ActualValue"]);
                            Notify(nameof(Title), nameof(ActualValue));
                        } else if (result.Status == Common.EditStatus.Deleted) {
                            Mediator.Publish(new ValueDeletedMessage(_variable.Name, _value.Title));
                        }
                        Service.SaveVariable(_variable);
                    }
                    return;
                }
                if (value)
                    Service.SetVariable(_variable.Name, _value.ActualValue);
                _selected = value;
            }
        }
    }
}