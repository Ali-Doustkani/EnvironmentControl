using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EnvironmentControl {
    public class Service : IService {
        public void SetVariable(string value) {
            Environment.SetEnvironmentVariable("MY_VAR", value, EnvironmentVariableTarget.Machine);
        }

        public string GetVariable() {
            return Environment.GetEnvironmentVariable("MY_VAR", EnvironmentVariableTarget.Machine);
        }

        public async Task<VariableValue[]> LoadItems() {
            try {
                var json = await File.ReadAllTextAsync("db.json");
                return JsonConvert.DeserializeObject<Db>(json).Values;
            }
            catch (FileNotFoundException) {
                return new VariableValue[0];
            }
        }
    }
}
