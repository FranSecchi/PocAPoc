using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WordFactory : MonoBehaviour
{
    [SerializeField] private TextAsset sheet;
    protected List<string> wordList = new List<string>();
    public abstract GameObject getWordObject();
    private void Awake()
    {
        ReadSheet();
    }

    void ReadSheet()
    {
        string[] texts = sheet.text.Split(new char[] { ';' });
        foreach (string word in texts)
        {
            wordList.Add(word.Trim());
        }
    }
    public List<string> GetWordList()
    {
        return wordList;
    }
}
