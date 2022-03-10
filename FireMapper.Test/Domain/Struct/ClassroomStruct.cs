namespace FireMapper.Test.Domain
{
    [FireCollection("Classroom Structs")]
    public readonly struct ClassroomStruct
    {
        public ClassroomStruct(string token, string teacher)
        {
            Token = token;
            Teacher = teacher;
        }

        [property: FireKey] public string Token { get; init; }
        public string Teacher { get; init; }
    }
}