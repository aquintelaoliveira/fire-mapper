using System;
using System.Collections.Generic;

namespace FireMapper
{
    public abstract class AbstractSimplePropertyBinder : AbstractPropertyBinder
    {
        public AbstractSimplePropertyBinder(Type type, string name, bool isFireIgnore) : base(type, name, isFireIgnore) { }

        public override void Add(object target)
        {
            // do nothing
        }

        public override void Update(object target)
        {
            // do nothing
        }

        public override void Delete(object target)
        {
            // do nothing
        }

        public override object GetObjectValueFromDictionary(Dictionary<string, object> dictionary)
        {
            if (!IsFireIgnore)
            {
                return dictionary[Name] != null ? dictionary[Name] : throw new KeyNotFoundException($"There is no entry in database for key {Name}!");
            }
            else
            {
                return GetDefaultValue(PropertyType);
            }
        }

        public override void AddPropertyToDictionary(object target, Dictionary<string, object> dictionary)
        {
            if (!IsFireIgnore)
            {
                dictionary.Add(Name, GetValue(target));
            }
        }

    }
}