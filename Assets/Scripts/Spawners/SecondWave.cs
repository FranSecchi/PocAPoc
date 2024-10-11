using System.Collections.Generic;
using UnityEngine;

public class SecondWave : WaveStrategy
{
    private int max;
    private List<Transform> availableSpawnPoints;
    public override void Spawn()
    {
        time += Time.deltaTime;
        if (time < timeInterval) return;

        ResetSpawnPoints();
        int r = Random.Range(1, max + 1);
        for (int i = 0; i < r && availableSpawnPoints.Count > 0; ++i)
            InstantiateWord(WordDifficulty.EASY, typeof(SimpleWord));
        if (r == 1)
            InstantiateWord(WordDifficulty.HARD, typeof(HardWord));

        time = 0f;
    }
    private void InstantiateWord(WordDifficulty difficulty, System.Type wordType)
    {
        Spawner spawner = spawners[Random.Range(0, spawners.Count)];
        WordFactory factory = spawner.getFactory(difficulty);
        WordStruct wordCont = factory.getWord();

        GameObject go = new GameObject();
        Word word = (Word)go.AddComponent(wordType);

        word.word = wordCont;
        word.spawner = spawner;
        SpawnAtPosition(go);
    }

    private void SpawnAtPosition(GameObject go)
    {
        int spawnIndex = Random.Range(0, availableSpawnPoints.Count);
        Transform spawnPoint = availableSpawnPoints[spawnIndex];
        availableSpawnPoints.RemoveAt(spawnIndex);

        go.transform.position = spawnPoint.position;
    }

    private void ResetSpawnPoints()
    {
        availableSpawnPoints = new List<Transform>();
        foreach (var spawner in spawners)
        {
            availableSpawnPoints.AddRange(spawner.spawnPoints);
        }
    }
    protected override void Init()
    {
        max = GameManager.Parameters.MaxSpawnSecondWave;
        timeInterval = GameManager.Parameters.SecondSpawnRate;
    }
}