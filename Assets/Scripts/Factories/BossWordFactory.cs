using System.Collections.Generic;
using UnityEngine;

internal class BossWordFactory : WordFactory
{
    public BossWordFactory(List<WordStruct> data) : base(data) { difficulty = WordDifficulty.BOSS; }

    public override WordStruct? getWord()
    {
        WordStruct ws = new WordStruct(data[Random.Range(0, data.Count)]);
        ws.Type = WordType.FRASE;
        return ws;
    }
}