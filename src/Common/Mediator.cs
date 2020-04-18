using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnvironmentControl.Common {
    public class Mediator {
        public Mediator() {
            _handlers = new List<Tuple<Type, object>>();
        }

        private readonly List<Tuple<Type, object>> _handlers;

        public Task Publish<T>(T arg)
            where T : IMessage {
            _handlers.ForEach(item => {
                if (item.Item1 == arg.GetType()) {
                    if (item.Item2 is Action<T> action)
                        action(arg);
                    else if (item.Item2 is Func<T, Task> task)
                        task(arg);
                }
            });
            return Task.CompletedTask;
        }

        public void Subscribe<T>(Action<T> todo)
            where T : IMessage {
            _handlers.Add(new Tuple<Type, object>(typeof(T), todo));
        }

        public void Subscribe<T>(Func<T, Task> todo)
            where T : IMessage {
            _handlers.Add(new Tuple<Type, object>(typeof(T), todo));
        }
    }
}
