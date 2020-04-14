namespace EnvironmentControl.ViewModels {
    public class AppState {
        public AppState(State state) {
            State = state;
        }

        public State State { get; }
    }
}
