using System.Collections.Generic;

public abstract class WaveStrategy : ISpawn
{
    protected List<Spawner> spawners;
    protected float time = 0f;
    protected float timeInterval;
    public void SetSpawners(List<Spawner> spawners)
    {
        this.spawners = spawners;
        Init();
    }
    protected abstract void Init();
    public abstract void Spawn();
}