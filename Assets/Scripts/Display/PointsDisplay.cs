using UnityEngine;
using TMPro;
using System;
using System.Collections;
public class PointsDisplay : MonoBehaviour
{
    TextMeshPro m_TextMeshPro;
    private float fontSize;
    private Color color = Color.red;

    private float duration = 1.0f;
    private Vector2 moveDirection = Vector2.up;

    void Awake()
    {
        m_TextMeshPro = gameObject.AddComponent<TextMeshPro>();
        m_TextMeshPro.font = GameManager.Parameter.ClassicFont;
        m_TextMeshPro.fontSize = GameManager.Parameter.PointsFont;
        m_TextMeshPro.color = color;
        m_TextMeshPro.alignment = TextAlignmentOptions.Center;
    }
    public void Print(int points, Vector2 position)
    {
        m_TextMeshPro.text = points.ToString();
        m_TextMeshPro.transform.position = new Vector3(position.x, position.y, 0);
        StartCoroutine(MoveAndShrink());
    }
    private IEnumerator MoveAndShrink()
    {
        float elapsedTime = 0f;
        Vector2 startPosition = m_TextMeshPro.transform.position;
        float startSize = m_TextMeshPro.fontSize;
        Color color = m_TextMeshPro.color;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            m_TextMeshPro.transform.position = startPosition + (moveDirection * t/2f); 

            m_TextMeshPro.fontSize = Mathf.Lerp(startSize, startSize/2, t);
            if (t > 0.5f)
            {
                //implement here
                color.a = Mathf.Lerp(1, 0, (t - 0.5f) / 0.5f);
                m_TextMeshPro.color = color;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Destroy the GameObject after the animation
        Destroy(gameObject);
    }
}