using System.Threading.Tasks;

namespace EnvironmentControl.Services {
    public interface IService {
        void SetVariable(string value);
        string GetVariable();
        Task<VariableValue[]> LoadItems();
    }
}
