﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace EnvironmentControl.Domain {
    public class Variable {
        public Variable(string name)
            : this(name, new List<Value>()) { }

        [JsonConstructor]
        public Variable(string name, List<Value> values) {
            Name = name;
            Values = values;
        }

        public string Name { get; }

        public List<Value> Values { get; }

        public void AddValue(int id, string title, string actualValue) {
            var newValue = new Value(id, title, actualValue);
            Values.Add(newValue);
        }

        public Value UpdateValue(int id, string title, string actualValue) {
            var oldValue = Values.Single(x => x.Id == id);
            var newValue = new Value(id, title, actualValue);
            Values.Insert(Values.IndexOf(oldValue), newValue);
            Values.Remove(oldValue);
            return newValue;
        }

        public override string ToString() => $"{Name}: {Values.Count} values";
    }
}
