using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public enum TextType
{
    Simple,
    Dialeg
}
public class Localizator : MonoBehaviour
{

    public static Localizator Instance;

    Dictionary<string, LanguageData> TextData;
    private Language _currentLanguage;
    public Language DefaultLanguage;


    public TextAsset TextDataSheet;


    public delegate void LanguageChangeDelegate();
    public static LanguageChangeDelegate OnLanguageChangeDelegate;

    private void Awake()
    {
        if (Instance == null)
        {

            _currentLanguage = DefaultLanguage;
            ReadSheet();
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public static string GetText(string textKey, TextType type)
    {
        return Instance.TextData[textKey].GetText(Instance._currentLanguage);
    }
    public static string GetText(string textKey, Language language)
    {
        return Instance.TextData[textKey].GetText(language);
    }

    public static void SetLanguage(Language language)
    {
        Instance._currentLanguage = language;
        OnLanguageChangeDelegate?.Invoke();
    }
    void ReadSheet()
    {
        string[] texts = TextDataSheet.text.Split(new char[] { '\n' });
        for (int i = 1; i < texts.Length; i++)
        {
            if (texts.Length > 1)
                AddNewTextDataEntry(texts[i]);
        }
    }
    void AddNewTextDataEntry(string s)
    {
        string[] t = s.Split(new char[] { ';' });
        var languageData = new LanguageData(t);
        if (TextData == null)
            TextData = new Dictionary<string, LanguageData>();
        TextData.Add(t[0], languageData);
    }
}
