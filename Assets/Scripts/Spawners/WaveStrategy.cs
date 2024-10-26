﻿using System.Collections.Generic;
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

        int spawnIndex = Random.Range(0, spawner.availableSpawnPoints.Count);
        Transform spawnPoint = spawner.availableSpawnPoints[spawnIndex];
        spawner.availableSpawnPoints.RemoveAt(spawnIndex);

        WordStruct wordCont;

        WordFactory factory = spawner.getFactory(difficulty);
        wordCont = factory.getWord().Value;
        GameObject go = new GameObject();
        Word word = (Word)go.AddComponent(type);

        word.word = wordCont;
        word.spawner = spawner;
        word.difficulty = difficulty;
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

