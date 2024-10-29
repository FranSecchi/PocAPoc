using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;
public abstract class Word : MonoBehaviour
{
    //protected static bool inserted;
    public GameManager gameManager;
    public GameParameters parameter;
    public InputHandler subject;
    public Spawner spawner;

    public  WordStruct word;
    public WordDifficulty difficulty;
    protected string normalizedWord;
    protected IDisplayWord display;
    protected Vector2 goal;
    protected int points;
    protected float speed;
    protected string seq = "";
    protected abstract void Step();

    #region Enablers
    private void OnEnable()
    {
        subject.charPressed += OnCharPressed;
    }
    private void OnDisable()
    {
        subject.charPressed -= OnCharPressed;
    }
    private void OnDestroy()
    {
        subject.charPressed -= OnCharPressed;
    }
    #endregion

    private void Awake()
    {
        gameManager = GameManager.Instance;
        parameter = GameManager.Parameter;
        subject = gameManager.InputHandler;
        goal = gameManager.Goal.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
        gameManager.addWord(this);
        display = DisplayStrategyFactory.GetDisplay(word.Type);
        normalizedWord = NormalizeWord(word.Content.ToLower());
        display.Initialize(gameObject, word.Content);
        points = normalizedWord.Length;
    }
    protected virtual void Init()
    {
        switch (difficulty)
        {
            case WordDifficulty.EASY:
                speed = parameter.EasySpeed;
                break;
            case WordDifficulty.HARD:
                speed = parameter.HardSpeed;
                break;
            case WordDifficulty.MEDIUM:
                speed = parameter.MediumSpeed;
                break;
            case WordDifficulty.BOSS:
                speed = parameter.BossSpeed;
                break;
            default:
                speed = parameter.SimpleSpeed;
                break;
        }
    }
    public virtual void OnCharPressed(char key)
    {
        seq += key;
        if (seq == normalizedWord)
        {
            Remove(true);
        }
        else if (!normalizedWord.StartsWith(seq))
        {
            seq = "";
        }
        else gameManager.AnyWordMatched();
        display.UpdateDisplay(gameObject, seq, word.Content);
    }
    // Update is called once per frame
    void Update()
    {
        Step();
    }
    public void Remove()
    {
        gameManager.CheckLifes(word.Content.Length);
        display.PrintRemove(gameObject, 0);
        Destroy(gameObject);
    }
    protected void Remove(bool completed)
    {
        gameManager.removeWord(this, completed);
        display.PrintRemove(gameObject, completed?points:0);
        Destroy(gameObject);
    }
    private string NormalizeWord(string text)
    {
        // Decompose the text into base characters and diacritics
        var normalizedText = text.Normalize(NormalizationForm.FormD);

        // Remove diacritics except for 'ç'
        var result = string.Concat(
            normalizedText.Where(
                ch => CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark &&
                             char.IsLetterOrDigit(ch) || ch == '̧'
            )
        );

        // Replace decomposed 'ç' with its composed form and recompose the string
        return result.Replace("ç", "ç").Normalize(NormalizationForm.FormC);
    }
}
