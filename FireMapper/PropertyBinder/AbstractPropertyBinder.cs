using System;
using System.Collections.Generic;

namespace FireMapper
{
    public abstract class AbstractPropertyBinder : IPropertyBinder
    {
        protected readonly Type PropertyType;
        protected readonly string _Name;
        protected readonly bool IsFireIgnore;

        public AbstractPropertyBinder(Type type, string name, bool isFireIgnore)
        {
            this.PropertyType = type;
            this._Name = name;
            this.IsFireIgnore = isFireIgnore;
        }

        public virtual string Name
        {
            get { return this._Name; }
        }

        public object GetDefaultValue(Type target)
        {
            return target == typeof(string) ? "" : default;
        }

        public abstract object GetValue(object target);
        public abstract void Add(object target);
        public abstract void Update(object target);
        public abstract void Delete(object target);
        public abstract object GetObjectValueFromDictionary(Dictionary<string, object> dictionary);
        public abstract void AddPropertyToDictionary(object target, Dictionary<string, object> dictionary);
    }
}