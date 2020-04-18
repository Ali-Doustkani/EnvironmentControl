//using EnvironmentControl.Domain;
//using EnvironmentControl.Services;
//using Newtonsoft.Json;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Type = EnvironmentControl.Services.Type;

//namespace EnvironmentControl.Tests {
//    public class MockService : IService {
//        public MockService() {
//            var variables = new List<Variable>
//                {new Variable("myVar", new List<Value> {new Value(1, "first", "a"), new Value(2, "second", "b")})};
//            Db = new Db(200, 500, variables);
//            _environmentVariables = new Dictionary<string, string>();
//            _environmentVariables.Add("myVar", "a");
//        }

//        public readonly Db Db;
//        private readonly Dictionary<string, string> _environmentVariables;

//        public Task<Value> CreateValue(Variable variable, string title, string actualValue) {
//            var newid = Db.Variables.Single(x => x.Name == variable.Name).Values.Max(x => x.Id) + 1;
//            return Task.FromResult(new Value(newid, title, actualValue));
//        }

//        public void SetVariable(string name, string value) {
//            if (!_environmentVariables.ContainsKey(name))
//                _environmentVariables.Add(name, value);
//            else
//                _environmentVariables[name] = value;
//        }

//        public string GetValueOf(string variableName) {
//            return _environmentVariables[variableName];
//        }

//        public Task<Variable> GetVariable(string variableName) {
//            var str = JsonConvert.SerializeObject(Db);
//            var otherDb = JsonConvert.DeserializeObject<Db>(str);
//            return Task.FromResult(otherDb.Variables.Single(x => x.Name == variableName));
//        }

//        public WindowsVariable[] GetVariables() =>
//            _environmentVariables.Keys.Select(x => new WindowsVariable(Type.System, x)).ToArray();

//        public Task<LoadResult> Load() {
//            var str = JsonConvert.SerializeObject(Db);
//            var otherDb = JsonConvert.DeserializeObject<Db>(str);
//            return Task.FromResult(LoadResult.Successful(otherDb.Variables, otherDb.Top, otherDb.Left));
//        }

//        public Task SaveCoordination(double top, double left) {
//            Db.Top = top;
//            Db.Left = left;
//            return Task.CompletedTask;
//        }

//        public Task SaveVariable(Variable variable) {
//            var existing = Db.Variables.SingleOrDefault(x => x.Name == variable.Name);
//            if (existing == null) {
//                Db.Variables.Add(variable);
//            } else {

//                Db.Variables.Insert(Db.Variables.IndexOf(existing), variable);
//                Db.Variables.Remove(existing);
//            }
//            return Task.CompletedTask;
//        }

//        public Task DeleteVariable(string variableName) {
//            var toDelete = Db.Variables.Single(x => x.Name == variableName);
//            Db.Variables.Remove(toDelete);
//            return Task.CompletedTask;
//        }

//        public Task DeleteValue(string variableName, int valueId) {
//            var variable = Db.Variables.Single(x => x.Name == variableName);
//            var toDelete = variable.Values.Single(x => x.Id == valueId);
//            variable.Values.Remove(toDelete);
//            return Task.CompletedTask;
//        }
//    }
//}
