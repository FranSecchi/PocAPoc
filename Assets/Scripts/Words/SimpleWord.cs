using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWord : MonoBehaviour, IWordController
{
    public string word;
    public int points;
    private string seq = "";
    private WordDisplay display;
    public void HandleInput(char key)
    {
        seq += key;
        //Add check
        if (!word.StartsWith(seq))
        {
            seq = "";
        }
        if (seq == word)
        {
            GameManager.RemoveWord(this);
            Destroy(gameObject);
        }
        display.UpdateDisplay(seq, word);
    }

    // Start is called before the first frame update
    void Start()
    {
        display = gameObject.AddComponent<WordDisplay>();
        display.Initialize(word);

    }

}
