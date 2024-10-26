using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "GameParameters", menuName = "Settings/GameParameters")]
public class GameParameters : ScriptableObject
{
    [SerializeField] public int startWave = 0;
    [Header("General Parameters")]
    [SerializeField] private int lifes = 3;
    [SerializeField] private int combo = 3;
    [SerializeField] private int multipleWaves = 1;
    [Range(0, 1)]
    [SerializeField] private float newWordProbability = 1;
    [SerializeField] private float progressiveSpeed = 0;

    [Space(10)]
    [Header("Word Speeds")]
    [SerializeField] private int simplePoints = 0;
    [SerializeField] private float simpleSpeed = 5f;
    [SerializeField] private float easySpeed = 5f;
    [SerializeField] private float mediumSpeed = 5f;
    [SerializeField] private float hardSpeed = 10f;
    [Header("Words")]
    [SerializeField] private float timeToFadeWord = 3f;

    [Space(10)]
    [Header("Word Display Settings")]
    [SerializeField] private float goalRadius = 1f;
    [SerializeField] private float classicFontSize = 5f;
    [SerializeField] private TMP_FontAsset classicFont;
    [SerializeField] private float epicFont = 5f;
    [SerializeField] private float pointsFont = 5f;
    [SerializeField] private float spacing = 5f;
    [SerializeField] private float waveAmplitude = 5f;
    [SerializeField] private float waveFreq = 5f;

    [Space(10)]
    [Header("Wave Settings")]
    [Header("Wave Tuto")]
    [SerializeField] private float tutoFirstSpawn = 5f;
    [SerializeField] private float tutoSecondSpawn = 5f;
    [SerializeField] private float tutoThirdSpawn = 5f;
    [SerializeField] private float tutoFourthSpawn = 5f;
    [SerializeField] private float tutoLastSpawn = 5f;
    [Header("Easy Wave")]
    [SerializeField] private float easyWaitTime = 5f;
    [SerializeField] private float easySpawnRate = 5f;
    [SerializeField] private int e_wordsPerBurst = 3;
    [SerializeField] private int e_numberBursts = 10;
    [SerializeField] private float e_burstCooldown = 5f;
    [Range(0,1)]
    [SerializeField] private float easy_ProbabilityMedium = 0f;
    [Header("Medium Wave")]
    [SerializeField] private float mediumWaitTime = 5f;
    [SerializeField] private float mediumSpawnRate = 5f;
    [SerializeField] private float m_burstCooldown = 5f;
    [SerializeField] private int m_wordsPerBurst = 3;
    [SerializeField] private int m_numberBursts = 10;
    [Range(0, 1)]
    [SerializeField] private float medium_ProbabilityEasy = 0f;
    [Range(0, 1)]
    [SerializeField] private float medium_ProbabilityHard = 0f;
    [Range(0, 1)]
    [SerializeField] private float medium_ProbabilityTwoWords = 0f;
    [Header("Frases wave")]
    [SerializeField] private float frasesWaitTime = 5f;
    [SerializeField] private float frasesWordsSpawnRate = 5f;
    [SerializeField] private float frasesSpawnRate = 5f;
    [SerializeField] private int phrasesForNextWave = 10;
    [Header("Hard Wave")]
    [SerializeField] private float hardWaitTime = 5f;
    [SerializeField] private float hardSpawnRate = 5f;
    [SerializeField] private float h_burstCooldown = 5f;
    [SerializeField] private int h_wordsPerBurst = 3;
    [SerializeField] private int h_numberBursts = 10;
    [Range(0, 1)]
    [SerializeField] private float h_ProbabilityEasy = 0f;
    [Range(0, 1)]
    [SerializeField] private float h_ProbabilityHard = 0f;
    [Range(0, 1)]
    [SerializeField] private float h_ProbabilityTwoWords = 0f;
    [Range(0, 1)]
    [SerializeField] private float h_fadeProb = 0f;

