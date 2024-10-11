using UnityEngine;

public class SecondWave : ISpawn
{
    private WordFactory factory;
    private float time = 0f;
    private float timeInterval = 5f;
    public void Spawn(Spawner info)
    {
        time += Time.deltaTime;
        if (time < timeInterval) return;
        if (factory == null) factory = info.getFactory(WordDifficulty.HARD);
        time = 0f;
        WordStruct wordCont = factory.getWord();

        GameObject go = new GameObject();
        Word word = go.AddComponent<HardWord>();

        word.word = wordCont;
        word.spawner = info;

        go.transform.position = info.spawnPoints[Random.Range(0, info.spawnPoints.Count)].position; //Temporal
    }
}