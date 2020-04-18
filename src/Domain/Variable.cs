using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnvironmentControl.Domain {
    public class Variable {
        private Variable(string name)
            : this(name, new Value[0]) { }

        [JsonConstructor]
        private Variable(string name, Value[] values) {
            Name = name;
            _values = values.ToList();
        }

        public static Variable Create(string name, string value) {
            var result = new Variable(name);
            result._values.Add(new Value(1, "Default", value));
            return result;
        }

        private readonly List<Value> _values;

        public string Name { get; }

        public Value[] Values => _values.ToArray();

        public async Task<int> AddValue(IIdGenerator idGenerator, string title, string actualValue) {
            var newid = await idGenerator.Generate(Name);
            var newValue = new Value(newid, title, actualValue);
            _values.Add(newValue);
            return newid;
        }

        public void UpdateValue(int id, string title, string actualValue) {
            // todo: domain logics
            var oldValue = Values.Single(x => x.Id == id);
            var newValue = new Value(id, title, actualValue);
            _values.Insert(_values.IndexOf(oldValue), newValue);
            _values.Remove(oldValue);
        }

        public void RemoveValue(int id) {
            _values.Remove(Values.Single(x => x.Id == id));
        }

        public override string ToString() => $"{Name}: {_values.Count} values";
    }
}
