using UnityEngine;
using TMPro;

internal class EpicDisplay : IDisplayWord
{
    TextMeshPro m_TextMeshPro;
    private float fontSize = 6;
    private Color color = Color.black;
    private TextAlignmentOptions alignment = TextAlignmentOptions.Center;
    public void Initialize(GameObject gameObject, string word)
    {
        m_TextMeshPro = gameObject.AddComponent<TextMeshPro>();
        m_TextMeshPro.fontSize = fontSize;
        m_TextMeshPro.alignment = alignment;
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
        string highlighted = "<color=purple>" + currentSequence + "</color>" + fullWord.Substring(currentSequence.Length);
        m_TextMeshPro.text = highlighted;
    }
}