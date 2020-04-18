using EnvironmentControl.Common;

namespace EnvironmentControl.ViewModels {
    public class ValueDeletedMessage : IMessage {
        public ValueDeletedMessage(string variableName, string title)
        {
            VariableName = variableName;
            Title = title;
        }

        public string VariableName { get; }

        public string Title { get; }
    }
}
