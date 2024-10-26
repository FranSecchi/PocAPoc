using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedWordFactory : WordFactory
{
    private int idx = 0;
    public LimitedWordFactory(List<WordStruct> data) : base(data) { difficulty = WordDifficulty.EASY; }
    public override WordStruct? getWord()
    {
        if (idx == data.Count)
            return null;
        WordStruct ws = new WordStruct(data[idx]);
        ++idx;
        return ws;
    }
}
