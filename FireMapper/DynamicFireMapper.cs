using System;
using System.Collections.Generic;
using System.Reflection;
using FireSource;

namespace FireMapper
{
    public class DynamicFireMapper : AbstractFireMapper
    {
        public DynamicFireMapper(Type objectType, IFactory factory) : base(objectType, factory) { }

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
                    DynamicComplexPropertyBinderBuilder builder = new DynamicComplexPropertyBinderBuilder(t);
                    Type propertyType = builder.GeneratePropertyBinderFor(p, GetFireKeyProperty(p.PropertyType));
                    propertyBinder = (IPropertyBinder)Activator.CreateInstance(
                        propertyType,
                        new object[] { p.PropertyType, IsFireIgnore(p), new DynamicFireMapper(p.PropertyType, Factory) });
                }
                else
                {
                    DynamicSimplePropertyBinderBuilder builder = new DynamicSimplePropertyBinderBuilder(t);
                    Type propertyType = builder.GeneratePropertyBinderFor(p);
                    propertyBinder = (IPropertyBinder)Activator.CreateInstance(
                        propertyType, 
                        new object[] { p.PropertyType, IsFireIgnore(p) });
                }
                propertyBinderList.Add(propertyBinder);
            }
            PropertyBinderDictionary.Add(t, propertyBinderList);
        }
    }
}