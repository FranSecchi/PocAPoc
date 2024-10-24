using System.Collections.Generic;
using UnityEngine;

public class HardWave : WaveStrategy
{
    private float comboprob;
    private float easyprob;
    private float hardprob;
    private int bursts;
    private int max;
    private float cooldown;
    private float t = 0f;
    public override void Spawn()
    {
        if (bursts > numberWords)
        {
            JumpWave();
            return;
        }
        time += Time.deltaTime;
        if (time < cooldown) return;
        t += Time.deltaTime;
        if (t < timeInterval) return;
        if (wordsSpawned < max)
        {
            t = 0f;
            Combo();
        }
        else
        {
            ResetSpawnPoints();
            ++bursts;
            wordsSpawned = 0;
            time = 0f;
        }
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
        if (rand < easyprob)
        {
            Instantiate(spawner, WordDifficulty.EASY, typeof(EasyWord));
        }
        else if (rand < easyprob + hardprob)
        {
            Instantiate(spawner, WordDifficulty.HARD, typeof(HardWord));
        }
        else
        {
            Instantiate(spawner, WordDifficulty.MEDIUM, typeof(MediumWord));
        }
    }

    protected override void Init()
    {
        GameParameters param = GameManager.Parameter;
        cooldown = param.Medium_BurstCooldown;
        max = param.Medium_WordsPerBurst;
        timeInterval = param.HardSpawnRate;
        numberWords = param.Hard_ManyBursts;
        timeForWave = param.HardWaitTime;
        comboprob = param.Hard_ComboProbability;
    }

    public override void JumpWave()
    {
        GameManager.Instance.jumpWave(timeForWave);
    }
}