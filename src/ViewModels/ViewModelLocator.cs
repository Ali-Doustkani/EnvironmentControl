using EnvironmentControl.Services;

namespace EnvironmentControl.ViewModels {
    public class ViewModelLocator {
        public MainViewModel MainViewModel => new MainViewModel(new Service(), new DialogService());
    }
}
