using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstWave : WaveStrategy, ISpawn
{
    public override void Spawn()
    {
        time += Time.deltaTime;
        if (time < timeInterval) return;

        Spawner spawner = spawners[Random.Range(0, spawners.Count)];
        WordFactory factory = spawner.getFactory(WordDifficulty.EASY);
        WordStruct wordCont = factory.getWord();

        GameObject go = new GameObject();
        Word word = go.AddComponent<SimpleWord>();

        word.word = wordCont;
        word.spawner = spawner;

        go.transform.position = spawner.spawnPoints[Random.Range(0, spawner.spawnPoints.Count)].position; //Temporal

        time = 0f;
    }

    protected override void Init()
    {
        timeInterval = GameManager.Parameters.FirstSpawnRate;
    }
}
