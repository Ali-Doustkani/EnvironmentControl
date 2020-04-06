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
            var dlg = Substitute.For<IDialogService>();
            svc.Load().Returns(Result());
            var vm = new MainViewModel(svc, dlg);

            vm.Load.Execute(null);

            vm.Items.Should().HaveCount(1);
            vm.Items[0].Values[0].Should().BeEquivalentTo(new Value("first", "a"));
            vm.Items[0].Values[1].Should().BeEquivalentTo(new Value("second", "b"));
        }

        [Fact]
        public void Set_environment_variable_when_a_value_is_selected() {
            var svc = Substitute.For<IService>();
            var dlg = Substitute.For<IDialogService>();
            svc.Load().Returns(Result());
            var vm = new MainViewModel(svc, dlg);
            vm.Load.Execute(null);

            vm.Items[0].SelectedValue = new Value("title", "current value");

            svc.Received().SetVariable("myVar", "current value");
        }

        [Fact]
        public void Select_the_current_value_on_load() {
            var svc = Substitute.For<IService>();
            var dlg = Substitute.For<IDialogService>();
            svc.Load().Returns(Result());
            svc.GetVariable("myVar").Returns("b");
            var vm = new MainViewModel(svc, dlg);

            vm.Load.Execute(null);

            vm.Items[0].SelectedValue.ActualValue.Should().Be("b");
        }

        [Fact]
        public void Select_nothing_if_the_current_value_is_not_available() {
            var svc = Substitute.For<IService>();
            var dlg = Substitute.For<IDialogService>();
            svc.Load().Returns(Result());
            svc.GetVariable("myVar").Returns("c");
            var vm = new MainViewModel(svc, dlg);

            vm.Load.Execute(null);

            vm.Items[0].SelectedValue.Should().BeNull();
        }

        [Fact]
        public void Set_coordination_when_loading() {
            var svc = Substitute.For<IService>();
            var dlg = Substitute.For<IDialogService>();
            var vm = new MainViewModel(svc, dlg);

            svc.Load().Returns(LoadResult.Successful(new Variable[0], 10, 20));
            vm.Load.Execute(null);
            vm.Top.Should().Be(10);
            vm.Left.Should().Be(20);
        }

        private LoadResult Result() => LoadResult.Successful(
            new[] { new Variable("myVar", new[] { new Value("first", "a"), new Value("second", "b") }) }, 200, 500);
    }
}
