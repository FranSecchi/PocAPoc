using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class MediumWave : WaveStrategy
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
            Instantiate(spawner, WordDifficulty.EASY);
        }
        else if(rand < easyprob + hardprob) 
        { 
            Instantiate(spawner, WordDifficulty.HARD);
        }
        else
        {
            Instantiate(spawner, WordDifficulty.MEDIUM);
        }
    }

    protected override void Init()
    {
        GameParameters param = GameManager.Parameter;
        max = param.Medium_WordsPerBurst;
        timeInterval = param.MediumSpawnRate;
        cooldown = param.Medium_BurstCooldown + max * timeInterval;
        numberWords = param.Medium_ManyBursts;
        timeForWave = param.MediumWaitTime;
        comboprob = param.Medium_ComboProbability;
    }

    public override void JumpWave()
    {
        GameManager.Instance.jumpWave(timeForWave);
    }
}