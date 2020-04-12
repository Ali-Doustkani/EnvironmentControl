using EnvironmentControl.Common;
using EnvironmentControl.Domain;

namespace EnvironmentControl.ViewModels {
    public delegate void ValueDeleted(Value deletedValue);

    public class RadioViewModel : ViewModel, ITypedViewModel {
        public RadioViewModel(Variable variable, Value value, bool selected) {
            _variable = variable;
            _value = value;
            _selected = selected;
            _state = State.Normal;
        }

        private readonly Variable _variable;
        private Value _value;
        private bool _selected;
        private State _state;

        public int Type => 1;

        public string Title => _value.Title;

        public string ActualValue => _value.ActualValue;

        public event ValueDeleted ValueDeleted;

        public bool Selected {
            get => _selected;
            set {
                if (_state == State.Editing) {
                    var result = Dialog.ShowValueEditor(_value);
                    if (result.Accepted) {
                        if (result.Status == EditStatus.Edited) {
                            _value = _variable.UpdateValue(_value, result["Title"], result["ActualValue"]);
                            Notify(nameof(Title), nameof(ActualValue));
                        }
                        else if (result.Status == EditStatus.Deleted) {
                            ValueDeleted?.Invoke(_value);
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

        public void SetState(State state) {
            _state = state;
        }
    }
}