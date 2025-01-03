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
                case WordType.FADE:
                    strategies.Add(type, new SmokeDisplay());
                    break;
                case WordType.FRASE:
                    strategies.Add(type, new ClassicDisplay());
                    break;
                case WordType.STATIC:
                    strategies.Add(type, new StaticDisplay());
                    break;
                // Add more cases for other word types if needed
                default:
                    throw new InvalidOperationException("Unknown word type: " + type);
            }
        }
        return strategies[type];
    }
}