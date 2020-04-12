using EnvironmentControl.Domain;

namespace EnvironmentControl.Common {
    public interface IDialogService {
        DialogResult ShowValueEditor();
        DialogResult ShowValueEditor(Value value);
        DialogResult ShowVariableEditor();
        DialogResult ShowVariableEditor(Variable variable);
        void Accept();
        void Close();
    }
}
