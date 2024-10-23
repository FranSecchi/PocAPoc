using System.Collections.Generic;
using UnityEngine;

public class MediumWave : WaveStrategy
{
    private float comboprob;
    private float easyprob;
    private float hardprob;
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
        Combo();
        time = 0f;
    }
    private void Combo()
    {
        float r = Random.Range(0f, 1f);
        if (r < comboprob)
        {
            Instantiate();
            Instantiate();
            ++wordsSpawned;
        }
        else
            Instantiate();
        ++wordsSpawned;
    }
    private void Instantiate()
    {
        Spawner spawner = RandomSpawner();
        float rand = Random.Range(0f, 1f);
        WordDifficulty wordDifficulty;
        if (rand < easyprob) { wordDifficulty = WordDifficulty.EASY; }
        else if(rand < easyprob + hardprob) { wordDifficulty = WordDifficulty.HARD; }
        else wordDifficulty = WordDifficulty.MEDIUM;
        Instantiate(spawner, wordDifficulty, typeof(EasyWord));
    }

    protected override void Init()
    {
        GameParameters param = GameManager.Parameters;
        timeInterval = param.MediumSpawnRate;
        numberWords = param.WordsSecondWave;
        timeForWave = param.ThirdWaveWaitTime;
        comboprob = param.Medium_ComboProbability;
    }
}