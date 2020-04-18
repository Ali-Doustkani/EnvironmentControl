using System.Linq;
using System.Threading.Tasks;
using EnvironmentControl.Domain;

namespace EnvironmentControl.Services {
    public class IdGenerator : IIdGenerator {
        public IdGenerator(IDataAccessFactory factory) {
            _factory = factory;
        }

        private readonly IDataAccessFactory _factory;

        public async Task<int> Generate(string variableName) {
            return (await _factory.Create()).Db.Environment.Find(variableName).Values.Max(x => x.Id) + 1;
        }
    }
}
