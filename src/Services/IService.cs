using System.Collections.Generic;
using System.Threading.Tasks;
using EnvironmentControl.Domain;

namespace EnvironmentControl.Services {
    public interface IService {
        Task<Value> CreateValue(Variable variable, string title, string actualValue);
        void SetVariable(string name, string value);
        string GetValueOf(string variableName);
        Task<Variable> GetVariable(string variableName);
        WindowsVariable[] GetVariables();
        Task<LoadResult> Load();
        Task SaveCoordination(double top, double left);
        Task SaveVariable(Variable variables);
        Task DeleteVariable(string variableName);
        Task DeleteValue(string variableName, int valueId);
        Task UpdateValue(string variableName, int valueId, string newTitle, string newActualValue);
        Task<int> AddValue(string variableName, string title, string actualValue);
        Task<IEnumerable<dynamic>> GetValuesOf(string variableName);
        Task<dynamic> GetValue(string variableName, int id);
        Task AddVariable(string name);
    }
}
