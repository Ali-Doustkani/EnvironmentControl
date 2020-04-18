using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using EnvironmentControl.ViewModels.Common;

namespace EnvironmentControl.ViewModels {
    public class ValueEditorViewModel : ViewModel {
        public ValueEditorViewModel(bool editing, string variableName, int valueId) {
            _editing = editing;
            _variableName = variableName;
            _valueId = valueId;
            Ok = new RelayCommand(DoOk);
            Cancel = new RelayCommand(Dialog.Close);
            Delete = new RelayCommand(DoDelete);
        }

        public ValueEditorViewModel(bool editing, string variableName)
        : this(editing, variableName, -1) { }

        private readonly bool _editing;
        private readonly string _variableName;
        private readonly int _valueId;

        public ICommand Ok { get; }

        public ICommand Cancel { get; }

        public ICommand Delete { get; }

        public bool Deleted { get; private set; }

        public Visibility DeleteVisibility => _editing ? Visibility.Visible : Visibility.Collapsed;

        public string Title { get; set; }

        public string ActualValue { get; set; }

        public async Task Load() {
            if (!_editing)
                return;

            var value = await Service.GetValue(_variableName, _valueId);
            Title = value.Title;
            ActualValue = value.ActualValue;
            Notify(nameof(Title), nameof(ActualValue));
        }

        public Dictionary<string, string> ToDictionary() =>
            new Dictionary<string, string>
            {
                {nameof(Title), Title},
                {nameof(ActualValue), ActualValue}
            };

        private void DoDelete() {
            Deleted = true;
            Dialog.Accept();
        }

        private async Task DoOk() {
            if (_editing) {
                var result = await Service.UpdateValue(_variableName, _valueId, Title, ActualValue);
                if (!result.Succeeded) {
                    Dialog.Error(result.Message);
                    return;
                }

            } else {
                var result = await Service.AddValue(_variableName, Title, ActualValue);
                if (!result.Succeeded) {
                    Dialog.Error(result.Message);
                    return;
                }
            }

            Dialog.Accept();
        }
    }
}
