using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstWave : WaveStrategy
{
    private int max;
    public override void Spawn()
    {
        if(wordsSpawned > numberWords)
        {
            GameManager.Instance.AddWave(new SecondWave());
            GameManager.Instance.jumpWave(timeForWave);
            return;
        }
        time += Time.deltaTime;
        if (time < timeInterval) return;
        ResetSpawnPoints();
        int r = Random.Range(1, max+1);
        for (int i = 0; i < r; ++i)
            Instantiate();
        ++wordsSpawned;
        time = 0f;
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

        Instantiate(spawner, WordDifficulty.EASY, typeof(EasyWord));
    }


    protected override void Init()
    {
        GameParameters param = GameManager.Parameters;
        max = param.MaxSpawnFirstWave;
        timeInterval = param.FirstSpawnRate;
        numberWords = param.WordsFirstWave;
        timeForWave = param.SecondWaveWaitTime;
    }
}
