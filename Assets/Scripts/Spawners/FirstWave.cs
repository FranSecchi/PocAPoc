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
            //Instantiate(go);
            go.transform.position = info.spawnPoints[0].position; //Temporal
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
