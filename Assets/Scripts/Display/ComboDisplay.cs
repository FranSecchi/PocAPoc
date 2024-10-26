using UnityEngine;
using TMPro;
using System;
using UnityEngine.EventSystems;
using System.Collections;
using Unity.VisualScripting;

public class ComboDisplay : MonoBehaviour
{
    public float duration;
    public float fadeUpDistance = 30f;
    public GameObject doubleP;
    public GameObject comboP;
    public GameObject chainP;
    public GameObject comboLost;
    public GameObject pointsP;
    public TextMeshPro chainText;
    public TextMeshPro doubleText;
    public TextMeshPro comboText;
    public TextMeshPro pointsText;
    private Vector3 comboPosition;
    private Vector3 doublePosition;
    private Vector3 pointsPosition;
    private void Awake()
    {
        chainText.text = "";
        comboText.text = "";
        pointsText.text = "";
        doubleText.text = "";
        comboPosition = comboText.transform.position;
        doublePosition = doubleText.transform.position;
        pointsPosition = pointsText.transform.position;
    }
    internal void PrintCombo(int combo)
    {
        GameObject go = Instantiate(comboP, transform.parent);
        go.GetComponent<TextMeshPro>().text = "Ratxa! x" + combo.ToString();
        StartCoroutine(FadeOutText(go));
    }
    internal void PrintChain(int chainMultiplier)
    {
        GameObject go = Instantiate(chainP, transform.parent);
        go.GetComponent<TextMeshPro>().text = "Cadena! x" + chainMultiplier.ToString();
        StartCoroutine(FadeOutText(go));
    }
    internal void PrintLostCombo()
    {
        GameObject go = Instantiate(comboLost, transform.parent);
        StartCoroutine(FadeOutText(go));
    }
    internal void PrintDouble(int points)
    {
        GameObject go = Instantiate(doubleP, transform.parent);
        go.GetComponent<TextMeshPro>().text = "Doble! +"+points.ToString();
        StartCoroutine(FadeOutText(go));
    }

    internal void PrintPoints(int totalPoints)
    {
        GameObject go = Instantiate(pointsP, transform.parent);
        go.GetComponent<TextMeshPro>().text = "+" + totalPoints.ToString();
        StartCoroutine(FadeOutText(go));
    }
    private IEnumerator FadeOutText(GameObject go)
    {
        TextMeshPro textElement = go.GetComponent<TextMeshPro>();
        float elapsedTime = 0f;
        Color startColor = textElement.color;
        Vector3 startPosition = go.transform.position;
        Vector3 targetPosition = startPosition + Vector3.up * fadeUpDistance;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            // Fade out alpha
            Color currentColor = startColor;
            currentColor.a = Mathf.Lerp(1f, 0.3f, t);
            textElement.color = currentColor;

            // Move upwards
            go.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(go);
    }

}