namespace FireSource
{
    public class WeakDataSourceFactory : IFactory
    {
         public IDataSource CreateDataSource(
            string Collection, 
            string Key)     
        {
            return new WeakDataSource(Collection, Key);
        }
    }
}