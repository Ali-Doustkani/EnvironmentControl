using System;
using System.Collections.Generic;

namespace EnvironmentControl.Common {
    public class Mediator {
        public Mediator() {
            _handlers = new List<Tuple<Type, object>>();
        }

        private readonly List<Tuple<Type, object>> _handlers;

        public void Publish<T>(T arg)
            where T : IMessage {
            _handlers.ForEach(item => {
                if (item.Item1 == arg.GetType())
                    ((Action<T>)item.Item2)(arg);
            });
        }

        public void Subscribe<T>(Action<T> todo)
            where T : IMessage {
            _handlers.Add(new Tuple<Type, object>(typeof(T), todo));
        }
    }
}
