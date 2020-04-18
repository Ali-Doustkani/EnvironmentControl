using System.Threading.Tasks;

namespace EnvironmentControl.Services {
    public interface IDataAccess {
        public Db Db { get; }
        public Task SaveChanges();
    }
}
