using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;
public abstract class Word : MonoBehaviour
{
    public GameManager gameManager;
    public InputHandler subject;
    public Spawner spawner;

    public  WordStruct word;
    protected string normalizedWord;
    protected IDisplayWord display;
    protected Transform goal;
    protected int points;
    protected float speed;
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
        subject = gameManager.InputHandler;
        goal = gameManager.Goal;
    }
    // Start is called before the first frame update
    void Start()
    {
        display = DisplayStrategyFactory.GetDisplay(word.Type);
        gameManager.addWord(this);
        normalizedWord = RemoveAccents(word.Content);
        Init();
    }
    protected abstract void Init();
    public abstract void OnCharPressed(char key);
    // Update is called once per frame
    void Update()
    {
        Step();
    }

    protected void Remove(bool completed)
    {
        gameManager.removeWord(this, completed);
        display.PrintRemove(gameObject, completed?points:0);
        Destroy(gameObject);
    }
    private string RemoveAccents(string text)
    {
        return string.Concat(
            text.Normalize(NormalizationForm.FormD)
                .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
        ).Normalize(NormalizationForm.FormC);
    }
}
