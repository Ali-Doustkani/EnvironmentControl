using System.ComponentModel;

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
    }
}
