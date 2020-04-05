﻿using EnvironmentControl.Services;
using EnvironmentControl.ViewModels;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace EnvironmentControl.Tests.ViewModels {
    public class MainViewModelTests {

        [Fact]
        public void Load_saved_values_on_load() {
            var svc = Substitute.For<IService>();
            svc.Load().Returns(Result());
            var vm = new MainViewModel(svc);

            vm.Load.Execute(null);

            vm.Items.Should().HaveCount(1);
            vm.Items[0].Values[0].Should().BeEquivalentTo(new Value("first", "a"));
            vm.Items[0].Values[1].Should().BeEquivalentTo(new Value("second", "b"));
        }

        [Fact]
        public void Set_environment_variable_when_a_value_is_selected() {
            var svc = Substitute.For<IService>();
            svc.Load().Returns(Result());
            var vm = new MainViewModel(svc);
            vm.Load.Execute(null);

            vm.Items[0].SelectedValue = new Value("title", "current value");

            svc.Received().SetVariable("myVar", "current value");
        }

        [Fact]
        public void Select_the_current_value_on_load() {
            var svc = Substitute.For<IService>();
            svc.Load().Returns(Result());
            svc.GetVariable("myVar").Returns("b");
            var vm = new MainViewModel(svc);

            vm.Load.Execute(null);

            vm.Items[0].SelectedValue.ActualValue.Should().Be("b");
        }

        [Fact]
        public void Select_nothing_if_the_current_value_is_not_available() {
            var svc = Substitute.For<IService>();
            svc.Load().Returns(Result());
            svc.GetVariable("myVar").Returns("c");
            var vm = new MainViewModel(svc);

            vm.Load.Execute(null);

            vm.Items[0].SelectedValue.Should().BeNull();
        }

        [Fact]
        public void Set_sizes_when_loading() {
            var svc = Substitute.For<IService>();
            var vm = new MainViewModel(svc);

            svc.Load().Returns(LoadResult.Successful(new Variable[0], 0, 0));
            vm.Load.Execute(null);
            vm.Width.Should().Be(300);
            vm.Height.Should().Be(300);

            svc.Load().Returns(LoadResult.Successful(new Variable[0], 100, 300));
            vm.Load.Execute(null);
            vm.Width.Should().Be(100);
            vm.Height.Should().Be(300);
        }

        private LoadResult Result() => LoadResult.Successful(
            new[] { new Variable("myVar", new[] { new Value("first", "a"), new Value("second", "b") }) }, 200, 500);
    }
}
