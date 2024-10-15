using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordFactoryManager
{
    public static List<WordFactory> createFactories(TextAsset file)
    {
        TextAsset commonFile = Resources.Load<TextAsset>("CommonWords");

        List<WordFactory> factories = new List<WordFactory>();
        List<WordStruct> commonWords = parseSheet(commonFile);
        List<WordStruct> additionalWords = parseSheet(file);

        List<WordStruct> combinedWords = commonWords.Concat(additionalWords).ToList();
        Dictionary<WordDifficulty, List<WordStruct>> wordBuckets = categorizeWords(combinedWords); // Categorize by difficulty

        // Create the factories based on word categories
        factories.Add(new EasyWordFactory(wordBuckets[WordDifficulty.EASY]));
        factories.Add(new HardWordFactory(wordBuckets[WordDifficulty.HARD]));

        return factories;
    }

    internal static WordFactory createPhraseFactory()
    {
        TextAsset commonFile = Resources.Load<TextAsset>("Phrases");
        WordFactory factory = new BossWordFactory(parseSheet(commonFile));
        return factory;
    }

    private static Dictionary<WordDifficulty, List<WordStruct>> categorizeWords(List<WordStruct> words)
    {
        Dictionary<WordDifficulty, List<WordStruct>> dic = new Dictionary<WordDifficulty, List<WordStruct>>();

        foreach (var word in words)
        {
            int length = word.Content.Length;

            if(length >= 1 && length < 4) // Easy
            {
                if (!dic.ContainsKey(WordDifficulty.EASY))
                {
                    dic[WordDifficulty.EASY] = new List<WordStruct>();
                }
                dic[WordDifficulty.EASY].Add(word);
            }
            else if(length >= 4 && length < 7) // Medium
            {
                if (!dic.ContainsKey(WordDifficulty.HARD))
                {
                    dic[WordDifficulty.HARD] = new List<WordStruct>();
                }
                dic[WordDifficulty.HARD].Add(word);
            }
        }
        return dic;
    }

    private static List<WordStruct> parseSheet(TextAsset file)
    {
        List<WordStruct> list = new List<WordStruct>();
        string[] lines = file.text.Split(new char[] { '\n' });
        for (int i = 1; i < lines.Length; i++)
        {
            if (lines.Length > 1)
                list.Add(GetWord(lines[i]));
        }
        return list;
    }

    private static WordStruct GetWord(string v)
    {
        string[] t = v.Split(new char[] { ';' });
        WordStruct ws = new WordStruct(t[0], t[1], t[2]);
        return ws;
    }
}