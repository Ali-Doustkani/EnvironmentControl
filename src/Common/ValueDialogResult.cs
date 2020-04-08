namespace EnvironmentControl.Common {
    public class ValueDialogResult {
        private ValueDialogResult(bool accepted, string title, string actualValue, ValueEditStatus status) {
            Accepted = accepted;
            Title = title;
            ActualValue = actualValue;
            Status = status;
        }

        public bool Accepted { get; }

        public ValueEditStatus Status { get; }

        public string Title { get; }

        public string ActualValue { get; }

        public static ValueDialogResult Added(string title, string actualValue) => new ValueDialogResult(true, title, actualValue, ValueEditStatus.Added);

        public static ValueDialogResult Edited(string title, string actualValue) => new ValueDialogResult(true, title, actualValue, ValueEditStatus.Edited);

        public static ValueDialogResult Deleted() => new ValueDialogResult(true, string.Empty, string.Empty, ValueEditStatus.Deleted);

        public static ValueDialogResult Failed() => new ValueDialogResult(false, string.Empty, string.Empty, ValueEditStatus.None);
    }

    public enum ValueEditStatus {
        None,
        Added,
        Edited,
        Deleted
    }
}
