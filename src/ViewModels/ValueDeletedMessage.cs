using EnvironmentControl.ViewModels.Common;

namespace EnvironmentControl.ViewModels {
    public class ValueDeletedMessage : IMessage {
        public ValueDeletedMessage(string variableName, int id)
        {
            VariableName = variableName;
            Id = id;
        }

        public string VariableName { get; }

        public int Id { get; }
    }
}
