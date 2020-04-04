using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EnvironmentControl.Common;
using EnvironmentControl.Services;

namespace EnvironmentControl.ViewModels {
    public class MainViewModel : ViewModel {
        public MainViewModel(IService service) {
            _service = service;
            Load = new RelayCommand(DoLoad);
        }

        private readonly IService _service;

        public ICommand Load { get; }

        public VariableViewModel[] Items { get; private set; }

        private async Task DoLoad() {
            var items = new List<VariableViewModel>();
            var variables = await _service.LoadItems();
            foreach (var variable in variables) {
                var selectedValue = variable.Values.SingleOrDefault(x => x.ActualValue == _service.GetVariable(variable.Name));
                items.Add(new VariableViewModel(_service, variable, selectedValue));
            }
            Items = items.ToArray();
            Notify(nameof(Items));
        }
    }
}
