using EnvironmentControl.Domain;

namespace EnvironmentControl.Common {
    public interface IDialogService {
        DialogResult ShowValueEditor(string variableName);
        DialogResult ShowValueEditor(string variableName, Value value);
        DialogResult ShowVariableSelector();
        void Error(string message);
        void Accept();
        void Close();
    }
}
