using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "GameParameters", menuName = "Settings/GameParameters")]
public class GameParameters : ScriptableObject
{
    [SerializeField] public int startWave = 0;
    [Header("General Parameters")]
    [SerializeField] private int lifes = 3;
    [SerializeField] private int combo = 3;

    [Space(10)]
    [Header("Word Speeds")]
    [SerializeField] private int simplePoints = 0;
    [SerializeField] private float simpleSpeed = 5f;
    [SerializeField] private float easySpeed = 5f;
    [SerializeField] private float hardSpeed = 10f;

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
    [SerializeField] private float tutoWaveSpawnRate = 5f;
    [Header("Easy Wave")]
    [SerializeField] private float easyWaitTime = 5f;
    [SerializeField] private float easySpawnRate = 5f;
    [SerializeField] private int wordsPerBurst = 3;
    [SerializeField] private int numberBursts = 10;
    [SerializeField] private float burstCooldown = 5f;
    [Range(0,1)]
    [SerializeField] private float easy_ProbabilityMedium = 0f;
    [Header("Medium Wave")]
    [SerializeField] private float mediumWaitTime = 5f;
    [SerializeField] private float mediumSpawnRate = 5f;
    [SerializeField] private int WordsForThirdWave = 10;
    [Range(0, 1)]
    [SerializeField] private float medium_ProbabilityEasy = 0f;
    [Range(0, 1)]
    [SerializeField] private float medium_ProbabilityHard = 0f;
    [Range(0, 1)]
    [SerializeField] private float medium_ProbabilityTwoWords = 0f;
    [Header("Frases wave")]
    [SerializeField] private float frasesWaitTime = 5f;
    [SerializeField] private float frasesSpawnRate = 5f;
    [SerializeField] private int WordsForFourthWave = 10;
    [Header("Wave 4")]
    [SerializeField] private float fourthWaveWaitTime = 5f;

    // Words
    public int SimplePoints => simplePoints;
    public float SimpleSpeed => simpleSpeed / 10f;
    public float EasySpeed => easySpeed / 10f;
    public float HardSpeed => hardSpeed / 10f;
    // General
    public float GoalRadius => goalRadius;
    public int Lifes => lifes;
    public int ComboMultiplier => combo;
    // Display
    public float ClassicFontSize => classicFontSize;
    public TMP_FontAsset ClassicFont => classicFont;
    public float EpicFont => epicFont;
    public float PointsFont => pointsFont;
    public float LettersSpacing => spacing;
    public float WaveAmplitude => waveAmplitude;
    public float WaveFrequency => waveFreq;
    // Tuto
    public float TutoSpawnRate => tutoWaveSpawnRate;
    // Easy wave
    public float EasyWaitTime => easyWaitTime;
    public float EasySpawnRate => easySpawnRate;
    public float BurstCooldown => burstCooldown;
    public float Easy_MediumProbability => easy_ProbabilityMedium;
    public int ManyBursts => numberBursts;
    public int WordsPerBurst => wordsPerBurst;
    // Medium wave
    public float MediumWaitTime => mediumWaitTime;
    public float MediumSpawnRate => mediumSpawnRate;
    public int WordsSecondWave => WordsForThirdWave;
    public float Medium_EasyProbability => medium_ProbabilityEasy;
    public float Medium_HardProbability => medium_ProbabilityHard;
    public float Medium_ComboProbability => medium_ProbabilityTwoWords;
    // Frase wave
    public float ThirdWaveWaitTime => frasesWaitTime;
    public float ThirdSpawnRate => frasesSpawnRate;
    public int WordsThirdWave => WordsForFourthWave;
    // Hard wave
    public float FourthWaveWaitTime => fourthWaveWaitTime;
}