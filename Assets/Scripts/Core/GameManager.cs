using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform Goal;
    public HUD hud;
    public Animator anim;
    public List<GameParameters> parameters;
    public WaveManager waveManager;
    public AudioManager audioManager;
    public GameObject hpBar;
    public static GameManager Instance;

    private Queue<ISpawn> waves = new Queue<ISpawn>();
    private List<WordStruct> newWords;
    private List<WordStruct> frases;
    private Dictionary<Spawner,List<Word>> allWords;
    private PointsManager pointsManager;
    private InputHandler inputHandler;
    private int lastFrameCalled = -1;
    private GameParameters parameter;

    private float maxhp;
    private float hp;
    //private int lifes = 3;
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
        waveManager.Wait(true);
        waves = new Queue<ISpawn>();
        maxhp = parameter.maxHp;
        hp = maxhp;
    }
    public void AnyWordMatched()
    {
        anyWordMatched = true;
    }
    public void FirstStart()
    {
        hpBar.SetActive(true);
        waves.Enqueue(new TutoWave());
        StartGame();
    }
    public void PauseGame()
    {
        if (hp <= 0f || !started) return;
        hpBar.SetActive(!paused);
        paused = !paused;
        Time.timeScale = paused ? 0f : 1f;
        audioManager.Pause(paused);
        hud.SetPoints(pointsManager.totalPoints);
        hud.SetFrases(frases);
        hud.SetWords(newWords);
        hud.Pause(paused);
    }
    private void StartGame()
    {
        audioManager.Play(true);
        started = true;
        waves.Enqueue(new EasyWave());
        RemoveWords();
        hp = maxhp;
        hud.UpdateHp(hp, maxhp);
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

    public void CheckLifes(int length)
    {
        //--lifes;
        //hud.SetLifes(lifes);
        if(hp <= 0f) return;
        float i = hp - length;
        hp = MathF.Max(i, 0f);
        hud.UpdateHp(hp, maxhp);
        if (hp <= 0)
        {
            Finish();
        }
    }

    private void Finish()
    {
        if(waveCoro != null) StopCoroutine(waveCoro);
        waveManager.Wait(true);
        hpBar.SetActive(false);
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
        audioManager.Play(false);
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
        waveManager.Wait(true);
        yield return new WaitForSeconds(sec);
        waveManager.Wait(false);
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
            anim.SetTrigger("write");
            float i = hp + parameter.gainHpFactor;
            hp = Mathf.Min(maxhp, i);
            hud.UpdateHp(hp, maxhp);
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
        else CheckLifes(word.word.Content.Length);
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
                word.Remove();
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
            hp -= parameter.loseHpFactor;
            pointsManager.BreakCombo();
        }
        anyWordMatched = false;
    }
}
