namespace EnvironmentControl.Services {
    public class Variable {
        public Variable(string name, Value[] values) {
            Name = name;
            Values = values;
        }

        public string Name { get; }
        public Value[] Values { get; }
    }
}
