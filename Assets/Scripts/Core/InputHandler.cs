using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    public KeysManager keysManager;
    public KeyButton barra;
    public event Action<char> charPressed;
    public event Action escapePressed;
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Return)) 
            { 
                escapePressed?.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                keysManager.Randomize();
                if(barra.transform.parent.gameObject.activeSelf) barra.ForcePress();
            }
            else if (!string.IsNullOrEmpty(Input.inputString))
            {
                char keyPressed = Input.inputString[0];
                keyPressed = char.ToLower(keyPressed);
                if (char.IsLetterOrDigit(keyPressed) || char.IsPunctuation(keyPressed) || char.IsSymbol(keyPressed))
                {
                    if (keysManager.ContainsKey(char.ToUpper(keyPressed)))
                    {
                        charPressed?.Invoke(keyPressed);
                        if (charPressed != null) GameManager.Instance.CheckCombos();
                    }
                }
            }
        }
    }

    public void OnKeyClick(TextMeshProUGUI key)
    {
        char keyPressed = key.text[0];
        keyPressed = char.ToLower(keyPressed);
        Debug.Log(keyPressed);
        if (char.IsLetterOrDigit(keyPressed) || char.IsPunctuation(keyPressed) || char.IsSymbol(keyPressed))
        {
            charPressed?.Invoke(keyPressed);
            if (charPressed != null) GameManager.Instance.CheckCombos();
        }
    }
}
