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
            Data.Add((Language)i - 1, rawData[i]);
        }
    }

    public string GetText(Language language)
    {
        return Data[language];
    }
}

public enum Language
{
    Catalan = 0,
    Spanish = 1,
}


