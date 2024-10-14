using UnityEngine;
using TMPro;

internal class EpicDisplay : IDisplayWord
{
    TextMeshPro m_TextMeshPro;
    private Color color = Color.black;
    public void Initialize(GameObject gameObject, string word)
    {
        m_TextMeshPro = gameObject.AddComponent<TextMeshPro>();
        m_TextMeshPro.fontSize = GameManager.Parameters.EpicFont;
        m_TextMeshPro.alignment = TextAlignmentOptions.Center;
        m_TextMeshPro.color = color;
        m_TextMeshPro.text = word;
        gameObject.AddComponent<TextWaveAnimation>();
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
        string highlighted = "<color=purple>" + currentSequence + "</color>" + fullWord.Substring(currentSequence.Length);
        m_TextMeshPro.text = highlighted;
    }
}