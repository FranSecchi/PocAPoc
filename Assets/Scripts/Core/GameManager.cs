using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform Goal;
    public HUD hud;
    public GameParameters parameters;
    public WaveManager waveManager;
    public static GameManager Instance;

    private List<WordStruct> newWords;
    private Dictionary<Spawner,List<Word>> allWords;
    private PointsManager pointsManager;
    private InputHandler inputHandler;
    private int currentWave = -1;
    private int lifes = 3;

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
        newWords = new List<WordStruct>();
        RestartGame();
    }
    public void RestartGame()
    {
        lifes = Parameters.Lifes;
        hud.SetLifes(lifes);
        currentWave = -1;
        waveManager.enabled = true;
        CheckWave();
    }

    private void CheckLifes()
    {
        --lifes;
        hud.SetLifes(lifes);
        if (lifes == 0)
        {
            waveManager.enabled = false;
            RemoveWords();
            hud.SetWords(newWords);
        }
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
        else if (currentWave == 1 && pointsManager.totalPoints > Parameters.PointsSecondWave)
        {
            ISpawn sp = new ThirdWave();
            waveManager.setSpawner(sp);
            currentWave = 1;
        }
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
        if (completed)
        {
            CheckWave();
            pointsManager.sumPoints(word, completed);
            if (!newWords.Any(w => w.Content == word.word.Content))
            {
                newWords.Add(word.word);
            }
        }
        else CheckLifes();
    }
    private void RemoveWords()
    {
        foreach (KeyValuePair<Spawner, List<Word>> entry in allWords)
        {
            Spawner spawner = entry.Key;
            List<Word> words = entry.Value;

            foreach (Word word in words)
            {
                if (word != null) word.Remove();
            }
        }
    }
}
