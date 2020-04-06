using System.Windows;
using System.Windows.Media;

namespace EnvironmentControl.Views {
    public partial class SimpleButton {
        public SimpleButton() {
            InitializeComponent();
        }

        public static readonly DependencyProperty MouseOverBackgroundProperty = DependencyProperty.Register("MouseOverBackground", typeof(Brush), typeof(SimpleButton));

        public Brush MouseOverBackground {
            get => (Brush)GetValue(MouseOverBackgroundProperty);
            set => SetValue(MouseOverBackgroundProperty, value);
        }
    }
}
