using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class LanguageDropDown : MonoBehaviour
{
    public TMP_Dropdown languageDropdown;
    public List<Language> languages;

    private void Start()
    {
        if (languageDropdown == null)
        {
            languageDropdown = GetComponent<TMP_Dropdown>();
        }

        PopulateDropdown();

        languageDropdown.onValueChanged.AddListener(OnLanguageSelected);
    }

    private void PopulateDropdown()
    {
        languageDropdown.ClearOptions();

        List<string> options = new List<string>();

        foreach (var language in languages)
        {
            options.Add(language.ToString());
        }

        languageDropdown.AddOptions(options);
    }

    private void OnLanguageSelected(int index)
    {
        Language selectedLanguage = languages[index];

        Localizator.SetLanguage(selectedLanguage);
    }
}
