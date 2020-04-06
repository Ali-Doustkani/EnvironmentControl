using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EnvironmentControl.Common;
using EnvironmentControl.Services;

namespace EnvironmentControl.ViewModels {
    public class MainViewModel : ViewModel {
        public MainViewModel(IService service, IDialogService dialog) {
            _service = service;
            _dialog = dialog;
            Load = new RelayCommand(DoLoad);
            Closing = new RelayCommand(DoClosing);
            Close = new RelayCommand(_dialog.CloseWindow);
        }

        private readonly IService _service;
        private readonly IDialogService _dialog;

        public ICommand Load { get; }
        public ICommand Close { get; }
        public ICommand Closing { get; }

        public VariableViewModel[] Items { get; private set; }

        public double Top { get; set; }

        public double Left { get; set; }

        private async Task DoLoad() {
            var items = new List<VariableViewModel>();
            var result = await _service.Load();
            Top = result.Top;
            Left = result.Left;
            foreach (var variable in result.Variables) {
                var selectedValue = variable.Values.SingleOrDefault(x => x.ActualValue == _service.GetVariable(variable.Name));
                items.Add(new VariableViewModel(_service, variable, selectedValue));
            }
            Items = items.ToArray();
            Notify(nameof(Items), nameof(Top), nameof(Left));
        }

        private async Task DoClosing() {
            await _service.SaveCoordination(Top, Left);
        }
    }
}
