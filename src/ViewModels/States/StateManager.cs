using System;

namespace EnvironmentControl.ViewModels.States {
    public class StateManager {
        private AppState _current;

        public StateManager() {
            Current = new AppState(EditStatus.Normal);
        }

        public event Action<AppState> StateChanged;

        public AppState Current {
            get => _current;
            private set {
                _current = value;
                StateChanged?.Invoke(_current);
            }
        }

        public void SetState(Func<AppState, AppState> action) {
            Current = action(Current);
        }
    }
}
