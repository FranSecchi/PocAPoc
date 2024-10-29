using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Text;
using System.Globalization;

public class Duck : MonoBehaviour
{
    public Animator anim;
    public GameObject pop;
    public TextMeshPro tmp;
    public float showTime;
    public float min;
    public float max;
    protected IDisplayWord display;
    private string normalized;
    private string text;
    private string seq = "";
    private float time = 0;
    private float currentTime = 0;
    private Coroutine coro;
    private void Awake()
    {
        text = tmp.text;
        pop.SetActive(false);
    }
    private void Start()
    {
        normalized = NormalizeWord(text.ToLower());
        display = DisplayStrategyFactory.GetDisplay(WordType.STATIC);
        currentTime = Random.Range(min, max);
    }
    private void Update()
    {
        time += Time.deltaTime;
        if(coro == null && time > currentTime)
        {
            coro = StartCoroutine(Show());
        }
    }

    private IEnumerator Show()
    {
        GameManager.Instance.InputHandler.charPressed += OnCharPressed;
        pop.SetActive(true);
        anim.SetTrigger("cuac");
        yield return new WaitForSeconds(showTime);
        GameManager.Instance.InputHandler.charPressed -= OnCharPressed;
        pop.SetActive(false);
        time = 0f;
        currentTime = Random.Range(min, max);
        coro = null;
    }

    private void OnCharPressed(char key)
    {
        seq += key;
        if (seq == normalized)
        {
            GameManager.Instance.RemoveWords();
            display.PrintRemove(tmp.gameObject, 0);
            pop.SetActive(false);
        }
        else if (!normalized.StartsWith(seq))
        {
            seq = "";
        }
        else GameManager.Instance.AnyWordMatched();
        display.UpdateDisplay(tmp.gameObject, seq, text);
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
