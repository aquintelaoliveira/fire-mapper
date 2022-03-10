namespace FireMapper.Test.Domain
{
    [FireCollection("Courses")]
    public record Course(
        [property: FireKey] string Id,
        string Name,
        CourseType CourseType,
        [property: FireIgnore] string Description,
        [property: FireIgnore] double Price,
        Instructor Instructor) { }
}
