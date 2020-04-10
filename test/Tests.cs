using EnvironmentControl.Common;
using EnvironmentControl.Domain;
using EnvironmentControl.ViewModels;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace EnvironmentControl.Tests.ViewModels {
    public class Tests {
        public Tests() {
            _service = new MockService();
            _dialog = Substitute.For<IDialogService>();
            ServiceLocator.Instance = Substitute.For<IServiceLocator>();
            ServiceLocator.Instance.Service.Returns(_service);
            ServiceLocator.Instance.Dialog.Returns(_dialog);
        }

        private readonly MockService _service;
        private readonly IDialogService _dialog;

        [Fact]
        public void Load_saved_values_on_load() {
            var vm = new MainViewModel();

            vm.Load.Execute(null);

            vm.Items.Should().HaveCount(1);
            vm.Items[0].Values[0].Should().BeEquivalentTo(new Value("first", "a"));
            vm.Items[0].Values[1].Should().BeEquivalentTo(new Value("second", "b"));
        }

        [Fact]
        public void Set_environment_variable_when_a_value_is_selected() {
            var vm = new MainViewModel();
            vm.Load.Execute(null);

            ((RadioViewModel)vm.Items[0].Values[0]).Selected = true;

            _service.GetVariable("myVar").Should().Be("a");
        }

        [Fact]
        public void Select_the_current_value_on_load() {
            var vm = new MainViewModel();

            vm.Load.Execute(null);

            ((RadioViewModel)vm.Items[0].Values[0]).Selected.Should().BeTrue();
        }

        [Fact]
        public void Select_nothing_if_the_current_value_is_not_available() {
            var vm = new MainViewModel();
            _service.SetVariable("myVar", "c");

            vm.Load.Execute(null);

            ((RadioViewModel)vm.Items[0].Values[0]).Selected.Should().BeFalse();
        }

        [Fact]
        public void Add_a_new_value_for_a_variable() {
            _dialog.ShowValueEditor().Returns(ValueDialogResult.Added("third", "c"));
            var vm = new MainViewModel();
            vm.Load.Execute(null);

            ((ButtonViewModel)vm.Items[0].Values[2]).NewValue.Execute(null);

            vm.Items[0].Values.Should().HaveCount(4);
            _service.Db.Variables.Should().HaveCount(1);
            _service.Db.Variables[0].Values.Should().HaveCount(3);
            _service.Db.Variables[0].Values.Should().BeEquivalentTo(new Value("first", "a"), new Value("second", "b"), new Value("third", "c"));
        }

        [Fact]
        public void Edit_a_value_of_variable() {
            _dialog.ShowValueEditor(Arg.Any<Value>()).Returns(ValueDialogResult.Edited("first2", "a2"));
            var vm = new MainViewModel();
            vm.Load.Execute(null);

            vm.Edit.Execute(null);
            ((RadioViewModel)vm.Items[0].Values[0]).Selected = true;

            vm.Items[0].Values.Should().HaveCount(3);
            _service.Db.Variables.Should().HaveCount(1);
            _service.Db.Variables[0].Values.Should().HaveCount(2);
            _service.Db.Variables[0].Values[0].Title.Should().Be("first2");
            _service.Db.Variables[0].Values[0].ActualValue.Should().Be("a2");
        }

        [Fact]
        public void Delete_a_value() {
            _dialog.ShowValueEditor(Arg.Any<Value>()).Returns(ValueDialogResult.Deleted());
            var vm = new MainViewModel();
            vm.Load.Execute(null);

            vm.Edit.Execute(null);
            ((RadioViewModel)vm.Items[0].Values[0]).Selected = true;

            vm.Items[0].Values.Should().HaveCount(2);
            _service.Db.Variables.Should().HaveCount(1);
            _service.Db.Variables[0].Values.Should().HaveCount(1);
            _service.Db.Variables[0].Values[0].Title.Should().Be("second");
            _service.Db.Variables[0].Values[0].ActualValue.Should().Be("b");
        }
    }
}
