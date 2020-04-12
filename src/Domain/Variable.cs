﻿using System.Collections.Generic;
using Newtonsoft.Json;

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

        public Value UpdateValue(Value value, string title, string actualValue) {
            var newValue = new Value(title, actualValue);
            Values.Insert(Values.IndexOf(value), newValue);
            Values.Remove(value);
            return newValue;
        }
    }
}
