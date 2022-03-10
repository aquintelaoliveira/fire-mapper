namespace FireSource
{
    public interface IFactory {
        IDataSource CreateDataSource(string Collection, string Key);
    }
}