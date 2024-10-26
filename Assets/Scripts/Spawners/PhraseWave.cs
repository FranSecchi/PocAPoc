using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhraseWave : WaveStrategy
{
    private WordFactory factory;
    private Queue<WordStruct> currentWords;
    private WordStruct phrase;
    private int spawnerIndex;
    private float timeForFrase;
    private float waitTime = 0f;
    public override void Spawn()
    {
        if (wordsSpawned >= numberWords)
        {
            JumpWave();
            return;
        }
        time += Time.deltaTime;
        waitTime += Time.deltaTime;
        if (time < timeInterval || waitTime < timeForFrase) return;
        if(currentWords.Count <= 0)
        {
            ++wordsSpawned;
            currentWords = GetWords(factory.getWord().Value);
            waitTime = 0f;
        }
        else Instantiate();

    }

    protected override void Init()
    {
        GameParameters param = GameManager.Parameter;
        factory = WordFactoryManager.createPhraseFactory();
        phrase = factory.getWord().Value;
        currentWords = GetWords(phrase);
        spawnerIndex = 0;
        timeForFrase = param.PhraseSpawnRate;
        timeInterval = param.PhraseWordsSpawnRate;
        numberWords = param.PhraseNextWave;
        timeForWave = param.PharseWaitTime;
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

    public override void JumpWave()
    {
        GameManager.Instance.addPhrase(phrase);
        GameManager.Instance.jumpWave(timeForWave);
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