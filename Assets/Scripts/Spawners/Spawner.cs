using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public TextAsset Sheet;
    public List<Transform> spawnPoints;
    private List<WordFactory> factories;


    private void Start()
    {
        factories = WordFactoryManager.createFactories(Sheet);
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

