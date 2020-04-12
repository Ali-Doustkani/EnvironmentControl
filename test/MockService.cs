using EnvironmentControl.Domain;
using EnvironmentControl.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnvironmentControl.Tests {
    public class MockService : IService {
        public MockService() {
            var variables = new List<Variable>
                {new Variable("myVar", new List<Value> {new Value("first", "a"), new Value("second", "b")})};
            Db = new Db(200, 500, variables);
            _environmentVariables = new Dictionary<string, string>();
            _environmentVariables.Add("myVar", "a");
        }

        public readonly Db Db;
        private readonly Dictionary<string, string> _environmentVariables;

        public void SetVariable(string name, string value) {
            if (!_environmentVariables.ContainsKey(name))
                _environmentVariables.Add(name, value);
            else
                _environmentVariables[name] = value;
        }

        public string GetVariable(string name) {
            return _environmentVariables[name];
        }

        public string[] GetVariables() => _environmentVariables.Keys.ToArray();

        public Task<LoadResult> Load() {
            var str = JsonConvert.SerializeObject(Db);
            var otherDb = JsonConvert.DeserializeObject<Db>(str);
            return Task.FromResult(LoadResult.Successful(otherDb.Variables, otherDb.Top, otherDb.Left));
        }

        public Task SaveCoordination(double top, double left) {
            Db.Top = top;
            Db.Left = left;
            return Task.CompletedTask;
        }

        public Task SaveVariable(Variable variable) {
            var existing = Db.Variables.SingleOrDefault(x => x.Name == variable.Name);
            if (existing == null) {
                Db.Variables.Add(variable);
            }
            else {

                Db.Variables.Insert(Db.Variables.IndexOf(existing), variable);
                Db.Variables.Remove(existing);
            }
            return Task.CompletedTask;
        }
    }
}
