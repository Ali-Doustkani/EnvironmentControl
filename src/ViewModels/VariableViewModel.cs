using EnvironmentControl.Services;
using System.Collections.Generic;
using System.Linq;

namespace EnvironmentControl.ViewModels {
    public class VariableViewModel {
        public VariableViewModel(IService service, Variable variable) {
            _service = service;
            _variable = variable;
        }

        private readonly IService _service;
        private readonly Variable _variable;

        public string Name => _variable.Name;

        public IValueItem[] Values {
            get {
                var ret = new List<IValueItem>();
                var selectedValue = _service.GetVariable(_variable.Name);
                ret.AddRange(_variable.Values.Select(x => new RadioViewModel(_service, _variable, x, x.ActualValue == selectedValue)));
                ret.Add(new ButtonViewModel());
                return ret.ToArray();
            }
        }
    }

}