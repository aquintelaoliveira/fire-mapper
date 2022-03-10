using System;
using System.Collections.Generic;
using System.Reflection;
using FireSource;

namespace FireMapper
{
    public class FireDataMapper : AbstractFireMapper
    {

        public FireDataMapper(Type objectType, IFactory factory) : base(objectType, factory) { }

        /// <summary>
        /// Iterares over every property of object of type t, creates a property binder and saves it on a dictionary.
        /// </summary>
        /// <param name="t">Object type</param>
        protected override void CreatePropertyBinderDictionary(Type t)
        {
            List<IPropertyBinder> propertyBinderList = new List<IPropertyBinder>();
            foreach (PropertyInfo p in t.GetProperties())
            {
                IPropertyBinder propertyBinder;
                if (IsFireCollection(p.PropertyType))
                {
                    propertyBinder = new ComplexPropertyBinder(
                        p,
                        IsFireIgnore(p),
                        GetFireKeyName(p.PropertyType),
                        new FireDataMapper(p.PropertyType, Factory));
                }
                else
                {
                    propertyBinder = new SimplePropertyBinder(
                        p,
                        IsFireIgnore(p));
                }
                propertyBinderList.Add(propertyBinder);
            }
            PropertyBinderDictionary.Add(t, propertyBinderList);
        }
    }
}    

    