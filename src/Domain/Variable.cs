using System.Collections.Generic;

namespace EnvironmentControl.Domain {
    public class Variable {
        public Variable(string name, List<Value> values) {
            Name = name;
            Values = values;
        }

        public string Name { get; }
        public List<Value> Values { get; }
    }
}
