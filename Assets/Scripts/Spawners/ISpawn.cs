using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawn
{
    void Spawn();
    void Set(List<Spawner> spawners);
    void JumpWave();
}
