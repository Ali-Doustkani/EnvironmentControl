using System.Threading.Tasks;

namespace EnvironmentControl.Domain {
    public interface IIdGenerator {
        Task<int> Generate(string variableName);
    }
}
