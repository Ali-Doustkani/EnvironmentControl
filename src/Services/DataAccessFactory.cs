using System.IO;
using System.Threading.Tasks;
using EnvironmentControl.Domain;
using Newtonsoft.Json;

namespace EnvironmentControl.Services {
    public class DataAccessFactory : IDataAccessFactory {
        public async Task<IDataAccess> Create() {
            if (!File.Exists("db.json"))
                return new DataAccess(new Db(0, 0, new Environment()));
            string json = await File.ReadAllTextAsync("db.json");
            return new DataAccess(JsonConvert.DeserializeObject<Db>(json));
        }
    }
}
