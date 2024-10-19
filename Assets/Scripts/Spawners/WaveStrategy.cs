using System.Collections.Generic;
using UnityEngine;

public abstract class WaveStrategy : ISpawn
{
    protected List<Spawner> spawners;
    protected float time = 0f;
    protected float timeInterval;
    protected float timeForWave;
    protected int numberWords;
    protected int wordsSpawned;
    public void SetSpawners(List<Spawner> spawners)
    {
        this.spawners = spawners;
        Init();
    }
    protected abstract void Init();
    public abstract void Spawn();
    protected void Instantiate(Spawner spawner, WordDifficulty difficulty, System.Type type)
    {
        int spawnIndex = Random.Range(0, spawner.availableSpawnPoints.Count);
        Transform spawnPoint = spawner.availableSpawnPoints[spawnIndex];
        spawner.availableSpawnPoints.RemoveAt(spawnIndex);

        WordFactory factory = spawner.getFactory(difficulty);
        WordStruct wordCont = factory.getWord();

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
}