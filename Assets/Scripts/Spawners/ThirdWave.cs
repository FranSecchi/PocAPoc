using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdWave : WaveStrategy
{
    private WordFactory factory;
    private Queue<WordStruct> currentWords;
    private WordStruct phrase;
    private int spawnerIndex;
    public override void Spawn()
    {
        time += Time.deltaTime;
        if (time < timeInterval) return;
        if(currentWords.Count <= 0)
        {
            GameManager.Instance.addPhrase(phrase);
            GameManager.Instance.jumpWave();
        }
        else Instantiate();

    }

    protected override void Init()
    {
        factory = WordFactoryManager.createPhraseFactory();
        phrase = factory.getWord();
        currentWords = GetWords(phrase);
        spawnerIndex = 0;
        timeInterval = GameManager.Parameters.ThirdSpawnRate;
    }
    protected void Instantiate()
    {
        if (spawnerIndex >= spawners.Count) spawnerIndex = 0;
        Spawner spawner = spawners[spawnerIndex];

        GameObject go = new GameObject();
        Word word = (Word)go.AddComponent(typeof(SimpleWord));

        word.word = currentWords.Dequeue();
        word.spawner = spawner;

        go.transform.position = spawner.transform.position;
        spawnerIndex++;
        time = 0f;
    }
    private Queue<WordStruct> GetWords(WordStruct ws)
    {
        string[] t = ws.Content.Split(new char[] { ' ' });
        Queue<WordStruct> list = new Queue<WordStruct>();
        for(int i = 0; i < t.Length; ++i)
        {
            list.Enqueue(new WordStruct(t[i], ws.Description, ws.Dialect, WordType.FRASE));
        }
        return list;
    }
}