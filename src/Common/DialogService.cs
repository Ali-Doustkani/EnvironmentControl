using EnvironmentControl.Domain;
using EnvironmentControl.ViewModels;
using EnvironmentControl.Views;
using System;
using System.Linq;
using System.Windows;
using Variable = EnvironmentControl.Domain.Variable;

namespace EnvironmentControl.Common {
    public class DialogService : IDialogService {
        public DialogResult ShowValueEditor() =>
            ShowEditorForAdd<ValueEditor>(new ValueEditorViewModel(false));

        public DialogResult ShowValueEditor(Value value) {
            var ctx = new ValueEditorViewModel(true) {Title = value.Title, ActualValue = value.ActualValue};
            return ShowEditorForEdit<ValueEditor>(ctx);
        }

        public DialogResult ShowVariableEditor() =>
            ShowEditorForAdd<VariableEditor>(new VariableEditorViewModel(false));

        public DialogResult ShowVariableEditor(Variable variable) {
            var ctx = new VariableEditorViewModel(true) {Name = variable.Name};
            return ShowEditorForEdit<VariableEditor>(ctx);
        }

        public void Accept() => CurrentWindow().DialogResult = true;

        public void Close() {
            try {
                CurrentWindow().DialogResult = false;
            }
            catch (InvalidOperationException) {
                CurrentWindow().Close();
            }
        }

        private Window CurrentWindow() => Application.Current.Windows.OfType<Window>().Single(x => x.IsActive);

        private DialogResult ShowEditorForAdd<TWindow>(EditorViewModel viewModel)
            where TWindow : Window, new() {
            var view = new TWindow { DataContext = viewModel };
            var result = view.ShowDialog();
            if (result == true) {
                return DialogResult.Added(viewModel.ToDictionary());
            }
            return DialogResult.Failed();
        }

        private DialogResult ShowEditorForEdit<TWindow>(EditorViewModel viewModel)
            where TWindow : Window, new() {
            var view = new TWindow { DataContext = viewModel };
            var result = view.ShowDialog();
            if (result == true) {
                if (viewModel.Deleted)
                    return DialogResult.Deleted();
                return DialogResult.Edited(viewModel.ToDictionary());
            }
            return DialogResult.Failed();
        }
    }
}
