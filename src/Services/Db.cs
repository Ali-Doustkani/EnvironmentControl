using EnvironmentControl.Domain;

namespace EnvironmentControl.Services {
    public class Db {
        public Db(double top, double left, Environment environment) {
            Top = top;
            Left = left;
            Environment = environment;
        }

        public double Top { get; set; }
        public double Left { get; set; }
        public Environment Environment { get; }
    }
}
