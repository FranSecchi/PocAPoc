using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action<char> charPressed;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (!string.IsNullOrEmpty(Input.inputString))
            {
                char keyPressed = Input.inputString[0];
                keyPressed = char.ToLower(keyPressed);
                if (char.IsLetterOrDigit(keyPressed) || char.IsPunctuation(keyPressed) || char.IsSymbol(keyPressed))
                {
                    charPressed?.Invoke(keyPressed);
                }
            }
        }
    }
}
