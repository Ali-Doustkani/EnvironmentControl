﻿using EnvironmentControl.Common;
using EnvironmentControl.Domain;
using EnvironmentControl.ViewModels;
using FluentAssertions;
using NSubstitute;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EnvironmentControl.Services;
using Xunit;

namespace EnvironmentControl.Tests.ViewModels {
    public class Tests {
        public Tests() {
            _main = new MainViewModel(new StateManager() );
            _service = new MockService();
            _dialog = Substitute.For<IDialogService>();
            var mediator = new Mediator();
            ServiceLocator.Instance = Substitute.For<IServiceLocator>();
            ServiceLocator.Instance.Service.Returns(_service);
            ServiceLocator.Instance.Dialog.Returns(_dialog);
            ServiceLocator.Instance.Mediator.Returns(mediator);
        }

        private readonly MainViewModel _main;
        private readonly MockService _service;
        private readonly IDialogService _dialog;

        [Fact]
        public void Load_saved_values_on_load() {
            Load();
            _main.Items.Should().HaveCount(2);
            GetValue(0, 0).Should().BeEquivalentTo(new Value("first", "a"));
            GetValue(0, 1).Should().BeEquivalentTo(new Value("second", "b"));
        }

        [Fact]
        public void Set_environment_variable_when_a_value_is_selected() {
            Load();
            ClickOn(0, 0);
            _service.GetVariable("myVar").Should().Be("a");
        }

        [Fact]
        public void Select_the_current_value_on_load() {
            Load();
            GetValue(0, 0).Selected.Should().BeTrue();
        }

        [Fact]
        public void Select_nothing_if_the_current_value_is_not_available() {
            _service.SetVariable("myVar", "c");
            Load();
            GetValue(0, 0).Selected.Should().BeFalse();
        }

        [Fact]
        public void Add_a_new_value_for_a_variable() {
            _dialog.ShowValueEditor().Returns(DialogResult.Added(
                new Dictionary<string, string>
                {
                    {"Title", "third" },
                    {"ActualValue", "c" }

                }));
            Load();

            ClickOn(0, 2);

            GetValues(0).Should().HaveCount(4);
            Db.Variables.Should().HaveCount(1);
            Db.Variables[0].Values.Should().HaveCount(3);
            Db.Variables[0].Values.Should().BeEquivalentTo(new Value("first", "a"), new Value("second", "b"), new Value("third", "c"));
        }

        [Fact]
        public void Edit_a_value_of_variable() {
            _dialog.ShowValueEditor(Arg.Any<Value>()).Returns(DialogResult.Edited(
                new Dictionary<string, string>
                {
                    {"Title", "first2" },
                    {"ActualValue", "a2" }
                }));
            Load();

            _main.Edit.Execute(null);
            ClickOn(0, 0);

            GetValues(0).Should().HaveCount(3);
            Db.Variables.Should().HaveCount(1);
            Db.Variables[0].Values.Should().HaveCount(2);
            Db.Variables[0].Values[0].Title.Should().Be("first2");
            Db.Variables[0].Values[0].ActualValue.Should().Be("a2");
        }

        [Fact]
        public void Delete_a_value() {
            _dialog.ShowValueEditor(Arg.Any<Value>()).Returns(DialogResult.Deleted());
            Load();

            _main.Edit.Execute(null);
            ClickOn(0, 0);

            GetValues(0).Should().HaveCount(2);
            Db.Variables.Should().HaveCount(1);
            Db.Variables[0].Values.Should().HaveCount(1);
            Db.Variables[0].Values[0].Title.Should().Be("second");
            Db.Variables[0].Values[0].ActualValue.Should().Be("b");
        }

        [Fact]
        public void Add_a_variable() {
            _dialog.ShowVariableSelector().Returns(DialogResult.Added(new Dictionary<string, string> { { "Name", "JAVA_HOME" } }));
            _service.SetVariable("JAVA_HOME", "defaultValue");
            Load();

            _main.Edit.Execute(null);
            ClickOn(1);

            _main.Items.Should().HaveCount(3);
            GetVariable(0).Name.Should().Be("myVar");
            GetVariable(1).Name.Should().Be("JAVA_HOME");
            GetValue(1, 0).Title.Should().Be("Default");
            GetValue(1, 0).ActualValue.Should().Be("defaultValue");
        }

        private Db Db => _service.Db;

        private void Load() {
            _main.Load.Execute(null);
            foreach (var item in _main.Items) {
                if (item is VariableViewModel variable) {
                    ObservableCollection<ITypedViewModel> values = variable.Values;
                }
            }
        }

        private void ClickOn(int variableIndex, int valueIndex = -1) {
            if (valueIndex == -1) {
                ((ButtonViewModel)_main.Items[variableIndex]).Command.Execute(null);
                return;
            }

            var value = ((VariableViewModel)_main.Items[variableIndex]).Values[valueIndex];
            if (value is RadioViewModel radio) {
                radio.Selected = true;
            } else if (value is ButtonViewModel button) {
                button.Command.Execute(null);
            }
        }

        private VariableViewModel GetVariable(int index) => (VariableViewModel)_main.Items[index];

        private ObservableCollection<ITypedViewModel> GetValues(int index) => ((VariableViewModel)_main.Items[index]).Values;

        private RadioViewModel GetValue(int variableIndex, int valueIndex) => (RadioViewModel)GetValues(variableIndex)[valueIndex];
    }
}
