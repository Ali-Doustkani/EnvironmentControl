namespace EnvironmentControl.Services {
    public class Value {
        public Value(string title, string actualValue) {
            Title = title;
            ActualValue = actualValue;
        }

        public string Title { get; }
        public string ActualValue { get; }
    }
}