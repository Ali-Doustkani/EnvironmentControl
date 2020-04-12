using EnvironmentControl.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EnvironmentControl.ViewModels {
    public class MainViewModel : ViewModel {
        public MainViewModel() {
            Load = new RelayCommand(DoLoad);
            Closing = new RelayCommand(DoClosing);
            Edit = new RelayCommand(DoEdit);
            _state = State.Normal;
        }

        private State _state;

        public ICommand Load { get; }

        public ICommand Closing { get; }

        public ICommand Edit { get; }

        public ITypedViewModel[] Items { get; private set; }

        public string EditText => _state == State.Editing ? "End Editing" : "Edit";

        public double Top { get; set; }

        public double Left { get; set; }

        private async Task DoLoad() {
            var result = await Service.Load();
            Top = result.Top;
            Left = result.Left;
            var items = result.Variables.Select(x => new VariableViewModel(x)).Cast<ITypedViewModel>().ToList();
            items.Add(new ButtonViewModel());
            Items = items.ToArray();
            Notify(nameof(Items), nameof(Top), nameof(Left));
        }

        private async Task DoClosing() {
            await Service.SaveCoordination(Top, Left);
        }

        private void DoEdit() {
            _state = _state == State.Normal ? State.Editing : State.Normal;
            foreach (var item in Items) {
                item.SetState(_state);
            }
            Notify(nameof(EditText));
        }
    }
}
