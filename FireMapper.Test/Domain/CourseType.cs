namespace FireMapper.Test.Domain
{
    [FireCollection("Course Types")]
    public record CourseType(
        [property: FireKey] string Id,
        string Type,
        ProgrammingLanguage ProgrammingLanguage) { }
}
