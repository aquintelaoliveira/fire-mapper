namespace FireMapper.Test.Domain
{
    [FireCollection("Programming Languages")]
    public record ProgrammingLanguage(
        [property: FireKey] string Id,
        string Language) { }   
}
