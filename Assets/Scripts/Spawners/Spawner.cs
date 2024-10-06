using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public WordFactory simpleWordFactory;
    public WordFactory hardWordFactory;
    private ISpawn spawner;
    private List<IWordController> activeWords;
    void Start()
    {
        activeWords = new List<IWordController>();  
        simpleWordFactory = GetComponent<SimpleWordFactory>();
        hardWordFactory = GetComponent<HardWordFactory>();
    }
    public void setSpawner(ISpawn spawner)
    { 
        Destroy(GetComponent(typeof(ISpawn)));
        this.spawner = spawner; 
    }
    public IWordController spawn()
    {
        IWordController word = spawner.Spawn(this);
        if(word != null) activeWords.Add(word);
        return word;
    }
}
