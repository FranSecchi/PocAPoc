using UnityEngine;
using TMPro;
using System;
using UnityEngine.EventSystems;
using System.Collections;

internal class ComboDisplay : MonoBehaviour
{
    public float duration;
    TextMeshPro m_TextMeshPro;

    internal void Print(int points)
    {
        m_TextMeshPro.text = "+"+points.ToString();
        StartCoroutine(Disappear());
    }

    void Awake()
    {
        m_TextMeshPro = gameObject.GetComponent<TextMeshPro>();
    }
    private IEnumerator Disappear()
    {
        float elapsedTime = 0f;
        Color startColor = m_TextMeshPro.color;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            Color currentColor = startColor;
            currentColor.a = Mathf.Lerp(1f, 0f, t);
            m_TextMeshPro.color = currentColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Destroy the GameObject after the animation
        Destroy(gameObject);
    }
}