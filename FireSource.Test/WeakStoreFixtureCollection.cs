using Xunit;

namespace FireSource.Test
{
    [CollectionDefinition("WeakStoreFixture collection")]
    public class WeakStoreFixtureCollection : ICollectionFixture<WeakStoreFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}