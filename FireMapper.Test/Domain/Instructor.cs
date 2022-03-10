namespace FireMapper.Test.Domain
{
    [FireCollection("Instructors")]
    public record Instructor(
        [property: FireKey] string Id,
        string Name,
        [property: FireIgnore] string Email) { }
}
