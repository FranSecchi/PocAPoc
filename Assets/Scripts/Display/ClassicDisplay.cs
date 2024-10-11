using UnityEngine;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;

public class ClassicDisplay : IDisplayWord
{
    TextMeshPro m_TextMeshPro;
    private float fontSize;
    private Color color = Color.black;
    public void Initialize(GameObject gameObject, string word)
    {
        m_TextMeshPro = gameObject.AddComponent<TextMeshPro>();
        m_TextMeshPro.fontSize = GameManager.Parameters.ClassicFont;
        m_TextMeshPro.alignment = TextAlignmentOptions.Center;
        m_TextMeshPro.color = color;
        m_TextMeshPro.text = word;
    }

    public void PrintRemove(GameObject gameObject, int points)
    {
        GameObject go = new GameObject("pointsDisplay");
        PointsDisplay pd = go.AddComponent<PointsDisplay>();
        pd.Print(points, gameObject.transform.position);
    }


    public void UpdateDisplay(GameObject gameObject, string currentSequence, string fullWord)
    {
        m_TextMeshPro = gameObject.GetComponent<TextMeshPro>();
        string highlighted = "<color=green>" + currentSequence + "</color>" + fullWord.Substring(currentSequence.Length);
        m_TextMeshPro.text = highlighted;
    }
}