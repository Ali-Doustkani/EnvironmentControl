using EnvironmentControl.Common;

namespace EnvironmentControl.ViewModels {
    public class ValueDeletedMessage : IMessage {
        public ValueDeletedMessage(string title)
        {
            Title = title;
        }

        public string Title { get; }
    }
}
