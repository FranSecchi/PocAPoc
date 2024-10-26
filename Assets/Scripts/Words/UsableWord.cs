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
        return string.Concat(
            text.Normalize(NormalizationForm.FormD)
                .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark &&
                             char.IsLetterOrDigit(ch))
        ).Normalize(NormalizationForm.FormC);
    }
}
