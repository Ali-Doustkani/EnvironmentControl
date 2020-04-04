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

        public VariableValue[] Items { get; private set; }

        private VariableValue _selectedVariableValue;
        public VariableValue SelectedVariableValue {
            get => _selectedVariableValue;
            set {
                _selectedVariableValue = value;
                _service.SetVariable(value.Value);
            }
        }

        private async Task DoLoad() {
            Items = await _service.LoadItems();
            _selectedVariableValue = Items.SingleOrDefault(x => x.Value == _service.GetVariable());
            Notify(nameof(Items), nameof(SelectedVariableValue));
        }
    }
}
