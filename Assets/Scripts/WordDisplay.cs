using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface WordDisplay 
{
    void Initialize(string word, float fontSize = 6f, Color? color = null);
    void UpdateDisplay(string currentSequence, string fullWord);
}
