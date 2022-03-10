using System;
using System.Collections.Generic;

namespace FireSource
{
    // The Singleton class defines the `GetInstance` method that serves as an
    // alternative to constructor and lets clients access the same instance of
    // this class over and over.
    class WeakDatabase
    {
        // The Singleton's constructor should always be private to prevent
        // direct construction calls with the `new` operator.
        private WeakDatabase() { }

        // The Singleton's instance is stored in a static field. There there are
        // multiple ways to initialize this field, all of them have various pros
        // and cons. In this example we'll show the simplest of these ways,
        // which, however, doesn't work really well in multithreaded program.
        private static WeakDatabase _instance;

        // This is the static method that controls the access to the singleton
        // instance. On the first run, it creates a singleton object and places
        // it into the static field. On subsequent runs, it returns the client
        // existing object stored in the static field.
        public static WeakDatabase GetInstance()
        {
            if (_instance == null)
            {
                _instance = new WeakDatabase();
            }
            return _instance;
        }

        // Singleton business logic, which can be executed on its instance.
        Dictionary<string, Dictionary<string, Dictionary<string, object>>> database = new Dictionary<string, Dictionary<string, Dictionary<string, object>>>();

        private KeyValuePair<string, Dictionary<string, object>> GetDoc(string Collection, string Key, object KeyValue)
        {
            foreach (var item in database[Collection])
            {
                if (item.Value[Key].Equals(KeyValue))
                {
                    return item;
                }
            }
            throw new KeyNotFoundException($"There is no entry in database for key {KeyValue}!");
        }

        public IEnumerable<Dictionary<string, object>> GetAll(string Collection)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (var item in database[Collection])
            {
                list.Add(item.Value);
            }
            return list;
        }

        public Dictionary<string, object> GetById(string Collection, string Key, object KeyValue)
        {
            Dictionary<string, object> doc = GetDoc(Collection, Key, KeyValue).Value;
            return doc;
        }

        public void Add(string Collection, Dictionary<string, object> obj)
        {
            if(!database.ContainsKey(Collection))
            {
                database.Add(Collection, new Dictionary<string, Dictionary<string, object>>());
            }
            database[Collection].Add(Guid.NewGuid().ToString(), obj);
        }

        public void Update(string Collection, string Key, Dictionary<string, object> obj)
        {
            var doc = GetDoc(Collection, Key, obj[Key]);
            database[Collection][doc.Key] = obj;
        }

        public void Delete(string Collection, string Key, object KeyValue)
        {
            var doc = GetDoc(Collection, Key, KeyValue);
            database[Collection].Remove(doc.Key);
        }

        public void Clear()
        {
            database = new Dictionary<string, Dictionary<string, Dictionary<string, object>>>();
        }

    }
}