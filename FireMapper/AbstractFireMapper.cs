using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using FireSource;

namespace FireMapper
{
    public abstract class AbstractFireMapper : IDataMapper
    {
        protected readonly Type ObjectType;
        protected readonly IFactory Factory;
        protected readonly IDataSource DataSource;
        protected readonly Dictionary<Type, List<IPropertyBinder>> PropertyBinderDictionary = new Dictionary<Type, List<IPropertyBinder>>();

        public AbstractFireMapper(Type objectType, IFactory factory)
        {
            this.ObjectType = objectType;
            this.Factory = factory;
            this.DataSource = factory.CreateDataSource(GetFireCollectionName(objectType), GetFireKeyName(objectType));
            CreatePropertyBinderDictionary(objectType);
        }

        /// <summary>
        /// Iterares over every property of object of type t, creates a property binder and saves it on a dictionary.
        /// </summary>
        /// <param name="t">Object type</param>
        protected abstract void CreatePropertyBinderDictionary(Type t);

        protected bool IsFireCollection(Type t)
        {
            return Attribute.IsDefined(t, typeof(FireCollectionAttribute));
        }

        protected bool IsFireKey(PropertyInfo p)
        {
            return Attribute.IsDefined(p, typeof(FireKeyAttribute));
        }

        protected bool IsFireIgnore(PropertyInfo p)
        {
            return Attribute.IsDefined(p, typeof(FireIgnoreAttribute));
        }

        private string GetFireCollectionName(Type t)
        {
            if(IsFireCollection(t))
            {
                return ((FireCollectionAttribute)t.GetCustomAttribute(typeof(FireCollectionAttribute))).Name;
            }
            else 
            {
                throw new InvalidOperationException($"{t.Name} is not a FireCollection!");
            }
        }

        protected string GetFireKeyName(Type t)
        {
            foreach (PropertyInfo p in t.GetProperties())
            {
                if (IsFireKey(p))
                {
                    return p.Name;
                }
            }
            throw new InvalidOperationException($"{t.Name} is missing a FireKey!");
        }

        protected PropertyInfo GetFireKeyProperty(Type t)
        {
            foreach (PropertyInfo p in t.GetProperties())
            {
                if (IsFireKey(p))
                {
                    return p;
                }
            }
            throw new InvalidOperationException($"{t.Name} is missing a FireKey!");
        }

        public IEnumerable GetAll()
        {
            IEnumerable<Dictionary<string, object>> dictionaries = DataSource.GetAll();
            List<object> list = new List<object>();
            foreach (Dictionary<string, object> d in dictionaries)
            {
                list.Add(GenericStrongTypeConstructor(ObjectType, d));
            }
            return list;
        }

        public object GetById(object keyValue)
        {
            return GenericStrongTypeConstructor(ObjectType, DataSource.GetById(keyValue));
        }

        public void Add(object obj)
        {
            foreach (IPropertyBinder pb in PropertyBinderDictionary[ObjectType])
            {
                pb.Add(obj);
            }
            DataSource.Add(GenericWeakTypeConstructor(obj));
        }

        public void Update(object obj)
        {
            foreach (IPropertyBinder pb in PropertyBinderDictionary[ObjectType])
            {
                pb.Update(obj);
            }
            DataSource.Update(GenericWeakTypeConstructor(obj));
        }

        public void Delete(object keyValue)
        {
            foreach (IPropertyBinder pb in PropertyBinderDictionary[ObjectType])
            {
                pb.Delete(DataSource.GetById(keyValue));
            }

            DataSource.Delete(keyValue);
        }

        /// <summary>
        /// Maps dictionary entries to object of type t.
        /// </summary>
        /// <param name="t">Object type</param>
        /// <param name="dictionary">Dictionary to be mapped</param>
        /// <returns>Object of type t</returns>
        protected object GenericStrongTypeConstructor(Type t, Dictionary<string, object> dictionary)
        {
            if (dictionary == null) return null;
            List<IPropertyBinder> propertyBinderList = PropertyBinderDictionary[t];
            int index = 0;
            object[] args = new object[propertyBinderList.Count];
            foreach (IPropertyBinder pb in PropertyBinderDictionary[ObjectType])
            {
                args[index++] = pb.GetObjectValueFromDictionary(dictionary);
            }
            return Activator.CreateInstance(t, args);
        }

        /// <summary>
        /// Maps properties from object to dictionary.
        /// </summary>
        /// <param name="obj">Object to be mapped</param>
        /// <returns>Mapped dictionary</returns>
        protected Dictionary<string, object> GenericWeakTypeConstructor(object obj)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            foreach (IPropertyBinder pb in PropertyBinderDictionary[obj.GetType()])
            {
                pb.AddPropertyToDictionary(obj, dict);
            }
            return dict;
        }
    }
}    

    