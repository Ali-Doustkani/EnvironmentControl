namespace EnvironmentControl {
    public class ViewModelLocator {
        public MainViewModel MainViewModel => new MainViewModel(new Service());
    }
}
