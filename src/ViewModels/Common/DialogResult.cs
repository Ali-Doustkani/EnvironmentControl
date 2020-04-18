using System.Collections.Generic;

namespace EnvironmentControl.ViewModels.Common {
    public class DialogResult {
        private DialogResult(bool accepted, EditStatus status, Dictionary<string, string> values) {
            Accepted = accepted;
            Status = status;
            Values = values;
        }

        public bool Accepted { get; }

        public EditStatus Status { get; }

        public Dictionary<string, string> Values { get; }

        public string this[string key] => Values[key];

        public static DialogResult Added(Dictionary<string, string> values) => new DialogResult(true, EditStatus.Added, values);

        public static DialogResult Edited(Dictionary<string, string> values) => new DialogResult(true, EditStatus.Edited, values);

        public static DialogResult Deleted() => new DialogResult(true, EditStatus.Deleted, new Dictionary<string, string>());

        public static DialogResult Failed() => new DialogResult(false, EditStatus.None, new Dictionary<string, string>());

    }

    public enum EditStatus {
        None,
        Added,
        Edited,
        Deleted
    }
}
