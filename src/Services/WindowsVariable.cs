namespace EnvironmentControl.Services {
    public class WindowsVariable {
        public WindowsVariable(Type type, string name) {
            Type = type;
            Name = name;
        }

        public Type Type { get; }
        public string Name { get; }
    }

    public enum Type {
        User,
        System
    }
}
