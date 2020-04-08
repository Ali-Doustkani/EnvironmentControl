namespace EnvironmentControl.ViewModels
{
    public interface IValueItem {
        ItemType Type { get; }

        void SetState(State state);
    }
}