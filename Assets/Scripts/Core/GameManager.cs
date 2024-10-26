using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform Goal;
    public HUD hud;
    public List<GameParameters> parameters;
    public WaveManager waveManager;
    public static GameManager Instance;

    private Queue<ISpawn> waves = new Queue<ISpawn>();
    private List<WordStruct> newWords;
    private List<WordStruct> frases;
    private Dictionary<Spawner,List<Word>> allWords;
    private PointsManager pointsManager;
    private InputHandler inputHandler;
    private int lastFrameCalled = -1;
    private GameParameters parameter;

    private int lifes = 3;
    private bool paused = false;
    private Coroutine waveCoro = null;
    private bool started = false;
    private bool anyWordMatched = false;
    private bool chain = false;
    private bool addedWord = false;
    public static GameParameters Parameter { get => Instance.parameter; }
    public InputHandler InputHandler { get => inputHandler;}
    public List<WordStruct> NewWords { get => newWords; }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Goal.position, parameter.GoalRadius);
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        if(inputHandler == null)
            inputHandler = gameObject.AddComponent<InputHandler>();
        parameter = parameters[0];
    }
    private void Start()
    {
        if (allWords == null)
            allWords = new Dictionary<Spawner, List<Word>>();
        if (pointsManager == null)
            pointsManager = GetComponent<PointsManager>();
        newWords = new List<WordStruct>();
        frases = new List<WordStruct>();
        inputHandler.escapePressed += PauseGame;
        waveManager.enabled = false;
        waves = new Queue<ISpawn>();
    }
    public void AnyWordMatched()
    {
        anyWordMatched = true;
    }
    public void FirstStart()
    {
        waves.Enqueue(new TutoWave());
        StartGame();
    }
    public void PauseGame()
    {
        if (lifes == 0 || !started) return;
        Time.timeScale = paused ? 1f : 0f;
        paused = !paused;
        hud.SetPoints(pointsManager.totalPoints);
        hud.SetFrases(frases);
        hud.SetWords(newWords);
        hud.Pause(paused);
    }
    private void StartGame()
    {
        started = true;
        waves.Enqueue(new EasyWave());
        waves.Enqueue(new MediumWave());
        waves.Enqueue(new PhraseWave());
        waves.Enqueue(new HardWave());
        RemoveWords();
        lifes = Parameter.Lifes;
        hud.SetLifes(lifes);
        pointsManager.totalPoints = 0;
        StartNextWave(Parameter.EasyWaitTime);
    }
    public void RestartGame()
    {
        if (paused) PauseGame();
        parameter = parameters[0];
        waveManager.SetProbs();
        waves.Enqueue(new EasyWave());
        StartGame();
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
        if(waveCoro != null) StopCoroutine(waveCoro);
        waveManager.enabled = false;
        chain = false;
        pointsManager.BreakCombo();
        RemoveWords();
        if (pointsManager.totalPoints > pointsManager.record)
        {
            pointsManager.record = pointsManager.totalPoints;
            hud.DisplayNewRecord(pointsManager.record);
        }
        hud.SetPoints(pointsManager.totalPoints);
        hud.SetWords(newWords);
        hud.SetFrases(frases);
        hud.OpenMenu();
        waves = new Queue<ISpawn>();
        parameter.ResetIncrement();
    }

    public void AddWave(ISpawn wave)
    {
        waves.Enqueue(wave);
    }

    private void StartNextWave(float waitTime)
    {
        ISpawn newWave = waves.Dequeue();
        waveManager.setSpawner(newWave);
        waveCoro = StartCoroutine(WaitForWave(waitTime));
    }

    private IEnumerator WaitForWave(float sec)
    {
        waveManager.enabled = false;
        yield return new WaitForSeconds(sec);
        waveManager.enabled = true;
        waveCoro = null;
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
            if (!newWords.Any(w => w.Content == word.word.Content) && word.word.Type != WordType.FRASE)
            {
                newWords.Add(word.word);
                pointsManager.displayWord(word.word.Content);
            }
            if (chain)
            {
                chain = false;
                pointsManager.Chain();
            }
            if (!CheckDouble(word))
            {
                pointsManager.Combo();
                pointsManager.sumPoints(word, completed);
            }
            lastFrameCalled = Time.frameCount;
        }
        else CheckLifes();
    }

    private bool CheckDouble(Word word)
    {
        if (Time.frameCount == lastFrameCalled)
        {
            pointsManager.Double(word);
            return true;
        }
        return false;
    }

    public void jumpWave(float time)
    {
        waveManager.AddWave();
        StartNextWave(time);
    }
    public void addPhrase(WordStruct word)
    {
        if (!frases.Any(w => w.Content == word.Content))
        {
            frases.Add(word);
        }
    }
    public void RemoveWords()
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
    public void IncrementParam(int i)
    {
        parameter = parameters[i];
    }
    public void CheckCombos()
    {
        if (anyWordMatched && Time.frameCount == lastFrameCalled)
        {
            chain = true;
        }
        else if (!anyWordMatched && Time.frameCount != lastFrameCalled)
        {
            chain = false;
            pointsManager.BreakCombo();
        }
        anyWordMatched = false;
    }
}
