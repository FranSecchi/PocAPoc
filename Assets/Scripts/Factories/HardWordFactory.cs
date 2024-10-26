using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardWordFactory : WordFactory
{
    private List<WordStruct> currentWords = new List<WordStruct>();
    public HardWordFactory(List<WordStruct> data) : base(data) { difficulty = WordDifficulty.HARD; }
    public override WordStruct? getWord()
    {
        WordStruct ws;
        float factor = GameManager.Parameter.NewWordProb;
        float minFactor = 0.2f;
        float decrementFactor = 0.02f;

        float adjustedFactor = factor - (decrementFactor * currentWords.Count);

        adjustedFactor = Mathf.Max(adjustedFactor, minFactor);

        if (currentWords.Count > 0 && Random.Range(0f, 1f) <= adjustedFactor)
        {
            int wordIndex = Random.Range(0, currentWords.Count);
            ws = currentWords[wordIndex];
        }
        else
        {
            ws = new WordStruct(data[Random.Range(0, data.Count)]);
            currentWords.Add(ws);
        }
        return ws;
    }
}
