using System;
using System.Collections;
using TMPro;
using UnityEngine;
public class PointsManager : MonoBehaviour
{
    public TextMeshPro wordText;
    public ComboDisplay comboDisplay;
    public float fadeTime;
    public float displayTime;
    [NonSerialized]
    public int totalPoints = 0;
    [NonSerialized]
    public int record = 0;
    private Coroutine displayCoroutine;
    private int combo = 1;
    private int chainMultiplier = 1;
    private int lastSum = 0;
    private bool chain = false;

    internal void displayWord(string word)
    {
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
            ResetText(); 
        }

        displayCoroutine = StartCoroutine(DisplayWordCoroutine(word));
    }

    private IEnumerator DisplayWordCoroutine(string word)
    { 

        wordText.text = word;
        wordText.alpha = 0f;

        yield return StartCoroutine(FadeText(0f, 1f, fadeTime));

        yield return new WaitForSeconds(displayTime);

        yield return StartCoroutine(FadeText(1f, 0f, fadeTime));

        ResetText();
        displayCoroutine = null; 
    }

    private IEnumerator FadeText(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            wordText.alpha = alpha;
            yield return null;
        }

        wordText.alpha = endAlpha;
    }

    private void ResetText()
    {
        wordText.text = "";     
        wordText.alpha = 0f;   
    }

    public void sumPoints(Word word, bool completed)
    {
        int sum = completed ? word.word.Content.Length * combo * chainMultiplier : 0;
        sum += chain ? lastSum * 4 : 0;
        totalPoints += sum;
        lastSum = sum;
        comboDisplay.PrintPoints(sum);
    }

    internal void Double(Word word)
    {
        int points= GameManager.Parameter.DoubleMultiplier*word.word.Content.Length*combo;

        comboDisplay.PrintDouble(points);
        totalPoints += points;
    }

    internal void BreakCombo()
    {
        if (combo == 1) return;
        combo = 1;
        chainMultiplier = 1;
        comboDisplay.PrintLostCombo();
    }

    internal void Combo()
    {
        ++combo;
        if (combo > 3) comboDisplay.PrintCombo((combo-1)*chainMultiplier);
    }

    internal void Chain()
    {
        ++chainMultiplier;
        comboDisplay.PrintChain(chainMultiplier);
    }
}