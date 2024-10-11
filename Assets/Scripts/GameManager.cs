using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform Goal;
    public GameParameters parameters;
    public WaveManager waveManager;
    public static GameManager Instance;

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
        CheckWave();
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
        CheckWave();
    }




    private void CheckWave()
    {
        if(currentWave < 0)
        {
            ISpawn sp = new FirstWave();
            waveManager.setSpawner(sp);
            currentWave = 0;
        }
        else if(currentWave == 0 && pointsManager.totalPoints > Parameters.PointsFirstWave)
        {
            ISpawn sp = new SecondWave();
            waveManager.setSpawner(sp);
            currentWave = 1;
        }
    }
}
