using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhraseWave : WaveStrategy
{
    private WordFactory factory;
    private Queue<WordStruct> currentWords;
    private WordStruct phrase;
    private Spawner spawner;
    private float timeForFrase;
    private int spawnerIndex;
    private float waitTime = 0f;
    private bool split;
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
            GameManager.Instance.addPhrase(phrase);
            ++wordsSpawned;
            currentWords = GetWords(factory.getWord().Value);
            PullPhrase();
            spawner = RandomSpawner();
            waitTime = 0f;
        }
        else Instantiate();

    }
    private void PullPhrase()
    {
        phrase = factory.getWord().Value;
        if (split) currentWords = GetWords(phrase);
        else
        {
            currentWords = new Queue<WordStruct>();
            currentWords.Enqueue(phrase);
        }
        timeForFrase = GameManager.Parameter.PhraseSpawnRate + timeInterval * currentWords.Count;
    }
    protected override void Init()
    {
        GameParameters param = GameManager.Parameter;
        factory = WordFactoryManager.createPhraseFactory();
        split = true;
        timeInterval = param.PhraseWordsSpawnRate;
        numberWords = param.PhraseNextWave;
        timeForWave = param.PharseWaitTime;
        spawner = RandomSpawner();
        spawnerIndex = 0;
        PullPhrase();
    }
    protected void Instantiate()
    {

        if (split && spawnerIndex >= spawners.Count) spawnerIndex = 0;
        if (split) spawner = spawners[spawnerIndex];
        GameObject go = new GameObject();
        Word word = (Word)go.AddComponent(typeof(FraseWord));

        word.word = currentWords.Dequeue();
        word.spawner = spawner;

        spawnerIndex++;
        go.transform.position = spawner.transform.position;
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