using UnityEngine;

[CreateAssetMenu(fileName = "GameParameters", menuName = "Settings/GameParameters")]
public class GameParameters : ScriptableObject
{
    [Header("General Parameters")]
    [SerializeField] private int lifes = 3;

    [Space(10)]
    [Header("Word Speeds")]
    [SerializeField] private float simpleSpeed = 5f;
    [SerializeField] private float hardSpeed = 10f;

    [Space(10)]
    [Header("Word Display Settings")]
    [SerializeField] private float goalRadius = 1f;
    [SerializeField] private float classicFont = 5f;
    [SerializeField] private float epicFont = 5f;
    [SerializeField] private float pointsFont = 5f;
    [SerializeField] private float spacing = 5f;
    [SerializeField] private float waveAmplitude = 5f;
    [SerializeField] private float waveFreq = 5f;

    [Space(10)]
    [Header("Wave Settings")]
    [Header("Wave 1")]
    [SerializeField] private float firstWaveSpawnRate = 5f;
    [SerializeField] private int maxSpawn1 = 3;
    [SerializeField] private int pointsForSecondWave = 10;
    [Header("Wave 1")]
    [SerializeField] private float secondWaveSpawnRate = 5f;
    [SerializeField] private int pointsForThirdWave = 10;
    [SerializeField] private int maxSpawn2 = 3;

    // Public properties to access the values
    public float SimpleSpeed => simpleSpeed / 10f;
    public float HardSpeed => hardSpeed / 10f;
    public float GoalRadius => goalRadius;
    public float ClassicFont => classicFont;
    public float EpicFont => epicFont;
    public float PointsFont => pointsFont;
    public float FirstSpawnRate => firstWaveSpawnRate;
    public float SecondSpawnRate => secondWaveSpawnRate;
    public int PointsFirstWave => pointsForSecondWave;
    public int PointsSecondWave => pointsForThirdWave;
    public int MaxSpawnFirstWave => maxSpawn1;
    public int MaxSpawnSecondWave => maxSpawn2;
    public float LettersSpacing => spacing;
    public float WaveAmplitude => waveAmplitude;
    public float WaveFrequency => waveFreq;
    public int Lifes => lifes;
}