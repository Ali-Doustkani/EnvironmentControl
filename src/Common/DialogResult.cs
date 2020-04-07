using EnvironmentControl.Domain;

namespace EnvironmentControl.Common {
    public class DialogResult {
        private DialogResult(bool accepted, Value value) {
            Accepted = accepted;
            Value = value;
        }

        public bool Accepted { get; }

        public Value Value { get; }

        public static DialogResult Succeeded(Value value) => new DialogResult(true, value);

        public static DialogResult Failed() => new DialogResult(false, null);
    }
}
