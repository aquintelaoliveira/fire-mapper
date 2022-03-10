using System.Collections.Generic;

namespace FireSource
{
    public record WeakDataSource(
        string Collection,
        string Key) : IDataSource
    {

        WeakDatabase database = WeakDatabase.GetInstance();

        public IEnumerable<Dictionary<string, object>> GetAll()
        {
            return database.GetAll(Collection);
        }

        public Dictionary<string, object> GetById(object KeyValue)
        {
            return database.GetById(Collection, Key, KeyValue);
        }

        public void Add(Dictionary<string, object> obj)
        {
            database.Add(Collection, obj);
        }

        public void Update(Dictionary<string, object> obj)
        {
            database.Update(Collection, Key, obj);
        }

        public void Delete(object KeyValue)
        {
            database.Delete(Collection, Key, KeyValue);
        }

    }
}
