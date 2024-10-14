using System.Collections.Generic;
using UnityEngine;

public class SecondWave : WaveStrategy
{
    private int max;
    public override void Spawn()
    {
        time += Time.deltaTime;
        if (time < timeInterval) return;

        ResetSpawnPoints();
        int r = Random.Range(1, max + 1);
        for (int i = 0; i < r; ++i)
            InstantiateWord(WordDifficulty.EASY, typeof(SimpleWord));
        if (r == 1)
            InstantiateWord(WordDifficulty.HARD, typeof(HardWord));

        time = 0f;
    }
    private void InstantiateWord(WordDifficulty difficulty, System.Type wordType)
    {

        Spawner spawner = null;
        List<Spawner> spawnersTemp = new List<Spawner>(spawners);
        do
        {
            spawner = spawnersTemp[Random.Range(0, spawnersTemp.Count)];
            spawnersTemp.Remove(spawner);
        } while (spawner.availableSpawnPoints.Count == 0 && spawnersTemp.Count > 0);
        if (spawnersTemp.Count == 0) return;
        Instantiate(spawner, difficulty, wordType);
    }


    private void ResetSpawnPoints()
    {
        foreach (var spawner in spawners)
        {
            spawner.availableSpawnPoints = new List<Transform>(spawner.spawnPoints);
        }
    }
    protected override void Init()
    {
        max = GameManager.Parameters.MaxSpawnSecondWave;
        timeInterval = GameManager.Parameters.SecondSpawnRate;
    }
}