using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EnvironmentControl.Services {
    public class DataAccess : IDataAccess {
        public DataAccess(Db db) {
            Db = db;
        }

        public Db Db { get; }

        public async Task SaveChanges() {
            string output = JsonConvert.SerializeObject(Db, Formatting.Indented);
            await File.WriteAllTextAsync("db.json", output);
        }
    }
}
