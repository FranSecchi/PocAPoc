using UnityEngine;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
internal class StaticDisplay : IDisplayWord
{
    TextMeshPro m_TextMeshPro;
    private Color color = Color.black;
    public void Initialize(GameObject gameObject, string word)
    {
        m_TextMeshPro = gameObject.GetComponent<TextMeshPro>();
        if (m_TextMeshPro == null) m_TextMeshPro = gameObject.AddComponent<TextMeshPro>();
        m_TextMeshPro.text = word;
        UpdateDisplay(gameObject, "", word);
    }

    public void PrintRemove(GameObject gameObject, int points)
    {
        if (points > 0)
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

        int highlightEnd = 0;
        int currentIndex = 0;

        while (currentIndex < currentSequence.Length && highlightEnd < fullWord.Length)
        {
            if (char.IsPunctuation(fullWord[highlightEnd]))
            {
                highlightEnd++;
            }
            highlightEnd++;
            currentIndex++;
        }
        string highlighted = "<color=green>" + fullWord.Substring(0, highlightEnd) + "</color>";
        string normal = fullWord.Substring(highlightEnd);
        if (normal.Length > 0)
        {
            string s = "";
            foreach (char c in normal)
            {
                if (!KeysManager.Contains(char.ToUpper(c)))
                {
                    s += "<color=red>" + c + "</color>";
                }
                else s += c;
            }
            normal = s;
        }

        m_TextMeshPro.text = highlighted + normal;
    }
}