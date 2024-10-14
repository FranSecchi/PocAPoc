using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<Spawner> spawners;

    private ISpawn spawner;
    private void Update()
    {
        spawn();
    }
    public void setSpawner(ISpawn spawner)
    {
        if (spawner != null) DestroyImmediate(this.spawner as Component);
        this.spawner = spawner;
        Debug.Log(spawner.GetType().ToString());
        spawner.SetSpawners(spawners);
    }
    public void spawn()
    {
        spawner.Spawn();
    }
}