namespace EnvironmentControl.ViewModels.Common {
    public interface IDialogService {
        DialogResult ShowValueEditor(string variableName);
        DialogResult ShowValueEditor(string variableName, int valueId);
        DialogResult ShowVariableSelector();
        void Error(string message);
        void Accept();
        void Close();
    }
}
