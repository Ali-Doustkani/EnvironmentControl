namespace EnvironmentControl.ViewModels
{
    public interface ITypedViewModel {
        int Type { get; }
        void SetState(State state);
    }
}