using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstWave : WaveStrategy, ISpawn
{
    private int max;
    private List<Transform> availableSpawnPoints;
    public override void Spawn()
    {
        time += Time.deltaTime;
        if (time < timeInterval) return;
        ResetSpawnPoints();
        int r = Random.Range(1, max+1);
        for (int i = 0; i < r && availableSpawnPoints.Count > 0; ++i)
            Instantiate();

        time = 0f;
    }

    private void ResetSpawnPoints()
    {
        availableSpawnPoints = new List<Transform>();
        foreach (var spawner in spawners)
        {
            availableSpawnPoints.AddRange(spawner.spawnPoints);
        }
    }

    private void Instantiate()
    {
        Spawner spawner = spawners[Random.Range(0, spawners.Count)];
        WordFactory factory = spawner.getFactory(WordDifficulty.EASY);
        WordStruct wordCont = factory.getWord();

        GameObject go = new GameObject();
        Word word = go.AddComponent<SimpleWord>();

        word.word = wordCont;
        word.spawner = spawner;

        int spawnIndex = Random.Range(0, availableSpawnPoints.Count);
        Transform spawnPoint = availableSpawnPoints[spawnIndex];
        availableSpawnPoints.RemoveAt(spawnIndex);

        go.transform.position = spawnPoint.position;
    }

    protected override void Init()
    {
        max = GameManager.Parameters.MaxSpawnFirstWave;
        timeInterval = GameManager.Parameters.FirstSpawnRate;
    }
}
