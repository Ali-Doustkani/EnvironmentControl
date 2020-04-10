using System;
using System.Linq;
using System.Windows;
using EnvironmentControl.Domain;
using EnvironmentControl.ViewModels;
using EnvironmentControl.Views;

namespace EnvironmentControl.Common {
    public class DialogService : IDialogService {
        public ValueDialogResult ShowValueEditor() {
            var view = new ValueEditor();
            var ctx = new ValueEditorViewModel(false);
            view.DataContext = ctx;
            var result = view.ShowDialog();
            if (result == true) {
                return ValueDialogResult.Added(ctx.Title, ctx.ActualValue);
            }
            return ValueDialogResult.Failed();
        }

        public ValueDialogResult ShowValueEditor(Value value) {
            var view = new ValueEditor();
            var ctx = new ValueEditorViewModel(true);
            ctx.Title = value.Title;
            ctx.ActualValue = value.ActualValue;
            view.DataContext = ctx;
            var result = view.ShowDialog();
            if (result == true) {
                if (ctx.Deleted)
                    return ValueDialogResult.Deleted();
                return ValueDialogResult.Edited(ctx.Title, ctx.ActualValue);
            }
            return ValueDialogResult.Failed();
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
    }
}
