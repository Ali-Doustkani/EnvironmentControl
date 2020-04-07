using System.Collections.Generic;
using EnvironmentControl.Domain;

namespace EnvironmentControl.Services {
    public class Db {
        public Db(double top, double left, List<Variable> variables) {
            Top = top;
            Left = left;
            Variables = variables;
        }

        public double Top { get; set; }
        public double Left { get; set; }
        public List<Variable> Variables { get; }
    }
}
