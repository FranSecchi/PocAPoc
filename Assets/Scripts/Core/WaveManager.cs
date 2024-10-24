using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<Spawner> spawners;
    private GameManager gameManager;
    private GameParameters param;
    private ISpawn spawner;
    private int waves;
    private int paramIndex = 0;
    private float easyWaveProbability;
    private float mediumWaveProbability;
    private float hardWaveProbability;
    private float phraseWaveProbability;
    private void Awake()
    {
        
        gameManager = GameManager.Instance; 
        param = GameManager.Parameter;
    }
    private void Start()
    {
        SetProbs();
        waves = -4;
    }

    private void SetProbs()
    {
        easyWaveProbability = param.EasyProb;
        mediumWaveProbability = param.MediumProb;
        hardWaveProbability = param.HardProb;
        phraseWaveProbability = param.PhraseProb;
    }

    private void Update()
    {
        spawn();
        if (Input.GetKeyDown(KeyCode.Space)) spawner.JumpWave();
    }
    public void setSpawner(ISpawn spawner)
    {
        waves++;
        if (waves > 0 && waves % param.WavesMultiple == 0)
        {
            ++paramIndex;
            if(paramIndex < gameManager.parameters.Count)
            {
                gameManager.IncrementParam(paramIndex);
                param = GameManager.Parameter;
                SetProbs();
            }
            else
            {
                param.Increment();
            }
            Debug.Log(param.EasySpeed);
        }
        if (spawner != null) DestroyImmediate(this.spawner as Component);
        this.spawner = spawner;
        Debug.Log(spawner.GetType().ToString());
        spawner.Set(spawners);
    }
    public void spawn()
    {
        spawner.Spawn();
    }
    public void AddWave()
    {
        float rand = Random.Range(0f, 1f);

        if (rand < easyWaveProbability)
        {
            gameManager.AddWave(new EasyWave());
        }
        else if (rand < easyWaveProbability + mediumWaveProbability)
        {
            gameManager.AddWave(new MediumWave());
        }
        else if (rand < easyWaveProbability + mediumWaveProbability + hardWaveProbability)
        {
            gameManager.AddWave(new HardWave());
        }
        else
        {
            gameManager.AddWave(new PhraseWave());
        }
    }
}