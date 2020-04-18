using System.Collections.Generic;
using EnvironmentControl.Domain;

namespace EnvironmentControl.Services {
    public class LoadResult {
        private LoadResult(string error, IEnumerable<Variable> variables, double top, double left) {
            Error = error;
            Variables = variables;
            Top = top;
            Left = left;
        }

        public static LoadResult Successful(IEnumerable<Variable> variables, double top, double left) => new LoadResult(null, variables, top, left);

        public string Error { get; }

        public bool Failed => !string.IsNullOrEmpty(Error);

        public IEnumerable<Variable> Variables { get; } //todo: decouple from domain

        public double Top { get; }

        public double Left { get; }
    }
}
