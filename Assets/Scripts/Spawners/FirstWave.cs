using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstWave : MonoBehaviour, ISpawn
{
    public float timeInterval = 5f;
    private float timePassed = 0f;
    private float time = 0f;
    public IWordController Spawn(Spawner info)
    {
        IWordController word = null;
        if(time > timeInterval)
        {
            GameObject go = info.simpleWordFactory.getWordObject();
            go.transform.position = info.spawnPoints[Random.Range(0, info.spawnPoints.Count)].position; //Temporal
            word = go.GetComponent<IWordController>();
            time = 0f;
        }
        return word;
    }
    
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }
}
