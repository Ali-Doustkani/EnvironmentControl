﻿using System.Collections.Generic;
using EnvironmentControl.Common;

namespace EnvironmentControl.ViewModels {
    public class ValueEditorViewModel : EditorViewModel {
        public ValueEditorViewModel(bool showDelete)
            : base(showDelete) { }

        public string Title { get; set; }

        public string ActualValue { get; set; }

        public override Dictionary<string, string> ToDictionary() =>
            new Dictionary<string, string>
            {
                {nameof(Title), Title},
                {nameof(ActualValue), ActualValue}
            };
    }
}
