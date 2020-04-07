using EnvironmentControl.Services;
using EnvironmentControl.ViewModels;
using FluentAssertions;
using Newtonsoft.Json;
using NSubstitute;
using System.Collections.Generic;
using System.IO;
using EnvironmentControl.Common;
using EnvironmentControl.Domain;
using Xunit;

namespace EnvironmentControl.Tests.ViewModels {
    public class Tests {
        public Tests() {
            var realService = new Service();
            _service = Substitute.For<IService>();
            _service.When(x => x.SaveVariable(Arg.Any<Variable>())).Do(async x => await realService.SaveVariable(x.Arg<Variable>()));
            _dialog = Substitute.For<IDialogService>();
            ServiceLocator.Instance = Substitute.For<IServiceLocator>();
            ServiceLocator.Instance.Service.Returns(_service);
            ServiceLocator.Instance.Dialog.Returns(_dialog);
        }

        private readonly IService _service;
        private readonly IDialogService _dialog;

        [Fact]
        public void Load_saved_values_on_load() {
            var vm = new MainViewModel();
            _service.Load().Returns(Result());

            vm.Load.Execute(null);

            vm.Items.Should().HaveCount(1);
            vm.Items[0].Values[0].Should().BeEquivalentTo(new Value("first", "a"));
            vm.Items[0].Values[1].Should().BeEquivalentTo(new Value("second", "b"));
        }

        [Fact]
        public void Set_environment_variable_when_a_value_is_selected() {
            var vm = new MainViewModel();
            _service.Load().Returns(Result());

            vm.Load.Execute(null);

            ((RadioViewModel)vm.Items[0].Values[0]).Selected = true;

            _service.Received().SetVariable("myVar", "a");
        }

        [Fact]
        public void Select_the_current_value_on_load() {
            var vm = new MainViewModel();
            _service.Load().Returns(Result());
            _service.GetVariable("myVar").Returns("b");

            vm.Load.Execute(null);

            ((RadioViewModel)vm.Items[0].Values[1]).ActualValue.Should().Be("b");
        }

        [Fact]
        public void Select_nothing_if_the_current_value_is_not_available() {
            var vm = new MainViewModel();
            _service.Load().Returns(Result());
            _service.GetVariable("myVar").Returns("c");

            vm.Load.Execute(null);

            ((RadioViewModel)vm.Items[0].Values[0]).Selected.Should().BeFalse();
        }

        [Fact]
        public void Set_coordination_when_loading() {
            var vm = new MainViewModel();
            _service.Load().Returns(LoadResult.Successful(new Variable[0], 10, 20));
            vm.Load.Execute(null);
            vm.Top.Should().Be(10);
            vm.Left.Should().Be(20);
        }

        [Fact]
        public void Add_a_new_value_for_a_variable() {
            _service.Load().Returns(Result());
            _dialog.ShowValueEditor().Returns(DialogResult.Succeeded(new Value("third", "c")));
            var vm = new MainViewModel();
            vm.Load.Execute(null);

            ((ButtonViewModel)vm.Items[0].Values[2]).NewValue.Execute(null);

            vm.Items[0].Values.Should().HaveCount(4);
            var db = ReadDb();
            db.Variables.Should().HaveCount(1);
            db.Variables[0].Values.Should().HaveCount(3);
            db.Variables[0].Values.Should().BeEquivalentTo(new Value("first", "a"), new Value("second", "b"), new Value("third", "c"));
        }

        private LoadResult Result() => LoadResult.Successful(
            new[] { new Variable("myVar", new List<Value> { new Value("first", "a"), new Value("second", "b") }) }, 200, 500);

        private Db ReadDb() {
            string json = File.ReadAllText("db.json");
            return JsonConvert.DeserializeObject<Db>(json);
        }
    }
}
