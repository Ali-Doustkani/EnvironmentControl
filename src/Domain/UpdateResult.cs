namespace EnvironmentControl.Domain {
    public class UpdateResult {
        private UpdateResult(string message) {
            Message = message;
        }

        public static UpdateResult Success() => new UpdateResult(string.Empty);

        public static UpdateResult Failed(string message) => new UpdateResult(message);

        public string Message { get; }

        public bool Succeeded => string.IsNullOrEmpty(Message);
    }
}
