using System;
using System.Collections.Generic;
using Libs.Analytics;
using Zenject;

namespace Core.Utils
{
    public interface IAnalyticSignalTypeGetter
    {
        public List<Type> GetSignalTypes();
    }
    
    public class AnalyticSignalTypeGetter : IAnalyticSignalTypeGetter
    {
        private readonly SignalBus _signalBus;

        public AnalyticSignalTypeGetter(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private readonly List<Type> _signalTypes = new List<Type>();

        public List<Type> GetSignalTypes()
        {
            if (_signalTypes.Count == 0)
            {
                foreach (var signalDeclaration in _signalBus.Declarations)
                {
                    var type = signalDeclaration.BindingId.Type;
                    if (!type.IsInterface && typeof(ISignalAnalytic).IsAssignableFrom(type))
                    {
                        _signalTypes.Add(type);   
                    }
                }
            }
            return _signalTypes;
        } 
    }
}