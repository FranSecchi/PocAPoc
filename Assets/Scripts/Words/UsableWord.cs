using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class UsableWord : MonoBehaviour
{
    public CameraAnimation ca;
    public GameObject dialeg;
    public GameObject escriu;
    protected IDisplayWord display;
    private string normalized;
    private string text;
    private string seq = "";
    private void Awake()
    {
        text = GetComponent<TextMeshPro>().text;
    }
    private void Start()
    {
        normalized = NormalizeWord(text.ToLower());
        display = DisplayStrategyFactory.GetDisplay(WordType.STATIC);
        GameManager.Instance.InputHandler.charPressed += OnCharPressed;
    }
    private void OnDisable()
    {
        GameManager.Instance.InputHandler.charPressed -= OnCharPressed;
    }
    private void OnCharPressed(char key)
    {
        seq += key;
        if (seq == normalized)
        {
            dialeg.SetActive(false);
            ca.Transition();
            Destroy(escriu);
            Destroy(gameObject);
        }
        else if (!normalized.StartsWith(seq))
        {
            seq = "";
        }
        else GameManager.Instance.AnyWordMatched();
        display.UpdateDisplay(gameObject, seq, text);
    }
    private string NormalizeWord(string text)
    {
        // Decompose the text into base characters and diacritics
        var normalizedText = text.Normalize(NormalizationForm.FormD);

        // Remove diacritics except for 'ç'
        var result = string.Concat(
            normalizedText.Where(
                ch => CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark &&
                             char.IsLetterOrDigit(ch) || ch == '̧'
            )
        );

        // Replace decomposed 'ç' with its composed form and recompose the string
        return result.Replace("ç", "ç").Normalize(NormalizationForm.FormC);
    }
}
