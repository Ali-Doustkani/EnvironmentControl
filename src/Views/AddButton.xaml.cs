using System.Windows;
using System.Windows.Media;

namespace EnvironmentControl.Views {
    public partial class AddButton {
        public AddButton() {
            InitializeComponent();
        }

        public static readonly DependencyProperty MouseOverBackgroundProperty = DependencyProperty.Register("MouseOverBackground", typeof(Brush), typeof(AddButton));
        public static readonly DependencyProperty MouseOverForegroundProperty = DependencyProperty.Register("MouseOverForeground", typeof(Brush), typeof(AddButton));

        public Brush MouseOverBackground {
            get => (Brush)GetValue(MouseOverBackgroundProperty);
            set => SetValue(MouseOverBackgroundProperty, value);
        }

        public Brush MouseOverForeground {
            get => (Brush)GetValue(MouseOverForegroundProperty);
            set => SetValue(MouseOverForegroundProperty, value);
        }
    }
}
