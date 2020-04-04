using System.Threading.Tasks;

namespace EnvironmentControl.Services {
    public interface IService {
        void SetVariable(string name, string value);
        string GetVariable(string name);
        Task<Variable[]> LoadItems();
    }
}
