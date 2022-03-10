namespace FireMapper.Test.Domain
{
    [FireCollection("Courses")]
    public struct CourseStruct
    {
        [property: FireKey] public string Id { get; init; }
        public string Name { get; init; }
        public CourseTypeStruct CourseType { get; init; }
        [property: FireIgnore] public string Description { get; init; }
        [property: FireIgnore] public double Price { get; init; }
        public InstructorStruct Instructor { get; init; }

        public CourseStruct(string id, string name, CourseTypeStruct courseType, string description, double price, InstructorStruct instrutor)
        {
            this.Id = id;
            this.Name = name;
            this.CourseType = courseType;
            this.Description = description;
            this.Price = price;
            this.Instructor = instrutor;
        }
    }
}
