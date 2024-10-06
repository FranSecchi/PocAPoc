using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordDisplay : MonoBehaviour
{
    private TextMeshPro m_TextMeshPro;

    public void Initialize(string word, float fontSize = 6f, Color? color = null)
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
}
