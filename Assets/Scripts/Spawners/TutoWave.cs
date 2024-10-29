using UnityEngine;

public class TutoWave : WaveStrategy
{
    WordFactory wordFactory;
    GameParameters param;
    private int i = 0;
    public override void Spawn()
    {
        time += Time.deltaTime;
        if (time < timeInterval) return;
        WordStruct? wordCont = wordFactory.getWord();
        if (!wordCont.HasValue) 
        {
            ResetSpawnPoints();
            JumpWave();
            return;
        }
        WordStruct wordS = wordCont.Value;
        GameObject go = new GameObject();
        GameObject r;
        Word word = (Word)go.AddComponent(typeof(FraseWord));
        Spawner spawner;
        switch (i)
        {
            case 0:
                spawner = spawners[2];
                timeInterval = param.TutoSecondSpawn;
                WordSpawner wordSpawner = go.AddComponent<WordSpawner>();
                wordSpawner.SpawnWord(1, go.transform);
                break;
            case 1:
                spawner = spawners[4];
                timeInterval = param.TutoThirdSpawn;
                break;
            case 2:
                spawner = spawners[1];
                timeInterval = param.TutoFourthSpawn;
                break;
            case 3:
                spawner = spawners[3];
                timeInterval = param.TutoSecondSpawn;
                break;
            case 4:
                spawner = spawners[2];
                timeInterval = param.TutoSecondSpawn;
                WordSpawner ws = go.AddComponent<WordSpawner>();
                ws.SpawnWord(0, go.transform);
                break;
            default:
                throw new System.Exception("Tuto bugg");
        }

        word.word = wordS;
        word.spawner = spawner;

        i++;
        go.transform.position = spawner.transform.position;
        time = 0f;
    }

    protected override void Init()
    {
        param = GameManager.Parameter;
        wordFactory = WordFactoryManager.createTutoFactory();
        timeInterval = param.TutoFirstSpawn;
        timeForWave = param.EasyWaitTime;
    }

    public override void JumpWave()
    {
        GameManager.Instance.jumpWave(timeForWave);
    }
}