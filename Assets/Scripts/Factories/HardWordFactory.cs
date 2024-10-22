using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardWordFactory : WordFactory
{
    public HardWordFactory(List<WordStruct> data) : base(data) { difficulty = WordDifficulty.HARD; }
    public override WordStruct? getWord()
    {
        WordStruct ws = new WordStruct(data[Random.Range(0, data.Count)]);
        ws.Type = WordType.HARD;
        return ws;
    }
}
