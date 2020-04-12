using EnvironmentControl.Domain;

namespace EnvironmentControl.Common {
    public interface IDialogService {
        DialogResult ShowValueEditor();
        DialogResult ShowValueEditor(Value value);
        DialogResult ShowVariableSelector();
        void Accept();
        void Close();
    }
}
