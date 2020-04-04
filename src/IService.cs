using System.Threading.Tasks;

namespace EnvironmentControl {
    public interface IService {
        void SetVariable(string value);
        string GetVariable();
        Task<VariableValue[]> LoadItems();
    }
}
