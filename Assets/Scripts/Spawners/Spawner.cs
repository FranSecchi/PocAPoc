using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public TextAsset Sheet;
    public List<Transform> spawnPoints;
    private List<WordFactory> factories;

    private ISpawn spawner;

    private void Start()
    {
        factories = WordFactoryManager.createFactories(Sheet);
    }
    public void setSpawner(ISpawn spawner)
    { 
        if(spawner != null) DestroyImmediate(this.spawner as Component);
        this.spawner = spawner;
    }
    public void spawn()
    {
        spawner.Spawn(this);
    }
    public WordFactory getFactory(WordDifficulty type)
    {
        foreach(WordFactory factory in factories)
        {
            if(factory.difficulty == type)
            {
                return factory;
            }
        }
        throw new InvalidOperationException($"No factory found for difficulty type: {type}");
    }
}

