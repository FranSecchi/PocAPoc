using UnityEngine;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;

public class ClassicDisplay : IDisplayWord
{
    TextMeshPro m_TextMeshPro;
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
        if(points > 0)
        {
            GameObject go = new GameObject("pointsDisplay");
            PointsDisplay pd = go.AddComponent<PointsDisplay>();
            pd.Print(points, gameObject.transform.position);
        }
        else
        {
            //GameObject go = new GameObject("fallDisplay");
            //go.transform.position = gameObject.transform.position;
            //TextMeshPro tmp = go.AddComponent<TextMeshPro>();
            //TextMeshPro source = gameObject.GetComponent<TextMeshPro>(); ;
            //tmp.text = source.text; // Copy text
            //tmp.font = source.font; // Copy font
            //tmp.fontSize = source.fontSize; // Copy font size
            //tmp.color = source.color; // Copy color
            //tmp.alignment = source.alignment;
            //go.AddComponent<TextFallAnimation>();
        }
    }


    public void UpdateDisplay(GameObject gameObject, string currentSequence, string fullWord)
    {
        m_TextMeshPro = gameObject.GetComponent<TextMeshPro>();

        string highlighted = "<color=green>" + fullWord.Substring(0, currentSequence.Length) + "</color>"
                           + fullWord.Substring(currentSequence.Length);

        m_TextMeshPro.text = highlighted;
    }
}