using System;
using System.Collections.Generic;

namespace FireMapper
{
    public interface IPropertyBinder
    {
        string Name { get; }
        object GetDefaultValue(Type target);
        object GetValue(object target);
        void Add(object target);
        void Update(object target);
        void Delete(object target);
        object GetObjectValueFromDictionary(Dictionary<string, object> dictionary);
        void AddPropertyToDictionary(object target, Dictionary<string, object> dictionary);
    }
}
