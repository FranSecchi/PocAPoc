using System.Collections.Generic;
using UnityEngine;

public class SecondWave : WaveStrategy
{
    private int max;
    public override void Spawn()
    {
        if (wordsSpawned > numberWords)
        {
            GameManager.Instance.AddWave(new ThirdWave());
            GameManager.Instance.jumpWave(timeForWave);
            return;
        }
        time += Time.deltaTime;
        if (time < timeInterval) return;

        ResetSpawnPoints();
        int r = Random.Range(1, max + 1);
        for (int i = 0; i < r; ++i)
            InstantiateWord(WordDifficulty.EASY, typeof(EasyWord));
        if (r == 1)
            InstantiateWord(WordDifficulty.HARD, typeof(HardWord));
        ++wordsSpawned;
        time = 0f;
    }
    private void InstantiateWord(WordDifficulty difficulty, System.Type wordType)
    {
        Spawner spawner = RandomSpawner();
        Instantiate(spawner, difficulty, wordType);
    }


    protected override void Init()
    {
        max = GameManager.Parameters.MaxSpawnSecondWave;
        timeInterval = GameManager.Parameters.SecondSpawnRate;
        numberWords = GameManager.Parameters.WordsSecondWave;
        timeForWave = GameManager.Parameters.ThirdWaveWaitTime;
    }
}