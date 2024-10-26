using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumWordFactory : WordFactory
{
    public MediumWordFactory(List<WordStruct> data) : base(data) { difficulty = WordDifficulty.MEDIUM; }
    public override WordStruct? getWord()
    {
        WordStruct ws = new WordStruct(data[Random.Range(0, data.Count)]);
        return ws;
    }
}
