namespace FireMapper.Test.Domain
{
    [FireCollection("Student Structs")]
    public readonly struct StudentStruct
    {
        public StudentStruct(string number, string name, ClassroomStruct classroom)
        {
            Number = number;
            Name = name;
            Classroom = classroom;
        }

        [property: FireKey] public string Number { get; init; }
        public string Name { get; init; }
        public ClassroomStruct Classroom { get; init; }
    }
}