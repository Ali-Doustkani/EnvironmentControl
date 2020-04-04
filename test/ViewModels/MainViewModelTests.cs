using EnvironmentControl.Services;
using EnvironmentControl.ViewModels;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace EnvironmentControl.Tests.ViewModels {
    public class MainViewModelTests {

        [Fact]
        public void Load_saved_values_on_load() {
            var svc = Substitute.For<IService>();
            svc.LoadItems().Returns(new[] { new VariableValue("a"), new VariableValue("b") });
            var vm = new MainViewModel(svc);

            vm.Load.Execute(null);

            vm.Items.Should().HaveCount(2);
            vm.Items[0].Should().BeEquivalentTo(new VariableValue("a"));
            vm.Items[1].Should().BeEquivalentTo(new VariableValue("b"));
        }

        [Fact]
        public void Set_environment_variable_when_a_value_is_selected() {
            var svc = Substitute.For<IService>();
            var vm = new MainViewModel(svc);

            vm.SelectedVariableValue = new VariableValue("current value");

            svc.Received().SetVariable("current value");
        }

        [Fact]
        public void Select_the_current_value_on_load() {
            var svc = Substitute.For<IService>();
            svc.LoadItems().Returns(new[] { new VariableValue("a"), new VariableValue("b") });
            svc.GetVariable().Returns("b");
            var vm = new MainViewModel(svc);

            vm.Load.Execute(null);

            vm.SelectedVariableValue.Value.Should().Be("b");
        }

        [Fact]
        public void Select_nothing_if_the_current_value_is_not_available() {
            var svc = Substitute.For<IService>();
            svc.LoadItems().Returns(new[] { new VariableValue("a"), new VariableValue("b") });
            svc.GetVariable().Returns("c");
            var vm = new MainViewModel(svc);

            vm.Load.Execute(null);

            vm.SelectedVariableValue.Should().BeNull();
        }
    }
}
