﻿using System;
using System.Linq;
using System.Windows;
using EnvironmentControl.Domain;
using EnvironmentControl.ViewModels;
using EnvironmentControl.Views;

namespace EnvironmentControl.Common {
    public class DialogService : IDialogService {
        public DialogResult ShowValueEditor() {
            var view = new ValueEditor();
            var ctx = new ValueEditorViewModel();
            view.DataContext = ctx;
            var result = view.ShowDialog();
            if (result == true) {
                return DialogResult.Succeeded(new Value(ctx.Title, ctx.ActualValue));
            }

            return DialogResult.Failed();
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
