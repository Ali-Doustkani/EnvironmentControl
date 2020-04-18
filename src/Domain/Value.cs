namespace EnvironmentControl.Domain {
    public class Value {
        public Value(int id, string title, string actualValue) {
            Id = id;
            Title = title;
            ActualValue = actualValue;
        }

        public int Id { get; }
        public string Title { get; }
        public string ActualValue { get; }
    }
}