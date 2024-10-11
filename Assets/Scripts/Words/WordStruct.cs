public enum WordType
{
    SIMPLE, MISTAKEN, HARD
}
public enum WordDifficulty
{
    EASY, MEDIUM, HARD
}
public struct WordStruct
{
    public string Content { get; set; }
    public string Description { get; set; }
    public string Dialect { get; set; }
    public WordType Type { get; set; }

    // Constructor for easy instantiation
    public WordStruct(string word, string description, string dialect, WordType type = WordType.SIMPLE)
    {
        Content = word;
        Description = description;
        Dialect = dialect;
        Type = type;
    }
    public WordStruct(WordStruct wordStruct)
    {
        Content = wordStruct.Content;
        Description = wordStruct.Description;
        Dialect = wordStruct.Dialect;
        Type = wordStruct.Type;
    }
}
