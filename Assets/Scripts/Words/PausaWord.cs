using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PausaWord : MonoBehaviour
{
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
        normalized = text.ToLower();
        display = DisplayStrategyFactory.GetDisplay(WordType.STATIC);
        GameManager.Instance.InputHandler.charPressed += OnCharPressed;
    }
    private void OnEnable()
    {
        seq = "";
    }
    private void OnDestroy()
    {
        GameManager.Instance.InputHandler.charPressed -= OnCharPressed;
    }
    private void OnCharPressed(char key)
    {
        seq += key;
        if (seq == normalized)
        {
            GameManager.Instance.PauseGame();
        }
        else if (!normalized.StartsWith(seq))
        {
            seq = "";
        }
        else GameManager.Instance.AnyWordMatched();
        display.UpdateDisplay(gameObject, seq, text);
    }
}
