using FireSource;

namespace FireMapper.Test.Domain
{
    [FireCollection("Dummy")]
    public record DummyClassWithoutFireKeyAttribute(int Number, string Name) { }
}