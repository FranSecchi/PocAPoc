using System;
using System.Collections.Generic;

public class DisplayStrategyFactory
{
    private static Dictionary<WordType, IDisplayWord> strategies;

    public static IDisplayWord GetDisplay(WordType type)
    {
        if (strategies == null) strategies = new Dictionary<WordType, IDisplayWord>();
        if (!strategies.ContainsKey(type))
        {
            switch (type)
            {
                case WordType.SIMPLE:
                    strategies.Add(type, new ClassicDisplay());
                    break;
                case WordType.HARD:
                    strategies.Add(type, new EpicDisplay());
                    break;
                case WordType.FRASE:
                    strategies.Add(type, new ClassicDisplay());
                    break;
                // Add more cases for other word types if needed
                default:
                    throw new InvalidOperationException("Unknown word type: " + type);
            }
        }
        return strategies[type];
    }
}