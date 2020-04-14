namespace EnvironmentControl.States {
    public class AppState {
        public AppState(EditStatus editStatus) {
            EditStatus = editStatus;
        }

        public EditStatus EditStatus { get; }
    }
}
