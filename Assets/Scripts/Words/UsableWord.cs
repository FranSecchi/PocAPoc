using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class UsableWord : MonoBehaviour
{
    public CameraAnimation ca;
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
        if (!normalized.StartsWith(seq))
        {
            seq = "";
        }
        display.UpdateDisplay(gameObject, seq, text);
        if (seq == normalized)
        {
            ca.Transition();
            Destroy(gameObject);
        }
    }
    private string NormalizeWord(string text)
    {
        return string.Concat(
            text.Normalize(NormalizationForm.FormD)
                .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark &&
                             char.IsLetterOrDigit(ch))
        ).Normalize(NormalizationForm.FormC);
    }
}
