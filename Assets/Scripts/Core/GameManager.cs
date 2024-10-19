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

    private Queue<ISpawn> waves = new Queue<ISpawn>();
    private List<WordStruct> newWords;
    private List<WordStruct> frases;
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
        frases = new List<WordStruct>();
        RestartGame();
    }
    public void RestartGame()
    {
        lifes = Parameters.Lifes;
        pointsManager.totalPoints = 0;
        hud.SetLifes(lifes);
        currentWave = Parameters.startWave;
        waveManager.enabled = true;
        waves = new Queue<ISpawn>();
        waves.Enqueue(new FirstWave());
        StartNextWave(Parameters.FirstWaveWaitTime);
    }

    private void CheckLifes()
    {
        --lifes;
        hud.SetLifes(lifes);
        if (lifes == 0)
        {
            Finish();
        }
    }

    private void Finish()
    {
        waveManager.enabled = false;
        RemoveWords();
        hud.SetWords(newWords);
        hud.SetFrases(frases);
        hud.OpenMenu();
    }

    public void AddWave(ISpawn wave)
    {
        waves.Enqueue(wave);
    }

    private void StartNextWave(float waitTime)
    {
        ISpawn newWave = waves.Dequeue();
        waveManager.setSpawner(newWave);
        StartCoroutine(WaitForWave(waitTime));
    }

    private IEnumerator WaitForWave(float sec)
    {
        waveManager.enabled = false;
        yield return new WaitForSeconds(sec);
        waveManager.enabled = true;
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
            pointsManager.sumPoints(word, completed);
            if (!newWords.Any(w => w.Content == word.word.Content) && word.word.Type != WordType.FRASE)
            {
                newWords.Add(word.word);
            }
        }
        else CheckLifes();
    }
    public void jumpWave(float time)
    {
        StartNextWave(time);
    }
    public void addPhrase(WordStruct word)
    {
        if (!frases.Any(w => w.Content == word.Content))
        {
            frases.Add(word);
        }
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
