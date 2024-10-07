using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Word : MonoBehaviour, WordDisplay, IWordController
{
    private TextMeshPro m_TextMeshPro;

    public string word;
    public int points;
    protected string seq = "";
    protected abstract void Step();
    public virtual void Initialize(string word, float fontSize = 6f, Color? color = null)
    {
        m_TextMeshPro = gameObject.AddComponent<TextMeshPro>();
        m_TextMeshPro.fontSize = fontSize;
        m_TextMeshPro.alignment = TextAlignmentOptions.Center;
        m_TextMeshPro.color = color ?? Color.black;
        m_TextMeshPro.text = word;
    }

    public void UpdateDisplay(string currentSequence, string fullWord)
    {
        string highlighted = "<color=green>" + currentSequence + "</color>" + fullWord.Substring(currentSequence.Length);
        m_TextMeshPro.text = highlighted;
    }
    protected void Remove(bool completed)
    {
        GameManager.RemoveWord(this);
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Initialize(word);
    }

    // Update is called once per frame
    void Update()
    {
        Step();
    }

    public abstract void HandleInput(char key);
}
