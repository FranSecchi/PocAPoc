using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public abstract class WaveStrategy : ISpawn
{
    protected List<Spawner> spawners;
    protected float time = 0f;
    protected float timeInterval;
    protected float timeForWave;
    protected int numberWords;
    protected int wordsSpawned;
    public void Set(List<Spawner> spawners)
    {
        this.spawners = spawners;
        ResetSpawnPoints();
        Init();
    }
    protected abstract void Init();
    public abstract void JumpWave();
    public abstract void Spawn();
    protected void Instantiate(Spawner spawner, WordDifficulty difficulty, System.Type type)
    {
        List<WordStruct> currentWords = GameManager.Instance.NewWords;

        int spawnIndex = Random.Range(0, spawner.availableSpawnPoints.Count);
        Transform spawnPoint = spawner.availableSpawnPoints[spawnIndex];
        spawner.availableSpawnPoints.RemoveAt(spawnIndex);

        WordStruct wordCont;

        float factor = GameManager.Parameter.NewWordProb;
        float minFactor = 0.2f;  // Minimum probability of selecting a new word
        float decrementFactor = 0.02f; // How much the probability decreases per word

        // Adjust the probability based on the count of currentWords
        float adjustedFactor = factor - (decrementFactor * currentWords.Count);

        // Clamp adjustedFactor to ensure it doesn't drop below the minimum factor
        adjustedFactor = Mathf.Max(adjustedFactor, minFactor);

        if (currentWords.Count > 0 && Random.Range(0f, 1f) <= adjustedFactor)
        {
            int wordIndex = Random.Range(0, currentWords.Count);
            wordCont = currentWords[wordIndex];
        }
        else
        {
            WordFactory factory = spawner.getFactory(difficulty);
            wordCont = factory.getWord().Value; 
        }
        GameObject go = new GameObject();
        Word word = (Word)go.AddComponent(type);

        word.word = wordCont;
        word.spawner = spawner;


        go.transform.position = spawnPoint.position;
    }
    protected void ResetSpawnPoints()
    {
        foreach (var spawner in spawners)
        {
            spawner.availableSpawnPoints = new List<Transform>(spawner.spawnPoints);
        }
    }

    protected Spawner RandomSpawner()
    {
        Spawner spawner = null;
        List<Spawner> spawnersTemp = new List<Spawner>(spawners);
        do
        {
            spawner = spawnersTemp[Random.Range(0, spawnersTemp.Count)];
            spawnersTemp.Remove(spawner);
        } while (spawner.availableSpawnPoints.Count == 0 && spawnersTemp.Count > 0);
        return spawner;
    }
}

