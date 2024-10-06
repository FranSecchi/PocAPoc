using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageData 
{
    public Dictionary<Language, string> Data;

    public LanguageData(string[] rawData)
    {
        Data = new Dictionary<Language, string>();
        for (int i = 1; i < rawData.Length; i++)
        {
            Data.Add((Language)i, rawData[i]);
        }
    }

    public string GetText(Language language)
    {
        return Data[language];
    }
}

public enum Language
{
    English= 1,
    Catalan,
    Spanish
}


