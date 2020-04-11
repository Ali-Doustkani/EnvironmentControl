using System.Windows;
using System.Windows.Media;

namespace EnvironmentControl.Views {
    public partial class TextButton {
        public TextButton() {
            InitializeComponent();
        }

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(Brush), typeof(TextButton));
        public static readonly DependencyProperty ColorOnMouseOverProperty = DependencyProperty.Register("ColorOnMouseOver", typeof(Brush), typeof(TextButton));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TextButton));

        public Brush Color {
            get => (Brush)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public Brush ColorOnMouseOver {
            get => (Brush)GetValue(ColorOnMouseOverProperty);
            set => SetValue(ColorOnMouseOverProperty, value);
        }

        public string Text {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
    }
}
