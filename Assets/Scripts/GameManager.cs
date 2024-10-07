using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private List<GameObject> spawners;

    private List<IWordController> allWords;
    private InputHandler inputHandler;
    private int currentWave = -1;
    private int totalPoints = 0;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    private void Start()
    {
        if(inputHandler == null)
            inputHandler = gameObject.AddComponent<InputHandler>();
        if (allWords == null)
            allWords = new List<IWordController>();
    }
    private void Update()
    {
        CheckWave();
        Step();
    }
    private void removeWord(IWordController word)
    {
        allWords.Remove(word);
        inputHandler.unsubscribe(word);
    }
    public static void RemoveWord(IWordController word)
    {
        Instance.removeWord(word);
    }
    private void Step()
    {
        //Spawner spawner = spawners[Random.Range(0, spawners.Count)];
        foreach (GameObject go in spawners)
        {
            Spawner spawner = go.GetComponent<Spawner>();
            IWordController word = spawner.spawn();
            if (word != null)
            {
                allWords.Add(word);
                inputHandler.subscribe(word);
            }
        }
    }

    private void CheckWave()
    {
        if(totalPoints > 10)
        {
            currentWave = 1;
        }
        else if(currentWave != 0)
        {
            foreach (GameObject go in spawners)
            {
                ISpawn sp = go.AddComponent<FirstWave>();
                Spawner spawner = go.GetComponent<Spawner>();
                spawner.setSpawner(sp);
            }
            currentWave = 0;
        }
    }
}
