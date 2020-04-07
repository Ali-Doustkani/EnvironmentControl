using EnvironmentControl.Common;
using EnvironmentControl.Services;

namespace EnvironmentControl.ViewModels {
    public interface IServiceLocator {
        IService Service { get; }
        IDialogService Dialog { get; }
    }
}
