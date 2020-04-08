using EnvironmentControl.Domain;

namespace EnvironmentControl.Common {
    public interface IDialogService {
        ValueDialogResult ShowValueEditor();
        ValueDialogResult ShowValueEditor(Value value);
        void Accept();
        void Close();
    }
}