    [Header("Wave probabilities")]
    [Range(0, 1)]
    [SerializeField] private float easyWaveProbability = 0.4f;
    [Range(0, 1)]
    [SerializeField] private float mediumWaveProbability = 0.3f;
    [Range(0, 1)]
    [SerializeField] private float hardWaveProbability = 0.2f;
    [Range(0, 1)]
    [SerializeField] private float phraseWaveProbability = 0.1f;

    private float increment = 0;
    public void Increment() { increment += progressiveSpeed; }
    public void ResetIncrement() { increment = 0; }
    private void OnValidate()
    {
        // Calculate the total sum
        float total = easyWaveProbability + mediumWaveProbability + hardWaveProbability + phraseWaveProbability;

        // If the total is greater than 1, normalize the probabilities
        if (total > 1f)
        {
            easyWaveProbability /= total;
            mediumWaveProbability /= total;
            hardWaveProbability /= total;
            phraseWaveProbability /= total;
        }
    }
    // Words
    public int SimplePoints => simplePoints; 
    public float SimpleSpeed => (simpleSpeed + increment) / 10f;
    public float EasySpeed => (easySpeed + increment) / 10f;
    public float MediumSpeed => (mediumSpeed + increment) / 10f;
    public float HardSpeed => (hardSpeed + increment) / 10f;

    // General
    public float GoalRadius => goalRadius;
    public int Lifes => lifes;
    public int DoubleMultiplier => combo;
    public float NewWordProb => newWordProbability;

    // Words
    public float TimeToFadeWord => timeToFadeWord;

    // Display
    public float ClassicFontSize => classicFontSize;
    public TMP_FontAsset ClassicFont => classicFont;
    public float EpicFont => epicFont;
    public float PointsFont => pointsFont;
    public float LettersSpacing => spacing;
    public float WaveAmplitude => waveAmplitude;
    public float WaveFrequency => waveFreq;
    // Tuto
    public float TutoFirstSpawn => tutoFirstSpawn;
    public float TutoSecondSpawn => tutoSecondSpawn;
    public float TutoThirdSpawn => tutoThirdSpawn;
    public float TutoFourthSpawn => tutoFourthSpawn;
    public float TutoLastSpawn => tutoLastSpawn;
    // Easy wave
    public float EasyWaitTime => easyWaitTime;
    public float EasySpawnRate => easySpawnRate;
    public float Easy_BurstCooldown => e_burstCooldown;
    public float Easy_MediumProbability => easy_ProbabilityMedium;
    public int Easy_ManyBursts => e_numberBursts;
    public int Easy_WordsPerBurst => e_wordsPerBurst;
    // Medium wave
    public float MediumWaitTime => mediumWaitTime;
    public float MediumSpawnRate => mediumSpawnRate;
    public float Medium_BurstCooldown => m_burstCooldown;
    public int Medium_ManyBursts => m_numberBursts;
    public int Medium_WordsPerBurst => m_wordsPerBurst;
    public float Medium_EasyProbability => medium_ProbabilityEasy;
    public float Medium_HardProbability => medium_ProbabilityHard;
    public float Medium_ComboProbability => medium_ProbabilityTwoWords;
    // Frase wave
    public float PharseWaitTime => frasesWaitTime;
    public float PhraseWordsSpawnRate => frasesWordsSpawnRate;
    public float PhraseSpawnRate => frasesSpawnRate;
    public int PhraseNextWave => phrasesForNextWave;
    // Hard wave
    public float HardWaitTime => hardWaitTime;
    public float HardSpawnRate => hardSpawnRate;
    public float Hard_BurstCooldown => h_burstCooldown;
    public int Hard_ManyBursts => h_numberBursts;
    public int Hard_WordsPerBurst => h_wordsPerBurst;
    public float Hard_EasyProbability => h_ProbabilityEasy;
    public float Hard_HardProbability => h_ProbabilityHard;
    public float Hard_ComboProbability => h_ProbabilityTwoWords;
    public float Hard_FadeProb => h_fadeProb;
    public int WavesMultiple => multipleWaves;
    public float EasyProb => easyWaveProbability;
    public float MediumProb => mediumWaveProbability;
    public float HardProb => hardWaveProbability;
    public float PhraseProb => phraseWaveProbability;

}