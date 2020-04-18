﻿using EnvironmentControl.Domain;
using EnvironmentControl.ViewModels;
using EnvironmentControl.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace EnvironmentControl.Common {
    public class DialogService : IDialogService {
        public DialogResult ShowValueEditor(string variableName) {
            var viewModel = new ValueEditorViewModel(false, variableName);
            var view = new ValueEditor { DataContext = viewModel };
            var result = view.ShowDialog();
            if (result == true) {
                return DialogResult.Added(viewModel.ToDictionary());
            }
            return DialogResult.Failed();
        }

        public DialogResult ShowValueEditor(string variableName, Value value) {
            var ctx = new ValueEditorViewModel(true, variableName, value.Id) { Title = value.Title, ActualValue = value.ActualValue };
            var view = new ValueEditor { DataContext = ctx };
            var result = view.ShowDialog();
            if (result == true) {
                if (ctx.Deleted)
                    return DialogResult.Deleted();
                return DialogResult.Edited(ctx.ToDictionary());
            }
            return DialogResult.Failed();
        }

        public DialogResult ShowVariableSelector() {
            var view = new VariableSelector();
            var vm = new VariableSelectorViewModel();
            view.DataContext = vm;
            var result = view.ShowDialog();
            if (result == true) {
                return DialogResult.Added(new Dictionary<string, string> { { "Name", vm.SelectedVariable.Name } });
            }
            return DialogResult.Failed();
        }

        public void Error(string message) => MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        public void Accept() => CurrentWindow().DialogResult = true;

        public void Close() {
            try {
                CurrentWindow().DialogResult = false;
            } catch (InvalidOperationException) {
                CurrentWindow().Close();
            }
        }

        private Window CurrentWindow() => Application.Current.Windows.OfType<Window>().Single(x => x.IsActive);
    }
}
