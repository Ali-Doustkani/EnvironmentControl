using EnvironmentControl.Common;
using EnvironmentControl.States;
using EditStatus = EnvironmentControl.States.EditStatus;

namespace EnvironmentControl.ViewModels {
    public class RadioViewModel : ViewModel, ITypedViewModel {
        public RadioViewModel(StateManager stateManager,  bool selected, string variableName, int id, string title, string actualValue) {
            _stateManager = stateManager;
            _selected = selected;
            VariableName = variableName;
            _id = id;
            Title = title;
            ActualValue = actualValue;
        }

        private readonly StateManager _stateManager;
        private readonly int _id;
        private bool _selected;

        public string VariableName { get; }

        public int Type => 1;

        public string Title { get; }

        public string ActualValue { get; }

        // todo: convert this property to ICommand
        public bool Selected {
            get => _selected;
            set {
                if (_stateManager.Current.EditStatus == EditStatus.Editing) {
                    var result = Dialog.ShowValueEditor(VariableName, _id);
                    if (result.Accepted) {
                        if (result.Status == Common.EditStatus.Edited) {
                            if (_selected && result["ActualValue"] != ActualValue) {
                                _selected = false;
                            }
                            Service.UpdateValue(_id, result["Title"], result["ActualValue"]).Wait();
                            Notify(nameof(Title), nameof(ActualValue));
                        } else if (result.Status == Common.EditStatus.Deleted) {
                            Mediator.Publish(new ValueDeletedMessage(VariableName, _id));
                        }
                    }
                    return;
                }
                if (value)
                    Service.SetVariable(VariableName, ActualValue);
                _selected = value;
            }
        }
    }
}