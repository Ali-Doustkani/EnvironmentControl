using EnvironmentControl.Services;
using EnvironmentControl.ViewModels.Common;
using EnvironmentControl.ViewModels.States;

namespace EnvironmentControl.ViewModels {
    public interface IServiceLocator {
        IService Service { get; }
        IDialogService Dialog { get; }
        Mediator Mediator { get; }
        StateManager StateManager { get; }
    }
}
