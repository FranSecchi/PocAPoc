using System.Collections;
using System.Collections.Generic;

public abstract class WordFactory
{
    public WordDifficulty difficulty;
    protected List<WordStruct> data = new List<WordStruct>();
    public abstract WordStruct? getWord();
    protected WordFactory(List<WordStruct> data)
    {
        this.data = data;
    }
    public List<WordStruct> GetWordList()
    {
        return data;
    }
    public void SetWordList(List<WordStruct> data)
    {
        this.data = data;
    }
}
