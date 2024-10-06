using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private List<Spawner> spawners;

    private List<IWordController> allWords;
    private InputHandler inputHandler;
    private float time = 0;
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
        foreach (Spawner spawner in spawners)
        {
            ISpawn ispawn = gameObject.AddComponent<FirstWave>();
            spawner.setSpawner(ispawn);//Temporal
        }
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time > 5f) 
            spawnNewWord();
    }
    public static void RemoveWord(IWordController word)
    {
        Instance.allWords.Remove(word);
        Instance.inputHandler.unsubscribe(word);
    }
    private void spawnNewWord()
    {
        Spawner spawner = spawners[Random.Range(0, spawners.Count)];
        //foreach(Spawner spawner in spawners)
        //{
            IWordController word = spawner.spawn();
            if (word != null)
            {
                allWords.Add(word);
                inputHandler.subscribe(word);
            }
        //}
        Debug.Log(allWords.Count);
        time = 0f;
    }
}
