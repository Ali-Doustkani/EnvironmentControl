﻿using System.Threading.Tasks;
using EnvironmentControl.Domain;

namespace EnvironmentControl.Services {
    public interface IService {
        void SetVariable(string name, string value);
        string GetVariable(string name);
        WindowsVariable[] GetVariables();
        Task<LoadResult> Load();
        Task SaveCoordination(double top, double left);
        Task SaveVariable(Variable variables);
        Task DeleteVariable(string variableName);
    }
}
