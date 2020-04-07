namespace EnvironmentControl.Common {
    public interface IDialogService {
        DialogResult ShowValueEditor();
        void Accept();
        void Close();
    }
}
