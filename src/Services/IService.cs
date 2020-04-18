using System.Collections.Generic;
using System.Threading.Tasks;
using EnvironmentControl.Domain;

namespace EnvironmentControl.Services {
    public interface IService {
        void SetVariable(string name, string value);
        bool IsSet(string variableName, string expectedValue);
        WindowsVariable[] GetVariables();
        Task<LoadResult> Load();
        Task SaveCoordination(double top, double left);
        Task DeleteVariable(string variableName);
        Task DeleteValue(string variableName, int valueId);
        Task<UpdateResult> UpdateValue(string variableName, int valueId, string newTitle, string newActualValue);
        Task<AddResult> AddValue(string variableName, string title, string actualValue);
        Task<IEnumerable<dynamic>> GetValuesOf(string variableName);
        Task<dynamic> GetValue(string variableName, int id);
        Task AddVariable(string name);
    }
}
