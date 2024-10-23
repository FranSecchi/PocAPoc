using UnityEngine;

public class TutoWave : WaveStrategy
{
    WordFactory wordFactory;
    public override void Spawn()
    {
        time += Time.deltaTime;
        if (time < timeInterval) return;
        WordStruct? wordCont = wordFactory.getWord();
        if (!wordCont.HasValue) 
        {
            GameManager.Instance.AddWave(new EasyWave());
            GameManager.Instance.jumpWave(timeForWave);
            return;
        }
        WordStruct wordS = wordCont.Value;
        ResetSpawnPoints();
        GameObject go = new GameObject();
        Word word = (Word)go.AddComponent(typeof(SimpleWord));
        Spawner spawner = RandomSpawner();
        int spawnIndex = Random.Range(0, spawner.availableSpawnPoints.Count);
        Transform spawnPoint = spawner.availableSpawnPoints[spawnIndex];
        spawner.availableSpawnPoints.RemoveAt(spawnIndex);

        word.word = wordS;
        word.spawner = spawner;


        go.transform.position = spawnPoint.position;
        time = 0f;
    }

    protected override void Init()
    {
        wordFactory = WordFactoryManager.createTutoFactory();
        timeInterval = GameManager.Parameters.TutoSpawnRate;
        timeForWave = GameManager.Parameters.EasyWaitTime;
    }
}