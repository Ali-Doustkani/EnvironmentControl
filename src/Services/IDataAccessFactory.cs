using System.Threading.Tasks;

namespace EnvironmentControl.Services {
    public interface IDataAccessFactory {
        Task<IDataAccess> Create();
    }
}
