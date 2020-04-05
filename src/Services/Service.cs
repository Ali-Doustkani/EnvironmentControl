using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EnvironmentControl.Services {
    public class Service : IService {
        public void SetVariable(string name, string value) {
            Environment.SetEnvironmentVariable(name, value, EnvironmentVariableTarget.Machine);
        }

        public string GetVariable(string name) {
            return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Machine);
        }

        public async Task<LoadResult> Load() {
            try {
                var db = await ReadDb();
                return LoadResult.Successful(db.Variables, db.Top, db.Left);
            }
            catch (FileNotFoundException) {
                return LoadResult.Failure("Db file not found!");
            }
        }

        public async Task SaveCoordination(double top, double left) {
            var db = await ReadDb();
            db.Top = top;
            db.Left = left;
            string output = JsonConvert.SerializeObject(db, Formatting.Indented);
            await File.WriteAllTextAsync("db.json", output);
        }

        private async Task<Db> ReadDb() {
            string json = await File.ReadAllTextAsync("db.json");
            return JsonConvert.DeserializeObject<Db>(json);
        }
    }
}
