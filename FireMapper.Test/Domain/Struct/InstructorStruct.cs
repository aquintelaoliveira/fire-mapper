namespace FireMapper.Test.Domain
{
    [FireCollection("Instructors")]
    public struct InstructorStruct
    {
        [property: FireKey] public string Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }

        public InstructorStruct(string id, string name, string email)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
        }
    }
}
