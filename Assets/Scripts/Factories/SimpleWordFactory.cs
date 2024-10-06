using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWordFactory : WordFactory
{
    public override GameObject getWordObject()
    {
        GameObject gameObject = new GameObject();
        SimpleWord sw = gameObject.AddComponent<SimpleWord>();
        sw.word = wordList[Random.Range(0,wordList.Count)];
        return gameObject;
    }
}
