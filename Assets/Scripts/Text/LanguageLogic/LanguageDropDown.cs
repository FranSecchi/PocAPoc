using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class LanguageDropDown : MonoBehaviour
{
    public Button buttonLanguage1; // Button to select the first language
    public Button buttonLanguage2; // Button to select the second language
    public Language language1;     // The language assigned to the first button
    public Language language2;     // The language assigned to the second button

    private void Start()
    {
        // Ensure buttons are assigned; if not, find them on the object
        if (buttonLanguage1 == null || buttonLanguage2 == null)
        {
            Debug.LogError("Buttons not assigned in inspector.");
            return;
        }

        // Assign language setting to each button's click event
        buttonLanguage1.onClick.AddListener(() => SetLanguage(language1));
        buttonLanguage2.onClick.AddListener(() => SetLanguage(language2));
    }

    private void SetLanguage(Language language)
    {
        Localizator.SetLanguage(language);
    }
}
