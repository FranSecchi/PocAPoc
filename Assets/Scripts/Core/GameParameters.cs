using UnityEngine;
using TMPro;
using TMPro.EditorUtilities;

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
    [Header("Wave 1")]
    [SerializeField] private float firstWaveWaitTime = 5f;
    [SerializeField] private float firstWaveSpawnRate = 5f;
    [SerializeField] private int maxSpawn1 = 3;
    [SerializeField] private int WordsForSecondWave = 10;
    [Header("Wave 2")]
    [SerializeField] private float secondWaveWaitTime = 5f;
    [SerializeField] private float secondWaveSpawnRate = 5f;
    [SerializeField] private int WordsForThirdWave = 10;
    [SerializeField] private int maxSpawn2 = 3;
    [Header("Wave 3")]
    [SerializeField] private float thirdWaveWaitTime = 5f;
    [SerializeField] private float thirdWaveSpawnRate = 5f;
    [SerializeField] private int WordsForFourthWave = 10;
    [Header("Wave 4")]
    [SerializeField] private float fourthWaveWaitTime = 5f;

    // Public properties to access the values
    public int SimplePoints => simplePoints;
    public float SimpleSpeed => simpleSpeed / 10f;
    public float EasySpeed => easySpeed / 10f;
    public float HardSpeed => hardSpeed / 10f;
    public float GoalRadius => goalRadius;
    public float ClassicFontSize => classicFontSize;
    public TMP_FontAsset ClassicFont => classicFont;
    public float EpicFont => epicFont;
    public float PointsFont => pointsFont;
    public float TutoSpawnRate => tutoWaveSpawnRate;
    public float FirstSpawnRate => firstWaveSpawnRate;
    public float FirstWaveWaitTime => firstWaveWaitTime;
    public float SecondWaveWaitTime => secondWaveWaitTime;
    public float ThirdWaveWaitTime => thirdWaveWaitTime;
    public float FourthWaveWaitTime => fourthWaveWaitTime;
    public float SecondSpawnRate => secondWaveSpawnRate;
    public float ThirdSpawnRate => thirdWaveSpawnRate;
    public int WordsFirstWave => WordsForSecondWave;
    public int WordsSecondWave => WordsForThirdWave;
    public int WordsThirdWave => WordsForFourthWave;
    public int MaxSpawnFirstWave => maxSpawn1;
    public int MaxSpawnSecondWave => maxSpawn2;
    public float LettersSpacing => spacing;
    public float WaveAmplitude => waveAmplitude;
    public float WaveFrequency => waveFreq;
    public int Lifes => lifes;
    public int ComboMultiplier => combo;
}