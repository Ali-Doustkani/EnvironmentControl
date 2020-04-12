using EnvironmentControl.Common;

namespace EnvironmentControl.ViewModels {
    public class VariableDeletedMessage : IMessage {
        public VariableDeletedMessage(string name) {
            Name = name;
        }

        public string Name { get; }
    }
}
