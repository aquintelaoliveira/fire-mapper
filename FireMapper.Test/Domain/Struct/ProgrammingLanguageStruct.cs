namespace FireMapper.Test.Domain
{
    [FireCollection("Programming Languages")]
    public struct ProgrammingLanguageStruct
    {
        [property: FireKey] public string Id { get; init; }
        public string Language { get; init; }

        public ProgrammingLanguageStruct(string id, string language)
        {
            this.Id = id;
            this.Language = language;
        }
    }
}
