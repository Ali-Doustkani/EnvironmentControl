﻿using EnvironmentControl.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnvironmentControl.Services {
    public class Service : IService {

        public Service(IDataAccessFactory factory) {
            _factory = factory;
        }

        private readonly IDataAccessFactory _factory;

        public void SetVariable(string name, string value) {
            System.Environment.SetEnvironmentVariable(name, value, System.EnvironmentVariableTarget.Machine);
        }

        public string GetValueOf(string variableName) {
            return System.Environment.GetEnvironmentVariable(variableName, System.EnvironmentVariableTarget.Machine);
        }

        public async Task<Variable> GetVariable(string variableName) {
            var access = await _factory.Create();
            return access.Db.Environment.Find(variableName);
        }

        public WindowsVariable[] GetVariables() {
            var ret = System.Environment.GetEnvironmentVariables(System.EnvironmentVariableTarget.User)
                .Keys
                .Cast<string>()
                .OrderBy(x => x)
                .Select(x => new WindowsVariable(Type.User, x))
                .ToList();
            ret.AddRange(System.Environment.GetEnvironmentVariables(System.EnvironmentVariableTarget.Machine)
                .Keys
                .Cast<string>()
                .OrderBy(x => x)
                .Select(x => new WindowsVariable(Type.System, x))
            );
            return ret.ToArray();
        }

        public async Task<LoadResult> Load() {
            var access = await _factory.Create();
            return LoadResult.Successful(access.Db.Environment.Variables, access.Db.Top, access.Db.Left);
        }

        public async Task SaveCoordination(double top, double left) {
            var access = await _factory.Create();
            access.Db.Top = top;
            access.Db.Left = left;
            await access.SaveChanges();
        }

        public async Task DeleteVariable(string variableName) {
            var access = await _factory.Create();
            access.Db.Environment.Remove(variableName);
            await access.SaveChanges();
        }

        public async Task DeleteValue(string variableName, int valueId) {
            var access = await _factory.Create();
            access.Db.Environment.Find(variableName).RemoveValue(valueId);
            await access.SaveChanges();
        }

        public async Task UpdateValue(string variableName, int valueId, string newTitle, string newActualValue) {
            var access = await _factory.Create();
            access.Db.Environment.Find(variableName).UpdateValue(valueId, newTitle, newActualValue);
            await access.SaveChanges();
        }

        public async Task<int> AddValue(string variableName, string title, string actualValue) {
            var access = await _factory.Create();
            var id = await access.Db.Environment.Find(variableName).AddValue(new IdGenerator(_factory), title, actualValue);
            await access.SaveChanges();
            return id;
        }

        public async Task<IEnumerable<dynamic>> GetValuesOf(string variableName) {
            var access = await _factory.Create();
            return access.Db
                .Environment
                .Find(variableName)
                .Values
                .Select(x => new { x.Id, x.Title, x.ActualValue });
        }

        public async Task<dynamic> GetValue(string variableName, int id) =>
            (await _factory.Create()).Db.Environment.FindValue(variableName, id);

        public async Task AddVariable(string name) {
            var access = await _factory.Create();
            access.Db.Environment.Add(name, GetValueOf(name));
            await access.SaveChanges();
        }
    }
}
