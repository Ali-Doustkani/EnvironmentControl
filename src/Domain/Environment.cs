using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace EnvironmentControl.Domain {
    public class Environment {
        public Environment() {
            _variables = new List<Variable>();
        }

        [JsonConstructor]
        public Environment(Variable[] variables) {
            _variables = variables.ToList();
        }

        private readonly List<Variable> _variables;

        public Variable[] Variables => _variables.ToArray();

        public Variable Find(string name) => _variables.Single(x => x.Name == name);

        public Value FindValue(string variableName, int valueId) =>
            Find(variableName).Values.Single(x => x.Id == valueId);

        public void Add(string name, string value) {
            _variables.Add(Variable.Create(name, value));
        }

        public void Remove(string variableName) {
            var toDelete = Find(variableName);
            _variables.Remove(toDelete);
        }
    }
}
