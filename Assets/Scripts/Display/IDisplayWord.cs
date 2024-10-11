using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDisplayWord 
{
    void Initialize(GameObject gameObject, string word);
    void UpdateDisplay(GameObject gameObject, string currentSequence, string fullWord);
    void PrintRemove(GameObject gameObject, int points);
}
