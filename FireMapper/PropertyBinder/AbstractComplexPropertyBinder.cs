using System;
using System.Collections.Generic;

namespace FireMapper
{
    public abstract class AbstractComplexPropertyBinder : AbstractPropertyBinder
    {
        protected IDataMapper DataMapper;

        public AbstractComplexPropertyBinder(Type type, string name, bool isFireIgnore, IDataMapper dataMapper) : base(type, name, isFireIgnore)
        {
            this.DataMapper = dataMapper;
        }

        public override void Add(object target)
        {
            DataMapper.Add(GetValue(target));
        }

        public override void Update(object target)
        {
            DataMapper.Update(GetValue(target));
        }

        public override void Delete(object target)
        {
            if (!IsFireIgnore)
            {
                DataMapper.Delete(((Dictionary<string, object>)target)[Name]);
            }
        }

        public override object GetObjectValueFromDictionary(Dictionary<string, object> dictionary)
        {
            if (!IsFireIgnore)
            {
                return DataMapper.GetById(dictionary[Name]);
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
                dictionary.Add(Name, GetFireKeyValue(GetValue(target)));
            }
        }

        public abstract object GetFireKeyValue(object target);
    }
}