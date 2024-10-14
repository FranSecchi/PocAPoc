using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstWave : WaveStrategy
{
    private int max;
    public override void Spawn()
    {
        time += Time.deltaTime;
        if (time < timeInterval) return;
        ResetSpawnPoints();
        int r = Random.Range(1, max+1);
        for (int i = 0; i < r; ++i)
            Instantiate();

        time = 0f;
    }

    private void ResetSpawnPoints()
    {
        foreach (var spawner in spawners)
        {
            spawner.availableSpawnPoints = new List<Transform>(spawner.spawnPoints);
        }
    }

    private void Instantiate()
    {
        Spawner spawner = null;
        List<Spawner> spawnersTemp = new List<Spawner>(spawners);
        do
        {
            spawner = spawnersTemp[Random.Range(0, spawnersTemp.Count)];
            spawnersTemp.Remove(spawner);
        } while (spawner.availableSpawnPoints.Count == 0 && spawnersTemp.Count > 0);
        if (spawnersTemp.Count == 0) return;

        Instantiate(spawner, WordDifficulty.EASY, typeof(SimpleWord));
    }


    protected override void Init()
    {
        max = GameManager.Parameters.MaxSpawnFirstWave;
        timeInterval = GameManager.Parameters.FirstSpawnRate;
    }
}
