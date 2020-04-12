using System.Collections.Generic;
using EnvironmentControl.Common;

namespace EnvironmentControl.ViewModels {
    public class VariableEditorViewModel : EditorViewModel {
        public VariableEditorViewModel(bool showDelete)
            : base(showDelete) { }

        public string Name { get; set; }

        public override Dictionary<string, string> ToDictionary() =>
            new Dictionary<string, string>
            {
                {nameof(Name), Name}
            };
    }
}
