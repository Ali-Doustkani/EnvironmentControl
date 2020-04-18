using System.Threading.Tasks;
using System.Windows.Input;
using EnvironmentControl.Common;
using EnvironmentControl.States;
using EditStatus = EnvironmentControl.States.EditStatus;

namespace EnvironmentControl.ViewModels {
    public class RadioViewModel : ViewModel, ITypedViewModel {
        public RadioViewModel(StateManager stateManager, bool selected, string variableName, int id, string title, string actualValue) {
            _stateManager = stateManager;
            _selected = selected;
            VariableName = variableName;
            _id = id;
            Title = title;
            ActualValue = actualValue;
            SelectValue = new RelayCommand(DoSelectValue);
        }

        private readonly StateManager _stateManager;
        private readonly int _id;
        private bool _selected;

        public ICommand SelectValue { get; }

        public string VariableName { get; }

        public int Type => 1;

        public string Title { get; private set; }

        public string ActualValue { get; private set; }

        public bool Selected {
            get => _selected;
            set {
                if (_stateManager.Current.EditStatus == EditStatus.Editing)
                    return;
                if (value)
                    Service.SetVariable(VariableName, ActualValue);
                _selected = value;
            }
        }

        private async Task DoSelectValue() {
            if (_stateManager.Current.EditStatus != EditStatus.Editing)
                return;

            var result = Dialog.ShowValueEditor(VariableName, _id);
            if (result.Accepted) {
                if (result.Status == Common.EditStatus.Edited) {
                    if (_selected && result["ActualValue"] != ActualValue) {
                        _selected = false;
                    }
                    Title = result["Title"];
                    ActualValue = result["ActualValue"];
                    Notify(nameof(Title), nameof(ActualValue));
                } else if (result.Status == Common.EditStatus.Deleted) {
                    await Mediator.Publish(new ValueDeletedMessage(VariableName, _id));
                }
            }
        }
    }
}