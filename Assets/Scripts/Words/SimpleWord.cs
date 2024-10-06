using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimpleWord : MonoBehaviour, IWordController
{
    public string word;
    public int points;
    private string seq = "";
    private TextMeshPro m_TextMeshPro;
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
        UpdateWordDisplay();
    }

    private void UpdateWordDisplay()
    {
        string highlighted = "<color=green>" + seq + "</color>" + word.Substring(seq.Length);
        m_TextMeshPro.text = highlighted;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_TextMeshPro = gameObject.AddComponent<TextMeshPro>();
        m_TextMeshPro.fontSize = 6;
        m_TextMeshPro.text = word;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
