using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyWordFactory : WordFactory
{
    public EasyWordFactory(List<WordStruct> data) : base(data) { difficulty = WordDifficulty.EASY; }
    public override WordStruct? getWord()
    {
        WordStruct ws = new WordStruct(data[Random.Range(0, data.Count)]);
        return ws;
    }
}
