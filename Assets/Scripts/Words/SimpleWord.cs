using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWord : Word, IWordController
{
    public override void HandleInput(char key)
    {
        seq += key;
        //Add check
        if (!word.StartsWith(seq))
        {
            seq = "";
        }
        if (seq == word)
        {
            Remove(true);
        }
        UpdateDisplay(seq, word);
    }
    protected override void Step()
    {
        throw new System.NotImplementedException();
    }
}
