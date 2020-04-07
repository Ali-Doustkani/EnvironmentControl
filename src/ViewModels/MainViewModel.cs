using EnvironmentControl.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EnvironmentControl.ViewModels {
    public class MainViewModel : ViewModel {
        public MainViewModel() {
            Load = new RelayCommand(DoLoad);
            Closing = new RelayCommand(DoClosing);
            Close = new RelayCommand(Dialog.Close);
        }

        public ICommand Load { get; }
        public ICommand Close { get; }
        public ICommand Closing { get; }

        public VariableViewModel[] Items { get; private set; }

        public double Top { get; set; }

        public double Left { get; set; }

        private async Task DoLoad() {
            var result = await Service.Load();
            Top = result.Top;
            Left = result.Left;
            Items = result.Variables.Select(x => new VariableViewModel(x)).ToArray();
            Notify(nameof(Items), nameof(Top), nameof(Left));
        }

        private async Task DoClosing() {
            await Service.SaveCoordination(Top, Left);
        }
    }
}
