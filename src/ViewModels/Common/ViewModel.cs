using System.ComponentModel;
using EnvironmentControl.Services;

namespace EnvironmentControl.ViewModels.Common {
    public abstract class ViewModel : INotifyPropertyChanged {
        protected ViewModel() {
            Mediator = new Mediator();
            Service = new Service(new DataAccessFactory());
            Dialog = new DialogService();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify(params string[] properties) {
            if (PropertyChanged == null)
                return;

            foreach (var prop in properties) {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }

        public IDialogService Dialog { get; }

        public IService Service { get; }

        public Mediator Mediator { get; }
    }
}
