﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EnvironmentControl.Domain;

namespace EnvironmentControl.Services {
    public class Service : IService {
        public async Task<Value> CreateValue(Variable variable, string title, string actualValue) {
            var db = await ReadDb();
            var newid = db.Variables.Single(x => x.Name == variable.Name).Values.Max(x => x.Id) + 1;
            return new Value(newid, title, actualValue);
        }

        public void SetVariable(string name, string value) {
            Environment.SetEnvironmentVariable(name, value, EnvironmentVariableTarget.Machine);
        }

        public string GetValueOf(string variableName) {
            return Environment.GetEnvironmentVariable(variableName, EnvironmentVariableTarget.Machine);
        }

        public async Task<Variable> GetVariable(string variableName) {
            var db = await ReadDb();
            return db.Variables.Single(x => x.Name == variableName);
        }

        public WindowsVariable[] GetVariables() {
            var ret = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.User)
                .Keys
                .Cast<string>()
                .OrderBy(x => x)
                .Select(x => new WindowsVariable(Type.User, x))
                .ToList();
            ret.AddRange(Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Machine)
                .Keys
                .Cast<string>()
                .OrderBy(x => x)
                .Select(x => new WindowsVariable(Type.System, x))
            );
            return ret.ToArray();
        }

        public async Task<LoadResult> Load() {
            try {
                var db = await ReadDb();
                return LoadResult.Successful(db.Variables, db.Top, db.Left);
            } catch (FileNotFoundException) {
                return LoadResult.Failure("Db file not found!");
            }
        }

        public async Task SaveCoordination(double top, double left) {
            var db = await ReadDb();
            db.Top = top;
            db.Left = left;
            await WriteDb(db);
        }

        public async Task SaveVariable(Variable variable) {
            var db = await ReadDb();
            var existing = db.Variables.SingleOrDefault(x => x.Name == variable.Name);
            if (existing == null) {
                db.Variables.Add(variable);
            } else {
                db.Variables.Insert(db.Variables.IndexOf(existing), variable);
                db.Variables.Remove(existing);
            }
            await WriteDb(db);
        }

        public async Task DeleteVariable(string variableName) {
            var db = await ReadDb();
            var toDelete = db.Variables.Single(x => x.Name == variableName);
            db.Variables.Remove(toDelete);
            await WriteDb(db);
        }

        private async Task<Db> ReadDb() {
            if (!File.Exists("db.json"))
                return new Db(0, 0, new List<Variable>());
            string json = await File.ReadAllTextAsync("db.json");
            return JsonConvert.DeserializeObject<Db>(json);
        }

        private async Task WriteDb(Db db) {
            string output = JsonConvert.SerializeObject(db, Formatting.Indented);
            await File.WriteAllTextAsync("db.json", output);
        }
    }
}
