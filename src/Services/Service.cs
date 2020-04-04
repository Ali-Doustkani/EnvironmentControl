using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EnvironmentControl.Services {
    public class Service : IService {
        public void SetVariable(string name, string value) {
            Environment.SetEnvironmentVariable(name, value, EnvironmentVariableTarget.Machine);
        }

        public string GetVariable(string name) {
            return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Machine);
        }

        public async Task<Variable[]> LoadItems() {
            try {
                var json = await File.ReadAllTextAsync("db.json");
                return JsonConvert.DeserializeObject<Db>(json).Variables;
            }
            catch (FileNotFoundException) {
                return new Variable[0];
            }
        }
    }
}
