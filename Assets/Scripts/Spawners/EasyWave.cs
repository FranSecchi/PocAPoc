using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyWave : WaveStrategy
{
    private int bursts;
    private int max;
    private float cooldown;
    private float prob;
    private float t = 0f;
    public override void Spawn()
    {
        if(bursts > numberWords)
        {
            GameManager.Instance.AddWave(new MediumWave());
            GameManager.Instance.jumpWave(timeForWave);
            return;
        }
        time += Time.deltaTime;
        if (time < cooldown) return;
        t += Time.deltaTime;
        if (t < timeInterval) return;
        if(wordsSpawned < max)
        {
            t = 0f;
            Instantiate();
            ++wordsSpawned;
        }
        else
        {
            ResetSpawnPoints();
            ++bursts;
            wordsSpawned = 0;
            time = 0f;
        }
    }

    private void Instantiate()
    {
        Spawner spawner = RandomSpawner();
        float rand = Random.Range(0f, 1f);
        WordDifficulty wordDifficulty;
        if (rand > prob) { wordDifficulty = WordDifficulty.EASY; }
        else { wordDifficulty = WordDifficulty.MEDIUM; }
        Instantiate(spawner, wordDifficulty, typeof(EasyWord));
    }


    protected override void Init()
    {
        GameParameters param = GameManager.Parameters;
        cooldown = param.BurstCooldown;
        max = param.WordsPerBurst;
        timeInterval = param.EasySpawnRate;
        numberWords = param.ManyBursts;
        timeForWave = param.MediumWaitTime;
        prob = param.Easy_MediumProbability;
        bursts = 0;
        wordsSpawned = 0;
    }
}
