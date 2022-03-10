namespace FireMapper.Test.Domain
{
    [FireCollection("Course Types")]
    public struct CourseTypeStruct
    {
        [property: FireKey] public string Id { get; init; }
        public string Type { get; init; }
        public ProgrammingLanguageStruct ProgrammingLanguage { get; init; }

        public CourseTypeStruct(string id, string type, ProgrammingLanguageStruct programmingLanguage)
        {
            this.Id = id;
            this.Type = type;
            this.ProgrammingLanguage = programmingLanguage;
        }
    }
}
