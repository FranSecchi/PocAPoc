using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform Goal;
    public GameParameters parameters;
    public static GameManager Instance;

    [SerializeField] private List<GameObject> spawners;

    private Dictionary<Spawner,List<Word>> allWords;
    private PointsManager pointsManager;
    private InputHandler inputHandler;
    private int currentWave = -1;

    public static GameParameters Parameters { get => Instance.parameters; }
    public InputHandler InputHandler { get => inputHandler;}

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Goal.position, parameters.GoalRadius);
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {
        if(inputHandler == null)
            inputHandler = gameObject.AddComponent<InputHandler>();
        if (allWords == null)
            allWords = new Dictionary<Spawner, List<Word>>();
        if (pointsManager == null)
            pointsManager = new PointsManager();
    }
    private void Update()
    {
        CheckWave();
        Step();
    }
    public void addWord(Word word)
    {
        if (!allWords.ContainsKey(word.spawner))
        {
            allWords[word.spawner] = new List<Word>();
        }
        allWords[word.spawner].Add(word);
    }

    public void removeWord(Word word, bool completed)
    {
        allWords[word.spawner].Remove(word);
        pointsManager.sumPoints(word, completed);
    }



    private void Step()
    {
        //Spawner spawner = spawners[Random.Range(0, spawners.Count)];
        foreach (GameObject go in spawners)
        {
            Spawner spawner = go.GetComponent<Spawner>();
            spawner.spawn();
        }
    }

    private void CheckWave()
    {
        if(pointsManager.totalPoints > 10 && currentWave == 0)
        {
            foreach (GameObject go in spawners)
            {
                ISpawn sp = new SecondWave();
                Spawner spawner = go.GetComponent<Spawner>();
                spawner.setSpawner(sp);
            }
            currentWave = 1;
        }
        else if(currentWave < 0)
        {
            foreach (GameObject go in spawners)
            {
                ISpawn sp = new FirstWave();
                Spawner spawner = go.GetComponent<Spawner>();
                spawner.setSpawner(sp);
            }
            currentWave = 0;
        }
    }
}
