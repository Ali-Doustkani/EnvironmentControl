namespace EnvironmentControl.Services {
    public class LoadResult {
        private LoadResult(string error, Variable[] variables, double top, double left) {
            Error = error;
            Variables = variables;
            Top = top;
            Left = left;
        }

        public static LoadResult Successful(Variable[] variables, double top, double left) => new LoadResult(null, variables, top, left);

        public static LoadResult Failure(string error) => new LoadResult(error, null, 0, 0);

        public string Error { get; }

        public bool Failed => !string.IsNullOrEmpty(Error);

        public Variable[] Variables { get; }

        public double Top { get; }

        public double Left { get; }
    }
}
