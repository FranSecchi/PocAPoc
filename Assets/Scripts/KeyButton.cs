using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyButton : MonoBehaviour
{
    private TextMeshProUGUI text;
    private Coroutine coro;
    private Color color;
    // Start is called before the first frame update
    void Start()
    {
        coro = null;
        text = GetComponentInChildren<TextMeshProUGUI>();
        Image keyText = GetComponent<Image>();
        color = keyText.color;
    }
    private void OnEnable()
    {
        GameManager.Instance.inputHandler.charPressed += Press;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDisable()
    {
        GameManager.Instance.inputHandler.charPressed -= Press;
        Image keyText = GetComponent<Image>();
        keyText.color = color;
        StopAllCoroutines();
    }
    private void Press(char c)
    {
        if (text.text.ToLower()[0] == c)
        {
            if(coro != null) StopCoroutine(coro);
            coro = StartCoroutine(SimulateButtonPress());
        }
    }
    public void ForcePress()
    {
        if (coro != null) StopCoroutine(coro);
        coro = StartCoroutine(SimulateButtonPress());
    }
    private IEnumerator SimulateButtonPress()
    {
        Image keyText = GetComponent<Image>();
        color = keyText.color; // Store the original color

        // Change the color to simulate the pressed effect
        keyText.color = Color.gray; // Example pressed color

        // Wait for a short duration (adjust time as needed)
        yield return new WaitForSeconds(0.1f);
        coro = null;
        // Revert back to the original color
        keyText.color = color;
    }
}
