using TMPro;
using UnityEngine;

public class SmokeDisplay : IDisplayWord
{
    private TextMeshPro m_TextMeshPro;

    public void Initialize(GameObject gameObject, string word)
    {
        m_TextMeshPro = gameObject.AddComponent<TextMeshPro>();
        m_TextMeshPro.font = GameManager.Parameter.ClassicFont;
        m_TextMeshPro.fontSize = GameManager.Parameter.EpicFont;
        m_TextMeshPro.alignment = TextAlignmentOptions.Center;
        m_TextMeshPro.color = Color.black;
        m_TextMeshPro.text = word;
        UpdateDisplay(gameObject, "", word);
        gameObject.AddComponent<TextWaveAnimation>().fade = true;
    }

    public void PrintRemove(GameObject gameObject, int points)
    {
        if (points > 0)
        {
            GameObject go = new GameObject("pointsDisplay");
            PointsDisplay pd = go.AddComponent<PointsDisplay>();
            pd.Print(points, gameObject.transform.position);
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
