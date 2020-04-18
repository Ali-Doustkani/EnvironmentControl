namespace EnvironmentControl.Domain {
    public class AddResult {
        private AddResult(int id, string message) {
            Id = id;
            Message = message;
        }

        public static AddResult Success(int id) => new AddResult(id, string.Empty);

        public static AddResult Failed(string message) => new AddResult(-1, message);

        public int Id { get; }

        public string Message { get; }

        public bool Succeeded => string.IsNullOrEmpty(Message);
    }
}
