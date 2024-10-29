using System;
using UnityEngine;

internal class WordSpawner : MonoBehaviour
{
    internal void SpawnWord(int v, Transform transform)
    {
        if(v > 0)
        {
            Instantiate(Resources.Load<GameObject>("AccentsTxt"), transform);
        }
        else
            Instantiate(Resources.Load<GameObject>("PunctTxt"), transform);
    }
}