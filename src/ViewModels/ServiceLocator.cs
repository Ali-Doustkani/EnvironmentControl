using EnvironmentControl.Common;
using EnvironmentControl.Services;

namespace EnvironmentControl.ViewModels {
    public class ServiceLocator : IServiceLocator {
        private ServiceLocator() { }

        private static IServiceLocator _instance;

        public static IServiceLocator Instance {
            get => _instance ??= new ServiceLocator();
            set => _instance = value;
        }

        private IDialogService _dialog;
        public IDialogService Dialog {
            get => _dialog ??= new DialogService();
            set => _dialog = value;
        }

        private IService _service;
        public IService Service {
            get => _service ??= new Service();
            set => _service = value;
        }
    }
}
