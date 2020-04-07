using System.ComponentModel;
using EnvironmentControl.Services;
using EnvironmentControl.ViewModels;

namespace EnvironmentControl.Common {
    public abstract class ViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify(params string[] properties) {
            if (PropertyChanged == null)
                return;

            foreach (var prop in properties) {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }

        public IDialogService Dialog => ServiceLocator.Instance.Dialog;

        public IService Service => ServiceLocator.Instance.Service;
    }
}
