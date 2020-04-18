using System.Collections.Generic;

namespace EnvironmentControl.Services {
    public class LoadResult {
        public LoadResult(IEnumerable<dynamic> variables, double top, double left) {
            Variables = variables;
            Top = top;
            Left = left;
        }

        public IEnumerable<dynamic> Variables { get; }

        public double Top { get; }

        public double Left { get; }
    }
}
